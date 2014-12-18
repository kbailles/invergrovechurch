(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('SermonService', SermonService);

    SermonService.$inject = [
        '$http'
    ];

    function SermonService($http) {

        this.getSermon = function (sermonId) {

            return $http({ method: 'GET', url: '/Sermon/GetById', params: { sermonId: sermonId } }).
                success(function(data, status, headers, config) {
                    return data;
                }).
                error(function(data, status, headers, config) {
                });
        }

        this.getSermons = function () {

            return $http({ cache: true, method: 'GET', url: '/Sermon/GetAll' }).
                success(function(data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
            });
        }
    }
})();
