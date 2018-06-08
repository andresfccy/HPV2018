'use strict';

var appSystematization = angular.module('prosperidad.systematization', []);

appSystematization.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('systematization');
    $httpProvider.interceptors.push('ErrorInterceptor');
});