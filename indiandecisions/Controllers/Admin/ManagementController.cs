using ID.Biz.Contract;
using ID.Common;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace indiandecisions.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        protected readonly IManagementBizManager _ManagementBizManager;

        public ManagementController(IManagementBizManager ManagementBizManager)
        {
            _ManagementBizManager = ManagementBizManager;
        }

        // GET: Management
        public ActionResult index(int? page)
        {
            List<ManagementVO> objManagementList = _ManagementBizManager.GetAllManagement(null);

            var pager = new Pager(objManagementList.Count(), page);

            var viewModel = new ManagementList
            {
                Management = objManagementList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            ViewBag.ManagementDll = new ManagementVO().ManagementCategory.ToSelectList<ManagementCategoryEnum>();
            return View("../Admin/Management/Index", viewModel);
        }

        [HttpPost]
        public ActionResult create(ManagementVO Management)
        {
            Boolean IsSaved = _ManagementBizManager.CreateUpdateManagement(Management);
            List<ManagementVO> objManagementList = _ManagementBizManager.GetAllManagement(null);
            return RedirectToAction("index", "management", objManagementList);
        }

        public JsonResult GetManagement(int? ManagementId)
        {
            List<ManagementVO> objManagementList = _ManagementBizManager.GetAllManagement(ManagementId);
            return Json(objManagementList.FirstOrDefault(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteManagement(Int64 ManagementId)
        {
            bool IsDeleted = _ManagementBizManager.DeleteManagement(ManagementId);
            List<ManagementVO> objManagementList = _ManagementBizManager.GetAllManagement(null);
            return RedirectToAction("Index", "management", objManagementList);

        }
    }
}