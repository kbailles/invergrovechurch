(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ContactCtrl', ContactController);

    ContactController.$inject = [
        'MessageService',
        '$scope'
    ];

    function ContactController(MessageService, $scope) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.closeAlert = closeAlert;
        vm.contactUsObj = {};
        vm.sendContactUsMessage = sendContactUsMessage;
        vm.resetContactUsForm = resetContactUsForm;

        //ui.bootstrap watches $scope object
        $scope.alerts = [];

        function sendContactUsMessage() {
            $scope.alerts.length = 0;
            vm.busy = true;

            MessageService.sendMessage(vm.contactUsObj).then(function (response) {

                if (response.status === 200 /* Response status OK */) {
                    $scope.alerts.push({ type: 'success', msg: 'Message successfully sent!' });
                    vm.resetContactUsForm();
                } else {
                    $scope.alerts.push({ type: 'danger', msg: 'Ops! We were unable to send your message!' });
                }

                vm.busy = false;
            },
            function (error) {
                $scope.alerts.push({ type: 'danger', msg: 'Ops! We were unable to send your message!' });

                vm.busy = false;
            });
        }

        function resetContactUsForm() {
            vm.contactUsObj = {};
            vm.humanTest = '';
            $scope.form.$setPristine();
        }

        function closeAlert(index) {
            //ui.bootstrap watches $scope object
            $scope.alerts.splice(index, 1);
        }
    }
})();