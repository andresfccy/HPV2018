angular.module('prosperidad.feedback')
    .factory('FeedbackService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_GENERAL;
            var baseUrl2 = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO;
            var validarTokenUrl = baseUrl + "/ValidarToken";
            var registrarEncuestaSatisfaccionUrl = baseUrl + "/RegistrarEncuestaSatisfaccion";
            var darEncuestaUrl = baseUrl2 + "/DarEncuesta";

            var paramDefaults = {};
            var actions = {
                validarToken: { method: 'POST', url: validarTokenUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                registrarEncuestaSatisfaccion: { method: 'POST', url: registrarEncuestaSatisfaccionUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEncuesta: { method: 'POST', url: darEncuestaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);