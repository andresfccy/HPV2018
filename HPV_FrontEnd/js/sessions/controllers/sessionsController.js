'use strict';
angular.module('prosperidad.sessions')
    .controller('LoginController',
    ['$rootScope', '$scope', '$cookieStore', 'SessionsBusiness', 'SessionsService', 'CommonsConstants', 'growl',
        '$translate', '$q', '$http','loading',
        function ($rootScope, $scope, dialogs, $cookieStore, SessionsBusiness, SessionsService, CommonsConstants, growl,
            $translate, $q, $http, loading) {
            $scope.data = { user: "", password: "" };
            $scope.logged = false;
            $scope.logIn = function () {
                var data = "grant_type=password&username=" + $scope.data.user + "&password=" + $scope.data.password;

                loading.startLoading("LoginController, function - $http");
                var deferred = $q.defer();

                $http({
                    url: CommonsConstants.factory.API_BASE_URL() + CommonsConstants.factory.API_LOGIN_PATH,
                    method: 'POST',
                    data: data,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).success(function (response) {
                    SessionsBusiness.setTokenToLocalStorage(response.access_token);
                    SessionsBusiness.setUserIdToLocalStorage(response['as:user_id']);
                    SessionsBusiness.setRolToLocalStorage(response.roles);
                    //$rootScope.$broadcast('refreshMainMenu', 1)
                    deferred.resolve(response);
                    loading.stopLoading("LoginController, function - $http");
                }).error(function (err, status) {
                    loading.stopLoading("LoginController, function - $http");
                    console.log(err, status);
                    if (status == 400) {
                        $translate(['login.messages.invalidLoginDesc']).then(function (translation) {
                            growl.addErrorMessage(translation['login.messages.invalidLoginDesc']);
                        });
                    }
                });
                return deferred.promise;

            };
            $scope.logOut = function () {
                SessionsBusiness.removeAllInfoFromLocalStorage();
            }
        }]);