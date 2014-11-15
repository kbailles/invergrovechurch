

angular.module('igchurch.directives').directive('sticky', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {
            $(element).sticky({ topSpacing: 0 });
        }
    };
});