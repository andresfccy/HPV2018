appSystematization
    .factory('SystematizationService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_SISTEMATIZACION;

            var darSistematizacionUrl = baseUrl + "/DarSistematizacion";
            var actualizarSistematizacionUrl = baseUrl + "/ActualizarSistematizacion";
            var consultarSistematizacionUrl = baseUrl + "/ConsultarSistematizacion";
            // Fase 2
            var darCategoriasXInstrumentoUrl = baseUrl + "/DarCategoriasXInstrumento";
            var darSubcategoriasXInstrumentoUrl = baseUrl + "/DarSubcategoriasXInstrumento";
            var darCategorizacionUrl = baseUrl + "/DarCategorizacion";
            var actualizarCategorizacionUrl = baseUrl + "/ActualizarCategorizacion";

            var paramDefaults = {};
            var actions = {
                darSistematizacion: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: darSistematizacionUrl
                },
                actualizarSistematizacion: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: actualizarSistematizacionUrl
                },
                consultarSistematizacion: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: consultarSistematizacionUrl
                },
                // Fase 2
                darCategoriasXInstrumento: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: darCategoriasXInstrumentoUrl
                },
                darSubcategoriasXInstrumento: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: darSubcategoriasXInstrumentoUrl
                },
                darCategorizacion: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: darCategorizacionUrl
                },
                actualizarCategorizacion: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: actualizarCategorizacionUrl
                }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);