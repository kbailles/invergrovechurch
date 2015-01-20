

(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('MemberModalCtrl',MemberModalController);

    MemberModalController.$inject = [
        '$modalInstance',
        '$rootScope'
    ];

    function MemberModalController($modalInstance) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.memberObj = {};
        vm.addMember = addMember;
        vm.$modalInstance = $modalInstance;
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

        function addMember() {
            vm.busy = true;
            $rootScope.$broadcast('addMember', vm.memberObj);
        }
    }
})();