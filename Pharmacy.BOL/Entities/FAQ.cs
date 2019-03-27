using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("FAQ")]
    public class FAQ
    {
        public int ID { get; set; }

        [StringLength(180), Column(TypeName = "varchar")]
        public string question { get; set; }
        [Column(TypeName = "text")]
        public string answer { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int PIndex { get; set; }
        public int FAQCategoryID { get; set; }
        public virtual FAQCategory FAQCategory { get; set; }

    }
}
