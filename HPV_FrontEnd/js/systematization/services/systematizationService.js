appSystematization
    .factory('SystematizationService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_SISTEMATIZACION;

            var darSistematizacionUrl = baseUrl + "/DarSistematizacion";
            var actualizarSistematizacionUrl = baseUrl + "/ActualizarSistematizacion";
            var consultarSistematizacionUrl = baseUrl + "/ConsultarSistematizacion";

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
                }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);