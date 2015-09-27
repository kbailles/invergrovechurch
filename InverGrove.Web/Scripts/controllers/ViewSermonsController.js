(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ViewSermonsCtrl', ViewSermonsController);

    ViewSermonsController.$inject = [
        '$window'
    ];

    function ViewSermonsController($window) {
        var vm = this;
    }
})();
