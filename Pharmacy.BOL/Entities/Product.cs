using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar"), StringLength(150), Display(Name = "Ürün Adı")]
        public string name { get; set; }
        [Column(TypeName = "varchar"), StringLength(500), Display(Name = "Açıklama")]
        public string description { get; set; }
        [Display(Name = "Fiyatı")]
        public decimal price { get; set; }
        [Display(Name = "Stock")]
        public int stock { get; set; }

        [Display(Name = "İndirim")]
        public bool IsDiscount { get; set; }
        [DataType(DataType.DateTime), Display(Name = "Son Kullanma Tarihi")]
        public DateTime expDate { get; set; }
        [DataType(DataType.DateTime), Display(Name = "Yayınlanma Tarihi")]
        public DateTime releaseDate { get; set; }
        [Column(TypeName = "varchar"), StringLength(50), Display(Name = "Ürün Barkodu")]
        public string barkod { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public int? SellerID { get; set; }
        public Seller Seller { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductProperty> ProductProperties { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
