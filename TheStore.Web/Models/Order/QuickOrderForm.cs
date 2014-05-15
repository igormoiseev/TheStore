using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheStore.Web.Models.Order
{
    public class QuickOrderForm
    {
        [HiddenInput]
        public string ReturnUrl { get; set; }

        [Display(Name = "Имя"), Required]
        public string Name { get; set; }

        [Display(Name = "Контактный телефон"), Required]
        public string Phone { get; set; }

        [Display(Name = "Email (не обязательно)")]
        public string Email { get; set; }
    }
}