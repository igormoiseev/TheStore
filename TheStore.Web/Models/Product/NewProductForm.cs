﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheStore.Web.Models.Product
{
    public class NewProductForm
    {
        public NewProductForm()
        {
            Options = new List<Domain.Option>();
        }

        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "Код")]
        public string Code { get; set; }

        [Required, Display(Name = "URL")]
        public string Url { get; set; }

        [Required, Display(Name = "Цена")]
        public decimal InitialPrice { get; set; }

        [Required, Display(Name = "Наценка (%)")]
        public decimal ProductFee { get; set; }

        [Required, Display(Name = "Доставка")]
        public decimal DeliveryCharge { get; set; }

        [Required, Display(Name = "Описание"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, Display(Name = "Категория"), DataType("CategoryId")]
        public int CategoryId { get; set; }

        [Required, Display(Name = "Производитель"), DataType("BrandId")]
        public int BrandId { get; set; }

        [Required, DataType("Options")]
        public List<Domain.Option> Options { get; set; }
    }
}