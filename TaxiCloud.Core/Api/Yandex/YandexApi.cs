using System;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NLog;
using System.Net;
using TaxiCloud.Core.Api.Yandex.Model;

namespace TaxiCloud.Core.Api.Yandex
{
    public class YandexApi
    {
        private readonly string _token;
        private readonly ILogger _logger;

        public YandexApi(string token)
        {
            _token = token;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IEnumerable<YandexDriverModel> GetDrivers()
        {
            var result = new List<YandexDriverModel>();
            var balances = GetBalances();
            var statuses = GetStatuses();
            foreach (var balance in balances)
            {
                var driver = GetFullDriverInfo(balance.Id);
                if (driver == null) continue;
                driver.YandexId = balance.Id;
                driver.Status = statuses[balance.Id];
                driver.Car.YandexId = driver.CarId;
                result.Add(driver);
            }
            return result;
        }

        public YandexDriverModel GetFullDriverInfo(string id)
        {
            try
            {
                var json = Get($"https://taximeter.yandex.rostaxi.org/api/driver/get?apikey={_token}&id={id}");
                var driver = JsonConvert.DeserializeObject<YandexDriverModel>(json);
                return driver;
            }
            catch
            {
                return null;
            }
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public Dictionary<string, string> GetStatuses()
        {
            var request = GetRequest("https://lk.taximeter.yandex.ru/dictionary/items/drivers");
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            var html = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var rows = doc.DocumentNode.SelectNodes("//table//tr").ToList();
            var result = new Dictionary<string, string>();
            foreach (var row in rows)
            {
                var columns = row.SelectNodes("td");
                if ((columns?.Count ?? 0) == 0)
                    continue;
                var id = row.GetAttributeValue("data-guid", null);
                if (id == null)
                    continue;
                var tds = row.SelectNodes("td");
                if ((tds?.Count ?? 0) < 2)
                    continue;
                if (tds == null) continue;
                var status = tds[1].InnerText;
                status = WebUtility.HtmlDecode(status);
                result.Add(id, status);
            }
            return result;
        }

        // ReSharper disable once UnusedMember.Global
        public void GetOrders()
        {
            var request = GetRequest("https://lk.taximeter.yandex.ru/clientapi/core/orders?filter=5");

            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            var html = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var rows = doc.DocumentNode.SelectNodes("//table//tr").ToList();
            foreach (var row in rows)
            {
                row.GetAttributeValue("data-driver", null);
                var columns = row.SelectNodes("td");
                if ((columns?.Count ?? 0) == 0)
                    continue;
                return;
            }
        }

        private static HttpWebRequest GetRequest(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie(".AspNetCore.Antiforgery.uGYhvnAGzi0", "CfDJ8Pj1S8DdfZlAigez8TXM6pD2Pv4JabMLPnTv2P1RfkDUfdZ6YEOjVYIQx9VOV8JLjduDAGP5JRppywvdJyYN0ktSr0xBSpYrZhAZSnirffSwyln6YpsbDlseRzkFkjvvjX06rTZBaQrcRJH9UEjq6Ls", "/", "lk.taximeter.yandex.ru"));
            request.CookieContainer.Add(new Cookie(".AspNetCore.Culture", "c%3Dru%7Cuic%3Dru", "/", "lk.taximeter.yandex.ru"));
            request.CookieContainer.Add(new Cookie("L", "W1hlWWJ1Cg1ceQJ/T1wAZ3d8XFAAX2J0E1JNB3s3Ri46IyZCAzUd.1521887767.13448.321148.b15272146d9384171de7749a26a3789d", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("Session_id", "3:1521887767.5.0.1521887767096:qy5OPg:14.1|603932145.0.2|179144.312832.s4OvV7gPP7l56TFl2R2oFkdNmE0", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("YandexPassport.Auth", "CfDJ8Pj1S8DdfZlAigez8TXM6pAz8N4kFZGrY%2B0UHUkRI3LXhqvIJygNFPI75qFPsYcX3UUucEnRl9d1Yk0dQTFjLLL7yv9YCzElf8DlxwBTdaJRfihOGnYsIY%2FBLC1gzNmYEjYXALEYXswaT0bLYTSP7nf7ft5UFlhCVM8B1%2FrSa2J7m94VkvuX4ku5m%2FfbWlDRhyu79yz8EgVWBnfXHk8oMMZOvjQtcxgao0Iflxw%2FdR%2FfGEiimPiXlfBDQ5icDlssdSJPQFtSoCPjjeajc%2FmvLdGrDoCeYnCWcrVrReQHiEA%2F", "/", "lk.taximeter.yandex.ru"));
            request.CookieContainer.Add(new Cookie("_ym_isad", "1", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("_ym_uid", "1521887743509543006", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("_ym_visorc_44292669", "w", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("_ym_visorc_784657", "b", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("culture_set", "1", "/", "lk.taximeter.yandex.ru"));
            request.CookieContainer.Add(new Cookie("i", "RbQRlnul/BmXNqwJFpLLufHcOLMtQrgaF8hi2M1i+kJdBzILG0J1mh30L52BNGWbS/YoLBK00jbbjvgiYQ6At7I4K34=", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("mda", "0", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("sessionid2", "3:1521887767.5.0.1521887767096:qy5OPg:14.1|603932145.0.2|179144.313844.NH7idhm3lTZJivly7EA_b2NoStw", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("user_db", "2bc339d70efe4286916472ced702d626", "/", "lk.taximeter.yandex.ru"));
            request.CookieContainer.Add(new Cookie("user_login", "taxi.automation", "/", "lk.taximeter.yandex.ru"));
            request.CookieContainer.Add(new Cookie("yandex_login", "taxi.automation", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("yandexuid", "8236819331521887744", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("yp", "1837247744.yrts.1521887744#1837247767.udn.cDp0YXhpLmF1dG9tYXRpb24%3D", "/", ".yandex.ru"));
            request.CookieContainer.Add(new Cookie("ys", "udn.cDp0YXhpLmF1dG9tYXRpb24%3D", "/", ".yandex.ru"));
            return request;
        }

        public IEnumerable<YandexBalanceModel> GetBalances()
        {
            var result = new List<YandexBalanceModel>();
            var json = Get($"https://taximeter.yandex.rostaxi.org/api/driver/balance?apikey={_token}");
            if (!(JsonConvert.DeserializeObject(json) is JObject jobjects)) return result.ToArray();
            foreach (var o in jobjects)
            {
                var b = new YandexBalanceModel
                {
                    Id = o.Key,
                    Balance = o.Value.Value<float>()
                };
                result.Add(b);
            }

            return result;
        }

        public static string Get(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            request.Method = "Get";

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
            return responseString;
        }

        public YandexDriverBalanceInfoModel GetLastJobPayForDriver(string driverYandexId)
        {
            var request = GetRequest($"https://lk.taximeter.yandex.ru/driver/items/pays?driver={driverYandexId}&q=");
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();

            var html = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var rows = doc.DocumentNode.SelectNodes("//table//tbody//tr");
            if (rows == null)
                return null;
            var result = new YandexDriverBalanceInfoModel
            {
                Balance = 0,
                DriverId = driverYandexId,
                IsToday = false,
                PaymentType = null,
                Pay = 0
            };
            foreach (var row in rows)
            {
                var tds = row.SelectNodes("td");
                if (tds == null)
                    continue;
                if (tds.Count == 4)
                {
                    if (result.IsToday)
                        return result;
                    var day = WebUtility.HtmlDecode(tds[0].InnerText);
                    var isToday = day.ToLowerInvariant().Contains("сегодня");
                    if (!isToday)
                        return result;
                    result.IsToday = true;
                }

                if (tds.Count == 13)
                {
                    var paymentType = WebUtility.HtmlDecode(tds[9].InnerText);
                    if (paymentType != "Job.Payment")
                        continue;

                    var balance = WebUtility.HtmlDecode(tds[12].InnerText).Trim().Replace(",", ".").Replace(" ", string.Empty);
                    if (!float.TryParse(balance, NumberStyles.Any, CultureInfo.InvariantCulture, out var balanceCasted))
                    {
                        _logger.Error($"Failed parse balance for {driverYandexId}, {balance}");
                        return result;
                    }

                    var pay = WebUtility.HtmlDecode(tds[11].InnerText).Trim().Replace(",", ".").Replace(" ", string.Empty);
                    if (!float.TryParse(pay, NumberStyles.Any, CultureInfo.InvariantCulture, out var payCasted))
                    {
                        _logger.Error($"Failed parse pay for {driverYandexId}, {pay}");
                        return result;
                    }

                    result.PaymentType = "Job.Payment";
                    result.Balance = balanceCasted;
                    result.Pay = payCasted;
                    return result;
                }
            }

            return result;
        }
    }
}