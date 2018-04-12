'use strict';

angular.module('prosperidad.lifeStory')
    .controller('LifeStoryDetailCoordinatorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'LifeStoryService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, LifeStoryService) {

        if (!SessionsBusiness.authorized(43)) {
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

            self.accept = accept;
            self.reject = reject;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("LifeStoryDetailCoordinatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                    }
                    loading.stopLoading("LifeStoryDetailCoordinatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailCoordinatorController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdHistoriaDeVida: $stateParams.id
                };
                loading.startLoading("LifeStoryDetailCoordinatorController, initialize - consultarHistoriaVida");
                var promesa = LifeStoryService.consultarHistoriaVida(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.lifeStory = o.HistoriaDeVida;
                    }
                    loading.stopLoading("LifeStoryDetailCoordinatorController, initialize - consultarHistoriaVida");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailCoordinatorController, initialize - consultarHistoriaVida");
                });
            }

            function accept() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdHistoriaVida: $stateParams.id,
                    MotivoRechazo: "",
                    Estado: "A"
                };
                loading.startLoading("LifeStoryDetailCoordinatorController, accept - registrarEstadoHistoriaVida");
                var promesa = LifeStoryService.registrarEstadoHistoriaVida(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Historia de vida aceptada con éxito.");
                        $state.go("lifeStoryListCoordinator");
                    }
                    loading.stopLoading("LifeStoryDetailCoordinatorController, accept - registrarEstadoHistoriaVida");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailCoordinatorController, accept - registrarEstadoHistoriaVida");
                });
            }

            function reject() {
                if (self.lifeStory.MotivoRechazo && self.lifeStory.MotivoRechazo != "") {
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdHistoriaVida: $stateParams.id,
                        MotivoRechazo: self.lifeStory.MotivoRechazo,
                        Estado: "R"
                    };
                    loading.startLoading("LifeStoryDetailCoordinatorController, reject - registrarEstadoHistoriaVida");
                    var promesa = LifeStoryService.registrarEstadoHistoriaVida(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Historia de vida rechazada con éxito.");
                            $state.go("lifeStoryListCoordinator");
                        }
                        loading.stopLoading("LifeStoryDetailCoordinatorController, reject - registrarEstadoHistoriaVida");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LifeStoryDetailCoordinatorController, reject - registrarEstadoHistoriaVida");
                    });
                }
                else {
                    growl.warning("Se debe especificar la razón por la cual se rechaza la historia de vida.");
                }
            }

            function seeDocument() {
                loading.startLoading("LifeStoryDetailCoordinatorController, seeDocument - $http");
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
                    loading.stopLoading("LifeStoryDetailCoordinatorController, seeDocument - $http");
                }).error(function (data, status, headers, config) {
                    if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                    if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                    loading.stopLoading("LifeStoryDetailCoordinatorController, submit - $http");
                });

            }

            function initializeForm(f) {
                self.form = f;
            }

            function goBack() {
                $state.go("lifeStoryListCoordinator");
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
