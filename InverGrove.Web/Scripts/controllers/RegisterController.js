(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('RegisterCtrl', RegisterController);

    function RegisterController() {
        var vm = this;

        vm.busy = false;
        vm.registerUser = registerUser;

        function registerUser() {
            vm.busy = true;
            $('form').submit();
        }
    }
})();