(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [
        'sermons',
        '$modal',
        '$scope'
    ];

    function ManageSermonsController(sermons, $modal, $scope) {
        var vm = this;

        vm.sermons = sermons.data;

        vm.openAddSermonModal = openAddSermonModal;
        vm.addSermon = addSermon;
        vm.editSermon = editSermon;
        vm.deleteSermon = deleteSermon;

        vm.tryThis = tryThis;

        $scope.sermonToEdit = {};

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
                templateUrl: '/Member/Sermon/Add',
                resolve: {
                }
            });
        }

        function tryThis() {
            alert('Yep!');
        }

        function addSermon() {
            
        }

        function editSermon(sermon) {
            $scope.sermonToEdit = sermon;

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

        function deleteSermon() {
            
        }
    }
})();
