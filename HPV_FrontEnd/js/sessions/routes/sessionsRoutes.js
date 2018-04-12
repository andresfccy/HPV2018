'use strict';

angular.module('prosperidad.commons').config(['$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {

            $stateProvider.state('logout', {
                url: '/logout',
                templateUrl: 'views/sessions/logout.html',
                controller: 'LogoutController'
            }).state('login', {
                url: '/login',
                templateUrl: 'views/sessions/login.html',
                controller: 'LoginController as ctrl',
                resolve: {
                    $title: function () { return 'Iniciar Sesión - Usuario'; }
                }
            }).state('welcomeLoggedIn', {
                url: '/Bienvenido',
                templateUrl: 'views/sessions/welcomeLoggedIn.html',
                controller: 'WelcomeController as ctrl',
                resolve: {
                    $title: function () { return '¡Bienvenido! - Usuario'; }
                }
            }).state('retrievePassword', {
                url: '/RecuperarContrasena',
                templateUrl: 'views/sessions/retrievePassword.html',
                controller: 'RetrievePasswordController as ctrl',
                resolve: {
                    $title: function () { return 'Recuperar Contraseña - Usuario'; }
                }
            }).state('changePassword', {
                url: '/asignarClave/:token',
                templateUrl: 'views/sessions/changePassword.html',
                controller: 'RetrievePasswordController as ctrl',
                resolve: {
                    $title: function () { return 'Cambiar Contraseña - Usuario'; }
                }
            });
        }
    ]).config(['$locationProvider',
        function ($locationProvider) {
            $locationProvider.hashPrefix('!');
        }
    ]);