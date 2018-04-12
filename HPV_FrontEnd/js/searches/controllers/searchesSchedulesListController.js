'use strict';

angular.module('prosperidad.searches')
    .controller('searchesSchedulesListController',
    ['$rootScope', '$scope', '$location', '$translate', 'growl', 'SearchesService', 'loading',
        'CommonsConstants', '$state', 'CommonsListasService', 'SessionsBusiness',
    function ($rootScope, $scope, $location, $translate, growl, SearchesService, loading,
        CommonsConstants, $state, CommonsListasService, SessionsBusiness) {

        if (!SessionsBusiness.authorized(32)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.seasons = [];
            self.identifySeasson = identifySeasson;
            self.search = {
                IdPeriodo: "",
                FiltroBusqueda: ""
            };
            self.schedules = [];
            self.parameters = [{}];
            self.paramsString = [];
            self.maxParameters = 6;
            self.opcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            $scope.maxResultsPerPage = 20;
            self.maxSize = 5;
            self.currentPage = 1;
            self.noOfPages = 0;
            self.total = 0;
            self.filtered = [];

            self.submit = submit;
            self.addParameter = addParameter;
            self.removeParameter = removeParameter;
            self.verifyIfEmpty = verifyIfEmpty;
            self.sendSearch = sendSearch;

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("searchesSchedulesListController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.search.IdPeriodo = self.identifySeasson();
                        console.log(self.search.IdPeriodo);
                        self.submit();
                    }
                    loading.stopLoading("searchesSchedulesListController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("searchesSchedulesListController, initialize - darLista(PERIODO)");
                });
            }

            function submit() {
                var req = angular.copy(self.search);
                loading.startLoading("searchesSchedulesListController, submit - ConsultarHorarios");
                var promesa = SearchesService.ConsultarHorarios(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.schedules = angular.copy(o.ListaHorario);
                        self.total = self.schedules.length;
                        self.filtered = self.schedules;
                        self.noOfPages = Math.ceil(self.schedules.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("searchesSchedulesListController, submit - ConsultarHorarios");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("searchesSchedulesListController, submit - ConsultarHorarios");
                });
            }

            function removeParameter(i) {
                self.parameters.splice(i, 1);
                self.paramsString.splice(i, 1);
            }

            function verifyIfEmpty(i) {
                if (self.paramsString[i] == "") {
                    self.removeParameter(i);
                }
            }

            function addParameter() {
                var i = {};
                if (self.parameters.length < self.maxParameters &&
                    self.parameters.length == self.paramsString.length) {
                    self.parameters.push(i);
                }
            }

            function identifySeasson() {
                var value = -1;
                if (self.seasons && getSeason()) {
                    console.log("entró")
                    self.seasons.map(function (o) {
                        console.log(o)
                        if (getSeason().NomPeriodo == o.Descripcion) {
                            console.log("entró if")
                            value = o.Valor;
                        }
                    })
                }
                return value;
            }

            function sendSearch() {
                self.search.FiltroBusqueda = "";
                self.paramsString.map(function (o) {
                    self.search.FiltroBusqueda += "," + o;
                })
                self.search.FiltroBusqueda = self.search.FiltroBusqueda.substr(1);
                self.submit();
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
