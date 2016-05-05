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
    public class UserController : Controller
    {

        protected readonly IUserBizManager _userBizManager;

        public UserController(IUserBizManager userBizManager)
        {
            _userBizManager = userBizManager;
        }


        public ActionResult Index()
        {
            List<UserVO> objUserList = _userBizManager.GetAllUser(null);
            return View("../Admin/User/Index", objUserList);
        }

        public JsonResult GetUser(int? UserId)
        {
            List<UserVO> objUserList = _userBizManager.GetAllUser(UserId);
            return Json(objUserList.FirstOrDefault(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult create(UserVO User)
        {
            Boolean IsSaved = _userBizManager.CreateUpdateUser(User);
            List<UserVO> objUserList = _userBizManager.GetAllUser(null);
            return RedirectToAction("Index", "User", objUserList);
        }

        [HttpPost]
        public ActionResult DeleteUser(int UserId, bool? IsSoftDelete)
        {
            bool IsDeleted = _userBizManager.DeleteUser(UserId, false);
            List<UserVO> objUserList = _userBizManager.GetAllUser(null);
            return RedirectToAction("Index", "User", objUserList);

        }
    }
}