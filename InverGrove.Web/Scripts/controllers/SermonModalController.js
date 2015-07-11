(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonModalCtrl', SermonModalController);

    SermonModalController.$inject = [
        '$modalInstance',
        '$window',
        'sermon',
        'soundCloudSermons',
        'SermonService'
    ];

    function SermonModalController($modalInstance, $window, sermon, soundCloudSermons, SermonService) {
        var vm = this;

        vm.sermon = angular.copy(sermon) || {};
        vm.sermon.tags = vm.sermon.tags || '';
        vm.soundCloudSermons = soundCloudSermons || [];
        vm.SermonService = SermonService;
        vm.$modalInstance = $modalInstance;

        vm.newTag = '';
        vm.busy = false;

        vm.dismissModal = dismissModal;
        vm.addTag = addTag;
        vm.removeTag = removeTag;
        vm.tagsAsArray = tagsAsArray;
        vm.addSermon = addSermon;
        vm.editSermon = editSermon;
        vm.deleteSermon = deleteSermon;

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }

        function addTag() {
            if (!(vm.newTag.trim())) {
                return;
            }

            var sermonTagsLength = !!vm.sermon.tags ? vm.sermon.tags.split(',').length : 0;

            vm.sermon.tags = vm.sermon.tags + (sermonTagsLength > 0 ? ',' : '') + vm.newTag.trim();
            vm.newTag = '';
        }

        function removeTag(index) {
            var tags = vm.sermon.tags.split(',');

            tags.splice(index, 1);
            vm.sermon.tags = tags.join(',');
        }

        function tagsAsArray() {
            return vm.sermon.tags.split(',').filter(Boolean);
        }

        function addSermon() {
            vm.busy = true;

            vm.SermonService.add(vm.sermon).then(function () {
                $window.location.reload();
            });
        }

        function editSermon() {
            vm.busy = true;

            vm.SermonService.update(vm.sermon).then(function () {
                $window.location.reload();
            });
        }

        function deleteSermon() {
            vm.busy = true;

            vm.SermonService.delete(vm.sermon).then(function () {
                $window.location.reload();
            });
        }
    }
})();
