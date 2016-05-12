using ID.Biz.Contract;
using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace indiandecisions.Controllers.Admin
{
    public class OptionController : Controller
    {
        protected readonly IOptionBizManager _OptionBizManager;

        public OptionController(IOptionBizManager OptionBizManager)
        {
            _OptionBizManager = OptionBizManager;
        }

        [Authorize]
        public ActionResult Index(int? page)
        {
            List<OptionVO> objOptionList = _OptionBizManager.GetAllOption(null, null);

            var pager = new Pager(objOptionList.Count(), page);

            var viewModel = new OptionList
            {
                Options = objOptionList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            ViewBag.OptionDll = objOptionList.Select(x => new SelectListItem { Text = x.OptionName, Value = x.OptionId.ToString() }).ToList();
            return View("../Admin/Option/Index", viewModel);
        }

        [Authorize]
        public ActionResult create(OptionVO Option)
        {
            Option.CreatedBy = HttpContext.User.Identity.Name;
            Boolean IsSaved = _OptionBizManager.CreateUpdateOption(Option);
            List<OptionVO> objOptionList = _OptionBizManager.GetAllOption(null, null);
            ViewBag.OptionDll = objOptionList.Select(x => new SelectListItem { Text = x.OptionName, Value = x.OptionId.ToString() }).ToList();
            return RedirectToAction("Index", "Option", objOptionList);
        }

        public JsonResult GetOption(int? OptionId)
        {
            List<OptionVO> objOptionList = _OptionBizManager.GetAllOption(OptionId,null);
            return Json(objOptionList.FirstOrDefault(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetAllOptions()
        {
            List<OptionVO> objOptionList = _OptionBizManager.GetAllOption(null, true);
            return Json(objOptionList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SetOptionActiveStatus(long OptionId, bool ActiveStatus)
        {
            bool IsDone = _OptionBizManager.SetOptionActiveStatus(OptionId, ActiveStatus);
            return Json(IsDone, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        [HttpPost]
        public ActionResult DeleteOption(int OptionId)
        {
            bool IsDeleted = _OptionBizManager.DeleteOption(OptionId);
            List<OptionVO> objOptionList = _OptionBizManager.GetAllOption(null, true);
            ViewBag.OptionDll = objOptionList.Select(x => new SelectListItem { Text = x.OptionName, Value = x.OptionId.ToString() }).ToList();
            return RedirectToAction("Index", "Option", objOptionList);

        }

    }
}