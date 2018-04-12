'use strict';

var appCommons = angular.module('prosperidad.feedback', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('feedback');
    $httpProvider.interceptors.push('ErrorInterceptor');
});