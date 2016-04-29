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
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, null, null, null);
            return View("../Admin/survey/index", objSurveyList);
        }

        [Route("survey/{CategoryId}/get-survey/{categoryName}")]
        public ActionResult SurveyByCategory(int CategoryId,String CategoryName)
        {
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, CategoryId, null, null);
            ViewBag.Heading = CategoryName;
            return View("../User/home/index", objSurveyList);
        }


        // GET: Survey
        public ActionResult create(long? id)
        {
            SetCategoryDropDown();
            SurveyVO objSurvey = null;
            if (id != null)
            {
                SurveyVO objTempSurvey = _surveyBizManager.GetAllSurvey(id).FirstOrDefault();
                objSurvey = objTempSurvey != null ? objTempSurvey : new SurveyVO();
            }
            else
                objSurvey = new SurveyVO();

            return View("../Admin/survey/create", objSurvey);
        }


        [HttpPost]
        public ActionResult create(SurveyVO Survey)
        {
            SetCategoryDropDown();
            Int64 SurveyId = Survey.SurveyId != 0 ? Survey.SurveyId - 1 : _surveyBizManager.GetLastGeneratedSurveyId();
            HttpPostedFileBase file = Request.Files["SurveyImage"];
            String path = string.Empty;
            string extension = "";
            if (file != null && file.ContentLength > 0)
            {
                extension = Path.GetExtension(file.FileName);
                path = Path.Combine(Server.MapPath("~/SurveyImages"), "Survey_" + (SurveyId + 1) + extension);
                file.SaveAs(path);
                Survey.PicturePath = "../SurveyImages/Survey_" + (SurveyId + 1) + extension;
            }

            bool IsCreated = _surveyBizManager.CreateUpdateSurvey(Survey);
            if (IsCreated)
            {
                ViewBag.Message = "Data Save Successfully.";
                return View("../Admin/survey/create", new SurveyVO());
            }
            return View("../Admin/survey/create", Survey);
        }


        private void SetCategoryDropDown()
        {
            List<CategoryVO> objCategoryList = _categoryBizManager.GetAllCategory(null);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
        }

        [HttpPost]
        public ActionResult DeleteSurvey(int SurveyId, bool? IsSoftDelete)
        {
            SurveyVO objSurvey = _surveyBizManager.GetAllSurvey(SurveyId).FirstOrDefault();
            DeleteImage(objSurvey.PicturePath);
            bool IsDeleted = _surveyBizManager.DeleteSurvey(SurveyId, false);
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null);
            // ViewBag.CategoryDll = objSurveyList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return RedirectToAction("Index", "Survey", objSurveyList);
        }

        private void DeleteImage(string RelativePath)
        {
            string completFileName = Server.MapPath(RelativePath);
            if (System.IO.File.Exists(completFileName))
            {
                System.IO.File.Delete(completFileName);
            }
        }


        [Route("survey/{surveyId}/survey-details/{suveryQs}")]
        public ActionResult SurveyDetails(long surveyId)
        {
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(surveyId, null, null, null);
            return View("../user/survey/Surveydetails", objSurveyList.SingleOrDefault());
        }


        public ActionResult VoteOnSurvey(long SurveyId, String UserVote)
        {
            Boolean IsVoted = _surveyBizManager.VoteOnSurvey(SurveyId, UserVote.ToLower());
            return Json(IsVoted, JsonRequestBehavior.AllowGet);
        }

    }
}