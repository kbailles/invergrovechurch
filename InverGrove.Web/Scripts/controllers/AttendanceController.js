(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AttendanceCtrl', AttendanceController);

    AttendanceController.$inject = [
        'AttendanceService',
        '$modal'
    ];

    function AttendanceController(AttendanceService, $modal) {
        var vm = this;

        vm.members = members;

        vm.openAddAttendanceModal = openAddAtendanceModal;
        
        vm.$modalInstance = null;

        function openAddAtendanceModal() {

            vm.$modalInstance = $modal.open({
                controller: 'AttendanceModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Attendance/Add',
                size: 'lg',
                resolve: {
                    member: function () {
                        return {};
                    }
                }
            });
        }
    }
})();