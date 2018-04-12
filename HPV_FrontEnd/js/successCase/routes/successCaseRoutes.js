'use strict';

angular.module('prosperidad.successCase').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('successCaseListFacilitator', {
                url: '/CasosDeExito',
                templateUrl: 'views/successCase/successCaseListFacilitator.html',
                controller: 'SuccessCaseListFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            }).state('successCaseDetailFacilitator', {
                url: '/CasosDeExito/:id',
                templateUrl: 'views/successCase/successCaseDetailFacilitator.html',
                controller: 'SuccessCaseDetailFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            }).state('successCaseDetailFacilitatorCreate', {
                url: '/CasosDeExito/Crear',
                templateUrl: 'views/successCase/successCaseDetailFacilitator.html',
                controller: 'SuccessCaseDetailFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            }).state('successCaseListCoordinator', {
                url: '/CasosDeExitoCoordinador',
                templateUrl: 'views/successCase/successCaseListCoordinator.html',
                controller: 'SuccessCaseListCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            }).state('successCaseDetailCoordinator', {
                url: '/CasosDeExitoCoordinador/:id',
                templateUrl: 'views/successCase/successCaseDetailCoordinator.html',
                controller: 'SuccessCaseDetailCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            }).state('successCaseListSupervisor', {
                url: '/CasosDeExitoSupervisor',
                templateUrl: 'views/successCase/successCaseListSupervisor.html',
                controller: 'SuccessCaseListSupervisorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            }).state('successCaseDetailSupervisor', {
                url: '/CasosDeExitoSupervisor/:id',
                templateUrl: 'views/successCase/successCaseDetailSupervisor.html',
                controller: 'SuccessCaseDetailSupervisorController as ctrl',
                resolve: {
                    $title: function () { return 'Casos de éxito'; }
                }
            })
        ;
    }
]);