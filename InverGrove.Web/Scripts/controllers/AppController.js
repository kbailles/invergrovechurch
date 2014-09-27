'use strict';

angular.module('app.controllers')

    .controller('AppCtrl', ['$scope', '$location', function ($scope, $location) {
        $scope.navbarCollapsed = true;

        $scope.isActive = function (route) {
            return !route ? '/' === $location.path() : $location.absUrl().indexOf(route) > -1;
        }
        $scope.go = function (path) {
            window.location.href = path;
        }
    }]);