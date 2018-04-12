'use strict';

var appCommons = angular.module('prosperidad.inscription', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('inscription');
    $httpProvider.interceptors.push('ErrorInterceptor');
});