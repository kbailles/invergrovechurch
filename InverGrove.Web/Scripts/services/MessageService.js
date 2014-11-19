(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.services')
        .service('MessageService', MessageService);

    MessageService.$inject = ['$http'];

    function MessageService($http) {

    }
})();
