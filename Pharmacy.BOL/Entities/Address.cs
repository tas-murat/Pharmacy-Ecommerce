using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Address")]
    public class Address
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100)]
        public string name { get; set; }
        [Column(TypeName = "varchar"), StringLength(250)]
        public string address { get; set; }
        public int SellerID { get; set; }
        public Seller Seller { get; set; }
        public int DistrictID { get; set; }
        public District District { get; set; }
    }
}
