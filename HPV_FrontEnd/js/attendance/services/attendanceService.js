angular.module('prosperidad.attendance')
    .factory('AttendanceService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ASISTENCIA;

            var darGruposXFacilitadorUrl = baseUrl + "/DarGruposXFacilitador";
            var darTalleresUrl = baseUrl + "/DarTalleres";
            var generarListaAsistenciaUrl = baseUrl + "/GenerarListaAsistencia";
            var darFacilitadorXCoordinadorUrl = baseUrl + "/DarFacilitadorXCoordinador";
            var darEntregableEstadoUrl = baseUrl + "/DarEntregableEstado";
            var darEntregableEstadoDetalleUrl = baseUrl + "/DarEntregableEstadoDetalle";
            var darAsistenciaEstadoUrl = baseUrl + "/DarAsistenciaEstado";
            var darAsistenteXFacilitadorUrl = baseUrl + "/DarAsistenteXFacilitador";
            var registrarAsistenciaUrl = baseUrl + "/RegistrarAsistencia";
            var registrarAprobacionUrl = baseUrl + "/RegistrarAprobacion";
            var darInscritosUrl = baseUrl + "/DarInscritos";

            var paramDefaults = {};
            var actions = {
                darGruposXFacilitador: { method: 'POST', url: darGruposXFacilitadorUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darTalleres: { method: 'GET', url: darTalleresUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                generarListaAsistencia: { method: 'POST', url: generarListaAsistenciaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darFacilitadorXCoordinador: { method: 'POST', url: darFacilitadorXCoordinadorUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEntregableEstado: { method: 'POST', url: darEntregableEstadoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEntregableEstadoDetalle: { method: 'POST', url: darEntregableEstadoDetalleUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darAsistenciaEstado: { method: 'POST', url: darAsistenciaEstadoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darAsistenteXFacilitador: { method: 'POST', url: darAsistenteXFacilitadorUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                registrarAsistencia: { method: 'POST', url: registrarAsistenciaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                registrarAprobacion: { method: 'POST', url: registrarAprobacionUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darInscritos: { method: 'POST', url: darInscritosUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);