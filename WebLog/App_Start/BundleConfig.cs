﻿using System.Web;
using System.Web.Optimization;

namespace WebLog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/countdown").Include(
                      "~/Scripts/jquery.countdown*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/MyAngular/mainModule.js*",
                      "~/Scripts/MyAngular/Services/WebLog.js*",
                      "~/Scripts/MyAngular/Controllers/MessageController*",
                      "~/Scripts/MyAngular/Controllers/ManageController*",
                      "~/Scripts/MyAngular/Controllers/TeacherController*"
                       ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-modal").Include(
                "~/Scripts/bootstrap-only-modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/Content/jquery-datatables").Include(
                "~/Content/DataTables/css/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-form").Include(
              "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/ownScript").Include(
                            "~/Scripts/Site.js"));


            bundles.Add(new StyleBundle("~/Content/bootstrap-modal").Include(
                  "~/Content/bootstrap-only-modal.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap-datepicker.css",
                      "~/Content/site.css",
                      "~/Content/site2.css"));

            bundles.Add(new StyleBundle("~/Content/bootstap-adds").Include(
                     "~/Content/bootstrap.css"
       ));

            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
            "~/Scripts/metro.js"));

            bundles.Add(new StyleBundle("~/Content/metro").Include(
                   "~/Content/metro*"));


        }
    }
}
