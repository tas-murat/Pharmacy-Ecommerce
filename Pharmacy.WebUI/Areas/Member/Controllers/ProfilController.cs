using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.WebUI.Areas.Member.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
        public ActionResult Index()
        {

            string username = HttpContext.User.Identity.Name;
            return View(repoSeller.GetBy(g=>g.username== username));
        }
        public ActionResult Edit(int? id)
        {
            return View(repoSeller.GetBy(g=>g.ID==id));
        }
        public ActionResult Sidebar()
        {
            return PartialView();
        }
    }
}