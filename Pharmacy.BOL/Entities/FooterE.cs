using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("FooterE")]
    public class FooterE
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Adı")]
        public string name { get; set; }

        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Linki")]
        public string link { get; set; }
        [Display(Name = "Footer in hangi Bölgesi")]
        public int check { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int pIndex { get; set; }

    }
}
