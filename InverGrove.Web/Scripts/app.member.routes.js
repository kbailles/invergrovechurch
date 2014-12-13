(function () {
	'use strict';

	var appName = igchurch.constants.APP_NAME;

	angular.module(appName)
        .config(routes);

	routes.$inject = ['$routeProvider'];

	function routes($routeProvider) {
		$routeProvider

            .when('/', {
            	templateUrl: '/Member/Home/Home',
            	controller: 'HomeCtrl',
            	controllerAs: 'vm'
            })

            .when('/Sermon/SermonDetail/:id', {
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

            .otherwise({ redirectTo: '/' });
	}
})();