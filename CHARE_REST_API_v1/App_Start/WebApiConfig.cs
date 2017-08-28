using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CHARE_REST_API_v1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;            
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
