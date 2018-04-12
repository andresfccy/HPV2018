angular.module('prosperidad.roles')
    .factory('RolesService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ROLES;
            var baseUrl2 = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_USUARIOS;
            var darRolesUrl = baseUrl + "/DarRoles";
            var darRolesUsuarioUrl = baseUrl2 + "/DarRoles";
            var consultarRolUrl = baseUrl + "/ConsultarRol";
            var actualizarRolUrl = baseUrl + "/ActualizarRol";
            var darOpcionesUsuarioUrl = baseUrl + "/DarOpcionesUsuario";

            var paramDefaults = {};
            var actions = {
                darRoles: { method: 'GET', url: darRolesUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                consultarRol: { method: 'POST', url: consultarRolUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarRol: { method: 'POST', url: actualizarRolUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darRolesUsuario: { method: 'POST', url: darRolesUsuarioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darOpcionesUsuario: { method: 'POST', url: darOpcionesUsuarioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);