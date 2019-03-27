using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Contacte")]
    public class Contacte
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(50)]
        public string name { get; set; }
        [Column(TypeName = "varchar"), StringLength(50)]
        public string email { get; set; }
        [Column(TypeName = "varchar"), StringLength(50)]
        public string phone { get; set; }
        [Column(TypeName = "varchar"), StringLength(200)]
        public string subject { get; set; }
        [Column(TypeName = "text")]
        public string message { get; set; }
        [Column(TypeName = "varchar"), StringLength(200)]
        public string file { get; set; }

    }
}
