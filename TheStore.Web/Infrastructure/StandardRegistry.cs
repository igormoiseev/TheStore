using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace TheStore.Web.Infrastructure
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.With(new ControllerConvention());
            });
        }
    }
}