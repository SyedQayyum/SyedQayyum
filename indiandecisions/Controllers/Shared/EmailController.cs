using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace indiandecisions.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult SendEmail()
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("indiandecisions@gmail.com"));  // replace with valid value 
            message.From = new MailAddress("indiandecisions@gmail.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Syed Qayyum", "sqm.syed@gmail.com", "Testing");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "indiandecisions@gmail.com",  // replace with valid value
                    Password = "SQMrnq_1432"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
               smtp.Send(message);
                return RedirectToAction("index","home");
            }
        }
    }
}