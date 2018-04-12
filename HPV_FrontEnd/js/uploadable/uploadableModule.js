'use strict';

var appCommons = angular.module('prosperidad.uploadable', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('uploadable');
    $httpProvider.interceptors.push('ErrorInterceptor');
});