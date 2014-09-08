'use strict';

/* Factories */

var app = angular.module('app.services', []);

app.factory('locationFactory', function() {
    var churchLocation = {
        address: 'Inver Grove Church of Christ',
        zoom: 13,
        markers: [{
            latitude: 44.822361,
            longitude: -93.058097
        }]
    }

    return churchLocation;
});