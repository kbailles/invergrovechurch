﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('RegisterCtrl', RegisterController);

    RegisterController.$inject = ['UserService','$scope'];

    function RegisterController(UserService,$scope) {
        var vm = this;

        vm.busy = false;
        vm.newUserObj = {};

        vm.createUser = createUser;

        //ui.bootstrap watches $scope object
        $scope.alerts = [];
        vm.closeAlert = closeAlert;
        vm.resetForm = resetForm;

        activate();

        function activate() {
            
        }

        function createUser() {

            // http://ambilykk.com/2014/06/15/angularjs-extracting-querystring/
            // http://stackoverflow.com/questions/11063673/whats-the-most-concise-way-to-read-query-parameters-in-angularjs
            var foo = $location.search()['code'];

            $scope.alerts.length = 0;

            $scope.$emit('loading-started');

            if (vm.newUserObj.password !== vm.newUserObj.passwordConfirm) {
                $scope.alerts.push({ type: 'warning', msg: 'Oops! Password and Confirm Password do not match.' });
                return;
            }

            UserService.registerUser(vm.newUserObj).then(function (response) {

                if (response.status === 200 /* Response status OK */) {
                    $scope.alerts.push({ type: 'success', msg: 'Account has been created!' });
                    vm.resetForm();
                } else {
                    $scope.alerts.push({ type: 'danger', msg: 'Ops! There was an error with setup. Please contact the site administrators!' });
                }

                $scope.$emit('loading-complete');
            },
           function (error) {
               $scope.alerts.push({ type: 'danger', msg: 'Ops! There was an error with setup. Please contact the site administrators!' });

               $scope.$emit('loading-complete');
           });

        }

        function resetForm() {
            vm.newUserObj = {};
            $scope.form.$setPristine();
        }

        function closeAlert(index) {
            //ui.bootstrap watches $scope object
            $scope.alerts.splice(index, 1);
        }
    }
})();