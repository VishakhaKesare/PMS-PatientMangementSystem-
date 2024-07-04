using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PatientManagementSoftware
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Route for Login controller with default action Index
            routes.MapRoute(
                name: "Login",
                url: "Login/{action}",
                defaults: new { controller = "Login", action = "Index" }
            );

            // Route for Admin_Dashboard controller with optional id parameter
            routes.MapRoute(
                name: "AdminDashboard",
                url: "Admin_Dashboard/{action}/{id}",
                defaults: new { controller = "Admin_Dashboard", action = "Index", id = UrlParameter.Optional }
            );

            // Default route for other controllers with optional id parameter
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Route for Doctor controller's EditDoctor action with optional doctorID parameter
            routes.MapRoute(
                name: "DoctorEdit",
                url: "Doctor/EditDoctor/{doctorID}",
                defaults: new { controller = "Doctor", action = "EditDoctor", doctorID = UrlParameter.Optional }
            );

            // Route for Invoicing controller's EditInvoice action with required invoiceID parameter
            routes.MapRoute(
                name: "EditInvoice",
                url: "Invoicing/EditInvoice/{invoiceID}",
                defaults: new { controller = "Invoicing", action = "EditInvoice" }
            );
        }
    }
}