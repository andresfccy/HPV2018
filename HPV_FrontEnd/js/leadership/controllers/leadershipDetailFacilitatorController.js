'use strict';

angular.module('prosperidad.leadership')
    .controller('LeadershipDetailFacilitatorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'LeadershipService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, LeadershipService) {

        if (!SessionsBusiness.authorized(47)) {
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
            self.groups = [];
            self.seasons = [];
            self.qualities = [{}];
            self.participants;
            self.selectedSeason;
            self.selectedAchievement;

            self.getParticipantsByGroup = getParticipantsByGroup;
            self.editionDisabled = editionDisabled;
            self.identifySeasson = identifySeasson;
            self.rechazado = false;

            self.submit = submit;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("LeadershipDetailFacilitatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                    }
                    loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darLista(PERIODO)");
                });



                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("LeadershipDetailFacilitatorController, initialize - darGruposXFacilitador");
                var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.groups = o.ListaGrupo;
                        var i = -1;
                        var i2 = -1;
                        self.groups.some(function (l, idx) { if (l.IdGrupo == 11) { i = idx; return true; } else if (l.IdGrupo == 12) { i2 = idx; return true; } })
                        //self.groups.splice(i, 1);
                        //self.groups.splice(i2, 1);
                        if (i != -1)
                            self.groups.splice(i, 1);
                        if (i2 != -1)
                            self.groups.splice(i2, 1);
                        self.getParticipantsByGroup();
                    }
                    loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darGruposXFacilitador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darGruposXFacilitador");
                });

                if (isEdition()) {
                    self.sent = true;
                    var req = {
                        IdLiderazgo: $stateParams.id
                    };
                    loading.startLoading("LeadershipDetailFacilitatorController, initialize - consultarLiderazgo");
                    var promesa = LeadershipService.consultarLiderazgo(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.leadership = o.Liderazgo;
                            if (self.leadership.IdEstado == "R") {
                                self.rechazado = true;
                                self.sent = false;
                            }

                            var req = {
                                IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                            };
                            loading.startLoading("LeadershipDetailFacilitatorController, initialize - darGruposXFacilitador");
                            var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    self.groups = o.ListaGrupo;
                                    var i = -1;
                                    self.groups.some(function (l, idx) { if (l.IdGrupo == 11) { i = idx; return true; } })
                                    self.groups.splice(i, 1);
                                    self.getParticipantsByGroup();
                                }
                                loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darGruposXFacilitador");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darGruposXFacilitador");
                            });

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
                        loading.stopLoading("LeadershipDetailFacilitatorController, initialize - consultarLiderazgo");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LeadershipDetailFacilitatorController, initialize - consultarLiderazgo");
                    });
                }
                else {
                    var req = { Categoria: "CUALIDADLIDERAZGO" };
                    loading.startLoading("LeadershipDetailFacilitatorController, initialize - darLista(CUALIDADLIDERAZGO)");
                    var promesa = CommonsListasService.darLista(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.qualities = o.ListaValor;
                        }
                        loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darLista(CUALIDADLIDERAZGO)");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LeadershipDetailFacilitatorController, initialize - darLista(CUALIDADLIDERAZGO)");
                    });
                }
            }

            function getParticipantsByGroup() {
                if (self.leadership.IdGrupoFacilitador && self.leadership.IdGrupoFacilitador != 0) {
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdGrupoFacilitador: self.leadership.IdGrupoFacilitador
                    };
                    loading.startLoading("LeadershipDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    var promesa = AttendanceService.darInscritos(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.participants = o.ListaInscrito;
                            if (self.participants.length == 0) {
                                growl.warning("No se encontraron inscritos en este grupo.");
                            }
                        }
                        loading.stopLoading("LeadershipDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LeadershipDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    });
                }
            }

            function submit() {
                var withoutYesNo = self.qualities.some(function (o) {
                    if (o.yesno != 1 && o.yesno != 0) {
                        return true;
                    }
                });
                if (withoutYesNo) {
                    growl.warning("Se debe contestar la totalidad de la encuesta.")
                }
                else
                    if (self.form.$valid) {

                        var qualitiesString = "";
                        self.qualities.map(function (o) {
                            qualitiesString += ";" + o.Valor + "," + (o.yesno == 1 ? 'S' : 'N');
                        });
                        qualitiesString = qualitiesString.substr(1);
                        self.leadership.Criterios = qualitiesString;
                        self.leadership.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                        if (!isEdition())
                            self.leadership.IdLiderazgo = 0;
                        console.log(self.leadership);

                        loading.startLoading("LeadershipDetailFacilitatorController, submit - ActualizarLiderazgo()");
                        var req = {
                            IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                            Liderazgo: self.leadership
                        }
                        var promesa = LeadershipService.actualizarLiderazgo(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                growl.success("El caso de liderazgo ha sido registrado exitosamente.");
                                $state.go("leadershipListFacilitator");
                                self.sent = true;
                            }
                            loading.stopLoading("LeadershipDetailFacilitatorController, submit - ActualizarLiderazgo()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("LeadershipDetailFacilitatorController, submit - ActualizarLiderazgo()");
                        });
                    }
            }

            function filteredParticipants() {
                if (self.participantsList && self.attendanceList)
                    return self.participantsList.filter(function (o) {
                        var esta = false;
                        self.attendanceList.map(function (p) {
                            if (o.IdInscrito == p.IdInscrito) {
                                esta = true;
                            }
                        })
                        if (!esta) {
                            return o;
                        }
                    })
            }

            function initializeForm(f) {
                self.form = f;
            }

            function editionDisabled() {
                if (self.leadership.IdEstado == "R" || !self.leadership.IdEstado || self.leadership.IdEstado == "R") {
                    return false;
                }
                return true;
            }

            function goBack() {
                $state.go("leadershipListFacilitator");
            }

            function isEdition() {
                if ($stateParams.id && $stateParams.id != "Crear") {
                    return true;
                }
                return false;
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
