'use strict';

angular.module('prosperidad.users')
    .controller('UsersController',
    ['$rootScope', '$scope', '$location', '$translate', 'growl', 'UsersService',
        'CommonsConstants', '$log', '$state', 'filterFilter', 'loading', 'SessionsBusiness',
    function ($rootScope, $scope, $location, $translate, growl, UsersService,
        CommonsConstants, $log, $state, filterFilter, loading, SessionsBusiness) {

        if (!SessionsBusiness.authorized(11)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {

            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.roles = [];
            self.users = [];
            self.opcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            self.listaCambiados = [];
            $scope.maxResultsPerPage = 20;
            self.maxSize = 5;
            self.currentPage = 1;
            self.noOfPages = 0;
            self.total = 0;
            self.filtered = [];
            $scope.search;

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.users, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {
                self.roles = [];

                var req = { FiltroBusqueda: "" };
                loading.startLoading("UsersController, initialize - darUsuarios");
                var promesa = UsersService.darUsuarios(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.users = angular.copy(o.ListaUsuario);
                        self.users.map(function (o) {
                            var list = o.Ciudad.split(',');
                            o.CiudadesList = list;
                        });

                        self.total = self.users.length;
                        self.filtered = self.users;
                        self.noOfPages = Math.ceil(self.users.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("UsersController, initialize - darUsuarios");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UsersController, initialize - darUsuarios");
                });
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }]);
