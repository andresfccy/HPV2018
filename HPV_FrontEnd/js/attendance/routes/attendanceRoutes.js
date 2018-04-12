'use strict';

angular.module('prosperidad.attendance').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('attendanceFacilitator', {
                url: '/ListaAsistencia',
                templateUrl: 'views/attendance/attendanceFacilitator.html',
                controller: 'AttendanceFacilitatorController as ctrl',
                resolve: {
                    $title: function () { return 'Listas de Asistencia - Operativo'; }
                }
            })

            .state('attendanceCoordinator', {
                url: '/ListaAsistenciaCoordinador',
                templateUrl: 'views/attendance/attendanceCoordinator.html',
                controller: 'AttendanceCoordinatorController as ctrl',
                resolve: {
                    $title: function () { return 'Listas de Asistencia - Operativo'; }
                }
            })
        ;
    }
]);