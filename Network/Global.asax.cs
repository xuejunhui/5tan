using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Network
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "404page", // Route name
              "Page404", // URL with parameters
              new { controller = "Network", action = "Page404"} // Parameter defaults
          );

            routes.MapRoute(
               "Cotnent_Default", // Route name
               "{seourl}.html", // URL with parameters
               new { controller = "Network", action = "Content", seourl = UrlParameter.Optional } // Parameter defaults
           );

            routes.MapRoute(
             "Guide_Default", // Route name
             "{seourl}/{autokey}.html", // URL with parameters
             new { controller = "Network", action = "Guide", seourl = UrlParameter.Optional, autokey = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
             "GuideCase_Default", // Route name
             "{seourl}/{cseourl}/{autokey}.html", // URL with parameters
             new { controller = "Network", action = "GuideCase"} // Parameter defaults
            );

            routes.MapRoute(
               "WebsiteCase", // Route name
               "WebsiteCase/{cseourl}", // URL with parameters
               new { controller = "Network", action = "WebsiteCase", cseourl = UrlParameter.Optional } // Parameter defaults
           );

            routes.MapRoute(
               "News", // Route name
               "News/{cseourl}", // URL with parameters
               new { controller = "Network", action = "News", cseourl = UrlParameter.Optional } // Parameter defaults
           );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Network", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

           

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}