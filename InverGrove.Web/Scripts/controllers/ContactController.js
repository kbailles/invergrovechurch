(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ContactCtrl', ContactController);

    ContactController.$inject = ['MessageService', '$window'];

    function ContactController(MessageService, $route) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.contactUsObj = {};
        vm.sendContactUsMessage = sendContactUsMessage;

        activate();

        /*
         * Private declarations
         */
        function activate() {

        }

        function sendContactUsMessage() {
            MessageService.sendMessage(vm.contactUsObj).then(function (response) {

                if (response.status === 200 /* Response status OK */) {
                    //For now reload the page when we send a contact us message...
                    $window.location.reload();
                } else {

                }
            });
        }
    }
})();