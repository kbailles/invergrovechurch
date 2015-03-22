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

        vm.members = [];
        vm.MemberService = MemberService;

        vm.openAddMemberModal = openAddMemberModal;
        //vm.openEditMemberModal = openEditMemberModal; // I think this should be a page and not a modal.  Will be LARGE
        vm.openDeleteMemberModal = openDeleteMemberModal;

        vm.showProfileTypeMessage = '';

        vm.$modalInstance = null;

        activate();

        function activate() {
            getUsers();
        }

        function getUsers() {

            $scope.$emit('loading-started');

            return MemberService.getAll().then(function (data) {
                vm.members = data.data;
                $scope.$emit('loading-complete');
            });
        }

        function openAddMemberModal() {

            vm.$modalInstance = $modal.open({
                controller: 'MemberModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/AddMember',
                resolve: {
                    user: function () {
                        return {};
                    }
                }
            });
        }

        function openEditMemberModal(member) {
            //vm.$modalInstance = $modal.open({
            //    controller: 'MemberModalCtrl',
            //    controllerAs: 'modalCtrl',
            //    templateUrl: '/Member/Member/Edit',
            //    resolve: {
            //        user: function () {
            //            return member;
            //        }
            //    }
            //});
        }

        function openDeleteMemberModal(member) {
            vm.$modalInstance = $modal.open({
                controller: 'MemberModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/Delete',
                resolve: {
                    user: function () {
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
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('editMember', function (event, member) {
            var memberToEdit = _.find(vm.members, function (s) {
                return s.memberId === member.memberId;
            });

            if (!memberToEdit) {
                return;
            }

            vm.UserService.update(member).then(function (response) {
                var index = vm.members.indexOf(memberToEdit);
                vm.members[index] = member;
            },
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('deleteMember', function (event, member) {
            var memberToDelete = _.find(vm.members, function (s) {
                return s.memberId === member.memberId;
            });

            if (!memberToDelete) {
                return;
            }

            vm.UserService.delete(memberToDelete).then(function (response) {
                var index = vm.members.indexOf(memberToDelete);

                if (index > -1) {
                    vm.members.splice(index, 1);
                }
            },
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });
    }
})();
