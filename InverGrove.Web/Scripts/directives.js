'use strict';

angular.module('igchurch.directives').directive('sticky', function () {
    return {
        restrict: 'A',
        link: function(scope, element) {
            $(element).sticky({ topSpacing: 0 });
        }
    };
});

angular.module('igchurch.directives').directive('gMap', function () {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            $(element).gMap(scope.$eval(attrs.ngModel));
        }
    };
});

angular.module('igchurch.directives').directive('revoslider', function () {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            $(element).revolution({
                delay:9000,
                startwidth:1170,
                startheight:370,
                hideThumbs:10
            });
        }
    };
});

angular.module('igchurch.directives').directive('scrollUp', function () {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            $(window).scroll(function () {
                if ($(this).scrollTop() > 100) {
                    $(element).fadeIn();
                } else {
                    $(element).fadeOut();
                }
            });

            $(element).click(function () {
                $('html, body').animate({
                    scrollTop: 0
                }, 600);
                return false;
            });
        }
    };
});

