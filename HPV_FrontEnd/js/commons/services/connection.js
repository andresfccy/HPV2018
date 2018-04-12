angular.module('prosperidad.commons')
    .factory('Connection', [
    function () {
        var factory = {
            API_HOSTNAME: 'http://localhost',
            API_PORT: '3394',

            API_BASE_URL: function () {
                var baseUrl = factory.API_HOSTNAME;
                if (factory.API_PORT.length > 0 || factory.API_PORT !== '80') {
                    baseUrl += ':' + factory.API_PORT;
                }
                return baseUrl;
            }
        };

        return factory;
    }
]);