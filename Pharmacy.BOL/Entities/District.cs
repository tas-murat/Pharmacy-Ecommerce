using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.BOL.Entities
{
    [Table("District")]
    public class District
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(100)]
        public string name { get; set; }
        [Column(TypeName = "varchar"), StringLength(3), Required()]
        public string CityPlaka { get; set; }
        public City City { get; set; }
        public ICollection<Address> Address { get; set; }
    }
}