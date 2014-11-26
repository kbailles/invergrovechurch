(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('BaseCtrl', BaseController);

    BaseController.$inject = [
        '$scope',
        '$location',
        'SermonService'
    ];

    function BaseController($scope, $location, SermonService) {
        var base = this;

        /*
         * Public declarations
         */
        base.navBarCollapsed = true;
        base.toggleNavBar = toggleNabBar;

        base.isRouteActive = isRouteActive;
        base.goToPath = goToPath;

        base.filteredSpeakers = filteredSpeakers;
        base.filteredTags = filteredTags;

        base.sermons = [];

        activate();

        /*
         * Private declarations
         */
        function activate() {
            return getSermons();
        }

        /*
         * Prefetch sermons to make Sermons page load faster...
         */
        function getSermons() {
            return SermonService.getSermons().then(function (data) {
                base.sermons = data.data;
            });
        }

        function filteredSpeakers() {
            return _.mapValues(_.groupBy(base.sermons, 'speaker'), function (r) { return r.length; });
        }

        function filteredTags() {
            return _.mapValues(_.groupBy(_.chain(base.sermons).pluck('tags').flatten().invoke('split', ',').flatten().invoke('trim').value()), function (r) { return r.length; });
        }

        function goToPath(path) {
            window.location.href = path;
        }

        function isRouteActive(route) {
            return !route ? '/' === $location.path() : $location.absUrl().indexOf(route) > -1;
        }

        function toggleNabBar() {
            base.navBarCollapsed = !base.navBarCollapsed;
        }
    }
})();