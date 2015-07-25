using System.Web.Optimization;

namespace InverGrove.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Core bundles
            bundles.Add(new StyleBundle("~/Content/css/core")
                .Include("~/bower_components/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Components/pickadate/v2-(deprecated)/themes/pickadate.04.inline-fixed.css", new CssRewriteUrlTransform())
                .Include("~/Content/less/app.min.css"));

            bundles.Add(new ScriptBundle("~/Components/scripts/critical").Include(
                "~/bower_components/jquery/dist/jquery.min.js",
                "~/bower_components/angular/angular.js"));

            bundles.Add(new StyleBundle("~/Components/css/revoslider")
                .Include("~/Content/css/rs-plugin/style.css", new CssRewriteUrlTransform())
                .Include("~/Components/rs-plugin/css/settings.css", new CssRewriteUrlTransform()));

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
                "~/Components/html5-boilerplate/css/normalize.css",
                "~/Components/html5-boilerplate/css/main.css")
                .Include("~/bower_components/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/less/login.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/Scripts/scripts/area/account").Include(
                "~/Components/modernizr/modernizr-2.6.2.min.js",
                "~/bower_components/lodash/lodash.min.js",
                "~/bower_components/moment/min/moment.min.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",

                "~/Scripts/common/namespace.js",
                "~/Scripts/namespace.variables.js",

                "~/Scripts/app.module.js",
                "~/Scripts/app.routes.js",
                //Factories
                "~/Scripts/factories/factories.module.js",
                //Filters
                "~/Scripts/filters/filters.module.js",
                //Services
                "~/Scripts/services/services.module.js",
                "~/Scripts/services/UserService.js",
                //Controllers
                "~/Scripts/controllers/controllers.module.js",
                "~/Scripts/controllers/AccountController.js",
                "~/Scripts/controllers/RegisterController.js",
                //Directives
                "~/Scripts/directives/directives.module.js",
                "~/Scripts/directives/googleMapDirective.js",
                "~/Scripts/directives/scrollUpDirective.js",
                "~/Scripts/directives/stickyElementDirective.js",
                "~/Scripts/directives/buttonLoadingDirective.js"));

            //Public area bundles
            bundles.Add(new ScriptBundle("~/Scripts/scripts/area/public").Include(
                "~/Components/modernizr/modernizr-2.6.2.min.js",
                "~/bower_components/lodash/lodash.min.js",
                "~/bower_components/moment/min/moment.min.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",

                "~/Scripts/common/namespace.js",
                "~/Scripts/namespace.variables.js",

                "~/Components/gmaps/jquery.gmap.min.js",
                "~/Components/sticky/jquery.sticky.js",
                "~/Components/pickadate/v2-(deprecated)/source/pickadate.min.js",

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
                "~/Scripts/directives/buttonLoadingDirective.js"));

            //Member area bundles
            bundles.Add(new ScriptBundle("~/Scripts/scripts/area/member").Include(
                "~/Components/modernizr/modernizr-2.6.2.min.js",
                "~/bower_components/lodash/lodash.min.js",
                "~/bower_components/moment/min/moment.min.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",

                "~/Scripts/common/namespace.js",
                "~/Scripts/namespace.variables.js",

                "~/Components/gmaps/jquery.gmap.min.js",
                "~/Components/sticky/jquery.sticky.js",
                "~/Components/pickadate/v2-(deprecated)/source/pickadate.min.js",

                "~/Scripts/app.module.js",
                "~/Scripts/app.member.routes.js",
                //Factories
                "~/Scripts/factories/factories.module.js",
                "~/Scripts/factories/googleMapChurchLocationFactory.js",
                "~/Scripts/factories/phoneNumberHelperFactory.js",
                //Filters
                "~/Scripts/filters/filters.module.js",
                "~/Scripts/filters/firstNameLastNameFilter.js",
                "~/Scripts/filters/selectedTagsFilter.js",
                //Services
                "~/Scripts/services/services.module.js",
                "~/Scripts/services/SermonService.js",
                "~/Scripts/services/MemberService.js",
                "~/Scripts/services/NewsEventsService.js",
                "~/Scripts/services/MessageService.js",
                //Controllers
                "~/Scripts/controllers/controllers.module.js",
                "~/Scripts/controllers/BaseController.js",
                "~/Scripts/controllers/ManageSermonsController.js",
                "~/Scripts/controllers/ManageMembersController.js",
                "~/Scripts/controllers/MemberModalController.js",
                "~/Scripts/controllers/SermonDetailController.js",
                "~/Scripts/controllers/SermonModalController.js",
                "~/Scripts/controllers/DirectoryController.js",
                //Directives
                "~/Scripts/directives/directives.module.js",
                "~/Scripts/directives/formatPhoneDirective.js",
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