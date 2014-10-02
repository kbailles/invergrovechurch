'use strict';

angular.module('app.controllers')

    .controller('ContactCtrl', ['$scope', '$http', 'LocationFactory', function ($scope, $http, locationFactory) {
        $scope.churchLocation = locationFactory;
        $scope.working = false;

        $scope.basicContact = function (contact) {
            $scope.working = true;
            debugger;
            $http.post('/api/contactform', contact).success(function (data, status, headers, config) {
                $scope.working = false;
            }).error(function (data, status, headers, config) {

                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });

        };
}]);