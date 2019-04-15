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
    [Authorize(Roles = "member")]
    public class AJAXController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<District> repoDistrict = new SqlRepository<District>();
        SqlRepository<Address> repoAddress = new SqlRepository<Address>();
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
            int sellerID = MemberFind.SellerID();

            Favorite favorite = repoFavorite.GetBy(g => g.SellerID == sellerID && g.ProductID == productID);
            if (favorite != null)
            {
                repoFavorite.Remove(favorite);
                return 0;
            }
            else
            {
                repoFavorite.Add(new Favorite { SellerID = sellerID, ProductID = productID });
                return 1;
            }
        }
        public ActionResult getDistrict(string plaka)
        {
            return Json(repoDistrict.GetAll().Where(w => w.CityPlaka == plaka).Select(s => new { s.ID, s.name }), JsonRequestBehavior.AllowGet);
        } public ActionResult getAddress(int ID)
        {
            int sellerID = MemberFind.SellerID();
            List<Address> addresses = repoAddress.GetAll().Include(i => i.Seller).Include(i => i.District).Include(i => i.District.City).Where(w => w.SellerID == sellerID).ToList();
            getMyAdress myAdress =new getMyAdress {
                name=addresses.FirstOrDefault(f=>f.ID==ID).address,
                city = addresses.FirstOrDefault(f=>f.ID==ID).District.City.name,
                district = addresses.FirstOrDefault(f=>f.ID==ID).District.name
            };
            
            return Json(myAdress, JsonRequestBehavior.AllowGet);
        }
    }
    public class getMyAdress{
        public string name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
    }
}