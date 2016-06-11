using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace indiandecisions.Controllers
{
    public class BaseController : Controller
    {
       


        protected bool SetCookie(String Name, String Value, int ExpiredAfterDays)
        {
            try
            {
                HttpCookie StudentCookies = new HttpCookie(Name);
                StudentCookies.Value = StringCipher.Encrypt(Value, "SQM");
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
            return StringCipher.Decrypt(Request.Cookies[Name].Value, "SQM");
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