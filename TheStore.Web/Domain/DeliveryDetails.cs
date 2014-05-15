using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    public enum DeliveryType
    {
        Courier,
        NovaPoshta
    }

    [Table("DeliveryDetails")]
    public class DeliveryDetails
    {
        [Key]
        public int DeliveryDetailsId { get; set; }

        public DeliveryType DeliveryType { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public int OrderId { get; set; }
    }
}