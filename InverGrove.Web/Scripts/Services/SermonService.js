(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('SermonService', SermonService);

    SermonService.$inject = ['$http'];

    function SermonService($http) {

        this.getSermons = function () {

            return $http({ method: 'GET', url: '/Sermon/GetSermons' }).
                success(function(data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
            });
        }
    }
})();