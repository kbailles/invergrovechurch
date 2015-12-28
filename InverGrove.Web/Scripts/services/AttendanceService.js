(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('AttendanceService', AttendanceService);

    AttendanceService.$inject = ['$http'];

    function AttendanceService($http) {

        this.getAll = function (userId) {

            return $http({ method: 'GET', url: '/Member/Attendance/ManageAttendance' }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }

        this.add = function (personAttendanceCollection) {

            return $http({ method: 'POST', url: '/Member/Attendance/Add', data: { attendancePersons: personAttendanceCollection } }).
                success(function (data, status, headers, config) {
                    return data;
                }).
                error(function (data, status, headers, config) {
                });
        }
    }
})();