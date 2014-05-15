using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    [Table("Customers")]
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}