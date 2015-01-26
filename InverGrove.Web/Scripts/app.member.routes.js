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

            .when('/Directory', {
                templateUrl: '/Member/Member/Directory',
                controller: 'DirectoryCtrl',
                controllerAs: 'vm'
            })

            .when('/ManageAttendance', {
                templateUrl: '/Member/Member/ManageAttendance',
                controller: 'ManageAttendanceCtrl',
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

            .when('/ManageMembers', {
                templateUrl: '/Member/Member/ManageMembers',
                controller: 'ManageMembersCtrl',
                controllerAs: 'vm'
            })

            .when('/ManageNewsEvents', {
                templateUrl: '/Member/NewsEvents/ManageNewsAndEvents',
                controller: 'ManageMembersCtrl',
                controllerAs: 'vm'
            })

            .when('/ManagePrayerRequests', {
                templateUrl: '/Member/PrayerRequest/ManagePrayerRequests',
                controller: 'ManageMembersCtrl',
                controllerAs: 'vm'
            })

            .otherwise({ redirectTo: '/' });
	}
})();