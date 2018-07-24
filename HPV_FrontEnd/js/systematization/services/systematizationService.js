appSystematization
    .factory('SystematizationService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_SISTEMATIZACION;

            var darSistematizacionUrl = baseUrl + "/DarSistematizacion";

            var paramDefaults = {};
            var actions = {
                darSistematizacion: {
                    method: 'POST',
                    headers: CommonsConstants.factory.CONTENT_TYPE_HEADER,
                    url: darSistematizacionUrl
                }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);