﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageUsersCtrl', ManageUsersController);

    ManageUsersController.$inject = [
        'users',
        'UserService',
        '$scope',
        '$modal'
    ];

    function ManageUsersController(users, UserService, $scope, $modal) {
        var vm = this;

        alert('If you see this, then something is finally working!');

        vm.users = users.data;
        vm.UserService = UserService;

        vm.openAddUserModal = openAddUserModal;
        vm.openEditUserModal = openEditUserModal;
        vm.openDeleteUserModal = openDeleteUserModal;

        vm.$modalInstance = null;

        activate();

        function activate() {
            getUsers();
        }

        function getUsers() {
            debugger;
            $scope.$emit('loading-started'); 

            return UserService.GetAll().then(function (data) {
                vm.users = data.data;
                $scope.$emit('loading-complete');
            });
        }

        function openAddUserModal() {
            vm.$modalInstance = $modal.open({
                controller: 'UserModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/User/Add',
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
                templateUrl: '/Member/User/Edit',
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
                templateUrl: '/Member/User/Delete',
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
