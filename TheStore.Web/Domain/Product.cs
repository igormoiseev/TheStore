using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            Photos = new List<Photo>();
        }

        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal ProductFee { get; set; }
        public decimal DeliveryCharge { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public decimal GetProductPrice()
        {
            return Math.Ceiling(InitialPrice + ((InitialPrice*ProductFee)/100) + DeliveryCharge);
        }
    }
}