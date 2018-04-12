'use strict';

var appCommons = angular.module('prosperidad.allocation', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('allocation');
    $httpProvider.interceptors.push('ErrorInterceptor');
});