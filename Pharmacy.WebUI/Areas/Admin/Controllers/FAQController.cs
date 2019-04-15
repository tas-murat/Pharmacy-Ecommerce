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
    [Authorize(Roles = "admin")]
    public class FAQController : Controller
    {
        SqlRepository<FAQ> repoFAQ = new SqlRepository<FAQ>();
        SqlRepository<FAQCategory> repoFAQCategory = new SqlRepository<FAQCategory>();
        // GET: Admin/FAQ
        public ActionResult Index()
        {
           
            return View(repoFAQ.GetAll().Include(i=>i.FAQCategory));
        }

        // GET: Admin/FAQ/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = repoFAQ.GetBy(g=>g.ID==id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // GET: Admin/FAQ/Create
        public ActionResult Create()
        {
            ViewBag.FAQCategoryID = new SelectList(repoFAQCategory.GetAll(), "ID", "name");
            return View();
        }

        // POST: Admin/FAQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken,ValidateInput(false)]
        public ActionResult Create(FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                repoFAQ.Add(fAQ);
                return RedirectToAction("Index");
            }

            ViewBag.FAQCategoryID = new SelectList(repoFAQCategory.GetAll(), "ID", "name", fAQ.FAQCategoryID);
            return View(fAQ);
        }

        // GET: Admin/FAQ/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = repoFAQ.GetBy(g => g.ID == id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            ViewBag.FAQCategoryID = new SelectList(repoFAQCategory.GetAll(), "ID", "name", fAQ.FAQCategoryID);
            return View(fAQ);
        }

        // POST: Admin/FAQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                repoFAQ.Update(fAQ);
                return RedirectToAction("Index");
            }
            ViewBag.FAQCategoryID = new SelectList(repoFAQCategory.GetAll(), "ID", "name", fAQ.FAQCategoryID);
            return View(fAQ);
        }

        // GET: Admin/FAQ/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = repoFAQ.GetBy(g => g.ID == id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // POST: Admin/FAQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FAQ fAQ = repoFAQ.GetBy(g => g.ID == id);
            repoFAQ.Remove(fAQ);
            return RedirectToAction("Index");
        }
    }
}
