using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using ID.Biz.Contract;
using ID.Biz.Implementation;
using ID.DAL.Contract;
using ID.DAL.Implementation;

namespace indiandecisions
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers


            #region Biz Layer

            container.RegisterType<ICategoryBizManager, CategoryBizManager>();
            container.RegisterType<ISurveyBizManager, SurveyBizManager>();
            container.RegisterType<IUserBizManager, UserBizManager>();

            #endregion

            #region Data Layer

            container.RegisterType<ICategoryDataManager, CategoryDataManager>();
            container.RegisterType<ISurveyDataManager, SurveyDataManager>();
            container.RegisterType<IUserDataManager, UserDataManager>();

            #endregion

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}