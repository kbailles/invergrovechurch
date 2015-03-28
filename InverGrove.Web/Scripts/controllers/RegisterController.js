(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('RegisterCtrl', RegisterController);

    RegisterController.$inject = [
        '$scope'
    ];

    function RegisterController($scope) {
        var vm = this;

        vm.header2 = 'Register As New User';
        vm.busy = false;

        //ui.bootstrap watches $scope object
        $scope.alerts = [];

        activate(); // set this thing off

        function activate()
        {
            alert('the RegisterController.js just fired off!');
        }
    }
})();