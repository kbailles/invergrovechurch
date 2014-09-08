'use strict';

/* Controllers */

var app = angular.module('app.controllers', ['ui.bootstrap']);

app.controller('AppCtrl', ['$scope' ,'$location', function($scope, $location) {
        $scope.navbarCollapsed = true;

        $scope.isActive = function(route) {
            return route === $location.path();
        }
        $scope.go = function(path) {
            $location.path(path);
        }
    }]);