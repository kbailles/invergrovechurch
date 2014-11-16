(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('BaseCtrl', BaseController);

    BaseController.$inject = [
        '$location'
    ];

    function BaseController($location) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.isRouteActive = isRouteActive;

        /*
         * Private declarations
         */
        function isRouteActive(route) {
            return !route ? '/' === $location.path() : $location.absUrl().indexOf(route) > -1;
        }
    }
})();