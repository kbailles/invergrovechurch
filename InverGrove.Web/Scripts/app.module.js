namespace('igchurch.constants').APP_NAME = 'igchurch';

(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName, [
        /**
         * Angular modules
         */
        'ngRoute',
        /**
         * Our reusable app code modules
         */
        appName + '.controllers',
        appName + '.directives',
        appName + '.factories',
        appName + '.services'
    ]);
})();