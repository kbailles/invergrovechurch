
/// <reference path="../dependencies/jasmine.js" />

/// <reference path="../../Components/angular/angular.js" />
/// <reference path="../../Components/angular-mocks/angular-mocks.js" />
/// <reference path="../../Components/angular-route/angular-route.js" />

/// <reference path="../../Components/angular-bootstrap/ui-bootstrap.js" />

/// <reference path="../../Scripts/app.js" />
/// <reference path="../../Scripts/common/services.js" />
/// <reference path="../../Scripts/common/factories.js" />
/// <reference path="../../Scripts/common/directives.js" />
/// <reference path="../../Scripts/common/filters.js" />

/// <reference path="../../Scripts/controllers/AppController.js" />
/// <reference path="../../Scripts/controllers/HomeController.js" />

'use strict';

/* jasmine specs for controllers go here */

describe('controllers', function(){
    beforeEach(module('app.controllers'));
    beforeEach(module('app.services'));

    it('should ....', inject(function ($controller) {
        //spec body
        var myCtrl1 = $controller('HomeCtrl', { $scope: {} });
        expect(myCtrl1).toBeDefined();
    }));
});