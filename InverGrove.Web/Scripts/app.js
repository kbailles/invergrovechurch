'use strict';

// Declare app level module which depends on filters, and services
angular.module('app', [
  'ngRoute',
  'app.filters',
  'app.services',
  'app.directives',
  'app.controllers'
]).
config(['$routeProvider', function($routeProvider) {
    $routeProvider.when('/', { templateUrl: '/Home/Home', controller: 'HomeCtrl' });
    $routeProvider.otherwise({ redirectTo: '/' });
}]);

angular.module('app.filters', []);
angular.module('app.services', []);
angular.module('app.directives', []);
angular.module('app.controllers', ['ui.bootstrap']);