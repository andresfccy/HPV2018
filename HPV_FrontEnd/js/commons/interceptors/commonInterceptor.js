angular.module('prosperidad.commons')
    .factory('ErrorInterceptor', ['$q', '$injector', function ($q, $injector) {
        var interceptor = {
            responseError: function (response) {
                if (response.status === 404) {
                    // get $http via $injector because of circular dependency problem
                    var $http = $http || $injector.get('$http');
                    var defer = $q.defer();
                    $http.get('404.html')
                        .then(function (result) {
                            response.status = 200;
                            response.data = result.data;
                            defer.resolve(response);
                        }, function () {
                            defer.reject(response);
                        });
                    return defer.promise;// response;
                } else {
                    return $q.reject(response);
                }
            }
        };
        return interceptor;
    }]);