﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ElevenNote.Web
{
    public class RouteConfig
    {
        //Conifigures the routing engine for MVC
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
