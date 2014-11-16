(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonCtrl', SermonController);

    SermonController.$inject = ['SermonService'];

    function SermonController(SermonService) {
        var vm = this,
            sermons = [];

        activate();

        function activate() {
            return getSermons();
        }

        function getSermons() {
            return SermonService.getSermons().then(function (data) {
                vm.sermons = data.data;
            });
        }

        function viewSermon(sermonId) {
            $window.location.href = 'SermonDetail?sermonId=' + sermonId;
        }
    }
})();