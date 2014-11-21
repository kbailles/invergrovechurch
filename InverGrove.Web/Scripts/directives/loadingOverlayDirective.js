(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('loadingOverlay', loadingOverlay);

    function loadingOverlay() {
        var directive = {
            link: link,
            restrict: 'A',
            template: '<div class="loading" style="display: none;"></div>'
        }
        return directive;

        function link(scope, element, attrs) {
            scope.$on('loading-started', function () {
                element.children(":first").css({ 'display': '' });
            });

            scope.$on('loading-complete', function () {
                element.children(":first").css({ 'display': 'none' });
            });
        }
    }
})();