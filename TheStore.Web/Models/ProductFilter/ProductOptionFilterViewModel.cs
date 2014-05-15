using System.Collections.Generic;

namespace TheStore.Web.Models.ProductFilter
{
    public class ProductOptionFilterViewModel
    {
        public List<Domain.Characteristic> Characteristics { get; set; }
        public Domain.ProductFilter Filter { get; set; }
    }
}