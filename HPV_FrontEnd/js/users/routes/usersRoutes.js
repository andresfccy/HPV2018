'use strict';

angular.module('prosperidad.users')
    .config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('users', {
                url: '/Usuarios',
                templateUrl: 'views/users/listUsers.html',
                controller: 'UsersController as ctrl',
                resolve: {
                    $title: function () { return 'Lista de Usuarios - Usuario'; }
                }
            })
            .state('editUser', {
                url: '/Usuarios/EditarUsuario/:IdUsuario',
                templateUrl: 'views/users/editUser.html',
                controller: 'EditUsersController as ctrl',
                resolve: {
                    $title: function () { return 'Edición de Usuarios - Usuario'; }
                }
            })
            .state('newUser', {
                url: '/Usuarios/NuevoUsuario',
                templateUrl: 'views/users/editUser.html',
                controller: 'EditUsersController as ctrl',
                resolve: {
                    $title: function () { return 'Adición de Usuarios - Usuario'; }
                }
            })
            .state('editProfile', {
                url: '/Administracion/MiCuenta',
                templateUrl: 'views/users/editProfile.html',
                controller: 'EditUsersController as ctrl',
                resolve: {
                    $title: function () { return 'Mi cuenta'; }
                }
            })
        ;
    }
]);