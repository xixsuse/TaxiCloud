using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using TaxiCloud.Core;
using TaxiCloud.Core.IoC;
using TaxiCloud.Core.Repository;

namespace TaxiCloud.WebApi
{
    internal static class Program
    {
        private static void Main()
        {
            IocKernel.Initialize(new IocConfiguration());

            //ClearDb();

            var core = IocKernel.Get<CoreProccessor>();

            CoreStarter.InitializeCore(core);

            var baseUrl = "http://*:3131";
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.ReadLine();
            }
        }

        private static void ClearDb()
        {
            var sql = IocKernel.Get<SqlRepository>();
            sql.Drivers.RemoveRange(sql.Drivers);
            sql.Users.RemoveRange(sql.Users);
            sql.Notifications.RemoveRange(sql.Notifications);
            sql.Cars.RemoveRange(sql.Cars);
            sql.SaveChanges();
        }

        //public class OptionsHttpMessageHandler : DelegatingHandler
        //{
        //    protected override Task<HttpResponseMessage> SendAsync(
        //        HttpRequestMessage request, CancellationToken cancellationToken)
        //    {
        //        if (request.Method == HttpMethod.Options)
        //        {
        //            Console.WriteLine("ITS A OPTIONS REQUEST");
        //            return Task.Factory.StartNew(() =>
        //            {
        //                var resp = new HttpResponseMessage(HttpStatusCode.OK);

        //                return resp;
        //            }, cancellationToken);
        //        }
        //        return base.SendAsync(request, cancellationToken);
        //    }
        //}

        //private static HttpConfiguration GetServer()
        //{
        //    var config = new HttpConfiguration();
        //    //if (Type.GetType("Mono.Runtime") != null)
        //    //{
        //    //    Console.WriteLine("ITS A MONO RUNTIME");
        //    //    config.MessageHandlers.Add(new OptionsHttpMessageHandler());
        //    //}
        //    //var cors = new EnableCorsAttribute("*", "*", "*");
        //    //config.EnableCors(cors);


        //    config.Formatters.Clear();
        //    config.Formatters.Add(new JsonMediaTypeFormatter());
        //    config.Formatters.JsonFormatter.SerializerSettings =
        //        new JsonSerializerSettings
        //        {
        //            ContractResolver = new CamelCasePropertyNamesContractResolver()
        //        };
        //    config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

        //    config.MapHttpAttributeRoutes();
        //    config.Routes.MapHttpRoute(
        //        "API Default", "api/{controller}/{action}/{id}",
        //        new { id = RouteParameter.Optional });
        //    return config;
        //}
    }

    public class MonoPatchingDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Host = request.Headers.GetValues("Host").FirstOrDefault();
            return await base.SendAsync(request, cancellationToken);
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(ConfigureWebApi());
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
            //Attribute Routing aktivieren

            if (Type.GetType("Mono.Runtime") != null)
            {
                config.MessageHandlers.Add(new MonoPatchingDelegatingHandler());
            }
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(

                "DefaultApi",

                "api/{controller}/{action}/{id}",

                new { id = RouteParameter.Optional });

            return config;
        }
    }
}
