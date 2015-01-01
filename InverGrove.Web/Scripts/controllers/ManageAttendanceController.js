(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageAttendanceCtrl', ManageAttendanceController);

    ManageAttendanceController.$inject = [
        '$modal'
    ];

    function ManageAttendanceController($modal) {
        var vm = this;

        vm.openAddAttendanceModal = openAddAttendanceModal;

        vm.$modalInstance = null;

        activate();

        function activate() {
        }

        function openAddAttendanceModal() {
            vm.$modalInstance = $modal.open({
                controller: 'AttendanceModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/AddAttendance'
            });
        }
    }
})();
