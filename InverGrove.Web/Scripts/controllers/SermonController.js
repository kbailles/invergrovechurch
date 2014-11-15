'use strict';

angular.module('igchurch.controllers')

    .controller('SermonCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {

        var fakeSermons = [
            {
                date: new Date(2014, 9, 26),
                sermonId: 171447693,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Boldly Respond to the Invitation'
            },
            {
                date: new Date(2014, 9, 25),
                sermonId: 171447888,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Boldly Trust Jesus'
            },
            {
                date: new Date(2014, 9, 24),
                sermonId: 171447578,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Speak Boldly'
            },
            {
                date: new Date(2014, 9, 23),
                sermonId: 171447641,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Boldly Believe in the Son of God'
            },
            {
                date: new Date(2014, 9, 22),
                sermonId: 171447791,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Boldly Seek After God'
            },
            {
                date: new Date(2014, 9, 21),
                sermonId: 171447331,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Biblical Boldness'
            },
            {
                date: new Date(2014, 9, 21),
                sermonId: 171447455,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Boldly Imitate Christ'
            },
            {
                date: new Date(2014, 9, 21),
                sermonId: 171447516,
                speaker: 'Antoine Halloway',
                tags: [
                    'Be Bold'
                ],
                title: 'Live Boldly'
            }
        ];

        $scope.sermons = fakeSermons;
        $scope.titleFilter = ''; //Initialize filter

        $scope.filteredSpeakers = function () {
            //TODO: Add defensive coding...
            return _.mapValues(_.groupBy($scope.sermons, 'speaker'), function (r) { return r.length; });
        };
        $scope.filteredTags = function () {
            //TODO: Add defensive coding...
            return _.mapValues(_.groupBy(_.chain($scope.sermons).pluck('tags').flatten().value()), function (r) { return r.length; });
        };

        $scope.sermonDetail = function (sermonId) {
            $window.location.href = 'SermonDetail?sermonId=' + sermonId;
        }
    }]);