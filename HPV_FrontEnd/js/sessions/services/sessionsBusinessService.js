angular.module('prosperidad.sessions')
    .factory('SessionsBusiness',
    ['$rootScope', '$sessionStorage', 'CommonsConstants', 'localStorageService', '$q',
    function ($rootScope, $sessionStorage, CommonsConstants, localStorageService, $q) {
        var factory = {};

        $rootScope.opcDef = $q.defer();
        $rootScope.opcProm = $rootScope.opcDef.promise;

        if (!$rootScope.$storage) {
            $rootScope.$storage = $sessionStorage;
        }

        factory.authorized = function (opc) {
            if ($rootScope.permissions && $rootScope.permissions.length > 0) {
                return factory.getRolFromLocalStorage() && $rootScope.permissions[factory.getRolFromLocalStorage()].indexOf(opc + "") >= 0;
            }
        }

        factory.getTokenFromLocalStorage = function () {
            return $rootScope.$storage[CommonsConstants.factory.TOKEN_KEY_LOCALSTORAGE];
        };

        factory.setTokenToLocalStorage = function (token) {
            $rootScope.$storage[CommonsConstants.factory.TOKEN_KEY_LOCALSTORAGE] = token;
        };

        factory.getUserIdFromLocalStorage = function () {
            return $rootScope.$storage[CommonsConstants.factory.USER_ID_LOCALSTORAGE];
        };

        factory.setUserIdToLocalStorage = function (userId) {
            $rootScope.$storage[CommonsConstants.factory.USER_ID_LOCALSTORAGE] = userId;
        };

        factory.getRolFromLocalStorage = function () {
            return $rootScope.$storage[CommonsConstants.factory.ROL_LOCALSTORAGE];
        };

        factory.setRolToLocalStorage = function (rol) {
            $rootScope.$storage[CommonsConstants.factory.ROL_LOCALSTORAGE] = rol;
        };

        factory.getNameFromLocalStorage = function () {
            return $rootScope.$storage[CommonsConstants.factory.NAME_LOCALSTORAGE];
        };

        factory.setNameToLocalStorage = function (name) {
            $rootScope.$storage[CommonsConstants.factory.NAME_LOCALSTORAGE] = name;
        };

        factory.removeAllInfoFromLocalStorage = function () {
            $sessionStorage.$reset();
            /*localStorageService.remove(CommonsConstants.factory.TOKEN_KEY_LOCALSTORAGE);
            localStorageService.remove(CommonsConstants.factory.USER_ID_LOCALSTORAGE);
            localStorageService.remove(CommonsConstants.factory.ROL_LOCALSTORAGE);
            localStorageService.remove(CommonsConstants.factory.NAME_LOCALSTORAGE);*/
        };

        factory.isLoggedIn = function () {
            var token = factory.getTokenFromLocalStorage();
            if (typeof token == 'undefined' || token == null || token == '') {
                return false;
            }

            return true;
        };

        return factory;
    }
    ]);
