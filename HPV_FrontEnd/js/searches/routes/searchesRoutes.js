'use strict';

angular.module('prosperidad.searches')
    .config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('searches', {
                url: '/Consulta/Inscritos',
                templateUrl: 'views/searches/listUsers.html',
                controller: 'SearchesUsersListController as ctrl',
                resolve: {
                    $title: function () { return 'Inscritos - Consulta'; }
                }
            })
            .state('searchUser', {
                url: '/Consulta/Inscritos/:Id/:Season',
                templateUrl: 'views/searches/user.html',
                controller: 'SearchesUserController as ctrl',
                resolve: {
                    $title: function () { return 'Ver Inscrito - Consulta'; }
                }
            })
            .state('searchesSchedules', {
                url: '/Consulta/Horarios',
                templateUrl: 'views/searches/listSchdules.html',
                controller: 'searchesSchedulesListController as ctrl',
                resolve: {
                    $title: function () { return 'Horarios - Consulta'; }
                }
            })
        ;
    }
]);