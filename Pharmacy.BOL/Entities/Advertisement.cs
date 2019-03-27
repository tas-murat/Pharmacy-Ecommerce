using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Advertisement")]
    public class Advertisement
    {
        public int ID { get; set; }

        [StringLength(150), Column(TypeName = "varchar"), Display(Name = "Reklam Adı")]
        public string Name { get; set; }

        [StringLength(150), Column(TypeName = "varchar"), Display(Name = "Resim Dosyası")]
        public string FPath { get; set; }

        [StringLength(150), Column(TypeName = "varchar"), Display(Name = "Reklam Linki")]
        public string Link { get; set; }

        [Display(Name = "Görüntülenme Sırası")]
        public int PIndex { get; set; }

    }
}
