(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageUsersController);

    ManageUsersController.$inject = [
        'UserService',
        'users',
        '$scope',
        '$modal'
    ];

    function ManageUsersController(UserService, users, $scope, $modal) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.users = users.data;
        vm.UserService = UserService;

        vm.openAddUserModal = openAddUserModal;
        vm.openEditUserModal = openEditUserModal;
        vm.openDeleteUserModal = openDeleteUserModal;

        vm.$modalInstance = null;

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function openAddUserModal() {
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Add',
                resolve: {
                    sermon: function () {
                        return {};
                    }
                }
            });
        }

        function openEditUserModal(sermon) {
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Edit',
                resolve: {
                    sermon: function () {
                        return sermon;
                    }
                }
            });
        }

        function openDeleteUserModal(sermon) {
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Delete',
                resolve: {
                    sermon: function () {
                        return sermon;
                    }
                }
            });
        }

        $scope.$on('addSermon', function (event, sermon) {
            if (!sermon) {
                return;
            }

            vm.SermonService.add(sermon).then(function (response) {
                vm.sermons.push(sermon);
            },
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('editSermon', function (event, sermon) {
            var sermonToEdit = _.find(vm.sermons, function (s) {
                return s.sermonId === sermon.sermonId;
            });

            if (!sermonToEdit) {
                return;
            }

            vm.SermonService.update(sermon).then(function (response) {
                var index = vm.sermons.indexOf(sermonToEdit);
                vm.sermons[index] = sermon;
            },
            function (error) {

            })
            .finally(function () {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('deleteSermon', function (event, sermon) {
            var sermonToDelete = _.find(vm.sermons, function (s) {
                return s.sermonId === sermon.sermonId;
            });

            if (!sermonToDelete) {
                return;
            }

            vm.SermonService.delete(sermonToDelete).then(function (response) {
                var index = vm.sermons.indexOf(sermonToDelete);

                if (index > -1) {
                    vm.sermons.splice(index, 1);
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
