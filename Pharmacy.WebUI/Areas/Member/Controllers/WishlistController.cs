using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        public ActionResult Index()
        {
            string sellerUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.username == sellerUserName);

            return View(repoFavorite.GetAll().Include(i => i.Product).Include(i => i.Product.Pictures).Include(i => i.Product.Seller).Where(w => w.SellerID == seller.ID));
        }
    }
}