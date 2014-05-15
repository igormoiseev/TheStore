using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public class OrderRegistry : Registry
    {
        public OrderRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("TheStore"));
                scan.AddAllTypesOf<IOrderSubmitter>();
            });
        }
    }
}