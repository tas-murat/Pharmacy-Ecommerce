using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Slider")]
    public class Slider
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Slider Başlığı")]
        public string title { get; set; }
        [Column(TypeName = "varchar"), StringLength(50), Display(Name = "Vurgulanacak Kelime")]
        public string important { get; set; }
        [Column(TypeName = "varchar"), StringLength(150), Display(Name = "Slider Acıklaması")]
        public string description { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Link")]
        public string link{ get; set; }
        [Column(TypeName = "varchar"), StringLength(50), Display(Name = "Link Başlığı")]
        public string linktitle { get; set; }

        [Column(TypeName = "varchar"), StringLength(200), Display(Name = "Slider Resmi")]
        public string image { get; set; }
        [Display(Name = "Slider Sırası")]
        public int pindex { get; set; }

    }
}
