using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TheStore.Web.Domain
{
    public class ProductFilter
    {
        private List<Brand> _brands;
        private List<Option> _options;
        public ReadOnlyCollection<Brand> Brands {
            get { return _brands.AsReadOnly(); }
        }
        public ReadOnlyCollection<Option> Options {
            get { return _options.AsReadOnly(); }
        }

        public ProductFilter()
        {
            _brands = new List<Brand>();
            _options = new List<Option>();
        }

        public void AddBrand(Brand brand)
        {
            _brands.Add(brand);
        }

        public void RemoveBrand(Brand brand)
        {
            var removeBrand = _brands.SingleOrDefault(x => x.BrandId == brand.BrandId);
            _brands.Remove(removeBrand);
        }

        public void AddOption(Option option)
        {
            _options.Add(option);
        }

        public void RemoveOption(Option option)
        {
            var removeOption = _options.SingleOrDefault(x => x.OptionId == option.OptionId);
            _options.Remove(removeOption);
        }

        public List<Product> Filter(IEnumerable<Product> products)
        {
            var result = new List<Product>();
            result.AddRange(_brands.Any() ? products.Where(x => _brands.Any(b => b.BrandId == x.BrandId)) : products);

            if (_options.Any())
            {
                result = result.Where(p => _options.Any(o => p.Options.Any( x=> x.OptionId == o.OptionId))).ToList();
            }

            return result;
        }

        public void Clear()
        {
            _brands.Clear();
            _options.Clear();
        }
    }
}