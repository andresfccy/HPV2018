'use strict';

var appCommons = angular.module('prosperidad.attendance', []);

appCommons.config(function ($translateProvider, $translatePartialLoaderProvider, $httpProvider) {
    $translatePartialLoaderProvider.addPart('attendance');
    $httpProvider.interceptors.push('ErrorInterceptor');
});