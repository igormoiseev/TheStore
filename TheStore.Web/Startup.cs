using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheStore.Web.Startup))]
namespace TheStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
