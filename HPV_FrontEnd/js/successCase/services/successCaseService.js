angular.module('prosperidad.successCase')
    .factory('SuccessCaseService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_CASOSDEEXITO;

            var darCasosDeExitoUrl = baseUrl + "/DarCasosDeExito";
            var actualizarCasoDeExitoUrl = baseUrl + "/ActualizarCasoDeExito";
            var validarCasoDeExitoUrl = baseUrl + "/ValidarCasoDeExito";
            var consultarCasoDeExitoUrl = baseUrl + "/ConsultarCasoDeExito";
            var darLogrosUrl = baseUrl + "/DarLogros";
            var registrarEstadoCasoDeExitoUrl = baseUrl + "/RegistrarEstadoCasoDeExito";

            var paramDefaults = {};
            var actions = {
                darCasosDeExito: { method: 'POST', url: darCasosDeExitoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarCasoDeExito: { method: 'POST', url: actualizarCasoDeExitoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                validarCasoDeExito: { method: 'POST', url: validarCasoDeExitoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarCasoDeExito: { method: 'POST', url: consultarCasoDeExitoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darLogros: { method: 'GET', url: darLogrosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                registrarEstadoCasoDeExito: { method: 'POST', url: registrarEstadoCasoDeExitoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);