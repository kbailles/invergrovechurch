(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('MemberService', MemberService);

    MemberService.$inject = ['$http'];

    function MemberService($http) {

        this.getUser = function (userId) {

            return $http({ method: 'GET', url: '/Member/Member/GetUser', params: { memberdId: memberId } }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

        this.getAll = function () {

            return $http({ method: 'GET', url: '/Member/Member/GetAllUsers' }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

    }
})();