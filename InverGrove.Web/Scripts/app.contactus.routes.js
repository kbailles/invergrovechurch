(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName)
        .config(routes);

    routes.$inject = ['$routeProvider'];

    function routes($routeProvider) {
        $routeProvider

            .when('/ContactUs', {
                templateUrl: '/Contact/ContactUs',
                controller: 'ContactCtrl',
                controllerAs: 'vm'
            })

            .otherwise({ redirectTo: '/ContactUs' });
    }
})();