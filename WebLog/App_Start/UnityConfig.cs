using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebLog.Core;
using WebLog.Core.Factory;
using WebLog.Core.Models;
using WebLog.Core.Services;
using WebLog.Persistance;
using WebLog.Persistance.Factory;
using WebLog.Persistance.Services;

namespace WebLog
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            //Include this line for Unity to resolve WebApi Controllers.
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            container.RegisterType<DbContext, LogDbContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IMailService, MailService>();
            container.RegisterType<IUserFactory, UserFactory>();
            container.RegisterType<IDataConversionService, DataConversionService>();
        }
    }
}