'use strict';

angular.module('prosperidad.uploadable')
    .controller('UploadableDetailFacilitatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'loading', '$http',
        'CommonsConstants', '$log', 'UploadableService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
        'AttendanceService', '$stateParams', 'moment', 'Connection', 'Upload',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, loading, $http,
        CommonsConstants, $log, UpleadableService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter,
        AttendanceService, $stateParams, moment, Connection, Upload) {

        if (!SessionsBusiness.authorized(28)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.initializeForm = initializeForm;
            self.form;
            self.getSeason = getSeason;
            self.submitAttendance = submitAttendance;
            self.sent = false;
            self.noWhere = "FALTA SOLICITAR APROBACIÓN";
            self.maxDate = new Date();

            self.participantsList = [];
            self.attendanceList = [];
            self.uploadables = [];
            self.seasons = [];
            self.workshops = [];
            self.documentsStates = [];
            self.assistanceStates = [];
            self.selectedSeason;
            self.selectedUploadable = 1;
            self.selectedWorkshop = 0;
            self.identifySeasson = identifySeasson;
            self.setAllAttended = setAllAttended;
            self.goBack = goBack;
            self.filteredParticipants = filteredParticipants;
            self.addParticipantToList = addParticipantToList;
            self.removeParticipantFromList = removeParticipantFromList;
            self.submitDocument = submitDocument;
            self.seeDocument = seeDocument;
            self.finishDocument = finishDocument;

            $scope.status = {
                open1: false,
                open2: false
            }

            self.opcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            $scope.maxResultsPerPage = 20;
            self.maxSize = 5;
            self.currentPage = 1;
            self.noOfPages = 0;
            self.total = 0;
            self.filtered = [];
            $scope.search;

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.attendanceList, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {
                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(ENTREGABLES)");
                var req = { Categoria: "ENTREGABLE" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadables = o.ListaValor;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ENTREGABLES)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ENTREGABLES)");
                });

                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                var req = { Categoria: "ESTADOSDOCUMENTO" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.documentsStates = o.ListaValor;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                });

                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSASISTENCIA)");
                var req = { Categoria: "ESTADOSASISTENCIA" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.assistanceStates = o.ListaValor;
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSASISTENCIA)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(ESTADOSASISTENCIA)");
                });

                var req = { Categoria: "PERIODO" };
                loading.startLoading("UploadableListFacilitatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();

                        loading.startLoading("UploadableDetailFacilitatorController, initialize - darEntregableEstadoDetalle()");
                        var req = {
                            IdPeriodo: self.selectedSeason,
                            IdEntregable: 1,
                            IdTaller: $stateParams.idTaller,
                            IdGrupoFacilitador: $stateParams.idGrupo
                        }
                        var promesa = AttendanceService.darEntregableEstadoDetalle(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.uploadable = o.EntregableEstado;
                                if (self.uploadable.EstadoAsistencia == self.assistanceStates[0].Descripcion || self.uploadable.EstadoAsistencia == self.assistanceStates[1].Descripcion) {
                                    self.sent = true;
                                }

                                if (!self.uploadable.FechaRealizacion || self.uploadable.FechaRealizacion == "") {
                                    delete self.uploadable.FechaRealizacion;
                                }
                                else {
                                    console.log(self.uploadable.FechaRealizacion)
                                    self.uploadable.FechaRealizacion = moment(self.uploadable.FechaRealizacion).toDate();
                                }
                            }
                            loading.stopLoading("UploadableDetailFacilitatorController, initialize - darEntregableEstadoDetalle()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("UploadableDetailFacilitatorController, initialize - darEntregableEstadoDetalle()");
                        });

                        loading.startLoading("UploadableDetailFacilitatorController, initialize - darAsistenteXFacilitador()");
                        var req = {
                            IdPeriodo: self.selectedSeason,
                            IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                        }
                        var promesa = AttendanceService.darAsistenteXFacilitador(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.participantsList = o.ListaAsistente;
                            }
                            loading.stopLoading("UploadableDetailFacilitatorController, initialize - darAsistenteXFacilitador()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("UploadableDetailFacilitatorController, initialize - darAsistenteXFacilitador()");
                        });

                        loading.startLoading("UploadableDetailFacilitatorController, initialize - darAsistenciaEstado()");
                        var req = {
                            IdPeriodo: self.selectedSeason,
                            IdEntregable: 1,
                            IdTaller: $stateParams.idTaller,
                            IdGrupoFacilitador: $stateParams.idGrupo
                        }
                        var promesa = AttendanceService.darAsistenciaEstado(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.attendanceList = o.ListaAsistenciaEstado;
                                self.total = self.attendanceList.length;
                                self.filtered = self.attendanceList;
                                self.noOfPages = Math.ceil(self.attendanceList.length / $scope.maxResultsPerPage);
                            }
                            loading.stopLoading("UploadableDetailFacilitatorController, initialize - darAsistenciaEstado()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("UploadableDetailFacilitatorController, initialize - darAsistenciaEstado()");
                        });
                    }
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableListFacilitatorController, initialize - darLista(PERIODO)");
                });
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

            function submitAttendance() {
                if (self.uploadable.EstadoAsistencia == self.assistanceStates[0].Descripcion
                    || self.uploadable.EstadoAsistencia == self.assistanceStates[1].Descripcion) {
                    growl.info("Ya se había enviado la información de asistencia.");
                }
                else if (self.form.$valid) {

                    var date = moment(self.uploadable.FechaRealizacion).format('YYYY-MM-DD');
                    if (self.uploadable.FechaRealizacion && self.uploadable.FechaRealizacion != "" && date.split('-').length == 3 && date.split('-')[0].length == 4 && date.split('-')[1].length == 2 && date.split('-')[2].length == 2) {
                        loading.startLoading("UploadableDetailFacilitatorController, submitAttendance - registrarAsistencia()");
                        var req = {
                            IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage(),
                            IdTaller: $stateParams.idTaller,
                            IdEntregable: 1,
                            IdPeriodo: self.selectedSeason,
                            IdGrupoFacilitador: $stateParams.idGrupo,
                            FechaRealizacion: date,
                            ListaAsistenciaInfo: angular.copy(self.attendanceList)
                        }
                        var promesa = AttendanceService.registrarAsistencia(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                growl.success("La marcación de asistencia fue guardada exitosamente y está pendiente por aprobación.");
                                self.uploadable.EstadoAsistencia = self.assistanceStates[1].Descripcion;
                                self.sent = true;
                            }
                            loading.stopLoading("UploadableDetailFacilitatorController, submitAttendance - registrarAsistencia()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("UploadableDetailFacilitatorController, submitAttendance - registrarAsistencia()");
                        });
                    } else {
                        growl.info("Digita o selecciona una fecha de realización válida.")
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
                loading.startLoading("UploadableDetailFacilitatorController, seeDocument - $http");
                $http({
                    method: 'POST',
                    url: Connection.API_BASE_URL() + '/Entregable/ConsultarEntregable.aspx'
                            + '?IdFacilitador=' + SessionsBusiness.getUserIdFromLocalStorage()
                            + '&IdTaller=' + $stateParams.idTaller
                            + '&IdEntregable=' + 1
                            + '&IdPeriodo=' + self.selectedSeason
                            + '&IdGrupoFacilitador=' + $stateParams.idGrupo,
                    responseType: "arraybuffer"
                }).success(function (data) {
                    var file = new Blob([data], { type: 'application/pdf' });
                    var fileURL = URL.createObjectURL(file);
                    window.open(fileURL, "_blank");
                    loading.stopLoading("UploadableDetailFacilitatorController, seeDocument - $http");
                }).error(function (data, status, headers, config) {
                    if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                    if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                    loading.stopLoading("UploadableDetailFacilitatorController, submit - $http");
                });
            }

            function submitDocument(file) {
                if (file && !file.$error) {
                    loading.startLoading("UploadableDetailFacilitatorController, submitDocument - upload");
                    Upload.upload({
                        url: Connection.API_BASE_URL() + '/Entregable/AdjuntarEntregable.aspx'
                            + '?IdFacilitador=' + SessionsBusiness.getUserIdFromLocalStorage()
                            + '&IdTaller=' + $stateParams.idTaller
                            + '&IdEntregable=' + 1
                            + '&IdPeriodo=' + self.selectedSeason
                            + '&IdGrupoFacilitador=' + $stateParams.idGrupo
                        ,
                        data: {
                            file: file
                        }
                    }).then(function (resp) {
                        self.uploadable.EstadoDocumento = self.noWhere;
                        growl.info("Se guardó correctamente el documento, revise que sea el documento correcto\n" +
                                    "y solicite su aprobación haciendo clic en el botón \"Solicitar aprobación\".");
                        loading.stopLoading("UploadableDetailFacilitatorController, submitDocument - upload");
                    }, function (resp) {
                        console.log('Error status: ' + resp.status);
                        growl.error("Ocurrió un problema subiendo el archivo.");
                        loading.stopLoading("UploadableDetailFacilitatorController, submitDocument - upload");
                    }, function (evt) {
                        var progressPercentage = parseInt(100.0 *
                                evt.loaded / evt.total);
                        self.log = 'progress: ' + progressPercentage +
                            '% ' + evt.config.data.file.name + '\n' +
                          self.log;
                    });
                }
            };

            function finishDocument() {
                loading.startLoading("UploadableDetailFacilitatorController, finishDocument - RegistrarAprobacion(D,P)");
                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdPeriodo: self.selectedSeason,
                    IdEntregable: 1,
                    IdTaller: $stateParams.idTaller,
                    IdGrupoFacilitador: $stateParams.idGrupo,
                    Categoria: "D",
                    Estado: "P",
                    MotivoRechazo: "",
                    FechaRealizacion: moment(self.uploadable.FechaRealizacion).format('YYYY-MM-DD')
                }
                var promesa = AttendanceService.registrarAprobacion(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadable.EstadoDocumento = self.documentsStates[1].Descripcion;
                        growl.success("Se solicitó la aprobación del documento exitosamente.");
                    }
                    loading.stopLoading("UploadableDetailFacilitatorController, finishDocument - RegistrarAprobacion(D,P)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailFacilitatorController, finishDocument - RegistrarAprobacion(D,P)");
                });
            }

            function initializeForm(f) {
                self.form = f;
            }

            function goBack() {
                $state.go("uploadableListFacilitator");
            }

            function setAllAttended(op) {
                if (!self.sent) {
                    self.attendanceList.map(function (o) { o.IndAsistencia = op; });
                }
            }

            function removeParticipantFromList(o) {
                if (!self.sent) {
                    self.attendanceList.splice(self.attendanceList.indexOf(o), 1);
                    self.total = self.attendanceList.length;
                    self.filtered = self.attendanceList;
                    self.noOfPages = Math.ceil(self.attendanceList.length / $scope.maxResultsPerPage);
                }
            }

            function addParticipantToList() {
                if (!self.sent) {
                    if (self.selectedParticipant && self.selectedParticipant.NombreInscrito != "") {
                        var o = angular.copy(self.selectedParticipant);
                        o.IndAsistencia = 1;
                        o.Agregado = true;
                        self.attendanceList.push(o);
                    }
                    self.total = self.attendanceList.length;
                    self.filtered = self.attendanceList;
                    self.noOfPages = Math.ceil(self.attendanceList.length / $scope.maxResultsPerPage);
                }
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
