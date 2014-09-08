'use strict';

/* Controllers */

app.controller('ContactCtrl', ['$scope', 'locationFactory', function($scope, locationFactory) {
    $scope.churchLocation = locationFactory;
}]);