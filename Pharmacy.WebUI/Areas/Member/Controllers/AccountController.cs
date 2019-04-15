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
using Pharmacy.WebUI.Areas.Member.ViewModels;
using Pharmacy.WebUI.Models;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize(Roles ="member")]
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
            ViewBag.Title = "Sora-Eczane| Hesabım";
            ViewBag.ActiveIndex = 3;
            ViewBag.ActiveSidebar = 2;
            int sellerID = MemberFind.SellerID();
            var products = repoProduct.GetAll().Include(p => p.Brand).Include(p => p.Seller).Include(i=>i.Pictures).Where(w=>w.SellerID==sellerID);
            return View(products.ToList());
        }

        // GET: Member/Acount/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.ActiveIndex = 3;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetAll().Include(b=>b.Brand).Include(i=>i.Pictures).FirstOrDefault(g=>g.ID==id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductDetailVM productDetailVM = new ProductDetailVM
            {
                product=product,
                Categories=repoCategory.GetAll().Include(i=>i.Children).Include(i => i.ProductCategories).Where(w=>w.ProductCategories.Any(a=>a.ProductID==id)).ToList(),
                Tags=repoTag.GetAll().Include(i=>i.ProductTags).Where(w=>w.ProductTags.Any(a=>a.ProductID==id)).ToList()
            };
            return View(productDetailVM);
        }

        // GET: Member/Acount/Create
        public ActionResult Create()
        {
            ViewBag.ActiveIndex = 3;
            ProductCreatVM productCreatVM = new ProductCreatVM
            {
                Categories = repoCategory.GetAll().Include(i => i.Children).ToList(),
                Brands= repoBrand.GetAll().ToList(),
                Tags =repoTag.GetAll().ToList()
            };
            return View(productCreatVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, string CategoriIDs, string newbrand,string chnewBrand, int[] Tags, List<HttpPostedFileBase> Pictures)
        {
            string memberUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.GLNNumber == memberUserName);
            int lastImgID = 0;
            if (!string.IsNullOrEmpty(newbrand) && !string.IsNullOrEmpty(chnewBrand))
            {
                int pindex = repoBrand.GetAll().OrderByDescending(o => o.PIndex).FirstOrDefault().PIndex+1;
                Brand brand = new Brand { name=newbrand,PIndex= pindex };
                repoBrand.Add(brand);
                product.BrandID = brand.ID;
            }
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
                        if (!Directory.Exists(Server.MapPath("~/Content/member/product/" + seller.GLNNumber + "/"))) Directory.CreateDirectory(Server.MapPath("~/Content/member/product/" + seller.GLNNumber + "/"));
                        string yol = Path.Combine(Server.MapPath("~/Content/member/product/"+ seller.GLNNumber+"/"), Path.GetFileName(FilePath.getfilePath(picture.FileName, lastImgID+""+sayac)));
                        picture.SaveAs(yol);
                        setPictures.Add(new Picture {
                            FPath = "/Content/member/product/" + seller.GLNNumber + "/" + Path.GetFileName(FilePath.getfilePath(picture.FileName, lastImgID + "" + sayac)),
                            pIndex = 1
                        });
                    }
                }
                if (setPictures.Count() > 0) product.Pictures = setPictures;
                product.releaseDate = DateTime.Now;
                product.SellerID = seller.ID;
                repoProduct.Add(product);
                if (!string.IsNullOrEmpty(CategoriIDs))
                {
                    string[] catIDs = CategoriIDs.Trim(',').Split(',');
                    foreach (string hbcID in catIDs)
                    {
                        int catID = Convert.ToInt32(hbcID);
                        repoProductCategory.Add(new ProductCategory {
                            CategoryID= catID,
                            ProductID=product.ID
                        });
                    }
                }
                if (Tags.Count() > 0)
                {
                    foreach (int hbtID in Tags)
                    {
                        repoProductTag.Add(new ProductTag
                        {
                            TagID = hbtID,
                            ProductID=product.ID
                        });
                    }
                }
                return RedirectToAction("Index");
            }



            ViewBag.BrandID = new SelectList(repoBrand.GetAll(), "ID", "name", product.BrandID);
            return View(product);
        }

        // GET: Member/Acount/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ActiveIndex = 3;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.GetAll().Include(i=>i.Brand).FirstOrDefault(g => g.ID == id);
            ViewBag.BrandID =repoBrand.GetAll();
            if (product == null)
            {
                return HttpNotFound();
            }
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
            ViewBag.ActiveIndex = 3;
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
