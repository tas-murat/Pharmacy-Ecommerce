using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Areas.Member.ViewModels
{
    public class MemberFind
    {
        public static int SellerID()
        {
            SqlRepository<Seller> repoSeller= new SqlRepository<Seller>();
            string memberUserName = HttpContext.Current.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.GLNNumber == memberUserName);
            return seller.ID;
        }
    }
}