using ID.Biz.Contract;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace indiandecisions.Controllers
{
    public class SurveyController : Controller
    {
        protected readonly ICategoryBizManager _categoryBizManager;
        protected readonly ISurveyBizManager _surveyBizManager;

        public SurveyController(ISurveyBizManager surveyBizManager, ICategoryBizManager categoryBizManager)
        {
            _surveyBizManager = surveyBizManager;
            _categoryBizManager = categoryBizManager;
        }


        // GET: Survey
        public ActionResult index()
        {
            List<SurveyVO> objSurveyList = new List<SurveyVO>();//_surveyBizManager.GetAllSurvey(null);
            return View("../Admin/survey/index", objSurveyList);
        }


        // GET: Survey
        public ActionResult create()
        {
            SetCategoryDropDown();
            return View("../Admin/survey/create", new SurveyVO());
        }

        [HttpPost]
        public ActionResult create(SurveyVO Survey)
        {
            SetCategoryDropDown();

            Int64 SurveyId = _surveyBizManager.GetLastGeneratedSurveyId();

            HttpPostedFileBase file = Request.Files["SurveyImage"];
            String path = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                path = Path.Combine(Server.MapPath("~/SurveyImages"), "Survey_" + (SurveyId+1));
                file.SaveAs(path);
            }

            Survey.PicturePath = path;
            _surveyBizManager.CreateUpdateSurvey(Survey);
            ViewBag.Message = "Data Save Successfully.";
            return View("../Admin/survey/create", Survey);
        }


        private void SetCategoryDropDown()
        {
            List<CategoryVO> objCategoryList = _categoryBizManager.GetAllCategory(null);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
        }

    }
}