using Pharmacy.BOL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Areas.Member.ViewModels
{
    public class SingleVM
    {
        public Product product { get; set; }
        public ICollection<Product> BestProducts { get; set; }
        public ICollection<Favorite> FavoriteProducts { get; set; }
    }
}