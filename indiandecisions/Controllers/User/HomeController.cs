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
        public ActionResult Index()
        {
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, null, null, null);
            ViewBag.Heading = "Latest Survey";
            return View("../user/home/index", objSurveyList);
        }


        [ActionName("page-not-found")]
        public ActionResult PageNotFound()
        {
            return View("../user/home/PageNotFound");
        }

        [ActionName("contact-us")]
        public ActionResult ContactUs()
        {
            return View("../user/home/ContactUs");
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