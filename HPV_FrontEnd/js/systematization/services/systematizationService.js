appSystematization
    .factory('SystematizationService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_SISTEMATIZACION;

            // var darHistoriasDeVidaUrl = baseUrl + "/DarHistoriasDeVida";

            var paramDefaults = {};
            var actions = {
                // darHistoriasDeVida: { method: 'POST', url: darHistoriasDeVidaUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);