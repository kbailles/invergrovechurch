(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonsCtrl', SermonsController);

    SermonsController.$inject = ['SermonService', '$scope', '$window'];

    function SermonsController(SermonService, $scope, $window) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermons = [];
        vm.sermonDetail = sermonDetail;

        vm.titleFilter = '';
        vm.speakerFilter = '';
        vm.tagsFilter = [];

        vm.filteredSpeakers = filteredSpeakers;
        vm.filteredTags = filteredTags;

        vm.toggleTagChecked = toggleTagChecked;

        activate();

        /*
         * Private declarations
         */
        function activate() {
            return getSermons();
        }

        function getSermons() {
            $scope.$emit('loading-started');

            return SermonService.getSermons().then(function (data) {
                vm.sermons = data.data;
                $scope.$emit('loading-complete');
            });
        }

        function sermonDetail(sermonId) {
            $window.location.href = 'SermonDetail?sermonId=' + sermonId;
        }

        function filteredSpeakers() {
            return _.mapValues(_.groupBy(vm.sermons, 'speaker'), function (r) { return r.length; });
        }

        function filteredTags() {
            return _.mapValues(_.groupBy(_.chain(vm.sermons).pluck('tags').flatten().invoke('split', ',').flatten().invoke('trim').value()), function (r) { return r.length; });
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
