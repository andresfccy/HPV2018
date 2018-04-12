angular.module('prosperidad.sessions')
    .factory('SessionInjector', ['SessionsBusiness', 'CommonsConstants', '$rootScope', '$q',
        function (SessionsBusiness, CommonsConstants, $rootScope, $q, $state) {

            var sessionInjector = {
                request: function (config) {
                    var tokenFromLocalStorage = SessionsBusiness.getTokenFromLocalStorage();
                    if (typeof tokenFromLocalStorage !== 'undefined' && tokenFromLocalStorage != null) {
                        config.headers[CommonsConstants.factory.TOKEN_HEADER] = 'Bearer ' + tokenFromLocalStorage;
                    } else {
                        config.headers[CommonsConstants.factory.TOKEN_HEADER] = '';
                    }
                    return config;
                },
                responseError: function (rejection) {
                    if (rejection.status == 401) {
                        $rootScope.$broadcast('401');
                    } else if (rejection.status == 403) {
                        $rootScope.$broadcast('403');
                    } else if (rejection.status == 500) {
                        $rootScope.$broadcast('500');
                    }
                    return $q.reject(rejection);
                }
            };
            return sessionInjector;
        }
    ]);