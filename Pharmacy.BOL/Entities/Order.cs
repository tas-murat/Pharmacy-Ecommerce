using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BOL.Entities
{
    [Table("Order")]
    public class Order
    {
        public int ID { get; set; }

        [StringLength(10), Column(TypeName = "varchar"), Display(Name = "Sipariş Numarası")]
        public string OrderNumber { get; set; }

        [StringLength(80), Column(TypeName = "varchar"), Display(Name = "Mail Adresi")]
        public string MailAddress { get; set; }

        [Display(Name = "Ödeme Seçeneği")]
        public PayOption PayOption { get; set; }

        [StringLength(50), Column(TypeName = "varchar"), Display(Name = "Teslimat Adı Soyadı")]
        public string DeliveryNameSurname { get; set; }

        [StringLength(30), Column(TypeName = "varchar"), Display(Name = "Teslimat Telefonu")]
        public string DeliveryPhone { get; set; }

        [StringLength(250), Column(TypeName = "varchar"), Display(Name = "Teslimat Adresi")]
        public string DeliveryAddress { get; set; }

        [StringLength(50), Column(TypeName = "varchar"), Display(Name = "Fatura Adı Soyadı/Ünvan")]
        public string InvoiceNameSurname { get; set; }

        [StringLength(30), Column(TypeName = "varchar"), Display(Name = "Fatura Vergi Dairesi")]
        public string InvoiceTaxOffice { get; set; }

        [StringLength(30), Column(TypeName = "varchar"), Display(Name = "Fatura T.C. No / Vergi Numarası")]
        public string InvoiceTaxNumber { get; set; }

        [StringLength(250), Column(TypeName = "varchar"), Display(Name = "Fatura Adresi")]
        public string InvoiceAddress { get; set; }

        [Display(Name = "Kargo Ücreti")]
        public decimal Shipment { get; set; }

        [Display(Name = "Sipariş Durumu")]
        public OrderStatus OrderStatus { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Son Giriş Tarihi")]
        public DateTime RecDate { get; set; }

        [StringLength(20), Column(TypeName = "Varchar"), Display(Name = "Son Giriş IP Adresi")]
        public string IPAddress { get; set; }
        
        public int SellerID { get; set; }
        public Seller Seller { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public enum PayOption
    {
        KapıdaOdeme = 1,
        HavaleEFT,
        KrediKarti
    }
    public enum OrderStatus
    {
        Hazırlanıyor = 1,
        Paketlendi,
        KargoVerildi,
        TeslimEdildi,
        TedarikEdilemedi,
        IptalEdildi
    }
}
