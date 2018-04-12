'use strict';

angular.module('prosperidad.leadership').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('leadershipListFacilitator', {
                url: '/Liderazgo',
                templateUrl: 'views/leadership/leadershipListFacilitator.html',
                controller: 'LeadershipListFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            }).state('leadershipDetailFacilitator', {
                url: '/Liderazgo/:id',
                templateUrl: 'views/leadership/leadershipDetailFacilitator.html',
                controller: 'LeadershipDetailFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            }).state('leadershipDetailFacilitatorCreate', {
                url: '/Liderazgo/Crear',
                templateUrl: 'views/leadership/leadershipDetailFacilitator.html',
                controller: 'LeadershipDetailFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            }).state('leadershipListCoordinator', {
                url: '/LiderazgoCoordinador',
                templateUrl: 'views/leadership/leadershipListCoordinator.html',
                controller: 'LeadershipListCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            }).state('leadershipDetailCoordinator', {
                url: '/LiderazgoCoordinador/:id',
                templateUrl: 'views/leadership/leadershipDetailCoordinator.html',
                controller: 'LeadershipDetailCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            }).state('leadershipListSupervisor', {
                url: '/LiderazgoSupervisor',
                templateUrl: 'views/leadership/leadershipListSupervisor.html',
                controller: 'LeadershipListSupervisorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            }).state('leadershipDetailSupervisor', {
                url: '/LiderazgoSupervisor/:id',
                templateUrl: 'views/leadership/leadershipDetailSupervisor.html',
                controller: 'LeadershipDetailSupervisorController as ctrl',
                resolve: {
                    $title: function () { return 'Ejemplos de Liderazgo'; }
                }
            })
        ;
    }
]);