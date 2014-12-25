(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [
        'sermons',
        'SermonService',
        '$scope',
        '$modal'
    ];

    function ManageSermonsController(sermons, SermonService, $scope, $modal) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermons = sermons.data;
        vm.soundCloudSermons = [];
        vm.SermonService = SermonService;

        vm.openAddSermonModal = openAddSermonModal;
        vm.openEditSermonModal = openEditSermonModal;
        vm.openDeleteSermonModal = openDeleteSermonModal;

        vm.$modalInstance = null;

        activate();

        /*
         * Private declarations
         */
        function activate() {
            SC.get('/users/' + igchurch.constants.SOUND_CLOUD_USERID + '/tracks', function (tracks) {
                vm.soundCloudSermons = tracks;
            });
        }

        function openAddSermonModal() {
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Add',
                resolve: {
                    sermon: function() {
                        return {};
                    },
                    soundCloudSermons: function() {
                        return vm.soundCloudSermons;
                    }
                }
            });
        }

        function openEditSermonModal(sermon) {
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Edit',
                resolve: {
                    sermon: function() {
                        return sermon;
                    },
                    soundCloudSermons: function () {
                        return vm.soundCloudSermons;
                    }
                }
            });
        }

        function openDeleteSermonModal(sermon) {
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Delete',
                resolve: {
                    sermon: function () {
                        return sermon;
                    },
                    soundCloudSermons: function () {
                        return vm.soundCloudSermons;
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
