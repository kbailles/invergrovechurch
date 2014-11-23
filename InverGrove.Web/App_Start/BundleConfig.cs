using System.Web.Optimization;

namespace InverGrove.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Core bundles
            bundles.Add(new StyleBundle("~/bundles/css/core").Include(
                "~/Components/html5-boilerplate/css/normalize.css",
                "~/Components/html5-boilerplate/css/main.css",
                "~/Components/bootstrap/css/bootstrap.min.css",
                "~/Components/font-awesome/css/font-awesome.min.css",
                "~/Content/less/app.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/core").Include(
                "~/Scripts/jquery/jquery-2.1.1.min.js",
                "~/Components/lodash/lodash.min.js",
                "~/Scripts/common/namespace.js",
                "~/Scripts/bootstrap/modernizr-2.6.2.min.js",
                "~/Components/angular/angular.min.js",
                "~/Components/angular-route/angular-route.min.js",
                "~/Components/bootstrap-ui/ui-bootstrap-tpls-0.12.0.min.js"));

            //Revolution Slider bundles
            bundles.Add(new StyleBundle("~/bundles/css/revoslider").Include(
                "~/Content/css/rs-plugin/style.css",
                "~/Components/rs-plugin/css/settings.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/revoslider").Include(
                "~/Components/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/Components/rs-plugin/js/jquery.themepunch.revolution.min.js"));

            //Plugins bundles
            bundles.Add(new ScriptBundle("~/bundles/scripts/plugins").Include(
                "~/Components/gmaps/jquery.gmap.min.js",
                "~/Components/sticky/jquery.sticky.js"));

            //Public area bundles
            bundles.Add(new ScriptBundle("~/bundles/scripts/area/public").Include(
                "~/Scripts/app.module.js",
                "~/Scripts/app.routes.js",
                //Factories
                "~/Scripts/factories/factories.module.js",
                "~/Scripts/factories/googleMapChurchLocationFactory.js",
                "~/Scripts/factories/homePageRevoSliderOptionsFactory.js",
                //Filters
                "~/Scripts/filters/filters.module.js",
                "~/Scripts/filters/selectedTagsFilter.js",
                //Services
                "~/Scripts/services/services.module.js",
                "~/Scripts/Services/SermonService.js",
                "~/Scripts/services/MessageService.js",
                //Controllers
                "~/Scripts/controllers/controllers.module.js",
                "~/Scripts/controllers/BaseController.js",
                "~/Scripts/controllers/HomeController.js",
                "~/Scripts/controllers/ContactController.js",
                "~/Scripts/controllers/SermonsController.js",
                "~/Scripts/controllers/SermonDetailController.js",
                //Directives
                "~/Scripts/directives/directives.module.js",
                "~/Scripts/directives/googleMapDirective.js",
                "~/Scripts/directives/revosliderDirective.js",
                "~/Scripts/directives/scrollUpDirective.js",
                "~/Scripts/directives/stickyElementDirective.js",
                "~/Scripts/directives/loadingOverlayDirective.js"));

            //Member area bundles
        }
    }
}