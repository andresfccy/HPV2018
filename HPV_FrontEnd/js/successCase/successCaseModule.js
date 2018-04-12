'use strict';

var appCommons = angular.module('prosperidad.successCase', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('successCase');
    $httpProvider.interceptors.push('ErrorInterceptor');
});