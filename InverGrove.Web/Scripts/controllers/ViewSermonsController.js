(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('ViewSermonsCtrl', ViewSermonsController);

    ViewSermonsController.$inject = [
        '$window'
    ];

    function ViewSermonsController($window) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.filteredSpeakers = filteredSpeakers;
        vm.filteredTags = filteredTags;
        vm.sermonDetail = sermonDetail;

        vm.sermons = sermons;

        vm.titleFilter = '';
        vm.speakerFilter = '';
        vm.tagsFilter = [];

        vm.toggleTagChecked = toggleTagChecked;


        function filteredSpeakers() {
            return _.mapValues(_.groupBy(vm.sermons, 'speaker'), function (r) { return r.length; });
        }

        function filteredTags() {
            var sermonTags =
                _.chain(vm.sermons)
                 .pluck('tags')
                 .flatten()
                 .invoke('split', ',')
                 .flatten()
                 .invoke('trim')
                 .value();

            return _.mapValues(_.groupBy(sermonTags), function (r) { return r.length; });
        }

        function sermonDetail(sermonId) {
            $window.location.href = 'SermonDetail?sermonId=' + sermonId;
        }

        function toggleTagChecked(value) {
            var index = vm.tagsFilter.indexOf(value);

            if (index > -1) {
                vm.tagsFilter.splice(index, 1);
            } else {
                vm.tagsFilter.push(value);
            }
        }
    }
})();
