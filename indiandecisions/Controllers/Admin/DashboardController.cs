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
    public class DashboardController : Controller
    {
        protected readonly IManagementBizManager _ManagementBizManager;

        public DashboardController(IManagementBizManager ManagementBizManager)
        {
            _ManagementBizManager = ManagementBizManager;
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            List<ManagementVO> ManagementList = _ManagementBizManager.GetAllManagement(null);
            ViewBag.Investment = ManagementList.Where(x => x.ManagementCategoryId == (int)ManagementCategoryEnum.Investment).Sum(x => x.RupeeOrMinute);
            ViewBag.Income = ManagementList.Where(x => x.ManagementCategoryId == (int)ManagementCategoryEnum.Income).Sum(x => x.RupeeOrMinute);
            ViewBag.TimeSpent = (ManagementList.Where(x => x.ManagementCategoryId == (int)ManagementCategoryEnum.TimeSpent).Sum(x => x.RupeeOrMinute)/(60*1.0)).ToString("#.##"); ;

            return View("../admin/dashboard/index");

        }
    }
}