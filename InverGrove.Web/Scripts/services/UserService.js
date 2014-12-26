(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('UserService', UserService);

    UserService.$inject = ['$http'];

    function UserService($http) {

        this.getUser = function (userId) {

            return $http({ method: 'GET', url: '/User/GetUser', params: { userdId: userId } }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

        this.getAll = function () {

            return $http({ method: 'GET', url: '/User/GetAllUsers' }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

    }
})();