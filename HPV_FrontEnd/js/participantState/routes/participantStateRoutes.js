'use strict';

angular.module('prosperidad.participantState').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('participantState', {
                url: '/EstadoParticipante',
                templateUrl: 'views/participantState/participantState.html',
                controller: 'ParticipantStateController as ctrl',
                resolve: {
                    $title: function () { return 'Estado del participante'; }
                }
            });
    }
]);