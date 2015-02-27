using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demo
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
               "Admin/{action}/{id}", // URL with parameters
               new { controller = "Account", action = "Main", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             "User_Upload", // Route name
             "UserUpload/UploadServer/{guid}_{dir}", // URL with parameters
             new { controller = "Upload", action = "UploadServer" }
             );

            routes.MapRoute(
                "Home_CategoryDefaulU", // Route name
                "CategoryU/{categoryURL}", // URL with parameters
                new { controller = "Home", action = "Region" }, // Parameter defaults
                new { categoryURL = "^[0-9a-zA-Z_]{1,}$" }
            );

            routes.MapRoute(
               "Home_CategoryDefaultA", // Route name
               "CategoryA/{categoryid}", // URL with parameters
               new { controller = "Home", action = "RegionByID" }, // Parameter defaults
               new { categoryid = "^[0-9]{1,}$" }
           );

            routes.MapRoute(
              "Home_GuideDetail", // Route name
              "GuideDetail/{autokey}.html", // URL with parameters
              new { controller = "Home", action = "GuideDetail" }, // Parameter defaults
              new { autokey = "^[0-9]{1,}$" }
            );

        routes.MapRoute(
               "Home_SummerDetailU", // Route name
               "TravelDetail/{seourl}.html", // URL with parameters
               new { controller = "Home", action = "Detail" }, // Parameter defaults
               new { seourl = "^[0-9a-zA-Z_]{1,}$" }
           );

        routes.MapRoute(
               "Home_SummerDetailA", // Route name
               "TravelDetailA/{autokey}.html", // URL with parameters
               new { controller = "Home", action = "DetailA" }, // Parameter defaults
               new { autokey = "^[0-9]{1,}$" }
           );

        routes.MapRoute(
              "Home_VisaDetail", // Route name
              "VisaCenter/{autokey}.html", // URL with parameters
              new { controller = "Home", action = "VisaDetail" }, // Parameter defaults
              new { autokey = "^[0-9]{1,}$" }
          );

        routes.MapRoute(
              "Home_VisaQusetionDetail", // Route name
              "VisaQuestionDetailA/{autokey}.html", // URL with parameters
              new { controller = "Home", action = "VisaQuestionDetailA" }, // Parameter defaults
              new { autokey = "^[0-9]{1,}$" }
          );

        routes.MapRoute(
              "Home_VisaQusetionDetailU", // Route name
              "VisaQuestionDetail/{seourl}.html", // URL with parameters
              new { controller = "Home", action = "VisaQuestionDetail" }, // Parameter defaults
              new { seourl = "^[0-9a-zA-Z_]{1,}$" }
          );

        routes.MapRoute(
            "Home_ContactDetail", // Route name
            "Contact/{autokey}.html", // URL with parameters
            new { controller = "Home", action = "Contact" }, // Parameter defaults
            new { autokey = "^[0-9]{1,}$" }
        );

        routes.MapRoute(
            "Home_ContactDetailU", // Route name
            "Contact/{seourl}.html", // URL with parameters
            new { controller = "Home", action = "Contact" }, // Parameter defaults
            new { seourl = "^[0-9a-zA-Z_]{1,}$" }
        );

            routes.MapRoute(
            "Home_TicketDetail", // Route name
            "TicketCenter/{autokey}.html", // URL with parameters
            new { controller = "Home", action = "TicketDetail" }, // Parameter defaults
            new { autokey = "^[0-9]{1,}$" }
            );

            routes.MapRoute(
            "Home_TicketDetailU", // Route name
            "TicketCenter/{seourl}.html", // URL with parameters
            new { controller = "Home", action = "TicketDetail" }, // Parameter defaults
            new { seourl = "^[0-9a-zA-Z_]{1,}$" }
            );
             routes.MapRoute(
            "Home_RaiderDetail", // Route name
            "RaiderDetail/{autokey}.html", // URL with parameters
            new { controller = "Home", action = "RaiderDetail" }, // Parameter defaults
            new { autokey = "^[0-9]{1,}$" }
            );

            routes.MapRoute(
            "Home_RaiderDetailU", // Route name
            "RaiderDetail/{seourl}.html", // URL with parameters
            new { controller = "Home", action = "RaiderDetail" }, // Parameter defaults
            new { seourl = "^[0-9a-zA-Z_]{1,}$" }
            );
            
        routes.MapRoute(
              "Home_Default", // Route name
              "{action}", // URL with parameters
              new { controller = "Home", action = "Index" } // Parameter defaults
          );
        

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },// Parameter defaults
                new String[] { "XUniversalCMS.Controllers" }
            );

            

            

            

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

        }
    }
}