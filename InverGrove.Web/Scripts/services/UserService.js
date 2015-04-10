(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('UserService',UserService);

    UserService.$inject = ['$http'];

    function UserService($http) {

        // Person is accessing Register page to set up first-time uid/pwd
        this.getPreRegister = function (authToken) {

            return $http({ method: 'GET', url: '/Account/GetPreRegister', params: { code: authToken } }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

        // Actually should be a register service ... maybe ...
        this.registerUser = function (userId) {

            return $http({ method: 'GET', url: '/Account/RegisterUser', params: { memberdId: memberId } }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

 


    }
})();