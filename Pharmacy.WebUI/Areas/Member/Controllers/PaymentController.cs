using Newtonsoft.Json;
using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using Pharmacy.WebUI.Areas.Member.Helpers;
using Pharmacy.WebUI.Areas.Member.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    public class PaymentController : Controller
    {
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Address> repoAddress = new SqlRepository<Address>();
        public ActionResult Index()
        {
            int sellerID = MemberFind.SellerID();
            List<Cart> carts = new List<Cart>();
            if (Request.Cookies["CKCart"] != null)
            {
                HttpCookie httpCookie = Request.Cookies["CKCart"];
                carts = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
            }
            if (carts.Count < 1) return RedirectToAction("CartEmpty");
            List<CartDetail> cartDetails = CartList.getCartList(carts);
            PaymentVM paymentVM = new PaymentVM
            {
                CartDetails = cartDetails,
                Seller=repoSeller.GetAll().Include(i=>i.Address).FirstOrDefault(f=>f.ID==sellerID),
                Addresses=repoAddress.GetAll().Include(i=>i.Seller).Include(i=>i.District).Include(i=>i.District.City).Where(w=>w.SellerID==sellerID).ToList()
            };
            return View(paymentVM);
        }
    }
}