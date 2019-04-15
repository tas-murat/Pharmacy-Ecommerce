using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Brand")]
    public class Brand
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Marka")]
        public string name { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int PIndex { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
