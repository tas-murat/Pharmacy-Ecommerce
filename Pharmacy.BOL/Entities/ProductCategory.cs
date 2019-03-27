using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.BOL.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key,Column(Order = 1)]
        public int ProductID { get; set; }

        [Key, Column(Order = 2)]
        public int CategoryID { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}