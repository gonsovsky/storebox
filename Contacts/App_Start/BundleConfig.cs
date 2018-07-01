using System.Web.Optimization;

namespace Contacts
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                "~/App/js/helper.js",

                "~/Scripts/Angular1.3.5/angular.js",
                "~/Scripts/Angular1.3.5/angular-route.js",
                "~/Scripts/Angular1.3.5/angular-resource.js",
                "~/Scripts/Angular1.3.5/angular-ui-bootstrap.js",

                "~/App/js/contactApp.js",
                "~/App/js/clientRoute.js",
                "~/App/js/contactDataService.js",
                "~/App/js/contactController.js",
                "~/App/js/contactControllerAdd.js",
                "~/App/js/contactControllerEdit.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
        }

    }
}
