﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AccountCtrl', AccountController);

    AccountController.$inject = [
        '$scope'
    ];

    function AccountController($scope) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.busy = false;
        vm.login = login;

        //ui.bootstrap watches $scope object
        $scope.alerts = [];

        /*
         * Private declarations
         */
        function login() {
            vm.busy = true;
            $('form').submit();
        }
    }
})();