(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [
        'SermonService',
        'sermons',
        '$modal'
    ];

    function ManageSermonsController(SermonService, sermons, $modal) {
        var vm = this;

        vm.sermons = sermons.data;

        vm.openAddSermonModal = openAddSermonModal;
        vm.openEditSermonModal = openEditSermonModal;
        vm.addSermon = addSermon;
        vm.editSermon = editSermon;
        vm.deleteSermon = deleteSermon;

        /*
         * Public declarations
         */

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function openAddSermonModal() {
            var modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Add',
                resolve: {
                    sermon: function() {
                        return {};
                    }
                }
            });

            modalInstance.result.then(function () {
            }, function () {
                //Clear out modal form here...
            });
        }

        function addSermon(sermon) {
            SermonService.add(sermon).then(function(response) {

            },
            function(error) {

            });
        }

        function editSermon(sermon) {
            SermonService.sermon(sermon).then(function (response) {

            },
            function (error) {

            });
        }

        function openEditSermonModal(sermon) {
            var modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Edit',
                resolve: {
                    sermon: function() {
                        return sermon;
                    }
                }
            });

            modalInstance.result.then(function () {
            }, function () {
                //Clear out modal form here...
            });
        }

        function deleteSermon(sermon) {
            SermonService.delete(sermon).then(function (response) {

            },
            function (error) {

            });
        }
    }
})();
