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

        [Authorize]
        public ActionResult Index(int? page)
        {
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null,null).OrderBy(x=>x.CategoryOrder).ToList();

            var pager = new Pager(objCategoryList.Count(), page);

            var viewModel = new CategoryList
            {
                Categories = objCategoryList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return View("../Admin/Category/Index", viewModel);
        }

        [Authorize]
        public ActionResult create(CategoryVO Category)
        {
            Category.CreatedBy = HttpContext.User.Identity.Name;
            Boolean IsSaved = _CategoryBizManager.CreateUpdateCategory(Category);
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null,true);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return RedirectToAction("Index", "Category", objCategoryList);
        }

        public JsonResult GetCategory(int? CategoryId)
        {
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(CategoryId,null);
            return Json(objCategoryList.FirstOrDefault(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetAllCategories()
        {
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null,true).OrderBy(x => x.CategoryOrder).ToList();
            return Json(objCategoryList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SetCategoryActiveStatus(long CategoryId, bool ActiveStatus)
        {
            bool IsDone = _CategoryBizManager.SetCategoryActiveStatus(CategoryId, ActiveStatus);
            return Json(IsDone, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        [HttpPost]
        public ActionResult DeleteCategory(int CategoryId, bool? IsSoftDelete)
        {
            bool IsDeleted = _CategoryBizManager.DeleteCategory(CategoryId, false);
            List<CategoryVO> objCategoryList = _CategoryBizManager.GetAllCategory(null,true);
            ViewBag.CategoryDll = objCategoryList.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            return RedirectToAction("Index", "Category", objCategoryList);

        }

    }
}