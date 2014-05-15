using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TheStore.Web.Domain
{
    public enum OrderState
    {
        New,
        QuickOrder,
        InProgress,
        Shipped,
        Completed,
        Error
    }

    [Table("Orders")]
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        [Key]
        public int OrderId { get; set; }

        public Guid OrderUrl { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderState OrderState { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string Description { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int? DeliveryDetailsId { get; set; }
        public virtual DeliveryDetails DeliveryDetails { get; set; }
        public decimal GetOrderTotal()
        {
            return OrderItems.Sum(x => x.Product.GetProductPrice()*x.Quantity);
        }
    }
}