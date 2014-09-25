'use strict';

angular.module('app.controllers')
    
    .controller('ContactCtrl', ['$scope', 'LocationFactory', function ($scope, locationFactory) {
        $scope.churchLocation = locationFactory;
    }]);