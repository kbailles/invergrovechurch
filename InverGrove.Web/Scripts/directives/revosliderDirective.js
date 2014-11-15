
angular.module('igchurch.directives').directive('revoslider', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(element).revolution({
                delay: 9000,
                startwidth: 1170,
                startheight: 370,
                hideThumbs: 10
            });
        }
    };
});