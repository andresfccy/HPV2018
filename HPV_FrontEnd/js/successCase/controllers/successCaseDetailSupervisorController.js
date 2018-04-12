'use strict';

angular.module('prosperidad.successCase')
    .controller('SuccessCaseDetailSupervisorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'SuccessCaseService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, SuccessCaseService) {

        if (!SessionsBusiness.authorized(46)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.initializeForm = initializeForm;
            self.form;
            self.goBack = goBack;

            self.successCase = {};
            self.selectedSeason;

            self.identifySeasson = identifySeasson;

            self.choose = choose;
            self.noChoose = noChoose;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("SuccessCaseDetailSupervisorController, initialize - darLista(PERIODO)");
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
                    loading.stopLoading("SuccessCaseDetailSupervisorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailSupervisorController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdCasoDeExito: $stateParams.id
                };
                loading.startLoading("SuccessCaseDetailSupervisorController, initialize - consultarCasoDeExito");
                var promesa = SuccessCaseService.consultarCasoDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.successCase = o.CasoDeExito;

                        loading.startLoading("SuccessCaseDetailSupervisorController, initialize - darLogros()");
                        var req = {}
                        var promesa = SuccessCaseService.darLogros(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.achievements = o.ListaLogro;
                                console.log(self.achievements)
                                var l = self.achievements.map(function (l) {
                                    if (l.IdLogro == self.successCase.IdLogro) { l.yesno = true; }

                                    self.successCase.Logros.split(',').map(function (o) { if (o == l.IdLogro && o != "") { l.yesno = true; } });
                                });
                                self.selectedAchievement = l[0];
                            }
                            loading.stopLoading("SuccessCaseDetailSupervisorController, initialize - darLogros()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("SuccessCaseDetailSupervisorController, initialize - darLogros()");
                        });

                        var req = { Categoria: "CUALIDADCASOEXITO" };
                        loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(CUALIDADCASOEXITO)");
                        var promesa = CommonsListasService.darLista(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.qualities = o.ListaValor;
                                var sCQuialities = self.successCase.Criterios.split(';');
                                self.qualities.map(function (o, idx) {
                                    if (o.Valor + "" == sCQuialities[idx].split(',')[0]) {
                                        o.yesno = (sCQuialities[idx].split(',')[1] == "S" ? 1 : 0);
                                    }
                                });
                                console.log(self.qualities);
                            }
                            loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(CUALIDADCASOEXITO)");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(CUALIDADCASOEXITO)");
                        });

                    }
                    loading.stopLoading("SuccessCaseDetailSupervisorController, initialize - consultarCasoDeExito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailSupervisorController, initialize - consultarCasoDeExito");
                });
            }

            function choose() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdCasoDeExito: $stateParams.id,
                    MotivoRechazo: "",
                    Estado: "E"
                };
                loading.startLoading("SuccessCaseDetailSupervisorController, accept - registrarEstadoCasoDeExito");
                var promesa = SuccessCaseService.registrarEstadoCasoDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Caso de éxito elegido con éxito.");
                        $state.go("successCaseListSupervisor");
                    }
                    loading.stopLoading("SuccessCaseDetailSupervisorController, accept - registrarEstadoCasoDeExito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailSupervisorController, accept - registrarEstadoCasoDeExito");
                });
            }

            function noChoose() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdCasoDeExito: $stateParams.id,
                    MotivoRechazo: self.successCase.MotivoRechazo,
                    Estado: "A"
                };
                loading.startLoading("SuccessCaseDetailSupervisorController, reject - registrarEstadoCasoDeExito");
                var promesa = SuccessCaseService.registrarEstadoCasoDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Se excluýó el caso de éxito de los elegidos.");
                        $state.go("successCaseListSupervisor");
                    }
                    loading.stopLoading("SuccessCaseDetailSupervisorController, reject - registrarEstadoCasoDeExito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailSupervisorController, reject - registrarEstadoCasoDeExito");
                });
            }

            function initializeForm(f) {
                self.form = f;
            }

            function goBack() {
                $state.go("successCaseListSupervisor");
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
