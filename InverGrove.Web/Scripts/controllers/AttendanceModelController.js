(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AttendanceModalCtrl', AttendanceModalController);

    AttendanceModalController.$inject = [
        '$modalInstance',
        '$window',
        'AttendanceService',
        'attendance'
    ];

    function AttendanceModalController($modalInstance, $window, AttendanceService, attendance) {
        var vm = this;
        vm.AttendanceService = AttendanceService;

        // defaults
        var defaults = {
            isUser: true,
            isMember: true,
            isVisitor: false
        };

        vm.busy = false;

        //vm.addUserSetupToForm = addUserSetupToForm;

        //vm.member = _.defaults(angular.copy(member) || {}, defaults);
        vm.addAttendance = addAttendance;
        vm.editAttendance = editAttendance;

        vm.$modalInstance = $modalInstance;
        vm.dismissModal = dismissModal;

        vm.boolToStr = boolToStr;

        function boolToStr(bool) {
            return bool ? 'Yes' : 'No';
        }      
        

        function addUserSetupToForm() {
            var isUser = vm.member.isUser;

            if (_.isBoolean(isUser)) {
                vm.requireEmail = isUser;
            } else if (_.isString(isUser)) {
                vm.requireEmail = isUser === 'true';
            }
        }

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }

        function addAttendance() {
            vm.busy = true;

            vm.AttendanceService.add(vm.member).then(function () {
                $window.location.reload();
            });
        }

        function editAttendance() {
            vm.busy = true;

            vm.AttendanceService.edit(vm.member).then(function () {
                $window.location.reload();
            });
        }        
    }
})();