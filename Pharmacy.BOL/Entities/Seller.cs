using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Seller")]
    public class Seller
    {
        public int ID { get; set; }
        [StringLength(13, ErrorMessage = "GLN Numarası 13 karekterden fazla olamaz"), Column(TypeName = "Varchar"), Display(Name = "GLN Numarası"), Required(ErrorMessage = "Gln Numarası boş geçilemez.")]
        public string GLNNumber { get; set; }
        [StringLength(50), Column(TypeName = "Varchar"), Display(Name = "Yetkili Adı"), Required(ErrorMessage = "Ad boş geçilemez.")]
        public string name { get; set; }
        [StringLength(50), Column(TypeName = "Varchar"), Display(Name = "Yetkili Soyadı"), Required(ErrorMessage = "Soyad boş geçilemez.")]
        public string surname { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Pharmacy Adı")]
        public string pharmacyName { get; set; }
        [Column(TypeName = "varchar"), StringLength(40), Display(Name = "Mail Adresi")]
        public string email { get; set; }
        [Column(TypeName = "varchar"), StringLength(20), Display(Name = "Telefon Numarası")]
        public string phone { get; set; }
     
        [StringLength(20), Column(TypeName = "Varchar"), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre boş geçilemez.")]
        public string password { get; set; }
        [NotMapped, Compare("password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string password2 { get; set; }
        [DataType(DataType.DateTime), Display(Name = "Son Giriş Tarihi")]
        public DateTime lastEntryDate { get; set; }
        [StringLength(20), Column(TypeName = "Varchar"), Display(Name = "Son Giriş IP Adresi")]
        public string lastEntryIP { get; set; }
        [StringLength(20), Column(TypeName = "Varchar"), Display(Name = "Rolü")]
        public string Rol { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
