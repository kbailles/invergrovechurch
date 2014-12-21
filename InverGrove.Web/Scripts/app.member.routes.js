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

            .when('/ManageSermons', {
                templateUrl: '/Member/Sermon/ManageSermons',
                controller: 'ManageSermonsCtrl',
                controllerAs: 'vm',
                resolve: {
                    sermons: ['SermonService', function (SermonService) {
                        return SermonService.getSermons();
                    }]
                }
            })

            .when('/ManageUsers', {
                templateUrl: '/Member/User/ManageUsers'
            })

            .otherwise({ redirectTo: '/' });
	}
})();