'use strict';

angular.module('app.controllers')

    .controller('SermonCtrl', ['$scope', '$http', function ($scope, $http) {

        var fakeSermons = [
            {
                date: new Date(2014, 9, 26),
                sermonId: 5,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Boldly Respond to the Invitation'
            },
            {
                date: new Date(2014, 9, 25),
                sermonId: 4,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold',
                    'Test2'
                ],
                title: 'Boldly Trust Jesus'
            },
            {
                date: new Date(2014, 9, 24),
                sermonId: 3,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold',
                    'Test1'
                ],
                title: 'Speak Boldly'
            },
            {
                date: new Date(2014, 9, 23),
                sermonId: 2,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold',
                    'Test1'
                ],
                title: 'Boldly Believe in the Son of God'
            },
            {
                date: new Date(2014, 9, 22),
                sermonId: 1,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold',
                    'Test2'
                ],
                title: 'Boldly Seek After God'
            }
        ];

        $scope.sermons = fakeSermons;

        $scope.filteredSpeakers = function () {
            return _.mapValues(_.groupBy($scope.sermons, 'speaker'), function (r) { return r.length; });
        };
        $scope.filterSpeakers = function (speaker) {
            $scope.speakerFilter = speaker;
        };

        $scope.filteredTags = function () {
            return _.mapValues(_.groupBy(_.chain($scope.sermons).pluck('tags').flatten().value()), function (r) { return r.length; });
        };
        $scope.filterTags = function (tag) {
            $scope.tagFilter = tag;
        };
    }]);