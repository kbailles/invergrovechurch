(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ContactCtrl', ContactController);

    ContactController.$inject = ['MessageService', '$route'];

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
            MessageService.sendMessage(vm.contactUsObj).then(function (success) {

                if (success) {
                    //For now reload the page when we send a contact us message...
                    $route.reload();
                } else {

                }
            });
        }
    }
})();