(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('igPickadate', igPickadate);

    igPickadate.$inject = [
    ];

    function igPickadate() {
        var directive = {
            link: link,
            restrict: 'A'
        }
        return directive;

        function link(scope, element, attrs) {
            element.pickadate({
                onStart: function () {
                    var date = new Date();
                    this.setDate(date.getFullYear(), date.getMonth() + 1, date.getDate());
                }
            });
        } // .link
    }
})();