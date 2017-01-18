using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebLog.Core.Common;

namespace WebLog.Resources
{

    public class LocalizedControllerActivator : IControllerActivator
    {
        private string _DefaultLanguage = "pl";

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            //Get the {language} parameter in the RouteData
           // string lang = requestContext.RouteData.Values["lang"]?.ToString() ?? _DefaultLanguage;
            //var lang = HttpContext.Current.Session["lang"].ToString();
            var lang = "pl";
            
            //if (lang != _DefaultLanguage)
            //{
            //    try
            //    {
                    Thread.CurrentThread.CurrentCulture =
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                //}
                //catch (Exception e)
                //{
                //    throw new NotSupportedException($"ERROR: Invalid language code '{lang}'.");
                //}
            //}

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}