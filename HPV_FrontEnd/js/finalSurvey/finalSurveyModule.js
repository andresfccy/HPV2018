'use strict';

var appCommons = angular.module('prosperidad.finalSurvey', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('finalSurvey');
    $httpProvider.interceptors.push('ErrorInterceptor');
});