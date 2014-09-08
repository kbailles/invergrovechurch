'use strict';

/* jasmine specs for controllers go here */

describe('controllers', function(){
  beforeEach(module('myApp.controllers'));


  it('should ....', inject(function($controller) {
    //spec body
    var myCtrl1 = $controller('HomeCtrl', { $scope: {} });
    expect(myCtrl1).toBeDefined();
  }));
});
