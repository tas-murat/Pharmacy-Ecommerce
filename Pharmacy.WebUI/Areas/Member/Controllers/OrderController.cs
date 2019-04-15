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
    public class OrderController : Controller
    {
        SqlRepository<OrderDetail> repoOrderDetail = new SqlRepository<OrderDetail>();
        public ActionResult Index()
        {
            ViewBag.ActiveIndex = 4;
            ViewBag.ActiveSidebar = 3;
            int sellerID = MemberFind.SellerID();
            return View(repoOrderDetail.GetAll().Include(i=>i.Order).Include(i => i.Product).Include(i => i.Order.Seller).Where(w=>w.Order.SellerID==sellerID));
        }
        public ActionResult Detail(int? id)
        {
            ViewBag.ActiveIndex = 4;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
    }
}