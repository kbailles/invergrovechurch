(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.controllers')
        .controller('SermonModalCtrl', SermonModalController);

    SermonModalController.$inject = [
        '$modalInstance',
        'sermon'
    ];

    function SermonModalController($modalInstance, sermon) {
        var vm = this;

        /*
         * Public declarations
         */
        vm.sermon = sermon;
        vm.sermon.tags = vm.sermon.tags || '';
        vm.$modalInstance = $modalInstance;

        vm.newTag = '';

        vm.dismissModal = dismissModal;
        vm.addTag = addTag;
        vm.removeTag = removeTag;
        vm.tagsAsArray = tagsAsArray;
        vm.addSermon = addSermon;
        vm.editSermon = editSermon;

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
            alert('Add sermon');
        }

        function editSermon() {
            alert('Edit sermon');
        }
    }
})();
