(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('NewsEventsService', NewsEventsService);

    NewsEventsService.$inject = ['$http'];

    function NewsEventsService($http) {

    }
})();