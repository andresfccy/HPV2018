'use strict';

angular.module('prosperidad.parameters').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider

            .state('parameters', {
                url: '/Parametros',
                templateUrl: 'views/parameters/parameters.html',
                controller: 'ParametersController as ctrl',
                resolve: {
                    $title: function () { return 'Parámetros'; }
                }
            });
    }
]);