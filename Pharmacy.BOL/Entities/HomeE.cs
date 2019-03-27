using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("HomeE")]
    public class HomeE
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Başlık")]
        public string title { get; set; }
        [Column(TypeName = "varchar"), StringLength(200), Display(Name = "Açıklama")]
        public string description { get; set; }
        [Column(TypeName = "varchar"), StringLength(150), Display(Name = "Resim Dosyası")]
        public string FPath { get; set; }

        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Linki")]
        public string link { get; set; }
        [Display(Name = "Hangi Bölgede Gösterilecek")]
        public int place { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int pIndex { get; set; }

    }
}
