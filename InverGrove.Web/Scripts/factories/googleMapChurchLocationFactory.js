(function() {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.factories')
        .factory('gMapChurchLocation', gMapChurchLocation);

    function gMapChurchLocation() {
        var location = {
            controls: {
                panControl: true,
                zoomControl: true,
                mapTypeControl: false,
                scaleControl: true,
                streetViewControl: true,
                overviewMapControl: true
            },
            address: '8777 Courthouse Blvd, Inver Grove Heights, MN 55077, United States',
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
