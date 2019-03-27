using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using Pharmacy.WebUI.Models;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        SqlRepository<Product> repoProduct = new SqlRepository<Product>();
        SqlRepository<Favorite> repoFavorite = new SqlRepository<Favorite>();
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<Brand> repoBrand = new SqlRepository<Brand>();
        SqlRepository<Picture> repoPicture = new SqlRepository<Picture>();
        SqlRepository<Category> repoCategory = new SqlRepository<Category>();
        SqlRepository<ProductCategory> repoProductCategory = new SqlRepository<ProductCategory>();
        SqlRepository<Tag> repoTag = new SqlRepository<Tag>();
        SqlRepository<ProductTag> repoProductTag = new SqlRepository<ProductTag>();
        
        public ActionResult Index()
        {
            string memberUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.username == memberUserName);
            var products = repoProduct.GetAll().Include(p => p.Brand).Include(p => p.Seller).Include(i=>i.Pictures).Where(w=>w.SellerID==seller.ID);
            return View(products.ToList());
        }

        // GET: Member/Acount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetBy(g=>g.ID==id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Member/Acount/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(repoBrand.GetAll(), "ID", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, List<HttpPostedFileBase> Pictures)
        {
            string memberUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.username == memberUserName);
            int lastImgID = 0;
            if (repoProduct.GetAll().Any()) {
                lastImgID = repoPicture.GetAll().OrderByDescending(o => o.ID).FirstOrDefault().ID;
            }
            if (!string.IsNullOrEmpty(product.name))
            {
                List<Picture> setPictures = new List<Picture>();
                int sayac = 0;
                foreach (var picture in Pictures)
                {
                    if (picture != null)
                    {
                        sayac++;
                        if (!Directory.Exists(Server.MapPath("~/Content/member/product/" + seller.username + "/"))) Directory.CreateDirectory(Server.MapPath("~/Content/member/product/" + seller.username + "/"));
                        string yol = Path.Combine(Server.MapPath("~/Content/member/product/"+ seller.username+"/"), Path.GetFileName(Tools.DosyaYoluOlustur(picture.FileName, lastImgID+""+sayac)));
                        picture.SaveAs(yol);
                        setPictures.Add(new Picture {
                            FPath = "/Content/member/product/" + seller.username + "/" + Path.GetFileName(Tools.DosyaYoluOlustur(picture.FileName, lastImgID + "" + sayac)),
                            pIndex = 1
                        });
                    }
                }
                if (setPictures.Count() > 0) product.Pictures = setPictures;
                product.releaseDate = DateTime.Now;
                product.SellerID = seller.ID;
                repoProduct.Add(product);
                return RedirectToAction("Index");
            }



            ViewBag.BrandID = new SelectList(repoBrand.GetAll(), "ID", "name", product.BrandID);
            return View(product);
        }

        // GET: Member/Acount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetBy(g => g.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(repoBrand.GetAll(), "ID", "name", product.BrandID);
            ViewBag.SellerID = new SelectList(repoSeller.GetAll(), "ID", "username", product.SellerID);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repoProduct.Update(product);
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(repoBrand.GetAll(), "ID", "name", product.BrandID);
            ViewBag.SellerID = new SelectList(repoSeller.GetAll(), "ID", "username", product.SellerID);
            return View(product);
        }

        // GET: Member/Acount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetBy(g => g.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Member/Acount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repoProduct.GetBy(g => g.ID == id);
            repoProduct.Remove(product);
            return RedirectToAction("Index");
        }
        
    }
}
