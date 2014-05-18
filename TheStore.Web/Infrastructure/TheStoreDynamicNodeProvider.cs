using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Ajax.Utilities;
using MvcSiteMapProvider;
using TheStore.Web.Data;
using TheStore.Web.Domain;

namespace TheStore.Web.Infrastructure
{
    public class TheStoreDynamicNodeProvider : DynamicNodeProviderBase
    {
        private readonly ApplicationDbContext _context;

        public TheStoreDynamicNodeProvider()
        {
            _context = new ApplicationDbContext();
        }

        public TheStoreDynamicNodeProvider(ApplicationDbContext context)
        {
            _context = context ?? new ApplicationDbContext();
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var nodes = new List<DynamicNode>();
            var products = new List<Product>();
            var brands = new List<Brand>();

            foreach (var category in _context.Categories.Include(x => x.Categories).Include(x => x.Products).Where(x => x.ParentCategory == null).ToList())
            {
                var categoryDynamicNode = new DynamicNode
                {
                    Key = "category_" + category.CategoryId,
                    Title = category.Name
                };
                categoryDynamicNode.RouteValues.Add("categoryUrl", category.CategoryUrl);
                categoryDynamicNode.Action = "Index";
                categoryDynamicNode.Controller = "Category";
                nodes.Add(categoryDynamicNode);

                if (category.Products.Any())
                {
                    products.AddRange(category.Products);

                    foreach (var product in category.Products)
                    {
                        var productDynamicNode = new DynamicNode
                                {
                                    Key = "product_" + product.ProductId,
                                    ParentKey = categoryDynamicNode.Key,
                                    Title = product.Name
                                };
                        productDynamicNode.RouteValues.Add("categoryUrl", product.Category.CategoryUrl);
                        productDynamicNode.RouteValues.Add("brandUrl", product.Brand.BrandUrl);
                        productDynamicNode.RouteValues.Add("productUrl", product.Url);
                        productDynamicNode.Action = "View";
                        productDynamicNode.Controller = "Product";
                        nodes.Add(productDynamicNode);
                    }
                }

                if (category.Categories.Any())
                {
                    foreach (var subCategory in category.Categories)
                    {
                        var subCategoryDynamicNode = new DynamicNode
                        {
                            Key = "subCategory_" + subCategory.CategoryId,
                            ParentKey = categoryDynamicNode.Key,
                            Title = subCategory.Name
                        };
                        subCategoryDynamicNode.RouteValues.Add("categoryUrl", subCategory.CategoryUrl);
                        subCategoryDynamicNode.Action = "Index";
                        subCategoryDynamicNode.Controller = "Category";
                        nodes.Add(subCategoryDynamicNode);

                        if (subCategory.Products.Any())
                        {
                            products.AddRange(subCategory.Products);

                            foreach (var product in subCategory.Products)
                            {
                                var productDynamicNode = new DynamicNode
                                {
                                    Key = "product_" + product.ProductId,
                                    ParentKey = subCategoryDynamicNode.Key,
                                    Title = product.Name
                                };
                                productDynamicNode.RouteValues.Add("categoryUrl", product.Category.CategoryUrl);
                                productDynamicNode.RouteValues.Add("brandUrl", product.Brand.BrandUrl);
                                productDynamicNode.RouteValues.Add("productUrl", product.Url);
                                productDynamicNode.Action = "View";
                                productDynamicNode.Controller = "Product";
                                nodes.Add(productDynamicNode);
                            }

                            brands = (from product in subCategory.Products select product.Brand).Distinct().ToList();

                            foreach (var brand in brands)
                            {
                                var brandDynamicNode = new DynamicNode
                                {
                                    Key = string.Format("category_{0}_brand_{1}", subCategory.CategoryId, brand.BrandId),
                                    ParentKey = subCategoryDynamicNode.Key,
                                    Title = brand.Name
                                };
                                brandDynamicNode.RouteValues.Add("categoryUrl", subCategory.CategoryUrl);
                                brandDynamicNode.RouteValues.Add("brandUrl", brand.BrandUrl);
                                brandDynamicNode.Action = "Index";
                                brandDynamicNode.Controller = "Brand";
                                nodes.Add(brandDynamicNode);
                            }
                        }
                    }
                }

                brands = (from product in products select product.Brand).Distinct().ToList();

                foreach (var brand in brands)
                {
                    var brandDynamicNode = new DynamicNode
                    {
                        Key = string.Format("category_{0}_brand_{1}", category.CategoryId, brand.BrandId),
                        ParentKey = categoryDynamicNode.Key,
                        Title = brand.Name
                    };
                    brandDynamicNode.RouteValues.Add("categoryUrl", category.CategoryUrl);
                    brandDynamicNode.RouteValues.Add("brandUrl", brand.BrandUrl);
                    brandDynamicNode.Action = "Index";
                    brandDynamicNode.Controller = "Brand";
                    nodes.Add(brandDynamicNode);
                }
            }

            return nodes;
        }
    }
}