using ID.Biz.Contract;
using ID.Common;
using ID.ValueObjects;
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

        protected readonly IEmailBizManager _emailBizManager;

        public EmailController(IEmailBizManager emailBizManager)
        {
            _emailBizManager = emailBizManager;
        }


        public ActionResult SendEmail(EmailVO objEmail)
        {
            _emailBizManager.SendEmailToAdmin(objEmail);
            return RedirectToAction("send-successfully", "home");
        }
    }
}