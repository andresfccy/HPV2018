'use strict';

angular.module('prosperidad.inscription').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('inscription', {
                url: '/Inscripcion',
                templateUrl: 'views/inscription/inscription.html',
                controller: 'InscriptionController as ctrl',
                resolve:{
                    $title: function () { return 'Inscripción - Participantes'; }
                }
            });
    }
]);