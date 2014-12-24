(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('btnLoading', btnLoading);

    btnLoading.$inject = [
    ];

    function btnLoading() {
        var directive = {
            link: link,
            restrict: 'A'
        }
        return directive;

        function link(scope, element, attrs) {
            scope.$watch(
                function () {
                    return scope.$eval(attrs.btnLoading);
                },
                function (value) {
                    if (value) {
                        if (!attrs.hasOwnProperty('ngDisabled')) {
                            element.addClass('disabled').attr('disabled', 'disabled');
                        }

                        element.data('resetText', element.html());
                        element.html(element.data('loading-text'));
                    } else {
                        if (!attrs.hasOwnProperty('ngDisabled')) {
                            element.removeClass('disabled').removeAttr('disabled');
                        }

                        element.html(element.data('resetText'));
                    }
                }
            );
        }
    }
})();