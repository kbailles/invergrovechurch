(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonModalCtrl', SermonModalController);

    SermonModalController.$inject = [
        '$modalInstance',
        '$scope',
        'sermon'
    ];

    function SermonModalController($modalInstance, $scope, sermon) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermon = sermon;
        vm.$modalInstance = $modalInstance;
        vm.$scope = $scope;

        vm.dismissModal = dismissModal;
        vm.addSermon = addSermon;
        vm.editSermon = editSermon;

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }

        function addSermon() {
            alert('Add sermon');
        }

        function editSermon() {
            alert('Edit sermon');
        }
    }
})();
