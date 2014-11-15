/// <reference path="../dependencies/core.references.js" />

/*
 * File being tested
 */
/// <reference path="../../Scripts/controllers/BaseController.js" />

'use strict';

describe('BaseCtrl', function () {
    //setup
    var appName = igchurch.constants.APP_NAME;
    beforeEach(module(appName + '.controllers'));

    it('should be defined', inject(function($controller) {
        //arrange
        var baseCtrl = $controller('BaseCtrl', { $scope: {} });

        //act

        //assert
        expect(baseCtrl).toBeDefined();
    }));

    //...
});