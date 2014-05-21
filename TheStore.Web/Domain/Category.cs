using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TheStore.Web.Domain
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            Categories = new List<Category>();
        }

        [Key]
        public int CategoryId { get; set; }
        [StringLength(70)]
        public string Name { get; set; }
        [StringLength(255)]
        public string CategoryUrl { get; set; }
        public string Description { get; set; }
        public int SequenceNumber { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public ICollection<Category> Categories { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public List<Brand> GetBrands()
        {
            var brands = (from product in Products select product.Brand).Distinct().ToList();
            return brands;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            if(this.Products.Any())
                products.AddRange(this.Products);

            if (this.Categories.Any())
            {
                foreach (var subCategory in this.Categories)
                {
                    if (subCategory.Products.Any())
                    {
                        products.AddRange(subCategory.Products);
                    }
                }
            }

            return products;
        }
    }
}