(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.factories')
        .factory('gMapChurchLocation', gMapChurchLocation);

    function gMapChurchLocation() {
        var location = {
            address: 'Inver Grove Church of Christ',
            zoom: 13,
            markers: [
                {
                    latitude: 44.822361,
                    longitude: -93.058097
                }
            ]
        };
        return location;
    }
})();
