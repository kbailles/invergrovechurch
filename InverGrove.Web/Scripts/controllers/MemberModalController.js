

(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('MemberModalCtrl',MemberModalController);

    MemberModalController.$inject = [
        '$modalInstance',
        '$rootScope',
        'member'
    ];

    function MemberModalController($modalInstance, $rootScope, member) {
        var vm = this;

        vm.personObj = {
            // defaults
            isUser:true
        };

        vm.busy = false;

        vm.addUserSetupToForm = addUserSetupToForm;
        vm.disableEmail = true;
        vm.requireEmail = false;

        vm.member = angular.copy(member) || {};
        vm.addPerson = addPerson;
        vm.deletePerson = deletePerson;

        vm.$modalInstance = $modalInstance;
        vm.dismissModal = dismissModal;

        activate();

        function activate() {
        }

        function addUserSetupToForm() {
            var isUser = vm.personObj.isUser;
            vm.disableEmail = (isUser === 'true') ? false : true;
            vm.requireEmail = (isUser === 'true') ? true : false;
        }

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }

        function addPerson() {
            vm.busy = true;
            $rootScope.$broadcast('addMember', vm.personObj);
        }

        function deletePerson() {
            vm.busy = true;
            $rootScope.$broadcast('deletePerson', vm.member);
        }
    }
})();