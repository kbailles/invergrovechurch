(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('DirectoryCtrl', DirectoryController);

    DirectoryController.$inject = [
    ];

    function DirectoryController() {
        var vm = this;

        /*
         * Public declarations
         */

        activate();

        /*
         * Private declarations
         */
    }
})();