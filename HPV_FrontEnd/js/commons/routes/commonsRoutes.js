'use strict';

angular.module('prosperidad.commons').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('403', {
                url: '/403',
                templateUrl: 'views/commons/403.html',
                controller: 'CommonsErrorController',
                resolve: {
                    $title: function () { return '¡Error!'; }
                }
            })
            .state('401', {
                url: '401',
                templateUrl: 'views/commons/401.html',
                controller: 'CommonsErrorController',
                resolve: {
                    $title: function () { return '¡Error!'; }
                }
            })
            .state('/500', {
                url: '/500',
                templateUrl: 'views/commons/500.html',
                controller: 'CommonsErrorController',
                resolve: {
                    $title: function () { return '¡Error!'; }
                }
            })
            .state('generateCertificate', {
                url: '/GenerarCertificado',
                templateUrl: 'views/commons/generateCertificate.html',
                controller: 'CertificateController as ctrl',
                resolve: {
                    $title: function () { return 'Generar certificado'; }
                }
            })
        ;
    }
]);