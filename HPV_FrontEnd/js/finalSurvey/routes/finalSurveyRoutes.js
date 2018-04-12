'use strict';

angular.module('prosperidad.finalSurvey')
    .config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('finalSurvey', {
                url: '/EncuestaFin/:id',
                templateUrl: 'views/finalSurvey/survey.html',
                controller: 'FinalSurveyController as ctrl',
                resolve: {
                    $title: function () { return 'Encuesta de salida'; }
                }
            })
        ;
    }
]);