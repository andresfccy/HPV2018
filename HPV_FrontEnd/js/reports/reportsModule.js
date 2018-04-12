'use strict';

var appCommons = angular.module('prosperidad.reports', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('reports');
    $httpProvider.interceptors.push('ErrorInterceptor');
});