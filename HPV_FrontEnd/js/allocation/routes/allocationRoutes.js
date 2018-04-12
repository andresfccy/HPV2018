'use strict';

angular.module('prosperidad.allocation').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('allocationAdmin', {
                url: '/UbicacionesAdministracion',
                templateUrl: 'views/allocation/allocationAdminList.html',
                controller: 'AllocationAdminController as ctrl',
                resolve: {
                    $title: function () { return 'Aprobación de Espacios Físicos - Operativo'; }
                }
            }).state('newAllocationAdmin', {
                url: '/UbicacionesAdministracion/Crear',
                templateUrl: 'views/allocation/allocationAdminEdit.html',
                controller: 'EditAllocationAdminController as ctrl',
                resolve: {
                    $title: function () { return 'Adición de Espacios Físicos - Operativo'; }
                }
            }).state('editAllocationAdmin', {
                url: '/UbicacionesAdministracion/Editar/:IdAllocation',
                templateUrl: 'views/allocation/allocationAdminEdit.html',
                controller: 'EditAllocationAdminController as ctrl',
                resolve: {
                    $title: function () { return 'Edición de Espacios Físicos - Operativo'; }
                }
            })

            .state('allocation', {
                url: '/Ubicaciones',
                templateUrl: 'views/allocation/allocationList.html',
                controller: 'AllocationController as ctrl',
                resolve: {
                    $title: function () { return 'Asignación de Espacios Físicos - Operativo'; }
                }
            }).state('newAllocation', {
                url: '/Ubicaciones/Crear',
                templateUrl: 'views/allocation/allocationEdit.html',
                controller: 'EditAllocationController as ctrl',
                resolve: {
                    $title: function () { return 'Adición de Espacios Físicos - Operativo'; }
                }
            }).state('editAllocation', {
                url: '/Ubicaciones/Editar/:IdAllocation',
                templateUrl: 'views/allocation/allocationEdit.html',
                controller: 'EditAllocationController as ctrl',
                resolve: {
                    $title: function () { return 'Edición de Espacios Físicos - Operativo'; }
                }
            });
    }
]);