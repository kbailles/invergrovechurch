//'use strict';

//angular.module('igchurch.controllers')

//    .controller('ContactCtrl', ['$scope', '$http', 'LocationFactory', function ($scope, $http, locationFactory) {
//        $scope.churchLocation = locationFactory;
//        $scope.working = false;

//        $scope.basicContact = function (contact) {
//            $scope.working = true;
//            debugger;
//            $http.post('/api/contactform', contact).success(function (data, status, headers, config) {
//                $scope.working = false;
//            }).error(function (data, status, headers, config) {

//                $scope.title = "Oops... something went wrong";
//                $scope.working = false;
//            });

//        };
//    }]);

(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ContactCtrl', ContactController);

    ContactController.$inject = ['$http'];

    function ContactController($http) {
        var vm = this;

        vm.basicContact = function () {
            $http.post('/api/contactform', contact).success(function(data, status, headers, config) {
                //$scope.working = false;
            }).error(function(data, status, headers, config) {
                //$scope.title = "Oops... something went wrong";
                //$scope.working = false;
            });
        }
    }
})();