angular.module('prosperidad.leadership')
    .factory('LeadershipService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_LIDERAZGO;

            var darLiderazgosUrl = baseUrl + "/DarLiderazgos";
            var actualizarLiderazgoUrl = baseUrl + "/ActualizarLiderazgo";
            var validarLiderazgoUrl = baseUrl + "/ValidarCantidadLiderazgo";
            var consultarLiderazgoUrl = baseUrl + "/ConsultarLiderazgo";
            var registrarEstadoLiderazgoUrl = baseUrl + "/RegistrarEstadoLiderazgo";

            var paramDefaults = {};
            var actions = {
                darLiderazgos: { method: 'POST', url: darLiderazgosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarLiderazgo: { method: 'POST', url: actualizarLiderazgoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                validarLiderazgo: { method: 'POST', url: validarLiderazgoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarLiderazgo: { method: 'POST', url: consultarLiderazgoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                registrarEstadoLiderazgo: { method: 'POST', url: registrarEstadoLiderazgoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);