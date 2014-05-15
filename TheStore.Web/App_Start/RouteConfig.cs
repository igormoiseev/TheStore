using System.Web.Mvc;
using System.Web.Routing;

namespace TheStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Order",
                url: "order-{orderUrl}",
                defaults: new { controller = "Order", action = "View" });
            
            routes.MapRoute(
                name: "Product",
                url: "category-{categoryUrl}/brand-{brandUrl}/product-{productUrl}",
                defaults: new { controller = "Product", action = "View" });

            routes.MapRoute(
                name: "Brand",
                url: "category-{categoryUrl}/brand-{brandUrl}",
                defaults: new { controller = "Brand", action = "Index" });

            routes.MapRoute(
                name: "Category",
                url: "category-{categoryUrl}",
                defaults: new {controller = "Category", action = "Index"});

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
