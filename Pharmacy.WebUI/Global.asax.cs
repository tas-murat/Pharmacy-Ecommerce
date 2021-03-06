﻿using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Pharmacy.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            SqlRepository<Seller> repoSeller = new SqlRepository<Seller>();
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string memberUserName = HttpContext.Current.User.Identity.Name;
                    string rol = repoSeller.GetAll().FirstOrDefault(g => g.GLNNumber == memberUserName).Rol;
                    string[] roles = rol.Split(',');
                    HttpContext.Current.User = new GenericPrincipal((FormsIdentity)HttpContext.Current.User.Identity, roles);
                }
            }
        }
    }
}
