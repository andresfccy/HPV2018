'use strict';

angular.module('prosperidad.leadership')
    .controller('LeadershipDetailSupervisorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'LeadershipService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, LeadershipService) {

        if (!SessionsBusiness.authorized(49)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.initializeForm = initializeForm;
            self.form;
            self.goBack = goBack;

            self.leadership = {};
            self.selectedSeason;

            self.identifySeasson = identifySeasson;

            self.choose = choose;
            self.noChoose = noChoose;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("LeadershipDetailSupervisorController, initialize - darLista(PERIODO)");
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
                    loading.stopLoading("LeadershipDetailSupervisorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailSupervisorController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdLiderazgo: $stateParams.id
                };
                loading.startLoading("LeadershipDetailSupervisorController, initialize - consultarLiderazgo");
                var promesa = LeadershipService.consultarLiderazgo(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.leadership = o.Liderazgo;

                        var req = { Categoria: "CUALIDADLIDERAZGO" };
                        loading.startLoading("LeadershipDetailFacilitatorController, initialize - darLista(CUALIDADLIDERAZGO)");
                        var promesa = CommonsListasService.darLista(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.qualities = o.ListaValor;
                                var sCQuialities = self.leadership.Criterios.split(';');
                                self.qualities.map(function (o, idx) {
                                    if (o.Valor + "" == sCQuialities[idx].split(',')[0]) {
                                        o.yesno = (sCQuialities[idx].split(',')[1] == "S" ? 1 : 0);
                                    }
                                });
                                console.log(self.qualities);
                            }
                            loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darLista(CUALIDADLIDERAZGO)");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darLista(CUALIDADLIDERAZGO)");
                        });

                    }
                    loading.stopLoading("LeadershipDetailSupervisorController, initialize - consultarLiderazgo");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailSupervisorController, initialize - consultarLiderazgo");
                });
            }

            function choose() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdLiderazgo: $stateParams.id,
                    MotivoRechazo: "",
                    Estado: "E"
                };
                loading.startLoading("LeadershipDetailSupervisorController, accept - registrarEstadoLiderazgo");
                var promesa = LeadershipService.registrarEstadoLiderazgo(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Caso de liderazgo elegido con éxito.");
                        $state.go("leadershipListSupervisor");
                    }
                    loading.stopLoading("LeadershipDetailSupervisorController, accept - registrarEstadoLiderazgo");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailSupervisorController, accept - registrarEstadoLiderazgo");
                });
            }

            function noChoose() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdLiderazgo: $stateParams.id,
                    MotivoRechazo: self.leadership.MotivoRechazo,
                    Estado: "A"
                };
                loading.startLoading("LeadershipDetailSupervisorController, reject - registrarEstadoLiderazgo");
                var promesa = LeadershipService.registrarEstadoLiderazgo(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Se excluýó el caso de liderazgo de los elegidos.");
                        $state.go("leadershipListSupervisor");
                    }
                    loading.stopLoading("LeadershipDetailSupervisorController, reject - registrarEstadoLiderazgo");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailSupervisorController, reject - registrarEstadoLiderazgo");
                });
            }

            function initializeForm(f) {
                self.form = f;
            }

            function goBack() {
                $state.go("leadershipListSupervisor");
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
