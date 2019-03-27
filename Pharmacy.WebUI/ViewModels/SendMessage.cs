using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Pharmacy.WebUI.Models
{
    public class SendMessage
    {
      public  bool MailGonder(string Name,string Email,string Subject,string Message)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("murattas721@gmail.com");
                mail.To.Add(new MailAddress("alkurtrecep@gmail.com"));
                mail.Subject = Subject;
                sb.Append("Aşağıda bilgileri bulunan bir ziyaretçinizden mesaj aldınız.<br/>");
                sb.Append("Adı: " + Name + "<br/>");
                sb.Append("Mail Adresi: " + Email + "<br/>");
                sb.Append("Mesajı: " +Message);
                mail.Body = sb.ToString();
                mail.IsBodyHtml = true;
                // SmtpClient smtpClient = new SmtpClient("mail.biltekno.com", 587);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("murattas721@gmail.com", "sifre");
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {

                return false;
              }
        }
    }
}