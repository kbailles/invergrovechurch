/// <reference path="../dependencies/core.references.js" />

/*
 * File being tested
 */
/// <reference path="../../Scripts/controllers/HomeController.js" />

'use strict';

describe('HomeCtrl', function () {
    //setup
    var appName = igchurch.constants.APP_NAME;
    beforeEach(module(appName + '.controllers'));

    it('should be defined', inject(function ($controller) {
        //arrange
        var homeCtrl = $controller('HomeCtrl', { $scope: {} });

        //act

        //assert
        expect(homeCtrl).toBeDefined();
    }));

    //...
});