using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.BOL.Entities
{
    [Table("ProductTag")]
    public class ProductTag
    {
        [Key,Column(Order = 1)]
        public int ProductID { get; set; }

        [Key, Column(Order = 2)]
        public int TagID { get; set; }

        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}