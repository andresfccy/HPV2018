'use strict';

angular.module('prosperidad.sessions')
    .controller('WelcomeController',
    ['$scope', 'SessionsBusiness', '$state', 'SessionsService', 'growl',
        function ($scope, SessionsBusiness, $state, SessionsService, growl) {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;

            function initialize() {
                // Do something at initialize
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    ]
);