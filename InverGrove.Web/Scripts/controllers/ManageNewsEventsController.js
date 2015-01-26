(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageNewsEventsCtrl', ManageNewsEventsController);

    ManageNewsEventsController.$inject = [
        'newsEvents',
        '$scope',
        '$modal'
    ];

    function ManageNewsEventsController(newsEvents, $scope, $modal) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.newsEvents = newsEvents.data;

        vm.openAddNewsEventModal = openAddNewsEventModal;
        vm.openEditNewsEventModal = openEditNewsEventModal;
        vm.openDeleteNewsEventModal = openDeleteNewsEventModal;

        vm.$modalInstance = null;

        function openAddNewsEventModal() {
            vm.$modalInstance = $modal.open({
                controller: 'NewsEventsModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/NewsEvents/Add',
                resolve: {
                    newsEvent: function () {
                        return {};
                    }
                }
            });
        }

        function openEditNewsEventModal(newsEvent) {
            vm.$modalInstance = $modal.open({
                controller: 'NewsEventsModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/NewsEvents/Edit',
                resolve: {
                    newsEvent: function () {
                        return newsEvent;
                    }
                }
            });
        }

        function openDeleteNewsEventModal(newsEvent) {
            vm.$modalInstance = $modal.open({
                controller: 'NewsEventsModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/NewsEvents/Delete',
                resolve: {
                    newsEvent: function () {
                        return newsEvent;
                    }
                }
            });
        }
    }
})();
