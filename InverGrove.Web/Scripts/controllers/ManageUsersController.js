﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ManageUsersCtrl', ManageUsersController);

    ManageUsersController.$inject = [
        '$modal',
        '$scope'
    ];

    function ManageUsersController($modal, $scope) {
        var vm = this;

        vm.openAddSermonModal = openAddSermonModal;
        vm.addSermon = addSermon;
        vm.editSermon = editSermon;
        vm.deleteSermon = deleteSermon;

        /*
         * Public declarations
         */

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function openAddSermonModal() {
            var modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Add',
                resolve: {
                    sermon: function () {
                        return {};
                    }
                }
            });

            modalInstance.result.then(function () {
            }, function () {
                //Clear out modal form here...
            });
        }

        function addSermon() {

        }

        function editSermon(sermon) {
            var modalInstance = $modal.open({
                controller: 'SermonModalCtrl',
                controllerAs: 'modalCtrl',
                templateUrl: '/Member/Sermon/Edit',
                resolve: {
                    sermon: function () {
                        return sermon;
                    }
                }
            });

            modalInstance.result.then(function () {
            }, function () {
                //Clear out modal form here...
            });
        }

        function deleteSermon() {

        }
    }
})();