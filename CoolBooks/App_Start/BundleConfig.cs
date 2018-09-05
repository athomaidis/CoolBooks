using System.Web;
using System.Web.Optimization;

namespace CoolBooks
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-3.3.1.slim.min.js",
                        "~/Scripts/jquery-3.3.1.min.js",
                        "~/Scripts/bootstrap.js",
                        "~/scripts/bootbox.js",
                        "~/scripts/boostrap.min.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/dataTables/jquery.dataTables.js",
                        "~/Scripts/dataTables/dataTables.bootstrap4.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/Flasmessage.js",
                          "~/Scripts/jquery-ui-*",
                          "~/Scripts/holder.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                    "~/Scripts/jquery.validate*",
                                    "~/Scripts/jquery.validate.unobtrusive.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/popper.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/bootstrap.min.css",
                         "~/content/home.css",
                         "~/Content/bootstrap.css",
                         "~/content/dataTables/css/dataTables.bootstrap4.css",
                         "~/Content/site.css",
                         "~/content/themes/base/jquery-ui.css",
                         "~/content/flash-messages.css"
                         ));

        }
    }
}
