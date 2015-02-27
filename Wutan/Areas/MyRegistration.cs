using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WTAN
{
    public class NetworkRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Network"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "Network_HomeDefault", // Route name
              "Network/{action}/{id}", // URL with parameters
              new { id = UrlParameter.Optional, controller = "Network", action = "Index" },// Parameter defaults
              new String[] { "WTAN.Network.Controllers" });


        }
    }
}