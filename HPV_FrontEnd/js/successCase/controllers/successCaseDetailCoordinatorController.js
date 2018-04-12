'use strict';

angular.module('prosperidad.successCase')
    .controller('SuccessCaseDetailCoordinatorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'SuccessCaseService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, SuccessCaseService) {

        if (!SessionsBusiness.authorized(44)) {
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

            self.accept = accept;
            self.reject = reject;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("SuccessCaseDetailCoordinatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                    }
                    loading.stopLoading("SuccessCaseDetailCoordinatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailCoordinatorController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdCasoDeExito: $stateParams.id
                };
                loading.startLoading("SuccessCaseDetailCoordinatorController, initialize - consultarCasoDeExito");
                var promesa = SuccessCaseService.consultarCasoDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.successCase = o.CasoDeExito;

                        loading.startLoading("SuccessCaseDetailCoordinatorController, initialize - darLogros()");
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
                            loading.stopLoading("SuccessCaseDetailCoordinatorController, initialize - darLogros()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("SuccessCaseDetailCoordinatorController, initialize - darLogros()");
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
                    loading.stopLoading("SuccessCaseDetailCoordinatorController, initialize - consultarCasoDeExito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailCoordinatorController, initialize - consultarCasoDeExito");
                });
            }

            function accept() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdCasoDeExito: $stateParams.id,
                    MotivoRechazo: "",
                    Estado: "A"
                };
                loading.startLoading("SuccessCaseDetailCoordinatorController, accept - registrarEstadoCasoDeExito");
                var promesa = SuccessCaseService.registrarEstadoCasoDeExito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Caso de éxito aceptado con éxito.");
                        $state.go("successCaseListCoordinator");
                    }
                    loading.stopLoading("SuccessCaseDetailCoordinatorController, accept - registrarEstadoCasoDeExito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailCoordinatorController, accept - registrarEstadoCasoDeExito");
                });
            }

            function reject() {
                if (self.successCase.MotivoRechazo && self.successCase.MotivoRechazo != "") {
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdCasoDeExito: $stateParams.id,
                        MotivoRechazo: self.successCase.MotivoRechazo,
                        Estado: "R"
                    };
                    loading.startLoading("SuccessCaseDetailCoordinatorController, reject - registrarEstadoCasoDeExito");
                    var promesa = SuccessCaseService.registrarEstadoCasoDeExito(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Caso de éxito rechazado con éxito.");
                            $state.go("successCaseListCoordinator");
                        }
                        loading.stopLoading("SuccessCaseDetailCoordinatorController, reject - registrarEstadoCasoDeExito");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("SuccessCaseDetailCoordinatorController, reject - registrarEstadoCasoDeExito");
                    });
                }
                else {
                    growl.warning("Se debe especificar la razón por la cual se rechaza el caso de éxito.");
                }
            }

            function initializeForm(f) {
                self.form = f;
            }

            function editionDisabled() {
                if (self.successCase.IdEstado == "P" || self.successCase.IdEstado == "A") {
                    return true;
                }
            }

            function goBack() {
                $state.go("successCaseListCoordinator");
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
