'use strict';

var appCommons = angular.module('prosperidad.users', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('users');
    $httpProvider.interceptors.push('ErrorInterceptor');
});