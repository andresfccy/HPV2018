'use strict';

angular.module('prosperidad.lifeStory').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('lifeStoryListFacilitator', {
                url: '/HistoriasDeVida',
                templateUrl: 'views/lifeStories/lifeStoriesListFacilitator.html',
                controller: 'LifeStoryListFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            }).state('lifeStoryDetailFacilitator', {
                url: '/HistoriasDeVida/:id',
                templateUrl: 'views/lifeStories/lifeStoriesDetailFacilitator.html',
                controller: 'LifeStoryDetailFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            }).state('lifeStoryDetailFacilitatorCreate', {
                url: '/HistoriasDeVida/Crear',
                templateUrl: 'views/lifeStories/lifeStoriesDetailFacilitator.html',
                controller: 'LifeStoryDetailFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            }).state('lifeStoryListCoordinator', {
                url: '/HistoriasDeVidaCoordinador',
                templateUrl: 'views/lifeStories/lifeStoriesListCoordinator.html',
                controller: 'LifeStoryListCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            }).state('lifeStoryDetailCoordinator', {
                url: '/HistoriasDeVidaCoordinador/:id',
                templateUrl: 'views/lifeStories/lifeStoriesDetailCoordinator.html',
                controller: 'LifeStoryDetailCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            }).state('lifeStoryListSupervisor', {
                url: '/HistoriasDeVidaSupervisor',
                templateUrl: 'views/lifeStories/lifeStoriesListSupervisor.html',
                controller: 'LifeStoryListSupervisorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            }).state('lifeStoryDetailSupervisor', {
                url: '/HistoriasDeVidaSupervisor/:id',
                templateUrl: 'views/lifeStories/lifeStoriesDetailSupervisor.html',
                controller: 'LifeStoryDetailSupervisorController as ctrl',
                resolve: {
                    $title: function () { return 'Historias de vida'; }
                }
            })
        ;
    }
]);