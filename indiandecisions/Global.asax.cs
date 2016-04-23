using indiandecisions;
using System.Web.Mvc;
using System.Web.Routing;

namespace IndianDecisions
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
                   
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
