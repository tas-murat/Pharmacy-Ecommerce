using Pharmacy.WebUI.Areas.Member.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Areas.Member.Helpers
{
    public class CartHelper
    {
        public static void AddCart(int productID, int quantity)
        {
            CreateCart();
            List<Cart> cartList = new List<Cart>();
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies["CKCart"];
            if (!string.IsNullOrEmpty(httpCookie.Value))
            {
                cartList = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
                if (cartList.Any(a => a.ProductID == productID)) cartList.FirstOrDefault(f => f.ProductID == productID).Quantity += quantity;
                else cartList.Add(new Cart { ProductID = productID, Quantity = quantity });
            }
            else cartList.Add(new Cart { ProductID = productID, Quantity = quantity });
            httpCookie.Value = JsonConvert.SerializeObject(cartList);
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        public static void UpdateCart(int productID, int quantity)
        {
            CreateCart();
            List<Cart> cartList = new List<Cart>();
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies["CKCart"];
            if (!string.IsNullOrEmpty(httpCookie.Value))
            {
                cartList = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
                if (cartList.Any(a => a.ProductID == productID)) cartList.FirstOrDefault(f => f.ProductID == productID).Quantity = quantity;
            }
            httpCookie.Value = JsonConvert.SerializeObject(cartList);
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        public static void DeleteCart(int productID)
        {
            CreateCart();
            List<Cart> cartList = new List<Cart>();
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies["CKCart"];
            if (!string.IsNullOrEmpty(httpCookie.Value))
            {
                cartList = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
                Cart deletedCart = cartList.Where(w => w.ProductID == productID).FirstOrDefault();
                cartList.Remove(deletedCart);
            }
            httpCookie.Value = JsonConvert.SerializeObject(cartList);
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        public static int CartCount()
        {
            if (HttpContext.Current.Request.Cookies["CKCart"] != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["CKCart"];
                List<Cart> cartList = new List<Cart>();
                cartList = JsonConvert.DeserializeObject<List<Cart>>(httpCookie.Value);
                return cartList.Sum(s => s.Quantity);
            }
            else return 0;
        }
       
        static void CreateCart()
        {
            if (HttpContext.Current.Request.Cookies["CKCart"] == null)
            {
                HttpCookie httpCookie = new HttpCookie("CKCart");
                httpCookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }
    }
}