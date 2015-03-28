(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName)
        .config(routes);

    routes.$inject = ['$routeProvider'];

    function routes($routeProvider) {
        $routeProvider

            .when('/Register', {
                templateUrl: '/Account/Register',
                controller: 'RegisterCtrl',
                controllerAs: 'vm'
            })


            .otherwise({ redirectTo: '/' });
    }
})();