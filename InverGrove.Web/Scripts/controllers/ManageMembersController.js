(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageMembersCtrl', ManageMembersController);

    ManageMembersController.$inject = [
        'MemberService',
        '$scope',
        '$modal'
    ];

    function ManageMembersController(MemberService, $scope, $modal) {
        var vm = this;

        vm.members = members;
        vm.MemberService = MemberService;

        vm.openAddMemberModal = openAddMemberModal;
        vm.openEditMemberModal = openEditMemberModal;
        vm.openDeleteMemberModal = openDeleteMemberModal;

        vm.$modalInstance = null;

        function openAddMemberModal() {

            vm.$modalInstance = $modal.open({
                controller: 'MemberModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/AddMember',
                size: 'lg',
                resolve: {
                    member: function () {
                        return {};
                    }
                }
            });
        }

        function openEditMemberModal(member) {
            vm.$modalInstance = $modal.open({
                controller: 'MemberModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/EditUser',
                resolve: {
                    member: function () {
                        return member;
                    }
                }
            });
        }

        function openDeleteMemberModal(member) {
            vm.$modalInstance = $modal.open({
                controller: 'MemberModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/DeleteMember',
                resolve: {
                    member: function () {
                        return member;
                    }
                }
            });
        }

        // broadcast from the modal
        $scope.$on('addMember', function (event, member) {

            if (!member) {
                return;
            }

            vm.MemberService.add(member).then(function (response) {
                vm.members.push(member);
            },
            function (error) {})
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('editMember', function (event, member) {

            if (!member) {
                return;
            }

            vm.MemberService.edit(member).then(function (response) {
                vm.members.push(member);
            },
            function (error) {})
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('deletePerson', function (event, member) {

            vm.MemberService.delete(member).then(function (response) {

                vm.members = _.reject(vm.members, { personId: member.personId });
            },
            function (error) {
                alert('oops'); // remove
            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });
    }
})();
