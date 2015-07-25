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

        // defaults
        var defaults = {
            isUser: true,
            phoneNumbers: [{ phone: '', phoneNumberTypeId: 1, phoneNumberType: 'Home' },
                           { phone: '', phoneNumberTypeId: 2, phoneNumberType: 'Mobile' },
                           { phone: '', phoneNumberTypeId: 3, phoneNumberType: 'Work' }]
        };

        vm.busy = false;

        vm.addUserSetupToForm = addUserSetupToForm;
        vm.disableEmail = true;
        vm.requireEmail = false;

        vm.member = _.defaults(angular.copy(member) || {}, defaults);
        vm.addPerson = addPerson;
        vm.editPerson = editPerson;
        vm.deletePerson = deletePerson;

        vm.$modalInstance = $modalInstance;
        vm.dismissModal = dismissModal;

        vm.boolToStr = boolToStr;

        function boolToStr(bool) {
            return bool ? 'Yes' : 'No';
        }

        vm.homePhone = getPhoneNumber(1);
        vm.mobile = getPhoneNumber(2);
        vm.workPhone = getPhoneNumber(3);

        vm.getPhoneNumber = getPhoneNumber;
        vm.setPhoneNumber = setPhoneNumber;
        vm.createPhoneNumber = createPhoneNumber;

        function getPhoneNumber(typeId) {
            var phoneNumber = _.find(vm.member.phoneNumbers, { phoneNumberTypeId: typeId });

            return phoneNumber ? formatNumber(phoneNumber.phone) : '';
        }

        function setPhoneNumber(type, typeId, number) {
            var phoneNumber = _.find(vm.member.phoneNumbers, { phoneNumberTypeId: typeId });

            if (phoneNumber) {
                phoneNumber.phone = number;
            } else {
                createPhoneNumber(type, typeId, number);
            }
        }

        function createPhoneNumber(type, typeId, number) {
            var newPhoneNumber = {
                phoneNumberType: type,
                phoneNumberTypeId: typeId,
                phone: number
            }

            if (!vm.member.phoneNumbers) {
                vm.member.phoneNumbers = [];
            }

            vm.member.phoneNumbers.push(newPhoneNumber);
        }

        function addUserSetupToForm() {
            var isUser = vm.member.isUser;
            vm.disableEmail = (isUser === 'true') ? false : true;
            vm.requireEmail = (isUser === 'true') ? true : false;
        }

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }

        function addPerson() {
            vm.busy = true;

            vm.MemberService.add(vm.member).then(function () {
                $window.location.reload();
            });
        }

        function editPerson() {
            vm.busy = true;

            setPhoneNumber('Home', 1, vm.homePhone);
            setPhoneNumber('Mobile', 2, vm.mobile);
            setPhoneNumber('Work', 3, vm.workPhone);

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

        function formatNumber(number) {
            var val = number.replace(/[^0-9]+/g, '');

            if (!val) {
                return '';
            }

            var area = val.substring(0, 3);
            var front = val.substring(3, 6);
            var end = val.substring(6, 10);

            if (front) {
                val = ("(" + area + ") " + front);
            }
            if (end) {
                val += ("-" + end);
            }

            return val;
        }
    }
})();