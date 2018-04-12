'use strict';

var appCommons = angular.module('prosperidad.leadership', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('leadership');
    $httpProvider.interceptors.push('ErrorInterceptor');
});