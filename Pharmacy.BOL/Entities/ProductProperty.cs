using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("ProductProperty")]
    public class ProductProperty
    {
        [Key, Column(Order = 1)]
        public int ProductID { get; set; }

        [Key, Column(Order = 2)]
        public int PropertyID { get; set; }

        public Product Product { get; set; }
        public Property Property { get; set; }

    }
}
