(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('BaseCtrl', BaseController);

    function BaseController() {
        var base = this;

        base.navBarCollapsed = true;

        base.toggleNavBar = toggleNabBar;
        base.goToPath = goToPath;

        function goToPath(path) {
            window.location.href = path;
        }

        function toggleNabBar() {
            base.navBarCollapsed = !base.navBarCollapsed;
        }
    }
})();