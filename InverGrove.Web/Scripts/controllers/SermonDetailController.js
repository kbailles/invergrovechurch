﻿(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonDetailCtrl', SermonDetailController);

    SermonDetailController.$inject = ['SermonService', '$routeParams', '$sce'];

    function SermonDetailController(SermonService, $routeParams, $sce) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermon = {};
        vm.soundCloudSermonSrc = '';

        activate();

        /*
         * Private declarations
         */
        function activate() {
            var sermonId = $routeParams.id;

            return getSermon(sermonId);
        }

        function getSermon(sermonId) {
            return SermonService.getSermon(sermonId).then(function (data) {
                vm.sermon = data.data;
                vm.soundCloudSermonSrc = $sce.trustAsResourceUrl('https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/' + vm.sermon.soundCloudId + '&amp;color=2eaef0&amp;auto_play=false&amp;hide_related=false&amp;show_comments=false&amp;show_user=true&amp;show_reposts=false&amp;show_artwork=false');
            });
        }
    }
})();