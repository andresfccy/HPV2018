angular.module('prosperidad.lifeStory')
    .factory('LifeStoryService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_HISTORIASDEVIDA;

            var darHistoriasDeVidaUrl = baseUrl + "/DarHistoriasDeVida";
            var actualizarHistoriaVidaUrl = baseUrl + "/ActualizarHistoriaVida";
            var validarCantidadHistoriasUrl = baseUrl + "/ValidarCantidadHistorias";
            var consultarHistoriaVidaUrl = baseUrl + "/ConsultarHistoriaVida";
            var registrarEstadoHistoriaVidaUrl = baseUrl + "/RegistrarEstadoHistoriaVida";

            var paramDefaults = {};
            var actions = {
                darHistoriasDeVida: { method: 'POST', url: darHistoriasDeVidaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarHistoriaVida: { method: 'POST', url: actualizarHistoriaVidaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                validarCantidadHistorias: { method: 'POST', url: validarCantidadHistoriasUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarHistoriaVida: { method: 'POST', url: consultarHistoriaVidaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                registrarEstadoHistoriaVida: { method: 'POST', url: registrarEstadoHistoriaVidaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);