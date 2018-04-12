'use strict';

var appCommons = angular.module('prosperidad.searches', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('searches');
    $httpProvider.interceptors.push('ErrorInterceptor');
});