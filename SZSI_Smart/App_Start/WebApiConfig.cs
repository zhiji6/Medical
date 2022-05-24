using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Client
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("*", "origin,x-requested-with,content-type,Authorization,tenantid", "*");
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //cors.SupportsCredentials = true;
            //config.EnableCors(cors);
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
