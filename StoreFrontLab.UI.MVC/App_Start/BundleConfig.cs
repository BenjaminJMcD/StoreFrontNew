using System.Web.Optimization;

namespace StoreFrontLab.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                    "~/Scripts/js/vendor/jquery-1.12.4.min.js",
                    "~/Scripts/js/vendor/modernizr-3.5.0.min.js",
                    "~/Scripts/js/animated.headline.js",
                    "~/Scripts/js/bootstrap.min.js",
                    "~/Scripts/js/contact.js",
                    "~/Scripts/js/gijgo.min.js",
                    "~/Scripts/js/hover-direction-snake.min.js",
                    "~/Scripts/js/jquery.ajaxchimp.min.js",
                    "~/Scripts/js/jquery.barfiller.js",
                    "~/Scripts/js/jquery.countdown.min.js",
                    "~/Scripts/js/jquery.counterup.min.js",
                    "~/Scripts/js/jquery.form.js",
                    "~/Scripts/js/jquery.magnific-popup.js",
                    "~/Scripts/js/jquery.nice-select.min.js",
                    "~/Scripts/js/jquery.paroller.min.js",
                    "~/Scripts/js/jquery.slicknav.min.js",
                    "~/Scripts/js/jquery.sticky.js",
                    "~/Scripts/js/jquery.validate.min.js",
                    "~/Scripts/js/mail-script.js",
                    "~/Scripts/js/main.js",
                    "~/Scripts/js/one-page-nav-min.js",
                    "~/Scripts/js/owl.carousel.min.js",
                    "~/Scripts/js/plugins.js",
                    "~/Scripts/js/popper.min.js",
                    "~/Scripts/js/price-range.js",
                    "~/Scripts/js/slick.min.js",
                    "~/Scripts/js/waypoints.min.js",
                    "~/Scripts/js/wow.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/css/animate.min.css",
                      "~/Content/assets/css/animated-headline.css",
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/flaticon.css",
                      "~/Content/assets/css/fontawesome-all.min.css",
                      "~/Content/assets/css/gijgo.css",
                      "~/Content/assets/css/hamburgers.min.css",
                      "~/Content/assets/css/magnific-popup.css",
                      "~/Content/assets/css/main.css",
                      "~/Content/assets/css/nice-select.css",
                      "~/Content/assets/css/owl.carousel.min.css",
                      "~/Content/assets/css/price_rangs.css",
                      "~/Content/assets/css/progressbar_barfiller.css",
                      "~/Content/assets/css/responsive.css",
                      "~/Content/assets/css/slick.css",
                      "~/Content/assets/css/slicknav.css",
                      "~/Content/assets/css/style.css",
                      "~/Content/assets/css/style.map",
                      "~/Content/assets/css/themify-icons.css",
                      "~/Content/Doc/css/font-awesome.min.css",
                      "~/Content/Doc/css/main.css",
                      "~/Content/Doc/css/normalize.min.css"
                        ));
        }
    }
}
