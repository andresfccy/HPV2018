'use strict';

angular.module('prosperidad.reallocation').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('reallocation', {
                url: '/Reasignacion',
                templateUrl: 'views/reallocation/reallocation.html',
                controller: 'ReallocationController as ctrl',
                resolve: {
                    $title: function () { return 'Reasignación de participantes - Operativo'; }
                }
            });
    }
]);