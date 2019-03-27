﻿using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using Pharmacy.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pharmacy.WebUI.Controllers
{
    public class HomeController : Controller
    {
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<HomeE> repoHomeE = new SqlRepository<HomeE>();
        SqlRepository<Slider> repoSlider = new SqlRepository<Slider>();
        SqlRepository<FAQCategory> repoFAQCategory = new SqlRepository<FAQCategory>();
        SqlRepository<Category> category = new SqlRepository<Category>();
        SqlRepository<Contacte> repoContacte = new SqlRepository<Contacte>();
        public ActionResult Index()
        {
            ViewBag.ActiveIndex = 1;
            @ViewBag.Title = "Sitemize Hoşgeldiniz";
            HomeVM homeVM = new HomeVM {
                homeE=repoHomeE.GetAll(),
                sliders=repoSlider.GetAll().OrderBy(o=>o.pindex).ToList()
            };
            return View(homeVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contacte contacte, HttpPostedFileBase file)
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
               bool kontrol= ms.MailGonder(contacte.name, contacte.email, contacte.subject, contacte.message);
                if (kontrol) TempData["sonuc"] = "Mesajınız başarıyla gönderildi";
                else TempData["sonuc"] = "Mesajınız gönderilemedi";
                return RedirectToAction("Index");
            }
           
            return View(contacte);
        }
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.rUrl= ReturnUrl;
            @ViewBag.Title = "Giriş Sayfası";
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password,string rUrl,string cbremember)
        {
            bool benihatirla = false;
            Seller seller = repoSeller.GetBy(w=>(w.username==username||w.email==username) && w.password== password);
            if (seller != null)
            {
                if (cbremember != null) benihatirla = true;
                FormsAuthentication.SetAuthCookie(username, benihatirla);
                Session["AdSoyad"] = seller.name+" "+ seller.surname;
                if (!string.IsNullOrEmpty(rUrl))
                    return Redirect(rUrl);
                else
                    return Redirect("/Member");
            }
            else
            {
                ViewBag.error = "Kullanıcı adı veya şifre yanlış";
               
                return View();
            }
        }
        public ActionResult Register()
        {
            @ViewBag.Title = "Üye Ol";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,username,name,surname,pharmacyName,email,phone,password,password2")]Seller seller)
        {
            if (!string.IsNullOrEmpty(seller.username) && !string.IsNullOrEmpty(seller.password))
            {
                if (!repoSeller.GetAll().Any(a => a.username == seller.username))
                {
                    seller.Rol = "member";
                    seller.lastEntryDate = DateTime.Now;
                    seller.lastEntryIP = Request.UserHostAddress;
                    repoSeller.Add(seller);
                    FormsAuthentication.SetAuthCookie(seller.username, false);
                    Session["AdSoyad"] = seller.name + " " + seller.surname;
                    return Redirect("/Member");
                }
                else ViewBag.kuladi = "Bu Kullanıcı Adı Daha önce kullanılmıştır";
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}