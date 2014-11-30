(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonsCtrl', SermonsController);

    SermonsController.$inject = ['$window'];

    function SermonsController($window) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermonDetail = sermonDetail;

        vm.titleFilter = '';
        vm.speakerFilter = '';
        vm.tagsFilter = [];

        vm.toggleTagChecked = toggleTagChecked;

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function sermonDetail(sermonId) {
            $window.location.href = 'SermonDetail?sermonId=' + sermonId;
        }

        function toggleTagChecked(value) {
            var index = vm.tagsFilter.indexOf(value);

            if (index > -1) {
                vm.tagsFilter.splice(index, 1);
            } else {
                vm.tagsFilter.push(value);
            }
        }
    }
})();
