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
    public class WishlistController : Controller
    {
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        public ActionResult Index()
        {
            ViewBag.ActiveIndex = 2;
            ViewBag.ActiveSidebar = 6;
            ViewBag.Title = "Sora-Eczane| Favorilerim";
            int sellerID = MemberFind.SellerID();

            return View(repoFavorite.GetAll().Include(i => i.Product).Include(i => i.Product.Pictures).Include(i => i.Product.Seller).Where(w => w.SellerID == sellerID));
        }
    }
}