(function () {
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

        function activate() { }

        function createUser() {

            $scope.$emit('loading-started');
            $scope.alerts.length = 0;


            alert('you submitted the form for adding uid/pwd for this person as a new user!!');

            var foo = vm.newUserObj;
            $scope.$emit('loading-complete');

            UserService.registerNewUser(vm.contactObj).then(function (response) {

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