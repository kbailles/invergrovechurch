

angular.module('igchurch.directives').directive('scrollUp', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
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