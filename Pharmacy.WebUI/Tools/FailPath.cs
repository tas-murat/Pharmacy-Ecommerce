using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Pharmacy.WebUI.Models
{
    public class FilePath
    {
        public static string getfilePath(string fileName, string ID)
        {
            string back = fileName.ToLower().Replace(" ", "").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("ı", "i").Replace("ö", "o").Replace("ü", "u");
            back = Path.GetFileNameWithoutExtension(back) + ID + Path.GetExtension(fileName);
            return back;
        }
    }
}