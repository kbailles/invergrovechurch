

(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('MemberModalCtrl',MemberModalController);

    MemberModalController.$inject = [
        '$modalInstance',
        '$rootScope'
    ];

    function MemberModalController($modalInstance, $rootScope) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.personObj = {
            // defaults
            isUser:true
            };
        vm.busy = false;

        vm.addPerson = addPerson;
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

        function addPerson() {
            vm.busy = true;
            $rootScope.$broadcast('addMember', vm.personObj);
        }
    }
})();