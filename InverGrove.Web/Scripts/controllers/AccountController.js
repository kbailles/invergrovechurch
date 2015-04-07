(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('AccountCtrl', AccountController);

    AccountController.$inject = [
        '$scope'
    ];

    function AccountController($scope) {
        var vm = this;

        vm.busy = false;
        vm.login = login;

        //ui.bootstrap watches $scope object
        $scope.alerts = [];
        vm.closeAlert = closeAlert;
        vm.resetForm = resetForm;

        activate();

        function activate() { }

        function login() {
            vm.busy = true;
            $('form').submit();
        }


        function resetForm() {
            $scope.form.$setPristine();
        }

        function closeAlert(index) {
            //ui.bootstrap watches $scope object
            $scope.alerts.splice(index, 1);
        }
    }
})();