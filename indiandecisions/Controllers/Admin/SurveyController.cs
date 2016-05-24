using ID.Biz.Contract;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace indiandecisions.Controllers
{
    public class SurveyController : BaseController
    {
        protected readonly ICategoryBizManager _categoryBizManager;
        protected readonly ISurveyBizManager _surveyBizManager;
        protected readonly IOptionBizManager _optionBizManager;

        public SurveyController(ISurveyBizManager surveyBizManager, ICategoryBizManager categoryBizManager, IOptionBizManager optionBizManager)
        {
            _surveyBizManager = surveyBizManager;
            _categoryBizManager = categoryBizManager;
            _optionBizManager = optionBizManager;
        }


        [Authorize]
        public ActionResult index()
        {
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, null, null, null);
            return View("../Admin/survey/index", objSurveyList);
        }

        [Route("survey/{CategoryId}/get-survey/{categoryName}")]
        public ActionResult SurveyByCategory(int? page, int CategoryId, String CategoryName)
        {
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(null, CategoryId, null, true);

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
            ViewBag.Heading = CategoryName;
            ViewBag.PageTitle = CategoryName;
            ViewBag.Link = "survey/" + CategoryId + "/get-survey/" + CategoryName + "?page=";
            return View("../User/home/index", viewModel);
        }


        [Authorize]
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
            {
                objSurvey = new SurveyVO();
                objSurvey.SurveyOptions = new List<SurveyOptionVO>();
                AddOptionToSurveyList(objSurvey);

            }

            return View("../Admin/survey/create", objSurvey);
        }

        private void AddOptionToSurveyList(SurveyVO Survey)
        {
            Survey.SurveyOptions.Add(new SurveyOptionVO
            {
                Id = 0,
                OptionId = 0
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult create(SurveyVO Survey, String AddOption)
        {

            SetCategoryDropDown();
            if (string.IsNullOrEmpty(AddOption))
            {

                Int64 SurveyId = Survey.SurveyId != 0 ? Survey.SurveyId - 1 : _surveyBizManager.GetLastGeneratedSurveyId();
                HttpPostedFileBase file = Request.Files["SurveyImage"];
                String path = string.Empty;
                string extension = "";
                if (file != null && file.ContentLength > 0)
                {
                    extension = Path.GetExtension(file.FileName);
                    path = Path.Combine(Server.MapPath("~/SurveyImages"), "Survey_" + (SurveyId + 1) + extension);
                    file.SaveAs(path);

                    Image imgOriginal = Image.FromFile(path);

                    //pass in whatever value you want
                    Image imgActual = Scale(imgOriginal);
                    imgOriginal.Dispose();
                    imgActual.Save(path);
                    imgActual.Dispose();

                    Survey.PicturePath = "../SurveyImages/Survey_" + (SurveyId + 1) + extension;
                }

                Survey.CreatedBy = HttpContext.User.Identity.Name;
                bool IsCreated = _surveyBizManager.CreateUpdateSurvey(Survey);
                if (IsCreated)
                {
                    ViewBag.Message = "Data Save Successfully.";
                    SurveyVO objSurvey = new SurveyVO();
                    objSurvey.SurveyOptions = new List<SurveyOptionVO>();
                    AddOptionToSurveyList(objSurvey);
                    return View("../Admin/survey/create", objSurvey);
                }
                return View("../Admin/survey/create", Survey);
            }
            else
            {
                AddOptionToSurveyList(Survey);
                return View("../Admin/survey/create", Survey);
            }
        }


        private void SetCategoryDropDown()
        {
            List<CategoryVO> objCategoryList = _categoryBizManager.GetAllCategory(null, null).OrderBy(x => x.CategoryOrder).ToList();
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();

            List<OptionVO> objOptionList = _optionBizManager.GetAllOption(null, null);
            ViewBag.OptionDll = objOptionList.Select(x => new SelectListItem { Text = x.OptionName, Value = x.OptionId.ToString() }).ToList();
        }

        [Authorize]
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
        public ActionResult SurveyDetails(long surveyId, string suveryQs)
        {
            ViewBag.PageTitle = suveryQs.Replace('-', ' ');
            List<SurveyVO> objSurveyList = _surveyBizManager.GetAllSurvey(surveyId, null, null, null);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ViewBag.JsonResult = jss.Serialize(GetResultJson(objSurveyList.SingleOrDefault()));
            SurveyVO ObjSurvey = objSurveyList.SingleOrDefault();
            ObjSurvey.IsVoted = CheckCookie("Voted_" + surveyId);
            return View("../user/survey/Surveydetails", ObjSurvey);
        }


        public ActionResult VoteOnSurvey(long SurveyId, long OptionId, String OptionValue)
        {
            Boolean IsVoted = _surveyBizManager.VoteOnSurvey(SurveyId, OptionId);
            if (IsVoted)
            {
                SetCookie("Voted_" + SurveyId, OptionValue, 8);
            }

            return Json(IsVoted, JsonRequestBehavior.AllowGet);
        }

        private List<SurveyResult> GetResultJson(SurveyVO Survey)
        {
            long Sum = 0;
            List<SurveyResult> objSurveyResult = new List<SurveyResult>();
            foreach (SurveyOptionVO SO in Survey.SurveyOptions)
            {
                Sum += SO.SurveyOptionCount;
            }
            Sum = Sum != 0 ? Sum : 1;

            foreach (SurveyOptionVO SO in Survey.SurveyOptions)
            {
                objSurveyResult.Add(new SurveyResult()
                {

                    name = SO.OptionName,
                    y = ((double)SO.SurveyOptionCount / Sum) * 100

                });
            }

            return objSurveyResult;
        }

        public ActionResult RatingOnSurvey(long SurveyId, Int16 Rating)
        {
            Boolean IsRated = _surveyBizManager.RatingOnSurvey(SurveyId, Rating);
            if (IsRated)
            {
                SetCookie("Rated_" + SurveyId, Rating.ToString(), 8);
            }
            return Json(IsRated, JsonRequestBehavior.AllowGet);
        }

        private Image Scale(Image imgPhoto)
        {
            float Width = 325;
            float Height = 240;
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }


        public ActionResult SetSurveyActiveStatus(long SurveyId, bool ActiveStatus)
        {
            bool IsDone = _surveyBizManager.SetSurveyActiveStatus(SurveyId, ActiveStatus);
            return Json(IsDone, JsonRequestBehavior.AllowGet);
        }



    }
}