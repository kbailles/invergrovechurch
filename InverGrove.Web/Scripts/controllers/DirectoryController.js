(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('DirectoryCtrl', DirectoryController);

    function DirectoryController() {
        var vm = this;

        vm.members = members;
    }
})();