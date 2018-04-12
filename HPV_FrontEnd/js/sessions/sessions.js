'use strict';

angular.module('prosperidad.sessions', [])
    .config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('sessions');
    $httpProvider.interceptors.push('SessionInjector');
});