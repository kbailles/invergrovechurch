'use strict';

/* Directives */

var app = angular.module('app.directives', []);

app.directive('appVersion', ['version', function(version) {
    return function(scope, elm, attrs) {
      elm.text(version);
    };
  }]);
app.directive('sticky', function() {
    return {
        restrict: 'A',
        link: function(scope, element) {
            $(element).sticky({ topSpacing: 0 });
        }
    };
  });
app.directive('gMap', function() {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            $(element).gMap(scope.$eval(attrs.ngModel));
        }
    };
});
app.directive('flexslider', function() {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            $(element).flexslider({ animation: 'slide', controlNav: false });
        }
    };
});
app.directive('revoslider', function() {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            $(element).revolution({
                delay:9000,
                startwidth:1170,
                startheight:348,
                hideThumbs:10
            });
        }
    };
});
app.directive('scrollUp', function() {
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

