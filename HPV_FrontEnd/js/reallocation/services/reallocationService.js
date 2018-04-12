angular.module('prosperidad.reallocation')
    .factory('ReallocationService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_SEARCHES;
            var espaciosUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_UBICACIONES;
            var consultarInscritosUrl = baseUrl + "/ConsultarInscritos";
            var consultarInscritoUrl = baseUrl + "/ConsultarInscrito";
            var consultarHorariosUrl = baseUrl + "/ConsultarHorarios";
            var reasignarEspacioUrl = espaciosUrl + '/ReasignarEspacio';
            

            var paramDefaults = {};
            var actions = {
                ConsultarInscritos: { method: 'POST', url: consultarInscritosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                ConsultarInscrito: { method: 'POST', url: consultarInscritoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                ConsultarHorarios: { method: 'POST', url: consultarHorariosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                ReasignarEspacio: { method: 'POST', url: reasignarEspacioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);