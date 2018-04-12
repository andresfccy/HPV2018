'use strict';

angular.module('prosperidad.searches')
    .controller('SearchesUsersListController',
    ['$rootScope','$scope', '$location', '$translate', 'growl', 'SearchesService', 'loading',
        'CommonsConstants', '$state', 'CommonsListasService', 'SessionsBusiness',
    function ($rootScope, $scope, $location, $translate, growl, SearchesService, loading,
        CommonsConstants, $state, CommonsListasService, SessionsBusiness) {

        if (!SessionsBusiness.authorized(31)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;

            self.search = {
                IdPeriodo: "1"
            };
            self.seassons = [];
            self.users = null;
            self.opcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            $scope.maxResultsPerPageus = 5;
            self.maxSize = 5;
            self.currentPage = 1;
            self.noOfPages = 0;
            self.total = 0;
            self.filtered = [];

            self.submit = submit;
            self.identifySeasson = identifySeasson;

            self.rolId = SessionsBusiness.getRolFromLocalStorage();
            $scope.$watch('maxResultsPerPageus', function (newv, oldv) {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPageus);
            });

            function initialize() {
                if (self.rolId == 6 || self.rolId == 7) {
                    self.supervisor = true;
                }

                var req = { Categoria: "PERIODO" };
                loading.startLoading("SearchesUsersListController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seassons = o.ListaValor;
                        self.search.IdPeriodo = self.identifySeasson();
                        submit();
                    }
                    loading.stopLoading("SearchesUsersListController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SearchesUsersListController, initialize - darLista(PERIODO)");
                });
            }

            function submit() {
                if (!self.supervisor) {
                    self.search.IdPeriodo = 0;
                }
                var req = angular.copy(self.search);
                loading.startLoading("SearchesUsersListController, submit - ConsultarInscritos");
                var promesa = SearchesService.ConsultarInscritos(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.users = angular.copy(o.ListaInscrito);
                        self.total = self.users.length;
                        self.filtered = self.users;
                        console.log($scope.maxResultsPerPageus)
                        self.noOfPages = Math.ceil(self.users.length / $scope.maxResultsPerPageus);
                    }
                    loading.stopLoading("SearchesUsersListController, submit - ConsultarInscritos");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SearchesUsersListController, submit - ConsultarInscritos");
                });
            }

            function identifySeasson() {
                var value = -1;
                if (self.seassons && self.getSeason()) {
                    self.seassons.map(function (o) {
                        if (self.getSeason().NomPeriodo == o.Descripcion) {
                            value = o.Valor;
                        }
                    })
                }
                return value;
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
