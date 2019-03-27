using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Models
{
    public class CategoryFAQCategory
    {
      public List<FAQCategory> faqCategories { get; set; }
      public List<Category> categories { get; set; }

    }
}