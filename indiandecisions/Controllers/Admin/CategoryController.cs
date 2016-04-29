using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ID.Biz.Contract;
using ID.ValueObjects;
using Core.Common;

namespace indiandecisions.Controllers
{
    public class CategoryController : Controller
    {


        protected readonly ICategoryBizManager _CategoryBizManager;

        public CategoryController(ISurveyBizManager surveyBizManager, ICategoryBizManager CategoryBizManager)
        {
            _CategoryBizManager = CategoryBizManager;
        }

        // GET: Category
        public ActionResult Index()
        {
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return View("../Admin/Category/Index", objCategoryList);
        }

        // GET: Category
        public ActionResult create(CategoryVO Category)
        {
            Boolean IsSaved = _CategoryBizManager.CreateUpdateCategory(Category);
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return RedirectToAction("Index", "Category", objCategoryList);
        }

        public JsonResult GetCategory(int? CategoryId)
        {
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(CategoryId);
            return Json(objCategoryList.FirstOrDefault(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetAllCategories()
        {
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null);
            return Json(objCategoryList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteCategory(int CategoryId, bool? IsSoftDelete)
        {
            bool IsDeleted = _CategoryBizManager.DeleteCategory(CategoryId, false);
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return RedirectToAction("Index", "Category", objCategoryList);

        }

    }
}