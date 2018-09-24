angular.module('prosperidad.commons')
    .factory('CommonsPeriodoActual', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarPeriodoVigente';
            var darParametroInicialUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_GENERAL + '/DarParametroInicial';
            var darVigenciasInscritoUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_GENERAL + '/DarVigenciasInscrito';
            var paramDefaults = {};
            var actions = {
                darPeriodo: { method: 'GET', url: baseUrl, headers: {}, isArray: true },
                darParametroInicial: { method: 'GET', url: darParametroInicialUrl, headers: {} },
                darVigenciasInscrito: { method: 'GET', url: darVigenciasInscritoUrl, headers: {} },
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);