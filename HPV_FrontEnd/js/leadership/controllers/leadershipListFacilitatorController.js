'use strict';

angular.module('prosperidad.leadership')
    .controller('LeadershipListFacilitatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$window', 'filterFilter',
        'CommonsConstants', '$state', 'SessionsBusiness', 'LeadershipService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $window, filterFilter,
        CommonsConstants, $state, SessionsBusiness, LeadershipService) {

        if (!SessionsBusiness.authorized(47)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;

            self.leadershipList = [];
            self.leadershipStates = [];
            self.selectedLeadershipState = "";
            self.submitSearch = submitSearch;
            self.identifySeasson = identifySeasson;
            self.goToLeadership = goToLeadership;
            self.createLeadership = createLeadership;
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
                self.filtered = filterFilter(self.leadershipList, term);
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
                loading.startLoading("LeadershipListFacilitatorController, submitSearch - darLiderazgos()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    FiltroBusqueda: $scope.search,
                    EstadoXConsultar: self.selectedLeadershipState
                }
                var promesa = LeadershipService.darLiderazgos(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.leadershipList = o.ListaLiderazgo;
                        self.total = self.leadershipList.length;
                        self.filtered = self.leadershipList;
                        self.noOfPages = Math.ceil(self.leadershipList.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("LeadershipListFacilitatorController, submitSearch - darLiderazgos()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipListFacilitatorController, submitSearch - darLiderazgos()");
                });
            }

            function goToLeadership(id) {
                $state.go("leadershipDetailFacilitator", { id: id });
            }

            function createLeadership() {

                loading.startLoading("LeadershipListFacilitatorController, createLeadership - validarLiderazgo()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                }
                var promesa = LeadershipService.validarLiderazgo(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {

                        $state.go("leadershipDetailFacilitatorCreate");

                    }
                    loading.stopLoading("LeadershipListFacilitatorController, createLeadership - validarLiderazgo()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipListFacilitatorController, createLeadership - validarLiderazgo()");
                });

            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
