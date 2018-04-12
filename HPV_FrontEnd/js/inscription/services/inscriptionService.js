angular.module('prosperidad.inscription')
    .factory('InscriptionService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/ValidarBeneficiario';
            var validarCorreoUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/ValidarCorreo';
            var darEncuestaUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarEncuesta';
            var guardarInscripcionUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/GuardarInscripcion';
            var generarCertificadoUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/GenerarCertificado';
            var paramDefaults = {};

            var actions = {
                validarUsuario: { method: 'POST', url: baseUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                validarCorreo: { method: 'POST', url: validarCorreoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEncuesta: { method: 'POST', url: darEncuestaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                guardarInscripcion: { method: 'POST', url: guardarInscripcionUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                generarCertificado: { method: 'POST', url: generarCertificadoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);