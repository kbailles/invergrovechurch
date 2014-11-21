(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.filters')
        .filter('selectedTags', selectedTags);

    function selectedTags() {
        return function (sermons, tags) {
            if (tags.length == 0) {
                return sermons;
            }

            return sermons.filter(function (sermon) {
                for (var i in tags) {
                    if (sermon.tags.indexOf(tags[i]) > -1) {
                        return true;
                    }
                }

                return false;
            });
        };
    }
})();