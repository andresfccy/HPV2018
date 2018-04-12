'use strict';

angular.module('prosperidad.uploadable')
    .controller('UploadableDetailCoordinatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'loading', '$http',
        'CommonsConstants', '$log', 'UploadableService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
        'AttendanceService', '$stateParams', 'moment','Connection',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, loading, $http,
        CommonsConstants, $log, UpleadableService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter,
        AttendanceService, $stateParams, moment, Connection) {

        if (!SessionsBusiness.authorized(29)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.uploadableList = [];
            self.uploadables = [];
            self.seasons = [];
            self.workshops = [];
            self.documentsStates = [];
            self.assistanceStates = [];
            self.selectedSeason;
            self.selectedUploadable = 1;
            self.identifySeasson = identifySeasson;
            self.acceptDocument = acceptDocument;
            self.rejectDocument = rejectDocument;
            self.acceptAttendance = acceptAttendance;
            self.rejectAttendance = rejectAttendance;
            self.sent = true;
            self.goBack = goBack;
            self.seeDocument = seeDocument;

            self.opcLimit = [
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
                self.filtered = filterFilter(self.uploadableList, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {
                loading.startLoading("UploadableDetailCoordinatorController, initialize - darLista(ENTREGABLES)");
                var req = { Categoria: "ENTREGABLE" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadables = o.ListaValor;
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(ENTREGABLES)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(ENTREGABLES)");
                });

                loading.startLoading("UploadableDetailCoordinatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                var req = { Categoria: "ESTADOSDOCUMENTO" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.documentsStates = o.ListaValor;
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(ESTADOSDOCUMENTO)");
                });

                loading.startLoading("UploadableDetailCoordinatorController, initialize - darLista(ESTADOSASISTENCIA)");
                var req = { Categoria: "ESTADOSASISTENCIA" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.assistanceStates = o.ListaValor;
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(ESTADOSASISTENCIA)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(ESTADOSASISTENCIA)");
                });

                var req = { Categoria: "PERIODO" };
                loading.startLoading("UploadableDetailCoordinatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();

                        loading.startLoading("UploadableDetailCoordinatorController, initialize - darEntregableEstadoDetalle()");
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
                            loading.stopLoading("UploadableDetailCoordinatorController, initialize - darEntregableEstadoDetalle()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("UploadableDetailCoordinatorController, initialize - darEntregableEstadoDetalle()");
                        });

                        loading.startLoading("UploadableDetailCoordinatorController, initialize - darAsistenciaEstado()");
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
                            loading.stopLoading("UploadableDetailCoordinatorController, initialize - darAsistenciaEstado()");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("UploadableDetailCoordinatorController, initialize - darAsistenciaEstado()");
                        });
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darLista(PERIODO)");
                });

                loading.startLoading("UploadableDetailCoordinatorController, initialize - darTalleres()");
                var req = {}
                var promesa = AttendanceService.darTalleres().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.workshops = o.ListaEntregableEstado;
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darTalleres()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, initialize - darTalleres()");
                });
            }

            function acceptDocument() {
                loading.startLoading("UploadableDetailCoordinatorController, acceptDocument - RegistrarAprobacion(D,A)");
                var req = {
                    IdFacilitador: $stateParams.idFacilitador,
                    IdPeriodo: self.selectedSeason,
                    IdEntregable: 1,
                    IdTaller: $stateParams.idTaller,
                    IdGrupoFacilitador: $stateParams.idGrupo,
                    Categoria: "D",
                    Estado: "A",
                    MotivoRechazo: "",
                    FechaRealizacion: moment(self.uploadable.FechaRealizacion).format('YYYY-MM-DD')
                }
                var promesa = AttendanceService.registrarAprobacion(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadable.EstadoDocumento = self.documentsStates[0].Descripcion;
                        growl.success("Se aceptó el documento con éxito.");
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, acceptDocument - RegistrarAprobacion(D,A)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, acceptDocument - RegistrarAprobacion(D,A)");
                });
            }

            function rejectDocument() {
                if (self.uploadable.MotivoRechazoDocumento && self.uploadable.MotivoRechazoDocumento != "") {
                    loading.startLoading("UploadableDetailCoordinatorController, rejectDocument - RegistrarAprobacion(D,R)");
                    var req = {
                        IdFacilitador: $stateParams.idFacilitador,
                        IdPeriodo: self.selectedSeason,
                        IdEntregable: 1,
                        IdTaller: $stateParams.idTaller,
                        IdGrupoFacilitador: $stateParams.idGrupo,
                        Categoria: "D",
                        Estado: "R",
                        MotivoRechazo: self.uploadable.MotivoRechazoDocumento,
                        FechaRealizacion: moment(self.uploadable.FechaRealizacion).format('YYYY-MM-DD')
                    }
                    var promesa = AttendanceService.registrarAprobacion(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.uploadable.EstadoDocumento = self.documentsStates[2].Descripcion;
                            growl.success("Se rechazó el documento con éxito.");
                        }
                        loading.stopLoading("UploadableDetailCoordinatorController, rejectDocument - RegistrarAprobacion(D,R)");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("UploadableDetailCoordinatorController, rejectDocument - RegistrarAprobacion(D,R)");
                    });
                } else {
                    growl.info("Debe especificar una razón del rechazo.")
                }
            }

            function acceptAttendance() {
                loading.startLoading("UploadableDetailCoordinatorController, acceptAttendance - RegistrarAprobacion(A,A)");
                var req = {
                    IdFacilitador: $stateParams.idFacilitador,
                    IdPeriodo: self.selectedSeason,
                    IdEntregable: 1,
                    IdTaller: $stateParams.idTaller,
                    IdGrupoFacilitador: $stateParams.idGrupo,
                    Categoria: "A",
                    Estado: "A",
                    MotivoRechazo: "",
                    FechaRealizacion: moment(self.uploadable.FechaRealizacion).format('YYYY-MM-DD')
                }
                var promesa = AttendanceService.registrarAprobacion(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.uploadable.EstadoAsistencia = self.assistanceStates[0].Descripcion;
                        growl.success("Se aceptó la asistencia con éxito.");
                    }
                    loading.stopLoading("UploadableDetailCoordinatorController, acceptAttendance - RegistrarAprobacion(A,A)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("UploadableDetailCoordinatorController, acceptAttendance - RegistrarAprobacion(A,A)");
                });
            }

            function rejectAttendance() {
                if (self.uploadable.MotivoRechazoAsistencia && self.uploadable.MotivoRechazoAsistencia != "") {
                    loading.startLoading("UploadableDetailCoordinatorController, rejectAttendance - RegistrarAprobacion(A,R)");
                    var req = {
                        IdFacilitador: $stateParams.idFacilitador,
                        IdPeriodo: self.selectedSeason,
                        IdEntregable: 1,
                        IdTaller: $stateParams.idTaller,
                        IdGrupoFacilitador: $stateParams.idGrupo,
                        Categoria: "A",
                        Estado: "R",
                        MotivoRechazo: self.uploadable.MotivoRechazoAsistencia,
                        FechaRealizacion: moment(self.uploadable.FechaRealizacion).format('YYYY-MM-DD')
                    }
                    var promesa = AttendanceService.registrarAprobacion(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.uploadable.EstadoAsistencia = self.assistanceStates[2].Descripcion;
                            growl.success("Se rechazó la asistencia con éxito.");
                        }
                        loading.stopLoading("UploadableDetailCoordinatorController, rejectAttendance - RegistrarAprobacion(A,R)");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("UploadableDetailCoordinatorController, rejectAttendance - RegistrarAprobacion(A,R)");
                    });
                } else {
                    growl.info("Debe especificar una razón del rechazo.")
                }
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

            function goBack() {
                $state.go("uploadableListCoordinator");
            }


            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
