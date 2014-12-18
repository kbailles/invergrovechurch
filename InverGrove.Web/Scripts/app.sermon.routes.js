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
                controllerAs: 'vm',
                resolve: {
                    sermons: ['SermonService', function (SermonService) {
                        return SermonService.getSermons();
                    }]
                }
            })

            .when('/SermonDetail/:id', {
                templateUrl: '/Sermon/SermonDetail',
                controller: 'SermonDetailCtrl',
                controllerAs: 'vm',
                resolve: {
                    sermon: ['$route', 'SermonService', function ($route, SermonService) {
                        var sermonId = $route.current.params.id;
                        return SermonService.getSermon(sermonId);
                    }]
                }
            })

            .otherwise({ redirectTo: '/ViewSermons' });
    }
})();