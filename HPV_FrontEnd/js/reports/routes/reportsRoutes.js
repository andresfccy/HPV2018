'use strict';

angular.module('prosperidad.reports').config(['$stateProvider',
    function ($stateProvider) {
        $stateProvider
            .state('reportsList', {
                url: '/Reportes',
                templateUrl: 'views/reports/filters.html',
                controller: 'ReportsController as ctrl',
                resolve: {
                    $title: function () { return 'Reportes'; }
                }
            })
            .state('reportsListTP', {
                url: '/ReportesProfesionalTerritorial',
                templateUrl: 'views/reports/filtersTP.html',
                controller: 'ReportsTerritorialProfController as ctrl',
                resolve: {
                    $title: function () { return 'Reportes - Profesional Territorial'; }
                }
            })
            .state('audit', {
                url: '/Auditoria',
                templateUrl: 'views/reports/audit.html',
                controller: 'AuditController as ctrl',
                resolve: {
                    $title: function () { return 'Auditoría'; }
                }
            })
            .state('indicatorsList', {
                url: '/Indicadores',
                templateUrl: 'views/reports/indicators.html',
                controller: 'IndicatorsController as ctrl',
                resolve: {
                    $title: function () { return 'Indicadores'; }
                }
            })
        ;
    }
]);