﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.factories')
        .factory('phoneNumberHelper', phoneNumberHelper);

    function phoneNumberHelper() {

        function formatPhoneNumber(number) {
            var val = number.replace(/[^0-9]+/g, '');

            if (!val) {
                return '';
            }

            var area = val.substring(0, 3);
            var front = val.substring(3, 6);
            var end = val.substring(6, 10);

            if (front) {
                val = ("(" + area + ") " + front);
            }
            if (end) {
                val += ("-" + end);
            }

            return val;
        }

        return {
            formatPhoneNumber: formatPhoneNumber
        }
    }
})();