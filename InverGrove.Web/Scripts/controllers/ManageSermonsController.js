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
            var rennieCounter = 0;
            var rennieTotalTime = 0;

            var bobbyShawCounter = 0;
            var bobbyShawTotalTime = 0;

            var othersCounter = 0;
            var othersTotalTime = 0;

            var everyoneCounter = 0;
            var everyoneTotalTime = 0;

            var longestSermon = null;
            var shortestSermon = null;

            getTracks('/users/' + igchurch.constants.SOUND_CLOUD_USERID + '/tracks',
                function(tracks) {
                    tallySermons(tracks.collection);

                    if (tracks.next_href) {
                        getTracks(tracks.next_href, function (tracks) {
                            tallySermons(tracks.collection);

                            console.log('Total Number of Lessons for Rennie Frazier: ' + rennieCounter);
                            console.log('Average Sermon Time for Rennie Frazier: ' + (rennieTotalTime / rennieCounter));

                            console.log('Total Number of Lessons for Bobby Shaw: ' + bobbyShawCounter);
                            console.log('Average Sermon Time for Bobby Shaw: ' + (bobbyShawTotalTime / bobbyShawCounter));

                            console.log('Total Number of Lessons for everyone besides Bobby Shaw and Rennie Frazier: ' + othersCounter);
                            console.log('Average Sermon Time for everyone besides Bobby Shaw and Rennie Frazier: ' + (othersTotalTime / othersCounter));

                            console.log('Total Number of Lessons for everyone: ' + everyoneCounter);
                            console.log('Average Sermon Time for everyone: ' + (everyoneTotalTime / everyoneCounter));

                            console.log('Shortest sermon title: ' + shortestSermon.title);
                            console.log('Shortest sermon description: ' + shortestSermon.description);
                            console.log('Shortest sermon duration: ' + (shortestSermon.duration / 1000) / 60);

                            console.log('Longest sermon title: ' + longestSermon.title);
                            console.log('Longest sermon description: ' + longestSermon.description);
                            console.log('Longest sermon duration: ' + (longestSermon.duration / 1000) / 60);
                        });
                    }
                });

            function tallySermons(tracks) {
                for (var i = 0; i < tracks.length; i++) {
                    var track = tracks[i];


                    everyoneCounter++;
                    everyoneTotalTime += (track.duration / 1000) / 60;

                    if (track.title.toLowerCase().includes("rfrazier") ||
                        track.description.toLowerCase().includes("rennie frazier")) {
                        rennieCounter++;
                        rennieTotalTime += (track.duration / 1000) / 60;
                    } else if (track.title.toLowerCase().includes("shaw")) {
                        bobbyShawCounter++;
                        bobbyShawTotalTime += (track.duration / 1000) / 60;
                    } else {
                        othersCounter++;
                        othersTotalTime += (track.duration / 1000) / 60;
                    }

                    longestSermon = longestSermon != null
                        ? longestSermon.duration < track.duration ? track : longestSermon
                        : track;
                    shortestSermon = shortestSermon != null
                        ? shortestSermon.duration > track.duration ? track : shortestSermon
                        : track;
                }
            }

            function getTracks(href, callback) {
                var threeMonthsAgo = new Date();
                threeMonthsAgo.setMonth(threeMonthsAgo.getMonth() - 3);

                SC.get(href, { limit: 300, from: threeMonthsAgo.toISOString(), to: new Date().toISOString(), createdAtlimit: 300, linked_partitioning: true }, function (tracks) {
                    callback(tracks);
//                    if (!!(tracks) && tracks.length > 0) {
//                        vm.soundCloudSermons = tracks;
//                    }
                });


//                SC.get('/users/' + igchurch.constants.SOUND_CLOUD_USERID + '/tracks', { limit: 300, linked_partitioning: true }, function (tracks) {
//
//                    if (!!(tracks) && tracks.length > 0) {
//                        vm.soundCloudSermons = tracks;
//                    }
//                });
            }
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
