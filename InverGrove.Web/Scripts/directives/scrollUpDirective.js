(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.directives')
        .directive('igScrollUp', igScrollUp);

    igScrollUp.$inject = ['$window'];

    function igScrollUp(window) {
        var directive = {
            link: link,
            restrict: 'A'
        }
        return directive;

        function link(scope, element, attrs) {
            var $window = angular.element(window);

            element.click(scrollUp);
            $window.bind('scroll', hideShowScrollElement);

            function hideShowScrollElement() {
                if ($window.scrollTop() > 100) {
                    element.fadeIn();
                } else {
                    element.fadeOut();
                }
            } // .hideShowScrollElement

            function scrollUp(e) {
                e.preventDefault();

                $('html, body').animate({
                    scrollTop: 0
                }, 600);
            } // .scrollUp
        } // .link
    }
})();