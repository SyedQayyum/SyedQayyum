using ID.Biz.Contract;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace indiandecisions.Controllers.Admin
{
    [Authorize]
    public class PreviewController : Controller
    {
        protected readonly ISurveyBizManager _surveyBizManager;
        protected readonly IEmailBizManager _emailBizManager;

        public PreviewController(ISurveyBizManager surveyBizManager)
        {
            _surveyBizManager = surveyBizManager;
        }

        // GET: Preview
        public ActionResult Index()
        {

            ViewBag.PageTitle = "Indian Decision | Preview";

            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, null, null, false);

            var viewModel = new SurveyList
            {
                ListSurvey = objSurveyList,
            };

            ViewBag.Heading = "Survey Preview";
            return View("../admin/preview/index", viewModel);
        }
    }
}