'use strict';

var appCommons = angular.module('prosperidad.participantState', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('participantState');
    $httpProvider.interceptors.push('ErrorInterceptor');
});