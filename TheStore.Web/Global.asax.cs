using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;
using TheStore.Web.Domain;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Tasks;

namespace TheStore.Web
{
    public class MvcApplication : HttpApplication
    {
        public IContainer Container 
        {
            get { return (IContainer) HttpContext.Current.Items["Container"]; }
            set { HttpContext.Current.Items["Container"] = value; }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));

            ObjectFactory.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new MvcRegistry());
                cfg.AddRegistry(new ActionFilterRegistry(() => Container ?? ObjectFactory.Container));
                cfg.AddRegistry(new TaskRegistry());
                cfg.AddRegistry(new OrderRegistry());
            });

            using (var container = ObjectFactory.Container.GetNestedContainer())
            {
                foreach (var task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (var task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }

            ModelBinders.Binders.Add(typeof(ProductFilter), new ProductFilterModelBinder());
            ModelBinders.Binders.Add(typeof(ShoppingCart), new ShoppingCartModelBinder());
        }

        public void Application_BeginRequest()
        {
            Container = ObjectFactory.Container.GetNestedContainer();

            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }
    }
}
