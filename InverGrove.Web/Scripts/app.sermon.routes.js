(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName)
        .config(routes);

    routes.$inject = ['$routeProvider'];

    function routes($routeProvider) {
        $routeProvider

            .when('/ViewSermons', {
                templateUrl: '/Sermon/ViewSermons',
                controller: 'ViewSermonsCtrl',
                controllerAs: 'vm'
            })

            .when('/SermonDetail/:id', {
                templateUrl: '/Sermon/SermonDetail',
                controller: 'SermonDetailCtrl',
                controllerAs: 'vm'
            })

            .otherwise({ redirectTo: '/ViewSermons' });
    }
})();