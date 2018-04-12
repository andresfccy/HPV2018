'use strict';

angular.module('prosperidad.lifeStory')
    .controller('LifeStoryDetailFacilitatorController',
    ['$rootScope', '$scope', 'CommonsListasService', 'growl', 'loading', 'Upload', '$state', '$stateParams', '$http',
        'CommonsConstants', 'SessionsBusiness', 'AttendanceService', 'Connection', 'LifeStoryService',
    function ($rootScope, $scope, CommonsListasService, growl, loading, Upload, $state, $stateParams, $http,
        CommonsConstants, SessionsBusiness, AttendanceService, Connection, LifeStoryService) {

        if (!SessionsBusiness.authorized(41)) {
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
            self.seasons = [];
            self.participants;
            self.selectedSeason
            self.modifyDocument = false;

            self.getParticipantsByGroup = getParticipantsByGroup;
            self.editionDisabled = editionDisabled;
            self.documentDisabled = documentDisabled;
            self.seeDocument = seeDocument;
            self.identifySeasson = identifySeasson;
            self.rechazado = false;

            self.submit = submit;
            self.sent = false;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("LifeStoryDetailFacilitatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                    }
                    loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darLista(PERIODO)");
                });

                loading.startLoading("LifeStoryDetailFacilitatorController, initialize - darLista(TIPOHISTORIAVIDA)");
                var req = { Categoria: "TIPOHISTORIAVIDA" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.storyTypes = o.ListaValor;
                    }
                    loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darLista(TIPOHISTORIAVIDA)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darLista(TIPOHISTORIAVIDA)");
                });

                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("LifeStoryDetailFacilitatorController, initialize - darGruposXFacilitador");
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
                        if( i != -1 )
                            self.groups.splice(i, 1);
                        if( i2 != -1)
                            self.groups.splice(i2, 1);
                        console.log("Después: ", self.groups);
                        self.getParticipantsByGroup();
                    }
                    loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darGruposXFacilitador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darGruposXFacilitador");
                });

                if (isEdition()) {

                    var req = {
                        IdHistoriaDeVida: $stateParams.id
                    };
                    loading.startLoading("LifeStoryDetailFacilitatorController, initialize - consultarHistoriaVida");
                    var promesa = LifeStoryService.consultarHistoriaVida(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.lifeStory = o.HistoriaDeVida;
                            self.lifeStory.IdTipoHistoriaDeVida += "";
                            if (self.lifeStory.IdEstado == "R") {
                                self.rechazado = true;
                            }
                            var req = {
                                IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                            };
                            loading.startLoading("LifeStoryDetailFacilitatorController, initialize - darGruposXFacilitador");
                            var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    console.log("Aquí")
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    self.groups = o.ListaGrupo;
                                    var i = -1;
                                    self.groups.some(function (l, idx) { if (l.IdGrupo == 11) { i = idx; return true; } })
                                    self.groups.splice(i, 1);
                                    self.getParticipantsByGroup();
                                }
                                loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darGruposXFacilitador");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - darGruposXFacilitador");
                            });

                        }
                        loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - consultarHistoriaVida");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LifeStoryDetailFacilitatorController, initialize - consultarHistoriaVida");
                    });
                }
            }

            function getParticipantsByGroup() {
                if (self.lifeStory.IdGrupoFacilitador && self.lifeStory.IdGrupoFacilitador != 0) {
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdGrupoFacilitador: self.lifeStory.IdGrupoFacilitador
                    };
                    loading.startLoading("LifeStoryDetailFacilitatorController, getParticipantsByGroup - darInscritos");
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
                        loading.stopLoading("LifeStoryDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LifeStoryDetailFacilitatorController, getParticipantsByGroup - darInscritos");
                    });
                }
            }

            function submit(file) {
                if (!file) {
                    growl.warning("Se debe seleccionar un documento.");
                }
                if ((file && !file.$error && !isEdition()) || (file && !file.$error && isEdition() && self.lifeStory.IdEstado == "R")) {
                    loading.startLoading("LifeStoryDetailFacilitatorController, submit - upload");
                    Upload.upload({
                        url: Connection.API_BASE_URL() + '/Entregable/AdjuntarEntregable.aspx'
                            + '?IdFacilitador=' + SessionsBusiness.getUserIdFromLocalStorage()
                            + '&IdTaller=' + self.lifeStory.IdInscrito
                            + '&IdEntregable=' + 3
                            + '&IdPeriodo=' + self.selectedSeason
                            + '&IdGrupoFacilitador=' + self.lifeStory.IdGrupoFacilitador,
                        data: {
                            file: file
                        }
                    }).then(function (resp) {
                        loading.stopLoading("LifeStoryDetailFacilitatorController, submit - upload");

                        if (self.form.$valid) {
                            loading.startLoading("LifeStoryDetailFacilitatorController, submit - ActualizarHistoriaVida()");
                            self.lifeStory.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                            var req = {
                                IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                                HistoriaDeVida: self.lifeStory
                            }
                            var promesa = LifeStoryService.actualizarHistoriaVida(req).$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    growl.success("La historia de vida ha sido registrada exitosamente.");
                                    $state.go("lifeStoryListFacilitator");
                                    self.sent = true;
                                }
                                loading.stopLoading("LifeStoryDetailFacilitatorController, submit - ActualizarHistoriaVida()");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("LifeStoryDetailFacilitatorController, submit - ActualizarHistoriaVida()");
                            });
                        }
                    }, function (resp) {
                        console.log('Error status: ' + resp.status);
                        growl.error("Ocurrió un problema subiendo el archivo.");
                        loading.stopLoading("LifeStoryDetailFacilitatorController, submit - upload");
                    }, function (evt) {
                        var progressPercentage = parseInt(100.0 *
                                evt.loaded / evt.total);
                        self.log = 'progress: ' + progressPercentage +
                            '% ' + evt.config.data.file.name + '\n' +
                          self.log;
                    });
                }
                if (isEdition()) {
                    if (self.form.$valid) {
                        loading.startLoading("LifeStoryDetailFacilitatorController, submit - ActualizarHistoriaVida()");
                        self.lifeStory.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                        var req = {
                            IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                            HistoriaDeVida: self.lifeStory
                        }
                        var promesa = LifeStoryService.actualizarHistoriaVida(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                growl.success("La historia de vida ha sido enviada exitosamente.");
                                $state.go("lifeStoryListFacilitator");
                                self.sent = true;
                            }
                            loading.stopLoading("LifeStoryDetailFacilitatorController, submit - ActualizarHistoriaVida()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("LifeStoryDetailFacilitatorController, submit - ActualizarHistoriaVida()");
                        });
                    }
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

            function seeDocument() {
                loading.startLoading("LifeStoryDetailFacilitatorController, seeDocument - $http");
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
                    loading.stopLoading("LifeStoryDetailFacilitatorController, seeDocument - $http");
                }).error(function (data, status, headers, config) {
                    if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                    if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                    loading.stopLoading("LifeStoryDetailFacilitatorController, submit - $http");
                });
            }

            function initializeForm(f) {
                self.form = f;
            }

            function editionDisabled() {
                if (self.lifeStory.IdEstado == "R" || !self.lifeStory.IdEstado || self.lifeStory.IdEstado == "") {
                    return false;
                }
                return true;
            }

            function documentDisabled() {
                if (self.lifeStory.IdEstado == "R" || !self.lifeStory.IdEstado || self.lifeStory.IdEstado == "") {
                    return false;
                }
                return true;
            }

            function goBack() {
                $state.go("lifeStoryListFacilitator");
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
