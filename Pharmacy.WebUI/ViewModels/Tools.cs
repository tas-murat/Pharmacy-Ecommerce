using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Models
{
    public class Tools
    {
        public static string DosyaYoluOlustur(string DosyaAdi, string ID)
        {
            string geri = DosyaAdi.ToLower().Replace(" ", "").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("ı", "i").Replace("ö", "o").Replace("ü", "u");
            geri=Path.GetFileNameWithoutExtension(geri) + ID + Path.GetExtension(DosyaAdi);
            return geri;
        }
    }
}