'use strict';

var appCommons = angular.module('prosperidad.roles', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('roles');
    $httpProvider.interceptors.push('ErrorInterceptor');
});