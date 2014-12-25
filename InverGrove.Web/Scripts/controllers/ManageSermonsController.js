(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [
        'SermonService',
        'sermons',
        '$scope',
        '$modal'
    ];

    function ManageSermonsController(SermonService, sermons, $scope, $modal) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.SermonService = SermonService;
        vm.sermons = sermons.data;

        vm.openAddSermonModal = openAddSermonModal;
        vm.openEditSermonModal = openEditSermonModal;
        vm.openDeleteSermonModal = openDeleteSermonModal;

        vm.$modalInstance = null;

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

        function openDeleteSermonModal(sermon) {
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
            vm.SermonService.update(sermon).then(function (response) {

            },
            function (error) {

            })
            .finally(function() {
                vm.$modalInstance.dismiss('cancel');
            });
        });

        $scope.$on('deleteSermon', function (event, sermon) {
            vm.SermonService.delete(sermon).then(function (response) {
                var index = vm.sermons.indexOf(sermon);

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
