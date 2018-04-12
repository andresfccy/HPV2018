angular.module('prosperidad.sessions')
    .factory('SessionsService', ['$resource', 'CommonsConstants', 'SessionsBusiness',
        function ($resource, CommonsConstants, SessionsBusiness) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_USUARIOS;
            var authenticateUserUrl = baseUrl + '/AutenticarUsuario';
            var retrievePasswordUrl = baseUrl + '/RecuperarContrasena';
            var asignarContrasenaXTokenUrl = baseUrl + '/AsignarContrasenaXToken';
            var paramDefaults = {};
            var actions = {
                authenticateUser: { method: 'POST', url: authenticateUserUrl, headers: {} },
                retrievePassword: { method: 'POST', url: retrievePasswordUrl, headers: {} },
                asignarContrasenaXToken: { method: 'POST', url: asignarContrasenaXTokenUrl, headers: {}}
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);
