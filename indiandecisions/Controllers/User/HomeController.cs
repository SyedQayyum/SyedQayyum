﻿using ID.Biz.Contract;
using ID.Common;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace indiandecisions.Controllers.User
{
    public class HomeController : BaseController
    {
        protected readonly ISurveyBizManager _surveyBizManager;
        protected readonly ICategoryBizManager _categoryBizManager;
        protected readonly IEmailBizManager _emailBizManager;

        public HomeController(ISurveyBizManager surveyBizManager, ICategoryBizManager categoryBizManager, IEmailBizManager emailBizManager)
        {
            _surveyBizManager = surveyBizManager;
            _categoryBizManager = categoryBizManager;
            _emailBizManager = emailBizManager;
        }



        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.PageTitle = "Home | Indian Decision";
            List<CategoryVO> objCategoryList = new List<CategoryVO>();

            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, null, null, true);
            if (Session["Categories"] == null)
            {
                objCategoryList = _categoryBizManager.GetAllCategory(null, true);
                Session["Categories"] = objCategoryList;
            }
            else
            {
                objCategoryList = Session["Categories"] as List<CategoryVO>;
            }

            foreach (SurveyVO ObjSurvey in objSurveyList)
            {
                if (CheckCookie("Voted_" + ObjSurvey.SurveyId))
                {
                    ObjSurvey.IsVoted = true;
                    ObjSurvey.VoteValue = GetCookie("Voted_" + ObjSurvey.SurveyId);
                }
                if (CheckCookie("Rated_" + ObjSurvey.SurveyId))
                {
                    ObjSurvey.IsRated = true;
                    ObjSurvey.RateValue = GetCookie("Rated_" + ObjSurvey.SurveyId);
                }
            }

            var pager = new Pager(objSurveyList.Count(), page);

            var viewModel = new SurveyList
            {
                ListSurvey = objSurveyList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            ViewBag.Heading = "Latest Survey";
            ViewBag.Link = "home/index?page=";
            ViewBag.SurveyCategory = objCategoryList;
            return View("../user/home/index", viewModel);
        }


        [ActionName("page-not-found")]
        public ActionResult PageNotFound()
        {
            ViewBag.PageTitle = "Page Not Found | Indian Decision";
            return View("../user/home/PageNotFound");
        }

        [ActionName("internal-server-error")]
        public ActionResult InternalServerError()
        {
            ViewBag.PageTitle = "Internal Server Error | Indian Decision";
            return View("../user/home/InternalServerError");
        }

        [ActionName("contact-us")]
        public ActionResult ContactUs()
        {
            ViewBag.PageTitle = "Contact Us | Indian Decision";
            EmailVO ContactUs = new EmailVO();
            ContactUs.ContactFor = ID.Common.ContactForEnum.General;
            ViewBag.PageHeading = "Contact Us";
            return View("../user/home/ContactUs", ContactUs);
        }

        [HttpPost]
        public ActionResult ContactUs(EmailVO objEmail)
        {
            //validate captcha 
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != objEmail.Captcha)
            {
                switch (objEmail.ContactFor)
                {
                    case ContactForEnum.General:

                        ViewBag.PageTitle = "Contact Us | Indian Decision";
                        ViewBag.PageHeading = "Contact Us";
                        break;
                    case ContactForEnum.Adverstisement:
                        ViewBag.PageTitle = "Advertise With Us | Indian Decision";
                        ViewBag.PageHeading = "Advertise With Us";
                        break;
                    case ContactForEnum.SurveyRequest:
                        ViewBag.PageTitle = "Suggest Survey To Us | Indian Decision";
                        ViewBag.PageHeading = "Suggest Survey To Us";
                        break;
                }
                ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
            }
            if (!ModelState.IsValid)
            {
                return View("../user/home/ContactUs", objEmail);
            }
            _emailBizManager.SendEmailToAdmin(objEmail);
            return RedirectToAction("send-successfully", "home");
        }

        [ActionName("suggest-survey-to-us")]
        public ActionResult SuggestSurvey()
        {
            ViewBag.PageTitle = "Suggest Survey To Us | Indian Decision";
            EmailVO SurveyReq = new EmailVO();
            SurveyReq.ContactFor = ID.Common.ContactForEnum.SurveyRequest;
            ViewBag.PageHeading = "Suggest Survey To Us";
            return View("../user/home/ContactUs", SurveyReq);
        }

        [ActionName("advertise-with-us")]
        public ActionResult AdvertiseWithUs()
        {
            ViewBag.PageTitle = "Advertise With Us | Indian Decision";
            EmailVO AdvertiseReq = new EmailVO();
            AdvertiseReq.ContactFor = ID.Common.ContactForEnum.Adverstisement;
            ViewBag.PageHeading = "Advertise With Us";
            return View("../user/home/ContactUs", AdvertiseReq);
        }


        [ActionName("about-us")]
        public ActionResult AboutUs()
        {
            ViewBag.PageTitle = "About Us | Indian Decision";
            return View("../user/home/AboutUs");
        }

        [ActionName("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            ViewBag.PageTitle = "Privacy Policy | Indian Decision";
            return View("../user/home/PrivacyPolicy");
        }

        [ActionName("terms-of-use")]
        public ActionResult TermsOfUse()
        {
            ViewBag.PageTitle = "Terms Of Use | Indian Decision";
            return View("../user/home/TermsOfUse");
        }


        [ActionName("send-successfully")]
        public ActionResult SendSuccess()
        {
            ViewBag.PageTitle = "Send Successfully | Indian Decision";
            return View("../user/home/SendSuccess");
        }

        [ActionName("login")]
        public ActionResult Login()
        {
            ViewBag.PageTitle = "Login | Indian Decision";
            return View("../admin/home/Login");
        }

        [HttpPost]
        public ActionResult Login(String UserName, String Password)
        {
            if (ModelState.IsValid)
            {
                if (_surveyBizManager.IsValidUser(UserName, Password))
                {

                    FormsAuthentication.SetAuthCookie(UserName, false);
                    return RedirectToAction("index", "dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View("../admin/home/Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("../user/home/logout");
        }



        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }


    }
}