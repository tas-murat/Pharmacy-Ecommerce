using Newtonsoft.Json;
using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using Pharmacy.WebUI.Areas.Member.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        public ActionResult Index()
        {
            List<Cart> carts = new List<Cart>();
            if (Request.Cookies["CKCart"] != null)
            {
                HttpCookie httpCookie = Request.Cookies["CKCart"];
                carts = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
            }
            List<CartDetail> cartDetails = new List<CartDetail>();
            foreach (Cart hbc in carts)
            {
                cartDetails.Add(new CartDetail
                {
                    ProductID = hbc.ProductID,
                    Quantity = hbc.Quantity,
                    FPath = repoProduct.GetAll().Include(i => i.Pictures).Where(w => w.ID == hbc.ProductID).FirstOrDefault().Pictures.FirstOrDefault(f => f.pIndex == 1).FPath,
                    Price = repoProduct.GetBy(g => g.ID == hbc.ProductID).price,
                    ProductName = repoProduct.GetBy(g => g.ID == hbc.ProductID).name
                }
                );
            }
            CartVM cartVM = new CartVM
            {
                CartDetails = cartDetails,
                // BestSellerProducts = repoProduct.GetAll().Include(i => i.Pictures).OrderByDescending(o => o.OrderDetails.Sum(s => s.Quantity)).Take(8).ToList()
                BestSellerProducts = repoProduct.GetAll().Include(i => i.Pictures).ToList()
            };
            return View(cartVM);
        }
    }
}