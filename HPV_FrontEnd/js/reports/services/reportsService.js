angular.module('prosperidad.reports')
    .factory('ReportsService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_REPORTES;
            var baseUrl2 = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_FECHA_CORTE;
            var darReportesUrl = baseUrl + "/DarReportes";
            var darFechaCorteUrl = baseUrl2 + "/DarFechaCorte";
            var consultarAuditoriaUrl = baseUrl + "/ConsultarAuditoria";
            var darCoordinadoresProfesionalUrl = baseUrl + "/DarCoordinadoresXProfesional";
            var darDeptosProfesionalUrl = baseUrl + "/DarDepartamentosXProfesional";
            var darMunicipiosProfesionalUrl = baseUrl + "/DarMunicipiosXProfesional";

            var paramDefaults = {};
            var actions = {
                darReportes: { method: 'POST', url: darReportesUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darFechaCorte: { method: 'POST', url: darFechaCorteUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarAuditoria: { method: 'POST', url: consultarAuditoriaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darCoordinadoresProfesional: { method: 'POST', url: darCoordinadoresProfesionalUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darDeptosProfesional: { method: 'POST', url: darDeptosProfesionalUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darMunicipiosProfesional: { method: 'POST', url: darMunicipiosProfesionalUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);