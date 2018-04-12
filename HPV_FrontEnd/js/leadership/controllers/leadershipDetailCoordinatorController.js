'use strict';

angular.module('prosperidad.leadership')
    .controller('LeadershipDetailCoordinatorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'LeadershipService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, LeadershipService) {

        if (!SessionsBusiness.authorized(48)) {
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

            self.accept = accept;
            self.reject = reject;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("LeadershipDetailCoordinatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                    }
                    loading.stopLoading("LeadershipDetailCoordinatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailCoordinatorController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdLiderazgo: $stateParams.id
                };
                loading.startLoading("LeadershipDetailCoordinatorController, initialize - consultarLiderazgo");
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
                    loading.stopLoading("LeadershipDetailCoordinatorController, initialize - consultarLiderazgo");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailCoordinatorController, initialize - consultarLiderazgo");
                });
            }

            function accept() {
                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdLiderazgo: $stateParams.id,
                    MotivoRechazo: "",
                    Estado: "A"
                };
                loading.startLoading("LeadershipDetailCoordinatorController, accept - registrarEstadoLiderazgo");
                var promesa = LeadershipService.registrarEstadoLiderazgo(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Caso de liderazgo aceptado con éxito.");
                        $state.go("leadershipListCoordinator");
                    }
                    loading.stopLoading("LeadershipDetailCoordinatorController, accept - registrarEstadoLiderazgo");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailCoordinatorController, accept - registrarEstadoLiderazgo");
                });
            }

            function reject() {
                if (self.leadership.MotivoRechazo && self.leadership.MotivoRechazo != "") {
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdLiderazgo: $stateParams.id,
                        MotivoRechazo: self.leadership.MotivoRechazo,
                        Estado: "R"
                    };
                    loading.startLoading("LeadershipDetailCoordinatorController, reject - registrarEstadoLiderazgo");
                    var promesa = LeadershipService.registrarEstadoLiderazgo(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Caso de liderazgo rechazado con éxito.");
                            $state.go("leadershipListCoordinator");
                        }
                        loading.stopLoading("LeadershipDetailCoordinatorController, reject - registrarEstadoLiderazgo");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LeadershipDetailCoordinatorController, reject - registrarEstadoLiderazgo");
                    });
                }
                else {
                    growl.warning("Se debe especificar la razón por la cual se rechaza el caso de liderazgo.");
                }
            }

            function initializeForm(f) {
                self.form = f;
            }

            function editionDisabled() {
                if (self.leadership.IdEstado == "P" || self.leadership.IdEstado == "A") {
                    return true;
                }
            }

            function goBack() {
                $state.go("leadershipListCoordinator");
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
