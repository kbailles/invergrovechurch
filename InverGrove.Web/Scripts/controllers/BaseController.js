(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('BaseCtrl', BaseController);

    BaseController.$inject = [
        '$location'
    ];

    function BaseController($location) {
        var base = this;

        /*
         * Public declarations
         */
        base.navBarCollapsed = false;
        base.toggleNavBar = toggleNabBar;

        base.isRouteActive = isRouteActive;
        base.goToPath = goToPath;

        function goToPath(path) {
            window.location.href = path;
        }

        function isRouteActive(route) {
            return !route ? '/' === $location.path() : $location.absUrl().indexOf(route) > -1;
        }

        function toggleNabBar() {
            base.navBarCollapsed = !base.navBarCollapsed;
        }
    }
})();