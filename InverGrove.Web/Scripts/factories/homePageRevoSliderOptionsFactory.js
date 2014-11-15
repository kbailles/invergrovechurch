(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.factories')
        .factory('homePageRevoSliderOptions', homePageRevoSliderOptions);

    function homePageRevoSliderOptions() {
        var sliderSetup = {
            delay: 9000,
            startwidth: 1170,
            startheight: 370,
            hideThumbs: 10
        };
        return sliderSetup;
    }
})();