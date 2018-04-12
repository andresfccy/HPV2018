'use strict';

angular.module('prosperidad.roles').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('rolesList', {
                url: '/Roles',
                templateUrl: 'views/roles/rolesList.html',
                controller: 'RolesController as ctrl',
                resolve: {
                    $title: function () { return 'Roles'; }
                }
            })
            .state('roleDetail', {
                url: '/Roles/:id',
                templateUrl: 'views/roles/rolesDetail.html',
                controller: 'RolesController as ctrl',
                resolve: {
                    $title: function () { return 'Detalle Rol'; }
                }
            })
            .state('newRole', {
                url: '/Roles/Nuevo',
                templateUrl: 'views/roles/rolesDetail.html',
                controller: 'RolesController as ctrl',
                resolve: {
                    $title: function () { return 'Detalle Rol'; }
                }
            })
        ;
    }
]);