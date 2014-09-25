'use strict';

angular.module('app.controllers')

    .controller('HomeCtrl', ['$scope', 'LocationFactory', function ($scope, locationFactory) {
        $scope.churchLocation = locationFactory;
    }]);