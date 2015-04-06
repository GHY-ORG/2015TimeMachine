using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _2015TimeMachineCookie
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //picture pool
            routes.MapRoute(
                "PicPool",
                "PicPool/Id/{id}/Size/{size}",
                new { controller = "PicPool", action = "Get"}
            );

            //search picture
            routes.MapRoute(
                name: "PictureSearch",
                url: "Picture/{action}/{input}/Order/{order}/Page/{page}",
                defaults: new { controller = "Picture", action = "Search" },
                constraints: new { input = @"\s", order = @"\d", page = @"[1-9][0-9]*" }
            );
            //picture order page
            routes.MapRoute(
                name: "PictureOrderPage",
                url: "Picture/Order/{order}/Page/{page}",
                defaults: new { controller = "Picture", action = "Index" },
                constraints: new { order = @"\d", page = @"[1-9][0-9]*" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
