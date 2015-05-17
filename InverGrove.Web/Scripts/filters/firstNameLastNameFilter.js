(function () {
    'use strict';

    var appName = igchurch.constants.APP_NAME;

    angular.module(appName + '.filters')
        .filter('firstNameLastName', firstNameLastName);

    function firstNameLastName() {
        return function (members, search) {
            if (!search) {
                return members;
            }

            return members.filter(function (member) {
                var fullName = member.firstName + ' ' + member.lastName;

                if (fullName.toLowerCase().indexOf(search.toLowerCase()) > -1) {
                    return true;
                }

                return false;
            });
        };
    }
})();