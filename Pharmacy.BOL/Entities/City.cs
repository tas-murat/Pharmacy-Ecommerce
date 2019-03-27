using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.BOL.Entities
{
    [Table("City")]
    public class City
    {
        [Key]
        [Column(TypeName = "varchar"), StringLength(3)]
        public string plaka { get; set; }
        [Column(TypeName = "varchar"), StringLength(100)]
        public string name { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}