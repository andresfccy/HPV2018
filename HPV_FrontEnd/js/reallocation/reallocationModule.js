'use strict';

var appCommons = angular.module('prosperidad.reallocation', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('reallocation');
    $httpProvider.interceptors.push('ErrorInterceptor');
});