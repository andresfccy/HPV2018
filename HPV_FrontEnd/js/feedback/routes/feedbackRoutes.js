'use strict';

angular.module('prosperidad.users')
    .config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('survey', {
                url: '/Encuesta/:token',
                templateUrl: 'views/feedback/survey.html',
                controller: 'FeedbackController as ctrl',
                resolve: {
                    $title: function () { return 'Encuesta de satisfacción'; }
                }
            })
        ;
    }
]);