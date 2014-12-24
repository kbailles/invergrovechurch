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
        vm.deleteSermon = deleteSermon;
        vm.showDeleteLoading = false;

        vm.$modalInstance = null;

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
            vm.$modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Add',
                resolve: {
                    sermon: function() {
                        return {};
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
                    }
                }
            });
        }

        function deleteSermon(sermon) {
            vm.showDeleteLoading = true;
            //SermonService.delete(sermon).then(function (response) {

            //},
            //function (error) {

            //});
        }
    }
})();
