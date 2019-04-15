using Pharmacy.WebUI.Areas.Member.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pharmacy.BLL.Repositories;
using Pharmacy.BOL.Entities;
using System.Data.Entity;

namespace Pharmacy.WebUI.Areas.Member.Helpers
{
    public class CartList
    {
      
        public static List<CartDetail> getCartList(List<Cart> carts)
        {
            SqlRepository<Product> repoProduct = new SqlRepository<Product>();
           
            List<CartDetail> cartDetails = new List<CartDetail>();
            foreach (Cart hbc in carts)
            {
                cartDetails.Add(new CartDetail
                {
                    ProductID = hbc.ProductID,
                    Quantity = hbc.Quantity,
                    FPath = repoProduct.GetAll().Include(i => i.Pictures).Where(w => w.ID == hbc.ProductID).FirstOrDefault().Pictures.FirstOrDefault(f => f.pIndex == 1).FPath,
                    Price = repoProduct.GetBy(g => g.ID == hbc.ProductID).price,
                    ProductName = repoProduct.GetBy(g => g.ID == hbc.ProductID).name
                }
                );
            }
            return cartDetails;
        }
    }
}