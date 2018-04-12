angular.module('prosperidad.allocation')
    .factory('AllocationService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_UBICACIONES;

            var darEspacioUrl = baseUrl + "/DarEspacioFisico";
            var darUnEspacioUrl = baseUrl + "/ConsultarEspacioFisico";
            var darDiasUrl = baseUrl + "/DarDias";
            var darDireccionesLugarUrl = baseUrl + "/DarDireccionesLugar";
            var darEspacioFisicoUrl = baseUrl + "/DarEspacioFisico";
            var darGruposUrl = baseUrl + "/DarGrupos";
            var darHorariosUrl = baseUrl + "/DarHorarios";
            var darLugaresMunicipioUrl = baseUrl + "/DarLugaresMunicipio";
            var actualizarEspacioFisicoUrl = baseUrl + "/ActualizarEspacioFisico";
            var aprobarEspacioFisicoUrl = baseUrl + "/AprobarEspacioFisico";
            var rechazarEspacioFisicoUrl = baseUrl + "/RechazarEspacioFisico";
            var darFacilitadoresUrl = baseUrl + "/";
            var darLugaresPorAprobarUrl = baseUrl + "/DarEspacioFisicoXAprobar";
            var darDeptosFacilitadorUrl = baseUrl + "/DarDeptosFacilitador";
            var darMunicipiosFacilitadorUrl = baseUrl + "/DarMunicipiosFacilitador";

            var paramDefaults = {};
            var actions = {
                darEspacio: { method: 'POST', url: darEspacioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darUnEspacio: { method: 'POST', url: darUnEspacioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darDias: { method: 'GET', url: darDiasUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darDireccionesLugar: { method: 'POST', url: darDireccionesLugarUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEspacioFisico: { method: 'POST', url: darEspacioFisicoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darGrupos: { method: 'GET', url: darGruposUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darHorarios: { method: 'GET', url: darHorariosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darLugaresMunicipio: { method: 'POST', url: darLugaresMunicipioUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                actualizarEspacioFisico: { method: 'POST', url: actualizarEspacioFisicoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                aprobarEspacioFisico: { method: 'POST', url: aprobarEspacioFisicoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                rechazarEspacioFisico: { method: 'POST', url: rechazarEspacioFisicoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darFacilitadores: { method: 'POST', url: darFacilitadoresUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darLugaresPorAprobar: { method: 'POST', url: darLugaresPorAprobarUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darDeptosFacilitador: { method: 'POST', url: darDeptosFacilitadorUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darMunicipiosFacilitador: { method: 'POST', url: darMunicipiosFacilitadorUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);