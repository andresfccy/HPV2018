angular.module('prosperidad.users')
    .factory('UsersService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_USUARIOS;
            var darUsuariosUrl = baseUrl + "/DarUsuarios";
            var consultarUsuarioUrl = baseUrl + "/ConsultarUsuario";
            var darCoordinadoresUrl = baseUrl + "/darCoordinadores";
            var ActualizarUsuarioUrl = baseUrl + "/ActualizarUsuario";
            var paramDefaults = {};
            var actions = {
                darUsuarios: { method: 'POST', url: darUsuariosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarUsuario: { method: 'POST', url: consultarUsuarioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darCoordinadores: { method: 'GET', url: darCoordinadoresUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarUsuario: { method: 'POST', url: ActualizarUsuarioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);