﻿using System.ComponentModel.DataAnnotations;

namespace TheStore.Web.Models.Characteristic
{
    public class NewCharacteristicForm
    {
        [Required, Display(Name = "Название")]
        public string Name { get; set; }

        [Required, Display(Name = "URL")]
        public string Url { get; set; }

        [Required, Display(Name = "Описание"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, Display(Name = "Категория"), DataType("CategoryId")]
        public int CategoryId { get; set; }

        [Required, Display(Name = "Порядковый номер")]
        public int SequenceNumber { get; set; }

        [Display(Name = "Фильтр")]
        public bool IsFilterable { get; set; }
    }
}