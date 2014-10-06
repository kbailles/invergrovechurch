'use strict';

angular.module('app.controllers')

    .controller('SermonCtrl', ['$scope', '$http', function ($scope, $http) {
        var fakeData = [
            {
                title: 'test1'
            },
            {
                title: 'test2'
            }
        ];

        $scope.sermons = fakeData;
    }]);