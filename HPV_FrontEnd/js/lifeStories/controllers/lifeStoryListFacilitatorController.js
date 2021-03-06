﻿'use strict';

angular.module('prosperidad.lifeStory')
    .controller('LifeStoryListFacilitatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$window', 'filterFilter',
        'CommonsConstants', '$state', 'SessionsBusiness', 'LifeStoryService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $window, filterFilter,
        CommonsConstants, $state, SessionsBusiness, LifeStoryService) {

        if (!SessionsBusiness.authorized(41)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;

            self.lifeStoryList = [];
            self.lifeStoryStates = [];
            self.selectedLifeStoryState = "";
            self.submitSearch = submitSearch;
            self.identifySeasson = identifySeasson;
            self.goToStoryLife = goToStoryLife;
            self.createStoryLife = createStoryLife;
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
                self.filtered = filterFilter(self.lifeStoryList, term);
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
                loading.startLoading("LifeStoryListFacilitatorController, submitSearch - darHistoriasDeVida()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    FiltroBusqueda: $scope.search,
                    FiltroEstado: self.selectedLifeStoryState
                }
                var promesa = LifeStoryService.darHistoriasDeVida(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.lifeStoryList = o.ListaHistoriaDeVida;
                        self.total = self.lifeStoryList.length;
                        self.filtered = self.lifeStoryList;
                        self.noOfPages = Math.ceil(self.lifeStoryList.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("LifeStoryListFacilitatorController, submitSearch - darHistoriasDeVida()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryListFacilitatorController, submitSearch - darHistoriasDeVida()");
                });
            }

            function goToStoryLife(id) {
                $state.go("lifeStoryDetailFacilitator", { id: id });
            }

            function createStoryLife() {
                loading.startLoading("LifeStoryListFacilitatorController, goToStoryLife - validarCantidadHistorias()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                }
                var promesa = LifeStoryService.validarCantidadHistorias(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        $state.go("lifeStoryDetailFacilitatorCreate");
                    }
                    loading.stopLoading("LifeStoryListFacilitatorController, goToStoryLife - validarCantidadHistorias()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryListFacilitatorController, goToStoryLife - validarCantidadHistorias()");
                });

            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
