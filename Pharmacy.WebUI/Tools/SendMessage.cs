using Eczane.WebUI.Tools;
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
    public class SendMessage:IMessage
    {
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        public  bool sendEmail(string Name,string Email,string Subject,string Message)
        {
            this.name = Name;
            this.email = Email;
            this.subject = Subject;
            this.message = Message;
            try
            {

                StringBuilder sb = new StringBuilder();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("murattas721@gmail.com");
                mail.To.Add(new MailAddress("alkurtrecep@gmail.com"));
                mail.Subject = subject;
                sb.Append("Aşağıda bilgileri bulunan bir ziyaretçinizden mesaj aldınız.<br/>");
                sb.Append("Adı: " + name + "<br/>");
                sb.Append("Mail Adresi: " + email + "<br/>");
                sb.Append("Mesajı: " +message);
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