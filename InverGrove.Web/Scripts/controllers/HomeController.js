(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('HomeCtrl', HomeController);

    function HomeController() {
        var vm = this;
    }
})();