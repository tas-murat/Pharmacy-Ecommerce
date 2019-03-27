using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Areas.Member.ViewModels
{
    public class CartVM
    {
        public ICollection<CartDetail> CartDetails { get; set; }
        public ICollection<Product> BestSellerProducts { get; set; }
    }
    public class CartDetail
    {
        public int ProductID { get; set; }
        public string FPath { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}