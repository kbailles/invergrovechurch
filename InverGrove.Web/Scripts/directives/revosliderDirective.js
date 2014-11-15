﻿(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('igRevoslider', igRevoslider);

    igRevoslider.$inject = ['homePageRevoSliderOptions'];

    function igRevoslider(homePageRevoSliderOptions) {
        var directive = {
            link: link,
            restrict: 'A'
        }
        return directive;

        function link(scope, element, attrs) {
            element.revolution(homePageRevoSliderOptions);
        }
    }
})();