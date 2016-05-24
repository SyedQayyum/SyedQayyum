using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace indiandecisions.Controllers
{
    public class BaseController : Controller
    {
        protected bool SetCookie(String Name, String Value, int ExpiredAfterDays)
        {
            try
            {
                HttpCookie StudentCookies = new HttpCookie(Name);
                StudentCookies.Value = Value;
                StudentCookies.Expires = DateTime.Now.AddDays(ExpiredAfterDays);
                Response.Cookies.Add(StudentCookies);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        protected String GetCookie(String Name)
        {
            return Request.Cookies[Name].Value;
        }


        protected bool CheckCookie(String Name)
        {
            if (Request.Cookies[Name] != null)
                return true;
            else
                return false;
        }
    }
}