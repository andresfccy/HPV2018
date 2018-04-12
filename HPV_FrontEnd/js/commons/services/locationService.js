angular.module('prosperidad.commons')
    .factory('CommonsUbicaciones', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarDepartamentos';
            var municipiosUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarMunicipiosXDepto';
            var diasUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarDiasDisponibles';
            var horasUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarHorariosDisponibles';
            var lugaresUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ALISTAMIENTO + '/DarLugaresDisponibles';
            var todosMunicipiosUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_USUARIOS + "/DarMunicipios";
            var paramDefaults = {};
            var actions = {
                darDepartamentos: { method: 'GET', url: baseUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darMunicipios: { method: 'POST', url: municipiosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darDias: { method: 'POST', url: diasUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darHoras: { method: 'POST', url: horasUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darLugares: { method: 'POST', url: lugaresUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darTodosMunicipios: { method: 'GET', url: todosMunicipiosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);