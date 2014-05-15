using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    [Table("Options")]
    public class Option
    {
        public Option()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int OptionId { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int CharacteristicId { get; set; }
        public virtual Characteristic Characteristic { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}