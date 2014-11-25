(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName)
        .config(routes);

    routes.$inject = ['$routeProvider'];

    function routes($routeProvider) {
        $routeProvider

            .when('/', {
                templateUrl: '/Home/Home',
                controller: 'HomeCtrl',
                controllerAs: 'vm'
            })

            .when('/Sermon/SermonDetail/:id', {
                templateUrl: '/Sermon/SermonDetail',
                controller: 'SermonDetailCtrl',
                controllerAs: 'vm'
            })

            .otherwise({ redirectTo: '/' });
    }
})();