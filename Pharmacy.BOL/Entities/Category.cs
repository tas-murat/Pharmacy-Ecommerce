using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Category")]
    public class Category
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Kategori Adı")]
        public string name { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int PIndex { get; set; }
        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
