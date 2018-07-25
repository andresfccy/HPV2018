﻿appSystematization
    .controller('SystematizationListAdminController',
    ['$rootScope', '$scope', '$location', '$state', '$translate',
        'growl', 'moment', 'loading', 'filterFilter',
        'CommonsConstants', 'CommonsListasService', 'SystematizationService', 'SessionsBusiness', 'CommonsListasService',
        function ($rootScope, $scope, $location, $state, $translate,
            growl, moment, loading, filterFilter,
            CommonsConstants, CommonListasService, SystematizationService, SessionsBusiness, CommonsListasService) {

            if (!SessionsBusiness.authorized(420)) {
                //if (false) {
                $state.go("home");
                growl.warning("Permisos insuficientes.");
            }
            else {
                var self = this;

                //Funciones
                self.init = initialize;
                self.goToSystematization = goToSystematization;
                self.submitSearch = submitSearch;

                // Variables
                self.opcLimit = [
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
                self.sysList;

                $scope.$watch('search', function (term) {
                    self.filtered = filterFilter(self.sysList, term) || [];
                    self.total = self.filtered.length;
                    self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
                });

                $scope.$watch('maxResultsPerPage', function () {
                    self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
                });

                function initialize() {
                    var req = { Categoria: "INSTRUMENTOS" };
                    var action = getCtrlName() + ", initialize - darLista(INSTRUMENTOS)";
                    loading.startLoading(action);
                    var promesa = CommonsListasService.darLista(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.instruments = o.ListaValor;
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                        });

                    submitSearch();
                }

                function identifySeasson() {
                    var value = -1;
                    if (self.seasons && getSeason()) {
                        self.seasons.map(function (o) {
                            if (getSeason().NomPeriodo == o.Descripcion) {
                                value = o.Valor;
                            }
                        })
                    }
                    return value;
                }

                function submitSearch() {
                    var action = getCtrlName() + ", submitSearch - darSistematizacionAdmin()"
                    loading.startLoading(action);
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        FiltroBusqueda: $scope.search,
                        InstrumentoXConsultar: self.selectedInst
                    }
                    var promesa = SystematizationService.darSistematizacion(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "111") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.sysList = o.ListaSistematizacion;
                            self.total = self.sysList.length;
                            self.filtered = self.sysList;
                            self.noOfPages = Math.ceil(self.sysList.length / $scope.maxResultsPerPage);
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });
                }

                function goToSystematization(id) {
                    $state.go("systematizationDetailAdmin", { id: id });
                }

                function getCtrlName() {
                    return "SystematizationListAdminController";
                }
            }
        }
    ]);