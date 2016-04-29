using ID.Biz.Contract;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}