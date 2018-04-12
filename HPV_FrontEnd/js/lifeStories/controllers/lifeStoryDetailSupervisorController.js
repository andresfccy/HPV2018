'use strict';

angular.module('prosperidad.lifeStory')
    .controller('LifeStoryDetailSupervisorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'LifeStoryService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, LifeStoryService) {

        if (!SessionsBusiness.authorized(45)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.initializeForm = initializeForm;
            self.form;
            self.goBack = goBack;

            self.lifeStory = {};
            self.storyTypes = [];
            self.groups = [];
            self.participants;
            self.selectedSeason
            self.modifyDocument = false;

            self.seeDocument = seeDocument;
            self.identifySeasson = identifySeasson;

            self.choose = choose;
            self.noChoose = noChoose;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("LifeStoryDetailSupervisorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();

                        if ($rootScope.selectedSeason == self.selectedSeason) {
                            $rootScope.selectedSeason = { p: "Se reinicia la variable para evitar que se acceda a la vista del detalle si no es por la lista." };
                            self.enabled = true;
                        }
                    }
                    loading.stopLoading("LifeStoryDetailSupervisorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailSupervisorController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdHistoriaDeVida: $stateParams.id
                };
                loading.startLoading("LifeStoryDetailSupervisorController, initialize - consultarHistoriaVida");
                var promesa = LifeStoryService.consultarHistoriaVida(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.lifeStory = o.HistoriaDeVida;
                        self.selectedSeason = self.lifeStory.IdPeriodo;
                    }
                    loading.stopLoading("LifeStoryDetailSupervisorController, initialize - consultarHistoriaVida");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailSupervisorController, initialize - consultarHistoriaVida");
                });
            }

            function choose() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdHistoriaVida: $stateParams.id,
                    MotivoRechazo: "",
                    Estado: "E"
                };
                loading.startLoading("LifeStoryDetailSupervisorController, accept - registrarEstadoHistoriaVida");
                var promesa = LifeStoryService.registrarEstadoHistoriaVida(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Historia de vida elegida con éxito.");
                        $state.go("lifeStoryListSupervisor");
                    }
                    loading.stopLoading("LifeStoryDetailSupervisorController, accept - registrarEstadoHistoriaVida");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailSupervisorController, accept - registrarEstadoHistoriaVida");
                });
            }

            function noChoose() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdHistoriaVida: $stateParams.id,
                    MotivoRechazo: self.lifeStory.MotivoRechazo,
                    Estado: "A"
                };
                loading.startLoading("LifeStoryDetailSupervisorController, reject - registrarEstadoHistoriaVida");
                var promesa = LifeStoryService.registrarEstadoHistoriaVida(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Se excluyó la historia de vida de las elegidas.");
                        $state.go("lifeStoryListSupervisor");
                    }
                    loading.stopLoading("LifeStoryDetailSupervisorController, reject - registrarEstadoHistoriaVida");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailSupervisorController, reject - registrarEstadoHistoriaVida");
                });
            }

            function seeDocument() {
                loading.startLoading("LifeStoryDetailSupervisorController, seeDocument - $http");
                $http({
                    method: 'POST',
                    url: Connection.API_BASE_URL() + '/Entregable/ConsultarEntregable.aspx'
                            + '?IdFacilitador=' + SessionsBusiness.getUserIdFromLocalStorage()
                            + '&IdTaller=' + self.lifeStory.IdInscrito
                            + '&IdEntregable=' + 3
                            + '&IdPeriodo=' + self.selectedSeason
                            + '&IdGrupoFacilitador=' + self.lifeStory.IdGrupoFacilitador,
                    responseType: "arraybuffer"
                }).success(function (data) {
                    var file = new Blob([data], { type: 'application/pdf' });
                    var fileURL = URL.createObjectURL(file);
                    window.open(fileURL, "_blank");
                    loading.stopLoading("LifeStoryDetailSupervisorController, seeDocument - $http");
                }).error(function (data, status, headers, config) {
                    if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                    if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                    loading.stopLoading("LifeStoryDetailSupervisorController, submit - $http");
                });

            }

            function initializeForm(f) {
                self.form = f;
            }

            function goBack() {
                $state.go("lifeStoryListSupervisor");
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

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
