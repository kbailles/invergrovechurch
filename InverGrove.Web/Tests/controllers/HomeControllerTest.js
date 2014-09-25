
/// <reference path="../dependencies/jasmine.js" />

/// <reference path="../../Components/angular/angular.js" />
/// <reference path="../../Components/angular-mocks/angular-mocks.js" />
/// <reference path="../../Components/angular-route/angular-route.js" />

/// <reference path="../../Components/angular-bootstrap/ui-bootstrap.js" />

/// <reference path="../../Scripts/app.js" />
/// <reference path="../../Scripts/factories/LocationFactory.js" />
/// <reference path="../../Scripts/controllers/HomeController.js" />

'use strict';

describe('HomeCtrl', function(){
    beforeEach(module('app.services')); //HomeCtrl is dependent on LocationFactory which exists in app.services
    beforeEach(module('app.controllers'));

    it('should be defined', inject(function ($controller) {
        //spec body
        var myCtrl1 = $controller('HomeCtrl', { $scope: {} });
        expect(myCtrl1).toBeDefined();
    }));

    //...
});