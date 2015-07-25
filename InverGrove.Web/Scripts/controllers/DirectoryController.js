(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('DirectoryCtrl', DirectoryController);

    DirectoryController.$inject = [
        'phoneNumberHelper'
    ];

    function DirectoryController(phoneNumberHelper) {
        var vm = this;

        vm.phoneNumberHelper = phoneNumberHelper;

        vm.nameFilter = '';
        vm.members = members;
    }
})();