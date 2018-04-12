'use strict';

angular.module('prosperidad.uploadable').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('uploadableListFacilitator', {
                url: '/Entregables/MarcacionAsistencia',
                templateUrl: 'views/uploadable/attendanceListFacilitator.html',
                resolve: {
                    $title: function () { return 'Marcación de Asistencia - Entregables'; }
                },
                controller: 'UploadableListFacilitatorController as ctrl'
            })
            .state('uploadableDetailFacilitator', {
                url: '/Entregables/MarcacionAsistencia/:idTaller/Taller/:idGrupo/Grupo',
                templateUrl: 'views/uploadable/attendanceDetailFacilitator.html',
                resolve: {
                    $title: function () { return 'Marcación de Asistencia Detalle - Entregables'; }
                },
                controller: 'UploadableDetailFacilitatorController as ctrl'
            })

            .state('uploadableListCoordinator', {
                url: '/Entregables/AprobacionMarcacionAsistencia',
                templateUrl: 'views/uploadable/attendanceListCoordinator.html',
                resolve: {
                    $title: function () { return 'Marcación de Asistencia - Entregables'; }
                },
                controller: 'UploadableListCoordinatorController as ctrl'
            })
            .state('uploadableDetailCoordinator', {
                url: '/Entregables/AprobacionMarcacionAsistencia/:idTaller/Taller/:idGrupo/Grupo/:idFacilitador/Facilitador',
                templateUrl: 'views/uploadable/attendanceDetailCoordinator.html',
                resolve: {
                    $title: function () { return 'Marcación de Asistencia Detalle - Entregables'; }
                },
                controller: 'UploadableDetailCoordinatorController as ctrl'
            })

            .state('uploadableListSupervisor', {
                url: '/Entregables/AprobacionMarcacionAsistenciaSupervisor',
                templateUrl: 'views/uploadable/attendanceListSupervisor.html',
                resolve: {
                    $title: function () { return 'Marcación de Asistencia - Entregables'; }
                },
                controller: 'UploadableListSupervisorController as ctrl'
            })
                .state('uploadableDetailSupervisor', {
                    url: '/Entregables/AprobacionMarcacionAsistenciaSupervisor/:idTaller/Taller/:idGrupo/Grupo/:idFacilitador/Facilitador',
                    templateUrl: 'views/uploadable/attendanceDetailSupervisor.html',
                    resolve: {
                        $title: function () { return 'Marcación de Asistencia Detalle - Entregables'; }
                    },
                    controller: 'UploadableDetailSupervisorController as ctrl'
            })
        ;
    }
]);