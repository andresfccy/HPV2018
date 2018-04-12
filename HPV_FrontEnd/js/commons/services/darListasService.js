angular.module('prosperidad.commons')
    .factory('CommonsListasService', ['$resource', 'CommonsConstants',
        function ($resource, CommonsConstants) {
            var baseUrl = CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.URL_WS_GENERAL + '/DarListaValor';
            var paramDefaults = {};
            var actions = {
                darLista: { method: 'POST', url: baseUrl, headers: CommonsConstants.factory.CONTENT_TYPE_HEADER }
            };
            return $resource(baseUrl, paramDefaults, actions);
        }
    ]);