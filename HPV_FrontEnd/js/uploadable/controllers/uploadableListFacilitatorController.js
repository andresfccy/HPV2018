'use strict';

angular.module('prosperidad.uploadable')
    .controller('UploadableListFacilitatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'loading', '$http',
        'CommonsConstants', '$log', 'UploadableService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
        'AttendanceService',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, loading, $http,
        CommonsConstants, $log, UpleadableService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter,
        AttendanceService) {

        if (!SessionsBusiness.authorized(28)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.submit = submit;

            self.uploadableList = [];
            self.uploadables = [];
            self.seasons = [];
            self.workshops = [];
            self.documentsStates = [];
            self.assistanceStates = [];
            self.selectedDocumentState = "S";
            self.selectedAttendanceState = "S";
            self.selectedSeason;
            self.selectedUploadable = 1;
            self.selectedWorkshop = 0;
            self.submitSearch = submitSearch;
            self.identifySeasson = identifySeasson;

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
            $scope.search;

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.uploadableList, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {
                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(ENTREGABLES)");
                var req = { Categoria: "ENTREGABLE" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadables = o.ListaValor;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ENTREGABLES)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ENTREGABLES)");
                });

                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                var req = { Categoria: "ESTADOSDOCUMENTO" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.documentsStates = o.ListaValor;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                });

                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSASISTENCIA)");
                var req = { Categoria: "ESTADOSASISTENCIA" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.assistanceStates = o.ListaValor;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSASISTENCIA)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSASISTENCIA)");
                });

                var req = { Categoria: "PERIODO" };
                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                        self.submitSearch();
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(PERIODO)");
                });

                loading.startLoading("UploadableListFacilitatorController, initialize - darTalleres()");
                var req = {}
                var promesa = AttendanceService.darTalleres().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.workshops = o.ListaTaller;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darTalleres()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darTalleres()");
                });
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
                console.log("Entro submitSearch")
                loading.startLoading("UploadableListFacilitatorController, submitSearch - darEntregableEstado()");
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdTaller: self.selectedWorkshop,
                    IdEntregable: self.selectedUploadable,
                    IdPeriodo: self.selectedSeason,
                    FiltroBusqueda: $scope.search,
                    IdEstadoDocumento: self.selectedDocumentState,
                    IdEstadoAsistencia: self.selectedAttendanceState
                }
                var promesa = AttendanceService.darEntregableEstado(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadableList = o.ListaEntregableEstado;
                        self.total = self.uploadableList.length;
                        self.filtered = self.uploadableList;
                        self.noOfPages = Math.ceil(self.uploadableList.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("UploadableListFacilitatorController, submitSearch - darEntregableEstado()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, submitSearch - darEntregableEstado()");
                });
            }

            function submit() {
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
