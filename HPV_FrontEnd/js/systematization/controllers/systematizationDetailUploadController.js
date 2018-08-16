appSystematization
    .controller('SystematizationDetailUploadController',
    ['$rootScope', '$scope', '$location', '$state', '$translate', '$stateParams', '$http',
        'growl', 'moment', 'loading', 'Upload',
        'CommonsConstants', 'CommonsListasService', 'SystematizationService', 'SessionsBusiness', 'CommonsListasService',
        'AttendanceService', 'Connection',
        function ($rootScope, $scope, $location, $state, $translate, $stateParams, $http,
            growl, moment, loading, Upload,
            CommonsConstants, CommonListasService, SystematizationService, SessionsBusiness, CommonsListasService,
            AttendanceService, Connection) {

            if (!SessionsBusiness.authorized(411)) {
                //if (false) {
                $state.go("home");
                growl.warning("Permisos insuficientes.");
            }
            else {
                var self = this;
                self.init = init;
                self.initForm = initForm;
                self.identifySeasson = identifySeasson;
                self.seeDocument = seeDocument;
                self.submit = submit;
                self.documentDisabled = documentDisabled;
                self.editionDisabled = editionDisabled;
                self.getParticipantsByGroup = getParticipantsByGroup;
                self.goBack = goBack;

                function init() {
                    var req = { Categoria: "PERIODO" };
                    var action = getCtrlName() + ", initialize - darLista(PERIODO)";
                    loading.startLoading(action);
                    var promesa = CommonsListasService.darLista(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.seasons = o.ListaValor;
                            self.selectedSeason = self.identifySeasson();
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });

                    var req = { Categoria: "INSTRUMENTOS" };
                    var action = getCtrlName() + ", initialize - darLista(INSTRUMENTOS)";
                    loading.startLoading(action);
                    var promesa = CommonsListasService.darLista(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.instruments = o.ListaValor;
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });

                    if (!isEdition()) {
                        var req = {
                            IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                        };
                        var action = getCtrlName() + ", initialize - darGruposXFacilitador";
                        loading.startLoading(action);
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
                                console.log("Después: ", self.groups);
                            }
                            loading.stopLoading(action);
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading(action);
                        });
                    } else {
                        var req = {
                            IdSistematizacion: $stateParams.id
                        };
                        var action = getCtrlName() + ", initialize - consultarHistoriaVida";
                        loading.startLoading(action);
                        var promesa = SystematizationService.consultarSistematizacion(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.sys = o.Sistematizacion;
                                self.sys.IdInstrumento += "";
                                if (self.sys.IdEstado == "R") {
                                    self.rechazado = true;
                                }
                                var req = {
                                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                                };
                                var action2 = getCtrlName() + ", initialize - darGruposXFacilitador";
                                loading.startLoading(action2);
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
                                    loading.stopLoading(action2);
                                }).catch(function (error) {
                                    console.log(error);
                                    loading.stopLoading(action2);
                                });

                            }
                            loading.stopLoading(action);
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading(action);
                        });
                    }
                }

                function submit(file) {
                    if (!file) {
                        growl.warning("Se debe seleccionar un documento.");
                    }
                    if (((file && !file.$error && !isEdition()) ||
                        (file && !file.$error && isEdition() && self.sys.IdEstado == "R")) &&
                        self.form.$valid) {
                        var action = getCtrlName() + ", submit - upload";
                        loading.startLoading(action);
                        Upload.upload({
                            url: Connection.API_BASE_URL() + '/Sistematizacion/AdjuntarInstrumento.aspx'
                                + '?IdFacilitador=' + SessionsBusiness.getUserIdFromLocalStorage()
                                + '&IdInscrito=' + self.sys.IdInscrito
                                + '&IdInstrumento=' + self.sys.IdInstrumento
                                + '&IdPeriodo=' + self.selectedSeason
                                + '&IdGrupoFacilitador=' + self.sys.IdGrupoFacilitador,
                            data: {
                                file: file
                            }
                        }).then(function (resp) {
                            loading.stopLoading(action);

                            if (self.form.$valid || true) {
                                var action2 = getCtrlName() + ", submit - ActualizarSistematizacion()";
                                loading.startLoading(action2);
                                self.sys.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                                self.sys.IdEstado = "P";
                                var req = {
                                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                                    Sistematizacion: self.sys
                                }
                                var promesa = SystematizationService.actualizarSistematizacion(req).$promise;
                                promesa.then(function (o) {
                                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                    } else {
                                        growl.success("Instrumento registrado exitosamente.");
                                        self.sent = true;
                                        goBack();
                                    }
                                    loading.stopLoading(action2);
                                }).catch(function (error) {
                                    console.log(error);
                                    loading.stopLoading(action2);
                                });
                            }
                        }, function (resp) {
                            console.log('Error status: ' + resp.status);
                            growl.error("Ocurrió un problema subiendo el archivo.");
                            loading.stopLoading(action);
                        }, function (evt) {
                            var progressPercentage = parseInt(100.0 *
                                evt.loaded / evt.total);
                            self.log = 'progress: ' + progressPercentage +
                                '% ' + evt.config.data.file.name + '\n' +
                                self.log;
                        });
                    }
                    //else if (isEdition()) {
                    //    if (self.form.$valid) {
                    //        var action = getCtrlName() + ", submit - ActualizarHistoriaVida()";
                    //        loading.startLoading(action);
                    //        self.sys.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                    //        var req = {
                    //            IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    //            HistoriaDeVida: self.sys
                    //        }
                    //        var promesa = SystematizationService.actualizarSistematizacion(req).$promise;
                    //        promesa.then(function (o) {
                    //            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    //            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    //                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    //            } else {
                    //                growl.success("Cambios registrados exitosamente.");
                    //                goBack();
                    //            }
                    //            loading.stopLoading(action);
                    //        }).catch(function (error) {
                    //            console.log(error);
                    //            loading.stopLoading(action);
                    //        });
                    //    }
                    //}
                }

                function seeDocument() {
                    var action = getCtrlName() + ", seeDocument - $http";
                    loading.startLoading(action);
                    $http({
                        method: 'POST',
                        url: Connection.API_BASE_URL() + '/Sistematizacion/ConsultarInstrumento.aspx'
                            + '?IdSistematizacion=' + self.sys.Id,
                        responseType: "arraybuffer"
                    }).success(function (data) {
                        var file = new Blob([data], { type: 'application/pdf' });
                        var fileURL = URL.createObjectURL(file);
                        window.open(fileURL, "_blank");
                        loading.stopLoading(action);
                    }).error(function (data, status, headers, config) {
                        if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                        if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                        loading.stopLoading(action);
                    });
                }

                function getParticipantsByGroup() {
                    if (self.sys.IdGrupoFacilitador && self.sys.IdGrupoFacilitador != 0) {
                        var req = {
                            IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                            IdGrupoFacilitador: self.sys.IdGrupoFacilitador
                        };
                        var action = getCtrlName() + ", getParticipantsByGroup - darInscritos";
                        loading.startLoading(action);
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
                            loading.stopLoading(action);
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading(action);
                        });
                    }
                }

                function initForm(f) {
                    self.form = f;
                }

                function editionDisabled() {
                    if (!self.sys || self.sys.IdEstado == "R" || !self.sys.IdEstado || self.sys.IdEstado == "") {
                        return false;
                    }
                    return true;
                }

                function documentDisabled() {
                    if (!self.sys || !self.sys.IdEstado || self.sys.IdEstado == "") {
                        return false;
                    }
                    return true;
                }

                function getCtrlName() {
                    return "SystematizationDetailUploadController";
                }

                function goBack() {
                    $state.go("systematizationListUpload");
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