angular.module('prosperidad.parameters')
    .factory('ParametersService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_PARAMETROS;
            var darParametrosUrl = baseUrl + "/DarParametros";
            var consultarParametroUrl = baseUrl + "/ConsultarParametro";
            var actualizarParametroUrl = baseUrl + "/ActualizarParametro";

            var darPeriodosUrl = baseUrl + "/DarPeriodos";
            var consultarPeriodoUrl = baseUrl + "/ConsultarPeriodo";
            var actualizarPeriodoUrl = baseUrl + "/ActualizarPeriodo";

            var paramDefaults = {};
            var actions = {
                darParametros: { method: 'POST', url: darParametrosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarParametro: { method: 'POST', url: consultarParametroUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarParametro: { method: 'POST', url: actualizarParametroUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },

                darPeriodos: { method: 'POST', url: darPeriodosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarPeriodo: { method: 'POST', url: consultarPeriodoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarPeriodo: { method: 'POST', url: actualizarPeriodoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);