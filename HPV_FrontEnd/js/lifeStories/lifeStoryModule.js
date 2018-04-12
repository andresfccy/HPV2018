'use strict';

var appCommons = angular.module('prosperidad.lifeStory', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('lifeStory');
    $httpProvider.interceptors.push('ErrorInterceptor');
});