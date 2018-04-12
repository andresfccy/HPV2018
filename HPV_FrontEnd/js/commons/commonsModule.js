'use strict';

var appCommons = angular.module ('prosperidad.commons', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('commons');
    $httpProvider.interceptors.push('ErrorInterceptor');
});