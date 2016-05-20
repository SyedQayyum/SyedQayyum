using ID.Biz.Contract;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace indiandecisions.Controllers.User
{
    public class HomeController : Controller
    {
        protected readonly ISurveyBizManager _surveyBizManager;

        public HomeController(ISurveyBizManager surveyBizManager)
        {
            _surveyBizManager = surveyBizManager;
        }



        // GET: Home
        public ActionResult Index(int? page)
        {
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, null, null, null);

            var pager = new Pager(objSurveyList.Count(), page);

            var viewModel = new SurveyList
            {
                ListSurvey = objSurveyList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            ViewBag.Heading = "Latest Survey";
            return View("../user/home/index", viewModel);
        }


        [ActionName("page-not-found")]
        public ActionResult PageNotFound()
        {
            return View("../user/home/PageNotFound");
        }

        [ActionName("contact-us")]
        public ActionResult ContactUs()
        {
            EmailVO ContactUs = new EmailVO();
            ContactUs.ContactFor = ID.Common.ContactForEnum.General;
            ViewBag.PageHeading = "Contact Us";
            return View("../user/home/ContactUs", ContactUs);
        }

        [ActionName("suggest-survey-to-us")]
        public ActionResult SuggestSurvey()
        {
            EmailVO SurveyReq = new EmailVO();
            SurveyReq.ContactFor = ID.Common.ContactForEnum.SurveyRequest;
            ViewBag.PageHeading = "Suggest Survey To Us";
            return View("../user/home/ContactUs", SurveyReq);
        }

        [ActionName("advertise-with-us")]
        public ActionResult AdvertiseWithUs()
        {

            EmailVO AdvertiseReq = new EmailVO();
            AdvertiseReq.ContactFor = ID.Common.ContactForEnum.Adverstisement;
            ViewBag.PageHeading = "Advertise With Us";
            return View("../user/home/ContactUs", AdvertiseReq);
        }


        [ActionName("about-us")]
        public ActionResult AboutUs()
        {
            return View("../user/home/AboutUs");
        }

        [ActionName("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View("../user/home/PrivacyPolicy");
        }

        [ActionName("terms-of-use")]
        public ActionResult TermsOfUse()
        {
            return View("../user/home/TermsOfUse");
        }


        [ActionName("send-successfully")]
        public ActionResult SendSuccess()
        {
            return View("../user/home/SendSuccess");
        }

        [ActionName("login")]
        public ActionResult Login()
        {
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
            return RedirectToAction("index", "home");
        }
    }
}