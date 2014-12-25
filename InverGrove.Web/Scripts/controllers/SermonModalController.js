(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonModalCtrl', SermonModalController);

    SermonModalController.$inject = [
        '$modalInstance',
        '$rootScope',
        'sermon',
        'SermonService'
    ];

    function SermonModalController($modalInstance, $rootScope, sermon, SermonService) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermon = sermon || {};
        vm.sermon.tags = vm.sermon.tags || '';
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

        activate();

        /*
         * Private declarations
         */
        function activate() {
        }

        function dismissModal() {
            $modalInstance.dismiss('cancel');
        }

        function addTag() {
            if (!(vm.newTag.trim())) {
                return;
            }

            var sermonTagsLength = vm.sermon.tags.split(',').length;

            vm.sermon.tags = vm.sermon.tags + (sermonTagsLength > 1 ? ',' : '') + vm.newTag.trim();
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

        function addSermon(sermon) {
            vm.busy = true;
            $rootScope.$broadcast('addSermon', sermon);
        }

        function editSermon(sermon) {
            vm.busy = true;
            $rootScope.$broadcast('editSermon', sermon);
        }

        function deleteSermon(sermon) {
            vm.busy = true;
            $rootScope.$broadcast('deleteSermon', sermon);
        }
    }
})();
