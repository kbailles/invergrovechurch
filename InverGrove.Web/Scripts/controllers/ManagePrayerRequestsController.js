(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManagePrayerRequestsCtrl', ManagePrayerRequestsController);

    ManagePrayerRequestsController.$inject = [
        'prayerRequests',
        'PrayerRequestService',
        '$scope',
        '$modal'
    ];

    function ManagePrayerRequestsController(prayerRequests, PrayerRequestService, $scope, $modal) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.prayerRequests = prayerRequests.data;
        vm.PrayerRequestService = PrayerRequestService;

        vm.openAddPrayerRequestModal = openAddPrayerRequestModal;
        vm.openEditPrayerRequestModal = openEditPrayerRequestModal;
        vm.openDeletePrayerRequestModal = openDeletePrayerRequestModal;

        vm.$modalInstance = null;

        function openAddPrayerRequestModal() {
            vm.$modalInstance = $modal.open({
                controller: 'PrayerRequestModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/PrayerRequest/Add',
                resolve: {
                    sermon: function () {
                        return {};
                    }
                }
            });
        }

        function openEditPrayerRequestModal(prayerRequest) {
            vm.$modalInstance = $modal.open({
                controller: 'PrayerRequestModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/PrayerRequest/Edit',
                resolve: {
                    sermon: function () {
                        return prayerRequest;
                    }
                }
            });
        }

        function openDeletePrayerRequestModal(prayerRequest) {
            vm.$modalInstance = $modal.open({
                controller: 'PrayerRequestModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/PrayerRequest/Delete',
                resolve: {
                    sermon: function () {
                        return prayerRequest;
                    }
                }
            });
        }
    }
})();
