(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AttendanceModalCtrl', AttendanceModalController);

    AttendanceModalController.$inject = [
        '$modalInstance'
    ];

    function AttendanceModalController($modalInstance) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.dismissModal = dismissModal;

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }
    }
})();
