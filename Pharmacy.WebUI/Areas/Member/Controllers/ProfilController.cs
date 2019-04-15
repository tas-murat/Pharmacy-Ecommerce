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
    public class ProfilController : Controller
    {
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        SqlRepository<Address> repoAddress = new SqlRepository<Address>();
        SqlRepository<City> repoCity = new SqlRepository<City>();
        SqlRepository<District> repoDistrict = new SqlRepository<District>();
        public ActionResult Index()
        {
            ViewBag.Title = "Sora-Eczane| Profilim";
            ViewBag.ActiveIndex = 2;
            ViewBag.ActiveSidebar = 1;
            int sellerID = MemberFind.SellerID();
            return View(repoSeller.GetBy(g => g.ID == sellerID));
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "Sora-Eczane| Profilim";
            return View(repoSeller.GetBy(g => g.ID == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seller model)
        {
            ViewBag.Title = "Sora-Eczane| Profilim";
            ViewBag.ActiveIndex = 2;
            Seller seller = repoSeller.GetBy(g => g.ID == model.ID);
            seller.GLNNumber = model.GLNNumber;
            seller.name = model.name;
            seller.surname = model.surname;
            seller.pharmacyName = model.pharmacyName;
            seller.email = model.email;
            seller.phone = model.phone;
            seller.password2 = seller.password;
            if (seller != null)
            {
                repoSeller.Update(seller);
                return RedirectToAction("Index");
            }

            return View(seller);
        }
        public ActionResult Sidebar()
        {
            return PartialView();
        }
        public ActionResult ChangePass(int? id)
        {
            ViewBag.ActiveIndex = 2;
            ViewBag.Title = "Sora-Eczane| Profilim";
            return View(repoSeller.GetBy(g => g.ID == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(int id, string oldPass, string newPass, string newPass2)
        {

            ViewBag.Title = "Sora-Eczane| Profilim";
            Seller seller = repoSeller.GetBy(g => g.ID == id);
            if (seller != null && !string.IsNullOrEmpty(oldPass) && !string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(newPass2))
            {
                if (seller.password == oldPass && newPass == newPass2)
                {
                    seller.password = newPass;
                    seller.password2 = newPass2;
                    repoSeller.Update(seller);
                    return RedirectToAction("Index");
                }
            }

            return View(seller);
        }
        public ActionResult Address()
        {
            ViewBag.ActiveIndex = 2;
            ViewBag.ActiveSidebar = 5;
            int sellerID = MemberFind.SellerID();
            return View(repoAddress.GetAll().Where(w => w.SellerID == sellerID).Include(i => i.Seller).Include(i => i.District).Include(i => i.District.City));
        }
        public ActionResult CreateAddress()
        {
            ViewBag.ActiveIndex = 2;
            ViewBag.ActiveSidebar = 5;
            return View(repoCity.GetAll());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAddress(string address, string name, int DistrictID)
        {
            int sellerID = MemberFind.SellerID();


            if (!string.IsNullOrEmpty(address) && DistrictID!=0 && !string.IsNullOrEmpty(name))
            {
                Address a = new Address
                {
                    address = address,
                    name = name,
                    DistrictID=DistrictID,
                    SellerID=sellerID

                };
                repoAddress.Add(a);
                return RedirectToAction("Address");
            }
            return View();
        }
    }
}