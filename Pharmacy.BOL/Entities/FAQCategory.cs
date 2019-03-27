using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("FAQCategory")]
    public class FAQCategory
    {
        public int ID { get; set; }

        [StringLength(100), Column(TypeName = "varchar")]
        public string name { get; set; }
        public virtual ICollection<FAQ> FAQs { get; set; }

    }
}
