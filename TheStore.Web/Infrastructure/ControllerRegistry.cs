using StructureMap.Configuration.DSL;

namespace TheStore.Web.Infrastructure
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(scan => scan.With(new ControllerConvention()));
        }
    }
}