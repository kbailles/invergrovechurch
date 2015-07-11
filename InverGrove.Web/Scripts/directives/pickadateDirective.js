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
            restrict: 'A',
            scope: {
                date: "="
            }
        }
        return directive;

        function link(scope, element, attrs) {
            element.pickadate({
                format: 'mmmm d yyyy',
                onStart: function () {
                    var date = scope.date ? moment(scope.date) : moment();
                    this.setDate(date.year(), date.month() + 1, date.date());
                }
            });
        } // .link
    }
})();