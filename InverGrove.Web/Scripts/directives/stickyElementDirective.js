﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('sticky', sticky);

    function sticky() {
        var directive = {
            link: link,
            restrict: 'A'
        }
        return directive;

        function link(scope, element, attrs) {
            element.sticky({ topSpacing: 0 });
        }
    }
})();