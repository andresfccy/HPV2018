'use strict';

var appCommons = angular.module('prosperidad.parameters', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('parameters');
    $httpProvider.interceptors.push('ErrorInterceptor');
});