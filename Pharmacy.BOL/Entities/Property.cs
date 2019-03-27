using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Property")]
    public class Property
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100)]
        public string name { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int PIndex { get; set; }
        public ICollection<ProductProperty> ProductProperties { get; set; }

    }
}
