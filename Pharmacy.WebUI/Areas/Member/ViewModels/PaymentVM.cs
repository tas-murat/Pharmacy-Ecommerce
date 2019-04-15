using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Areas.Member.ViewModels
{
    public class PaymentVM
    {
        public ICollection<CartDetail> CartDetails { get; set; }
        public Seller Seller { get; set; }
        public ICollection<Address> Addresses { get; set; }

    }
}