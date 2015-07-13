(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageMembersCtrl', ManageMembersController);

    ManageMembersController.$inject = [
        'MemberService',
        '$modal'
    ];

    function ManageMembersController(MemberService, $modal) {
        var vm = this;

        vm.members = _.where(members, { isDeleted: false });

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
                size: 'lg',
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
    }
})();
