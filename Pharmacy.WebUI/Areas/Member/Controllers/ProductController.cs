using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using Pharmacy.WebUI.Areas.Member.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize(Roles = "member")]
    public class ProductController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        public ActionResult Index(int? id)
        {
            ViewBag.Title = "Sora-Eczane| Ürün Detay";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetAll().Include(i => i.Pictures).Include(i => i.Seller).Where(g => g.ID == id).FirstOrDefault();
            int sellerID = MemberFind.SellerID();

            SingleVM singleVM = new SingleVM
            {
                product = product,
                BestProducts = repoProduct.GetAll().Include(i => i.Pictures).ToList(),
                FavoriteProducts= repoFavorite.GetAll().Include(i=>i.Product).Include(i => i.Product.Pictures).Where(w=>w.SellerID== sellerID).ToList()
            };
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(singleVM);
        }
    }
}