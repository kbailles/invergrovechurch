(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('MemberModalCtrl',MemberModalController);

    MemberModalController.$inject = [
        '$modalInstance',
        '$window',
        'MemberService',
        'member'
    ];

    function MemberModalController($modalInstance, $window, MemberService, member) {
        var vm = this;

        vm.MemberService = MemberService;

        vm.personObj = {
            // defaults
            isUser: true,
            phoneNumbers: [{ phone: '', phoneNumberTypeId: 1, phoneNumberType: 'Home' },
                           { phone: '', phoneNumberTypeId: 2, phoneNumberType: 'Mobile' },
                           { phone: '', phoneNumberTypeId: 3, phoneNumberType: 'Work' }]
        };

        vm.busy = false;

        vm.addUserSetupToForm = addUserSetupToForm;
        vm.disableEmail = true;
        vm.requireEmail = false;

        vm.member = angular.copy(member) || {};
        vm.addPerson = addPerson;
        vm.editPerson = editPerson;
        vm.deletePerson = deletePerson;

        vm.$modalInstance = $modalInstance;
        vm.dismissModal = dismissModal;

        vm.boolToStr = boolToStr;

        function boolToStr(bool) {
            return bool ? 'Yes' : 'No';
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

            vm.MemberService.add(vm.personObj).then(function () {
                $window.location.reload();
            });
        }

        function editPerson() {
            vm.busy = true;

            vm.MemberService.edit(vm.member).then(function () {
                $window.location.reload();
            });
        }

        function deletePerson() {
            vm.busy = true;

            vm.MemberService.delete(vm.member).then(function () {
                $window.location.reload();
            });
        }
    }
})();