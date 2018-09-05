angular.module('prosperidad.finalSurvey')
    .factory('FinalSurveyService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_GENERAL;
            var baseUrl2 = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO;
            var registrarEncuestaFinalUrl = baseUrl2 + "/RegistrarEncuestaFinal";
            var darEncuestaUrl = baseUrl2 + "/DarEncuesta";
            var darEncuestaInscritoUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarEncuestaInscrito';

            var paramDefaults = {};
            var actions = {
                registrarEncuestaFinal: { method: 'POST', url: registrarEncuestaFinalUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEncuesta: { method: 'POST', url: darEncuestaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEncuestaInscrito: { method: 'POST', url: darEncuestaInscritoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);