'use strict';

angular.module('prosperidad.successCase')
    .controller('SuccessCaseListFacilitatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$window', 'filterFilter',
        'CommonsConstants', '$state', 'SessionsBusiness', 'SuccessCaseService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $window, filterFilter,
        CommonsConstants, $state, SessionsBusiness, SuccessCaseService) {

        if (!SessionsBusiness.authorized(42)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;

            self.successCaseList = [];
            self.successCaseStates = [];
            self.selectedSuccessCaseState = "";
            self.submitSearch = submitSearch;
            self.identifySeasson = identifySeasson;
            self.goToSuccessCase = goToSuccessCase;
            self.createSuccessCase = createSuccessCase;
            $scope.search;

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

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.successCaseList, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {

                self.submitSearch();
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
                loading.startLoading("SuccessCaseListFacilitatorController, submitSearch - darCasosDeExito()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    FiltroBusqueda: $scope.search,
                    EstadoXConsultar: self.selectedSuccessCaseState
                }
                var promesa = SuccessCaseService.darCasosDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.successCaseList = o.ListaCasoDeExito;
                        self.total = self.successCaseList.length;
                        self.filtered = self.successCaseList;
                        self.noOfPages = Math.ceil(self.successCaseList.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("SuccessCaseListFacilitatorController, submitSearch - darCasosDeExito()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseListFacilitatorController, submitSearch - darCasosDeExito()");
                });
            }

            function goToSuccessCase(id) {
                $state.go("successCaseDetailFacilitator", { id: id });
            }

            function createSuccessCase() {

                loading.startLoading("SuccessCaseListFacilitatorController, createSuccessCase - validarCasoDeExito()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                }
                var promesa = SuccessCaseService.validarCasoDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {

                        $state.go("successCaseDetailFacilitatorCreate");

                    }
                    loading.stopLoading("SuccessCaseListFacilitatorController, createSuccessCase - validarCasoDeExito()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseListFacilitatorController, createSuccessCase - validarCasoDeExito()");
                });

            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
