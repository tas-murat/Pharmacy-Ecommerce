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
    public class AJAXController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        public ActionResult CardDetail()
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
            return Json(cartDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int addFavorite(int productID)
        {
            string memberUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.username == memberUserName);

            Favorite favorite = repoFavorite.GetBy(g => g.SellerID == seller.ID && g.ProductID == productID);
            if (favorite != null)
            {
                repoFavorite.Remove(favorite);
                return 0;
            }
            else
            {
                repoFavorite.Add(new Favorite { SellerID = seller.ID,ProductID=productID });
                return 1;
            }
        }
    }
}