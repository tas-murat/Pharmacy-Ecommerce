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
    [Authorize(Roles = "member")]
    public class CartController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        public ActionResult Index()
        {
            ViewBag.Title = "Sora-Eczane| Alışveriş Sepetim";
            int sellerID = MemberFind.SellerID();
            List<Cart> carts = new List<Cart>();
            if (Request.Cookies["CKCart"] != null)
            {
                HttpCookie httpCookie = Request.Cookies["CKCart"];
                carts = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
            }
            if (carts.Count<1) return RedirectToAction("CartEmpty");
            List<CartDetail> cartDetails = CartList.getCartList(carts);
            CartVM cartVM = new CartVM
            {
                CartDetails = cartDetails,
                BestSellerProducts = repoProduct.GetAll().Include(i => i.Pictures).Include(i => i.Seller).Include(i => i.Favorites).Where(w => w.SellerID != sellerID).ToList()
            };
            return View(cartVM);
        }
        public ActionResult CartEmpty()
        {
            return View();
        }
    }
}