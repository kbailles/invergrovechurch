(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('MessageService', MessageService);

    MessageService.$inject = [
        '$http'
    ];

    function MessageService($http) {

        this.sendMessage = function (messageObj) {

            return $http.post('/Contact/ContactUs', messageObj).
                success(function (data, status, headers, config) {
                    return true;
                }).
                error(function (data, status, headers, config) {
                    return false;
                });
        }
    }
})();