angular.module('prosperidad.uploadable')
    .factory('UploadableService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_ENTREGABLES;

            var darGruposXFacilitadorUrl = baseUrl + "/DarGruposXFacilitador";
            var darTalleresUrl = baseUrl + "/DarTalleres";
            var generarListaAsistenciaUrl = baseUrl + "/GenerarListaAsistencia";
            var darEntregableEstadoUrl = baseUrl + "/DarEntregableEstado";
            var darEntregableEstadoDetalleUrl = baseUrl + "/DarEntregableEstadoDetalle";

            var paramDefaults = {};
            var actions = {
                darGruposXFacilitador: { method: 'POST', url: darGruposXFacilitadorUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darTalleres: { method: 'GET', url: darTalleresUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                generarListaAsistencia: { method: 'POST', url: generarListaAsistenciaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEntregableEstado: { method: 'POST', url: darEntregableEstadoUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
                darEntregableEstadoDetalle: { method: 'POST', url: darEntregableEstadoDetalleUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER },
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);