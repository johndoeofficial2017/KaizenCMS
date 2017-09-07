using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UIServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Telerik.Reporting.Services.WebApi.ReportsControllerConfiguration.RegisterRoutes(config);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
