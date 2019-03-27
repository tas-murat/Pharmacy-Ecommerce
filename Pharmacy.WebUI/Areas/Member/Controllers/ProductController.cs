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
    [Authorize]
    public class ProductController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetAll().Include(i => i.Pictures).Include(i => i.Seller).Where(g => g.ID == id).FirstOrDefault();
            string memberUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.username == memberUserName);
            SingleVM singleVM = new SingleVM
            {
                product = product,
                BestProducts = repoProduct.GetAll().Include(i => i.Pictures).ToList(),
                FavoriteProducts= repoFavorite.GetAll().Include(i=>i.Product).Include(i => i.Product.Pictures).Where(w=>w.SellerID== seller.ID).ToList()
            };
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(singleVM);
        }
    }
}