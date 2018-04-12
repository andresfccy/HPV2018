'use strict';

angular.module('prosperidad.successCase')
    .controller('SuccessCaseDetailFacilitatorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'SuccessCaseService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, SuccessCaseService) {

        if (!SessionsBusiness.authorized(42)) {
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
            self.achievements = [];
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
                loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                    }
                    loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(PERIODO)");
                });



                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - darGruposXFacilitador");
                var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.groups = o.ListaGrupo;
                        console.log(o);
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
                    loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darGruposXFacilitador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darGruposXFacilitador");
                });

                if (isEdition()) {
                    self.sent = true;
                    var req = {
                        IdCasoDeExito: $stateParams.id
                    };
                    loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - consultarCasoDeExito");
                    var promesa = SuccessCaseService.consultarCasoDeExito(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.successCase = o.CasoDeExito;
                            console.log("CE: ", o.CasoDeExito)
                            if (self.successCase.IdEstado == "R") {
                                self.rechazado = true;
                                self.sent = false;
                            }
                            loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - darLogros()");
                            var req = {}
                            var promesa = SuccessCaseService.darLogros(req).$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    self.achievements = o.ListaLogro;
                                    var l = self.achievements.map(function (l) {
                                        if (l.IdLogro == self.successCase.IdLogro) { l.yesno = true; }

                                        self.successCase.Logros.split(',').map(function (o) { if (o == l.IdLogro && o != "") { l.yesno = true; } });
                                    });
                                    console.log(self.achievements);
                                    self.selectedAchievement = l[0];
                                }
                                loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLogros()");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLogros()");
                            });

                            var req = {
                                IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                            };
                            loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - darGruposXFacilitador");
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
                                loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darGruposXFacilitador");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darGruposXFacilitador");
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
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - consultarCasoDeExito");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - consultarCasoDeExito");
                    });
                }
                else {
                    loading.startLoading("SuccessCaseDetailFacilitatorController, initialize - darLogros()");
                    var req = {}
                    var promesa = SuccessCaseService.darLogros(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.achievements = o.ListaLogro;
                        }
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLogros()");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLogros()");
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
                        }
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(CUALIDADCASOEXITO)");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, initialize - darLista(CUALIDADCASOEXITO)");
                    });
                }
            }

            function getParticipantsByGroup() {
                if (self.successCase.IdGrupoFacilitador && self.successCase.IdGrupoFacilitador != 0) {
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdGrupoFacilitador: self.successCase.IdGrupoFacilitador
                    };
                    loading.startLoading("SuccessCaseDetailFacilitatorController, getParticipantsByGroup - darInscritos");
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
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("SuccessCaseDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    });
                }
            }

            function submit() {
                var withoutYesNo = self.qualities.some(function (o) {
                    if (o.yesno != 1 && o.yesno != 0) {
                        return true;
                    }
                });
                var withoutYesNoAchievmnts = self.achievements.some(function (o) {
                    if (o.yesno) {
                        return true;
                    }
                });
                if (withoutYesNo) {
                    growl.warning("Se debe contestar la totalidad de la encuesta.")
                }
                else if (!withoutYesNoAchievmnts) {
                    growl.warning("Se debe seleccionar al menos un Logro.")
                }
                else
                    if (self.form.$valid) {

                        var qualitiesString = "";
                        self.qualities.map(function (o) {
                            qualitiesString += ";" + o.Valor + "," + (o.yesno == 1 ? 'S' : 'N');
                        });
                        qualitiesString = qualitiesString.substr(1);
                        self.successCase.Criterios = qualitiesString;

                        var achievementsString = "";
                        self.achievements.map(function (o) {
                            if(o.yesno)
                                achievementsString += "," + o.IdLogro;
                        })
                        self.successCase.Logros = achievementsString.substr(1);
                        self.successCase.IdLogro = 0;

                        self.successCase.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                        if (!isEdition())
                            self.successCase.IdCasoDeExito = 0;
                        console.log(self.successCase);

                        loading.startLoading("SuccessCaseDetailFacilitatorController, submit - ActualizarCasoDeExito()");
                        var req = {
                            IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                            CasoDeExito: self.successCase
                        }
                        var promesa = SuccessCaseService.actualizarCasoDeExito(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                growl.success("El caso de éxito ha sido registrado exitosamente.");
                                $state.go("successCaseListFacilitator");
                                self.sent = true;
                            }
                            loading.stopLoading("SuccessCaseDetailFacilitatorController, submit - ActualizarCasoDeExito()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("SuccessCaseDetailFacilitatorController, submit - ActualizarCasoDeExito()");
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
                if (self.successCase.IdEstado == "R" || !self.successCase.IdEstado || self.successCase.IdEstado == "R") {
                    return false;
                }
                return true;
            }

            function goBack() {
                $state.go("successCaseListFacilitator");
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
