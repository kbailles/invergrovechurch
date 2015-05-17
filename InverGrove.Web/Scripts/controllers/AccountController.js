(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AccountCtrl', AccountController);

    function AccountController() {
        var vm = this;

        vm.busy = false;
        vm.login = login;

        function login() {
            vm.busy = true;
            $('form').submit();
        }
    }
})();