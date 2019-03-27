using Pharmacy.WebUI.Areas.Member.ViewModels;
using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pharmacy.WebUI.Areas.Member.Helpers;
using System.IO;
using Pharmacy.WebUI.Models;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize]
    public class HomeController : Controller
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
        // GET: Member/Home
        public ActionResult Index()
        {
            ViewBag.ActiveIndex = 1;
            IndexVM ındexVM = new IndexVM {
                products=repoProduct.GetAll().Include(i=>i.Pictures).Include(i => i.Seller).Include(i=>i.Favorites),
                Categories=repoCategory.GetAll().Include(i => i.Children).ToList(),
                Brands=repoBrand.GetAll().ToList(),
                Tags=repoTag.GetAll().ToList()
            };
            string memberUserName = HttpContext.User.Identity.Name;
            Seller seller = repoSeller.GetBy(g => g.username == memberUserName);
            ViewBag.MemberID = seller.ID;

            return View(ındexVM);
        }
        public ActionResult FAQ()
        {
            ViewBag.ActiveIndex = 5;
            return View(repoFAQCategory.GetAll().Include(i=>i.FAQs));
        }
        public ActionResult Contact()
        {
            ViewBag.ActiveIndex = 6;
            return View(repoFAQCategory.GetAll().Include(i => i.FAQs));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Contacte contacte, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    if (!Directory.Exists(Server.MapPath("~/Content/images/contact"))) Directory.CreateDirectory(Server.MapPath("~/Content/images/contact"));
                    string yol = Path.Combine(Server.MapPath("~/Content/images/contact"), Path.GetFileName(Tools.DosyaYoluOlustur(file.FileName, contacte.ID.ToString())));
                    file.SaveAs(yol);
                    contacte.file = "/Content/images/contact" + Path.GetFileName(Tools.DosyaYoluOlustur(file.FileName, contacte.ID.ToString()));
                }
                repoContacte.Add(contacte);
                SendMessage ms = new SendMessage();
                bool kontrol = ms.MailGonder(contacte.name, contacte.email, contacte.subject, contacte.message);
                if (kontrol) TempData["sonuc"] = "Mesajınız başarıyla gönderildi";
                else TempData["sonuc"] = "Mesajınız gönderilemedi";
                return RedirectToAction("Contact");
            }

            return View(contacte);
        }
        public ActionResult Header()
        {
            HeaderVM headerVM = new HeaderVM {
                Brands=repoBrand.GetAll().ToList(),
                Categories=repoCategory.GetAll().Include(i=>i.Children).ToList()
            };
            return PartialView(headerVM);
        }
       
        public ActionResult Footer()
        {
            FooterVM footerVM = new FooterVM
            {
                Tags = repoTag.GetAll().ToList(),
                FAQCategories = repoFAQCategory.GetAll().ToList(),
                Brands = repoBrand.GetAll().ToList(),
                FooterEs = repoFooterE.GetAll().ToList(),
                Categories = repoCategory.GetAll().Include(i => i.Children).ToList()
            };
            return PartialView(footerVM);
        }
        [HttpPost]
        public void AddCart(int productID, int quantity)
        {
            CartHelper.AddCart(productID, quantity);
        }
        [HttpPost]
        public void UpdateCart(int productID, int quantity)
        {
            CartHelper.UpdateCart(productID, quantity);
        }
        [HttpPost]
        public void DeleteCart(int productID)
        {
            CartHelper.DeleteCart(productID);
        }
    }
}