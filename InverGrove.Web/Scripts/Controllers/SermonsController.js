(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonsCtrl', SermonsController);

    SermonsController.$inject = ['SermonService', '$window'];

    function SermonsController(SermonService, $window) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermons = [];
        vm.sermonDetail = sermonDetail;
        vm.titleFilter = '';
        vm.filteredSpeakers = filteredSpeakers;
        vm.filteredTags = filteredTags;

        activate();

        /*
         * Private declarations
         */
        function activate() {
            return getSermons();
        }

        function getSermons() {
            return SermonService.getSermons().then(function (data) {
                vm.sermons = data.data;
            });
        }

        function sermonDetail(sermonId) {
            $window.location.href = 'SermonDetail?sermonId=' + sermonId;
        }

        function filteredSpeakers() {
            return _.mapValues(_.groupBy(vm.sermons, 'speaker'), function (r) { return r.length; });
        }

        function filteredTags() {
            //TODO: REFACTOR THIS IMMEDIATELY
            return _.mapValues(_.groupBy(_.chain(vm.sermons).pluck('tags').flatten().invoke('split', ',').flatten().invoke('trim').value()), function (r) { return r.length; });
        }
    }
})();