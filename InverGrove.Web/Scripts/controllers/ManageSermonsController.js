(function (_) {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [
        '$modal'
    ];

    function ManageSermonsController($modal) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermons = sermons;
        vm.soundCloudSermons = [];

        vm.openAddSermonModal = openAddSermonModal;
        vm.openEditSermonModal = openEditSermonModal;
        vm.openDeleteSermonModal = openDeleteSermonModal;

        vm.$modalInstance = null;

        activate();

        /*
         * Private declarations
         */
        function activate() {
            SC.get('/users/' + igchurch.constants.SOUND_CLOUD_USERID + '/tracks', { limit: 200 }, function (tracks) {
                if (!!(tracks) && tracks.length > 0) {
                    vm.soundCloudSermons = tracks;
                }
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
                    soundCloudSermons: function () {
                        var soundCloudIds = _.pluck(vm.sermons, 'soundCloudId');

                        return _.filter(vm.soundCloudSermons, function (track) {
                            return !_.includes(soundCloudIds, track.id);
                        });
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
                        var soundCloudIds = _.pluck(vm.sermons, 'soundCloudId');

                        return _.filter(vm.soundCloudSermons, function (track) {
                            return !_.includes(soundCloudIds, track.id) || track.id === sermon.soundCloudId;
                        });
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
    }
})( window._ );
