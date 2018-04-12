angular.module('prosperidad.commons')
    .factory('LocalStorageService', ['localStorageService', 'CommonsConstants', '$sessionStorage',
        function (localStorageService, CommonsConstants, $sessionStorage) {
            var factory = {};

            factory.get = function (key) {
                console.log("Get: ",key)
                return $sessionStorage.get(key);
            };

            factory.set = function (data, key) {
                console.log("Set", data, key)
                $sessionStorage.set(key, data);
            };

            factory.remove = function (key) {
                console.log("Remove", key)
                //$sessionStorage.remove(key);
                $sessionStorage.$reset();
            };

            return factory;
        }
    ]);