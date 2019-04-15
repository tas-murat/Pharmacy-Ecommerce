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
    public class CategoryController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Category> repoCategory = new SqlRepository<Category>();
        SqlRepository<Brand> repoBrand = new SqlRepository<Brand>();
        SqlRepository<Tag> repoTag = new SqlRepository<Tag>();
        SqlRepository<FooterE> repoFooterE = new SqlRepository<FooterE>();
        SqlRepository<FAQCategory> repoFAQCategory = new SqlRepository<FAQCategory>();
        SqlRepository<FAQ> repoFAQ = new SqlRepository<FAQ>();
        SqlRepository<Contacte> repoContacte = new SqlRepository<Contacte>();
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        public ActionResult Index()
        {
            ViewBag.Title = "Sora-Eczane| PazarYeri";
            ViewBag.ActiveIndex = 1;
           
            int sellerID = MemberFind.SellerID();
            List<Product> product = repoProduct.GetAll().Include(i => i.Favorites).Include(i => i.Pictures).Include(i => i.Seller).Include(i => i.ProductCategories.Select(s=>s.Category)).Where(w => w.SellerID != sellerID).ToList();
            CategoryVM categoryVM = new CategoryVM
            {
                products = product,
                Categories = repoCategory.GetAll().Include(i => i.Children).ToList(),
                Brands = repoBrand.GetAll().ToList(),
                Tags = repoTag.GetAll().ToList()
            };

            return View(categoryVM);
        }
        [HttpGet]
        public ActionResult Search(string search)
        {
            ViewBag.ActiveIndex = 1;
            ViewBag.Title = "Sora-Eczane| PazarYeri";
            int sellerID = MemberFind.SellerID();
            IQueryable<Product> product = repoProduct.GetAll().Include(i => i.Favorites).Include(i => i.Pictures).Include(i => i.Seller).Include(i => i.ProductCategories).Include(i => i.ProductCategories.Select(s=>s.Category)).Where(w => w.SellerID != sellerID);
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(w => w.name.Contains(search) || w.Brand.name.Contains(search) || w.ProductCategories.Any(a=>a.Category.name.Contains(search)));
            }
            CategoryVM categoryVM = new CategoryVM
            {
                products = product.ToList(),
                Categories = repoCategory.GetAll().Include(i => i.Children).ToList(),
                Brands = repoBrand.GetAll().ToList(),
                Tags = repoTag.GetAll().ToList()
            };
            return View("index", categoryVM);
        }
        public ActionResult filter(int? id, int?[] BrandID, string minPrice, string maxPrice,int? tagID)
        {
            int sellerID = MemberFind.SellerID();
            IQueryable<Product> product = repoProduct.GetAll().Include(i => i.Favorites).Include(i => i.Pictures).Include(i => i.Seller).Include(i => i.ProductCategories).Include(i => i.ProductCategories.Select(s => s.Category)).Include(i=>i.ProductTags).Include(i=>i.ProductTags.Select(s=>s.Tag)).Where(w => w.SellerID != sellerID);
            if (id.HasValue)
            {
                product = product.Where(w => w.ProductCategories.Any(a=>a.Category.ID==id));
            }
            if (tagID.HasValue)
            {
                product = product.Where(w => w.ProductTags.Any(a=>a.Tag.ID==tagID));
            }
            if (BrandID!=null)
            {
                product = product.Where(w => BrandID.Contains(w.BrandID));
            }
            if (!string.IsNullOrEmpty(minPrice) && !string.IsNullOrEmpty(maxPrice))
            {
                decimal price1 = Convert.ToDecimal(minPrice.Replace('.',','));
                decimal price2 = Convert.ToDecimal(maxPrice.Replace('.', ','));
                product = product.Where(w => w.price >= price1 && w.price <= price2);
            }
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.Categories = repoCategory.GetAll().Include(i => i.Children).ToList();
            categoryVM.Brands = repoBrand.GetAll().ToList();
            categoryVM.products = product.ToList();
            categoryVM.Tags = repoTag.GetAll().ToList();

            //CategoryVM categoryVM = new CategoryVM
            //{
            //    Categories = repoCategory.GetAll().Include(i => i.Children).Include(i => i.Products).ToList(),
            //    Brands = repoBrand.GetAll().Include(i => i.Products).ToList(),
            //    Products = repoProduct.GetAll().Where(w=>w.CategoryID==CatID).Include(i => i.Pictures).ToList()
            //};
            return View("Index", categoryVM);
        }
    }
}