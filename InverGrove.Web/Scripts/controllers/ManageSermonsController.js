(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [
        'sermons',
        '$modal'
    ];

    function ManageSermonsController(sermons, $modal) {
        var vm = this;

        vm.sermons = sermons.data;

        vm.openAddSermonModal = openAddSermonModal;

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

            //modalInstance.result.then(function (selectedItem) {
            //    $scope.selected = selectedItem;
            //}, function () {
            //    $log.info('Modal dismissed at: ' + new Date());
            //});
        }

        function addSermon() {
            
        }

        function editSermon() {
            
        }

        function deleteSermon() {
            
        }
    }
})();
