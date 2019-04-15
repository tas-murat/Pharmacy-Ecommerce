using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;

namespace Pharmacy.WebUI.Areas.Admin.Controllers
{
    public class SellersController : Controller
    {
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();

        // GET: Admin/Sellers
        public ActionResult Index()
        {
            return View(repoSeller.GetAll().Where(w=>w.Rol!="admin"));
        }

        // GET: Admin/Sellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = repoSeller.GetBy(g => g.ID == id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = repoSeller.GetBy(g => g.ID == id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Admin/Sellers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seller seller)
        {
            if (ModelState.IsValid)
            {
                repoSeller.Update(seller);
                return RedirectToAction("Index");
            }
            return View(seller);
        }

        // GET: Admin/Sellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = repoSeller.GetBy(g => g.ID == id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Admin/Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = repoSeller.GetBy(g => g.ID == id);
            repoSeller.Remove(seller);
            return RedirectToAction("Index");
        }
    }
}
