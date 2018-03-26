using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TaxiCloud.Core.Api.Wialon.Model;
using TaxiCloud.Core.Api.Wialon.Utils;
using TaxiCloud.Core.Repository.Model;

namespace TaxiCloud.Core.Api.Wialon
{
    public class WialonApi
    {
        private readonly string _token;

        public WialonApi(string token)
        {
            _token = token;
        }

        public List<Unit> GetUnits(string sid = null)
        {
            if (sid == null)
                sid = GetSid();
            if (sid == null)
                return null;
            var request = (HttpWebRequest)WebRequest.Create("https://hst-api.wialon.com/wialon/ajax.html?svc=core/update_data_flags&sid=" + sid);


            var postData = "params=%7B%22spec%22%3A%5B%7B%22type%22%3A%22type%22%2C%22data%22%3A%22avl_unit%22%2C%22flags%22%3A1025%2C%22mode%22%3A0%7D%5D%7D&sid=" + sid;
            var data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response1 = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();

            var units = JsonConvert.DeserializeObject<Unit[]>(responseString);

            return units.ToList();
        }

        private string GetSid()
        {
            var userInfo = GetUserInfo();
            return userInfo?.Eid;
        }

        public bool BlockUnit(Unit unit, string sid = null)
        {
            try
            {
                if (sid == null)
                    sid = GetSid();
                if (sid == null)
                    return false;
                var request = (HttpWebRequest)WebRequest.Create("https://hst-api.wialon.com/wialon/ajax.html?svc=core/batch&sid=" + sid);


                var postData = "params=%7B%22params%22%3A%5B%7B%22svc%22%3A%22unit%2Fexec_cmd%22%2C%22params%22%3A%7B%22itemId%22%3A" + unit.I + "%2C%22commandName%22%3A%22%D0%97%D0%B0%D0%B1%D0%BB%D0%BE%D0%BA%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D1%82%D1%8C%22%2C%22linkType%22%3A%22%22%2C%22param%22%3A%220a01%22%2C%22timeout%22%3A60%2C%22flags%22%3A0%7D%7D%5D%2C%22flags%22%3A0%7D&sid=" + sid;
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response1 = (HttpWebResponse)request.GetResponse();

                new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private int DateToUnix(DateTime value)
        {
            return (int)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        private WialonMessageModel[] GetUnitMessages(Unit unit, DateTime from, DateTime to, string sid = null)
        {

            try
            {
                if (sid == null)
                    sid = GetSid();
                if (sid == null)
                    return null;
                var request = (HttpWebRequest)WebRequest.Create("https://hst-api.wialon.com/wialon/ajax.html?svc=messages/load_interval&sid=" + sid);


                var postData = "params=%7B%22itemId%22%3A%22" + unit.I + "%22%2C%22timeFrom%22%3A" + DateToUnix(from) + "%2C%22timeTo%22%3A" + DateToUnix(to) + "%2C%22flags%22%3A0%2C%22flagsMask%22%3A0%2C%22loadCount%22%3A100%7D&sid=" + sid;
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response1 = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
                return JsonConvert.DeserializeObject<WialonUnitInfoModel>(responseString).Messages;
            }
            catch
            {
                return null;
            }
        }

        public bool IsUnitStanding(Unit unit, DateTime from, DateTime to, string sid = null)
        {
            var messages = GetUnitMessages(unit, from, to, sid);
            double maxDistance = 0.0;
            for (var i = 0; i < messages.Length; i++)
            {
                for (var j = 0; j < messages.Length; j++)
                {
                    if (i == j)
                        continue;
                    if (messages[i].Pos == null ||
                        messages[j].Pos == null)
                        continue;
                    var distance = new Coordinates(messages[i].Pos.Y, messages[i].Pos.X).DistanceTo(
                        new Coordinates(messages[j].Pos.Y, messages[j].Pos.X),
                        UnitOfLength.Meters
                        );
                    if (distance > maxDistance)
                        maxDistance = distance;
                }
            }
            return maxDistance < 5.0;
        }

        public bool TrySafeBlockUnit(Unit unit, out BlockError error, string sid = null)
        {
            if (sid == null)
                sid = GetSid();
            error = BlockError.UnknownSid;
            if (sid == null)
                return false;
            error = BlockError.Move;
            if (!IsUnitStanding(unit, DateTime.Now.AddMinutes(-5), DateTime.Now, sid))
                return false;
            error = BlockError.Request;
            if (!BlockUnit(unit, sid))
                return false;
            error = BlockError.None;
            return true;
        }

        // ReSharper disable once UnusedMember.Global
        public bool UnBlockUnit(Unit unit, string sid = null)
        {
            try
            {
                if (sid == null)
                    sid = GetSid();
                if (sid == null)
                    return false;
                var request = (HttpWebRequest)WebRequest.Create("https://hst-api.wialon.com/wialon/ajax.html?svc=core/batch&sid=" + sid);


                var postData = "params=%7B%22params%22%3A%5B%7B%22svc%22%3A%22unit%2Fexec_cmd%22%2C%22params%22%3A%7B%22itemId%22%3A" + unit.I + "%2C%22commandName%22%3A%22%D0%A0%D0%B0%D0%B7%D0%B1%D0%BB%D0%BE%D0%BA%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D1%82%D1%8C%22%2C%22linkType%22%3A%22%22%2C%22param%22%3A%22%22%2C%22timeout%22%3A60%2C%22flags%22%3A0%7D%7D%5D%2C%22flags%22%3A0%7D&sid=" + sid;
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response1 = (HttpWebResponse)request.GetResponse();

                new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserInfo GetUserInfo()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        ["params"] = "{\"token\":\"" + _token + "\",\"operateAs\":\"\",\"appName\":\"\",\"checkService\":\"\"}"
                    };
                    var response = client.UploadValues("https://hst-api.wialon.com/wialon/ajax.html?svc=token/login&sid=", values);

                    var responseString = Encoding.UTF8.GetString(response);
                    var res = JsonConvert.DeserializeObject<UserInfo>(responseString);
                    return res;
                }
            }
            catch
            {
                return null;
            }
        }

        public Unit GetUnit(Car car)
        {
            try
            {
                var sid = GetSid();
                var units = GetUnits(sid);
                foreach (var unit in units)
                {
                    var unitSig = unit.ToString().Split('-')[0].Trim().Replace(" ", string.Empty);
                    if (unitSig.ToLower() == car.Signal.ToLower())
                        return unit;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}