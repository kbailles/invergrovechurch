(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName, [
        /**
         * Angular modules
         */
        'ngRoute',
        /*
         * Bootstrap modules
         */
        'ui.bootstrap',
        /**
         * Our reusable app code modules
         */
        appName + '.controllers',
        appName + '.directives',
        appName + '.factories',
        appName + '.filters',
        appName + '.services'
    ]);
})();