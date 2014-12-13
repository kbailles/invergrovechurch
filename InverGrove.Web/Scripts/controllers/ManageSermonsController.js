(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageSermonsCtrl', ManageSermonsController);

    ManageSermonsController.$inject = [];

    function ManageSermonsController() {
        var vm = this;

        /*
         * Public declarations
         */

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }
    }
})();
