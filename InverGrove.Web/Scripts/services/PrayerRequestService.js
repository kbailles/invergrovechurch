(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('PrayerRequestService', PrayerRequestService);

    PrayerRequestService.$inject = ['$http'];

    function PrayerRequestService($http) {

    }
})();