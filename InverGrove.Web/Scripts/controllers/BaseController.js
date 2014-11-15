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

        vm.isRouteActive = function(route) {
            return !route ? '/' === $location.path() : $location.absUrl().indexOf(route) > -1;
        }
    }
})();