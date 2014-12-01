(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AccountCtrl', AccountController);

    AccountController.$inject = ['$scope', '$http'];

    function AccountController($scope, $http) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.loginCredentials = {};

        //ui.bootstrap watches $scope object
        $scope.alerts = [];

        /*
         * Private declarations
         */
    }
})();