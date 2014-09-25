/// <reference path="../dependencies/jasmine.js" />

/// <reference path="../../Components/angular/angular.js" />
/// <reference path="../../Components/angular-mocks/angular-mocks.js" />
/// <reference path="../../Components/angular-route/angular-route.js" />

/// <reference path="../../Components/angular-bootstrap/ui-bootstrap.js" />

/// <reference path="../../Scripts/app.js" />
/// <reference path="../../Scripts/controllers/AppController.js" />

'use strict';

describe('AppCtrl', function () {
    beforeEach(module('app.controllers'));

    it('should be defined', inject(function ($controller) {
        //spec body
        var myCtrl1 = $controller('AppCtrl', { $scope: {} });
        expect(myCtrl1).toBeDefined();
    }));

    //...
});