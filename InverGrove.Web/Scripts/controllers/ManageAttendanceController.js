(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageAttendanceCtrl', ManageAttendanceController);

    ManageAttendanceController.$inject = [
        '$modal'
    ];

    function ManageAttendanceController($modal) {
        var vm = this;

        vm.openAddUserModal = openAddUserModal;

        vm.$modalInstance = null;

        activate();

        function activate() {
        }

        function openAddUserModal() {
            vm.$modalInstance = $modal.open({
                controller: 'UserModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/Add',
                resolve: {
                    user: function () {
                        return {};
                    }
                }
            });
        }

        function openEditUserModal(user) {
            vm.$modalInstance = $modal.open({
                controller: 'UserModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/Edit',
                resolve: {
                    user: function () {
                        return user;
                    }
                }
            });
        }

        function openDeleteUserModal(user) {
            vm.$modalInstance = $modal.open({
                controller: 'UserModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Member/Delete',
                resolve: {
                    user: function () {
                        return user;
                    }
                }
            });
        }

        $scope.$on('addUser', function (event, user) {
            if (!user) {
                return;
            }

            vm.UserService.add(user).then(function (response) {
                vm.users.push(user);
            },
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('editUser', function (event, user) {
            var userToEdit = _.find(vm.users, function (s) {
                return s.userId === user.userId;
            });

            if (!userToEdit) {
                return;
            }

            vm.UserService.update(user).then(function (response) {
                var index = vm.users.indexOf(userToEdit);
                vm.users[index] = user;
            },
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('deleteUser', function (event, user) {
            var userToDelete = _.find(vm.users, function (s) {
                return s.userId === user.userId;
            });

            if (!userToDelete) {
                return;
            }

            vm.UserService.delete(userToDelete).then(function (response) {
                var index = vm.users.indexOf(useRAFToDelete);

                if (index > -1) {
                    vm.users.splice(index, 1);
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
