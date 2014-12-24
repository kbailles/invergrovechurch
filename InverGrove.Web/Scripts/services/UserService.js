(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('UserService', UserService);

    UserService.$inject = [
        '$http'
    ];

    function UserService($http) {

    }
})();