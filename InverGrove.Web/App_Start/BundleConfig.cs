using System.Web.Optimization;

namespace InverGrove.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Route bundles
            bundles.Add(new ScriptBundle("~/Scripts/scripts/route/public").Include(
                "~/Scripts/app.routes.js"));
            bundles.Add(new ScriptBundle("~/Scripts/scripts/route/sermon").Include(
                "~/Scripts/app.sermon.routes.js"));
            bundles.Add(new ScriptBundle("~/Scripts/scripts/route/contactus").Include(
                "~/Scripts/app.contactus.routes.js"));

            //Core bundles
            bundles.Add(new StyleBundle("~/Components/css/core").Include(
                "~/Components/html5-boilerplate/css/normalize.css",
                "~/Components/html5-boilerplate/css/main.css")
                .Include("~/Components/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Components/pickadate/v2-(deprecated)/themes/pickadate.04.inline-fixed.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/css/core").Include(
                "~/Content/less/app.min.css"));

            bundles.Add(new ScriptBundle("~/Components/scripts/core").Include(
                "~/Components/lodash/lodash.min.js",
                "~/Components/angular/angular.min.js",
                "~/Components/angular-route/angular-route.min.js",
                "~/Components/bootstrap-ui/ui-bootstrap-tpls-0.12.0.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/scripts/core").Include(
                "~/Scripts/jquery/jquery-2.1.1.min.js",
                "~/Scripts/common/namespace.js",
                "~/Scripts/namespace.variables.js",
                "~/Scripts/bootstrap/modernizr-2.6.2.min.js"));

            //Revolution Slider bundles
            bundles.Add(new StyleBundle("~/Content/css/revoslider").Include(
                "~/Content/css/rs-plugin/style.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Components/css/revoslider").Include(
                "~/Components/rs-plugin/css/settings.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/Components/scripts/revoslider").Include(
                "~/Components/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/Components/rs-plugin/js/jquery.themepunch.revolution.min.js"));

            //Plugins bundles
            bundles.Add(new ScriptBundle("~/Components/scripts/plugins").Include(
                "~/Components/gmaps/jquery.gmap.min.js",
                "~/Components/sticky/jquery.sticky.js",
                "~/Components/pickadate/v2-(deprecated)/source/pickadate.min.js"));

            //Login area bundles
            bundles.Add(new StyleBundle("~/Content/css/area/account").Include(
                "~/Content/less/login.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/Scripts/scripts/area/account").Include(
                "~/Scripts/app.module.js",
                "~/Scripts/app.routes.js",
                //Factories
                "~/Scripts/factories/factories.module.js",
                //Filters
                "~/Scripts/filters/filters.module.js",
                //Services
                "~/Scripts/services/services.module.js",
                //Controllers
                "~/Scripts/controllers/controllers.module.js",
                "~/Scripts/controllers/AccountController.js",
                //Directives
                "~/Scripts/directives/directives.module.js",
                "~/Scripts/directives/googleMapDirective.js",
                "~/Scripts/directives/scrollUpDirective.js",
                "~/Scripts/directives/stickyElementDirective.js",
                "~/Scripts/directives/loadingOverlayDirective.js",
                "~/Scripts/directives/buttonLoadingDirective.js"));

            //Public area bundles
            bundles.Add(new ScriptBundle("~/Scripts/scripts/area/public").Include(
                "~/Scripts/app.module.js",
                //Factories
                "~/Scripts/factories/factories.module.js",
                "~/Scripts/factories/googleMapChurchLocationFactory.js",
                "~/Scripts/factories/homePageRevoSliderOptionsFactory.js",
                //Filters
                "~/Scripts/filters/filters.module.js",
                "~/Scripts/filters/selectedTagsFilter.js",
                //Services
                "~/Scripts/services/services.module.js",
                "~/Scripts/services/SermonService.js",
                "~/Scripts/services/MessageService.js",
                //Controllers
                "~/Scripts/controllers/controllers.module.js",
                "~/Scripts/controllers/BaseController.js",
                "~/Scripts/controllers/HomeController.js",
                "~/Scripts/controllers/ContactController.js",
                "~/Scripts/controllers/ViewSermonsController.js",
                "~/Scripts/controllers/SermonDetailController.js",
                //Directives
                "~/Scripts/directives/directives.module.js",
                "~/Scripts/directives/googleMapDirective.js",
                "~/Scripts/directives/revosliderDirective.js",
                "~/Scripts/directives/scrollUpDirective.js",
                "~/Scripts/directives/stickyElementDirective.js",
                "~/Scripts/directives/loadingOverlayDirective.js",
                "~/Scripts/directives/buttonLoadingDirective.js"));

            //Member area bundles
            bundles.Add(new ScriptBundle("~/Scripts/scripts/area/member").Include(
                "~/Scripts/app.module.js",
                "~/Scripts/app.member.routes.js",
                //Factories
                "~/Scripts/factories/factories.module.js",
                "~/Scripts/factories/googleMapChurchLocationFactory.js",
                //Filters
                "~/Scripts/filters/filters.module.js",
                "~/Scripts/filters/selectedTagsFilter.js",
                //Services
                "~/Scripts/services/services.module.js",
                "~/Scripts/services/SermonService.js",
                "~/Scripts/services/MemberService.js",
                "~/Scripts/services/MessageService.js",
                //Controllers
                "~/Scripts/controllers/controllers.module.js",
                "~/Scripts/controllers/BaseController.js",
                "~/Scripts/controllers/HomeController.js",
                "~/Scripts/controllers/ContactController.js",
                "~/Scripts/controllers/ManageSermonsController.js",
                "~/Scripts/controllers/ManageMembersController.js",
                "~/Scripts/controllers/SermonDetailController.js",
                "~/Scripts/controllers/SermonModalController.js",
                "~/Scripts/controllers/ViewSermonsController.js",
                "~/Scripts/controllers/DirectoryController.js",
                //Directives
                "~/Scripts/directives/directives.module.js",
                "~/Scripts/directives/googleMapDirective.js",
                "~/Scripts/directives/revosliderDirective.js",
                "~/Scripts/directives/scrollUpDirective.js",
                "~/Scripts/directives/stickyElementDirective.js",
                "~/Scripts/directives/loadingOverlayDirective.js",
                "~/Scripts/directives/pickadateDirective.js",
                "~/Scripts/directives/buttonLoadingDirective.js"));
        }
    }
}