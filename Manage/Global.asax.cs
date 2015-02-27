using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Manage
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
              "Account", // Route name
              "{action}", // URL with parameters
              new { controller = "Account", action = "Main", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             "User_Upload", // Route name
             "UserUpload/UploadServer/{guid}_{dir}", // URL with parameters
             new { controller = "Upload", action = "UploadServer" }
             );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}/{id1}", // URL with parameters
                new { controller = "Account", action = "Login", id = UrlParameter.Optional, id1 = UrlParameter.Optional } // Parameter defaults
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