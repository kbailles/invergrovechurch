(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('igGoogleMap', igGoogleMap);

    igGoogleMap.$inject = ['gMapChurchLocation'];

    function igGoogleMap(gMapChurchLocation) {
        var directive = {
            link: link,
            restrict: 'A'
        }
        return directive;

        function link(scope, element, attrs) {
            element.gMap(gMapChurchLocation);
        }
    }
})();