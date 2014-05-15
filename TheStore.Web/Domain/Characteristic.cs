using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStore.Web.Domain
{
    [Table("Characteristics")]
    public class Characteristic
    {
        public Characteristic()
        {
            Options = new List<Option>();
        }

        [Key]
        public int CharacteristicId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int SequenceNumber { get; set; }
        public bool IsFilterable { get; set; }
        public virtual ICollection<Option> Options { get; set; }
    }
}