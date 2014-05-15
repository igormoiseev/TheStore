using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheStore.Web.Models.Order
{
    public class OrderCheckoutViewModel
    {
        [HiddenInput]
        public string ReturnUrl { get; set; }

        [Display(Name = "Имя"), Required]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия"), Required]
        public string LastName { get; set; }
        [Display(Name = "Контактный номер телефона"), Required]
        public string Phone { get; set; }
        [Display(Name = "Email (если хотите отслеживать заказ)"), Required]
        public string Email { get; set; }
        [Display(Name = "Город"), Required]
        public string City { get; set; }
        [Display(Name = "Улица"), Required]
        public string Street { get; set; }
        [Display(Name = "Номер дома"), Required, DataType("HouseNumber")]
        public string HouseNumber { get; set; }
        [Display(Name = "Индекс")]
        public string ZipCode { get; set; }
        [Display(Name = "Комментарий к заказу"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Domain.ShoppingCart ShoppingCart { get; set; }
    }
}