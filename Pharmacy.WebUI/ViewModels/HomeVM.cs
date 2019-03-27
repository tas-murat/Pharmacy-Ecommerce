using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Models
{
    public class HomeVM
    {
      public IQueryable<HomeE> homeE { get; set; }
      public List<Slider> sliders { get; set; }

    }
}