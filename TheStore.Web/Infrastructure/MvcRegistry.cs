using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using StructureMap.Configuration.DSL;
using TheStore.Web.Data;
using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public class MvcRegistry : Registry
    {
        public MvcRegistry()
        {
            For<BundleCollection>().Use(BundleTable.Bundles);
            For<RouteCollection>().Use(RouteTable.Routes);
            For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
            For<HttpSessionStateBase>().Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
            For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
            For<HttpServerUtilityBase>().Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
            For<Microsoft.AspNet.Identity.IUserStore<ApplicationUser>>().Use<Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>>();
            For<Microsoft.AspNet.Identity.IRoleStore<IdentityRole>>().Use<Microsoft.AspNet.Identity.EntityFramework.RoleStore<IdentityRole>>();
            For<System.Data.Entity.DbContext>().Use(() => new ApplicationDbContext());
        }
    }
}