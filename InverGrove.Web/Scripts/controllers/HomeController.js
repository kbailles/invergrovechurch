'use strict';

/* Controllers */

app.controller('HomeCtrl', ['$scope', 'locationFactory', function($scope, locationFactory) {
    $scope.churchLocation = locationFactory;
}]);