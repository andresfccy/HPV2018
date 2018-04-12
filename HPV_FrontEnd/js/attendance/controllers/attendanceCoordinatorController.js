'use strict';

angular.module('prosperidad.attendance')
    .controller('AttendanceCoordinatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'loading', '$http',
        'CommonsConstants', '$log', 'AttendanceService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, loading, $http,
        CommonsConstants, $log, AttendanceService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter) {

        if (!SessionsBusiness.authorized(25) && !SessionsBusiness.authorized(27)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.workshops = null;
            self.groups = null;
            self.facilitators = [];
            self.selectedWorkshops = [];
            self.toggleList = toggleList;
            self.selectAllWorkshops = selectAllWorkshops;
            self.selectedGroup = null;
            self.selectedFacilitator = null;
            self.form = {};
            self.initializeForm = initializeForm;
            self.submit = submit;
            self.groupsOk = false;
            self.facilitatorChanged = facilitatorChanged;

            self.rolId = SessionsBusiness.getRolFromLocalStorage();
            self.identifySeasson = identifySeasson;
            self.facilitatorsBySeason = facilitatorsBySeason;

            function initialize() {
                if (self.rolId == 6 || self.rolId == 7) {
                    self.supervisor = true;
                }

                var req = { Categoria: "PERIODO" };
                loading.startLoading("AttendanceCoordinatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                        self.seasonsOk = true;
                    }
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darLista(PERIODO)");
                });

                var req = {};
                loading.startLoading("AttendanceCoordinatorController, initialize - darTalleres");
                var promesa = AttendanceService.darTalleres().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.workshops = o.ListaTaller;
                    }
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darTalleres");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darTalleres");
                });

                var req = {
                    IdCoordinador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("AttendanceCoordinatorController, initialize - darFacilitadorXCoordinador");
                var promesa = AttendanceService.darFacilitadorXCoordinador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.facilitators = o.ListaFacilitador;
                    }
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darFacilitadorXCoordinador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darFacilitadorXCoordinador");
                });
            }

            function facilitatorsBySeason() {
                facilitatorChanged();
                var req = {
                    IdCoordinador: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdPeriodo: self.selectedSeason
                };
                loading.startLoading("AttendanceCoordinatorController, initialize - darFacilitadorXCoordinador");
                var promesa = AttendanceService.darFacilitadorXCoordinador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.facilitators = o.ListaFacilitador;
                    }
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darFacilitadorXCoordinador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darFacilitadorXCoordinador");
                });
            }

            function facilitatorChanged() {
                self.groups = [];
                self.groupsOk = false;
                if (self.selectedFacilitator) {
                    var req = {
                        IdFacilitador: self.selectedFacilitator,
                        IdPeriodo: self.selectedSeason
                    };
                    loading.startLoading("AttendanceCoordinatorController, facilitatorChanged - darGruposXFacilitador");
                    var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.groups = o.ListaGrupo;
                            self.groupsOk = true;
                        }
                        loading.stopLoading("AttendanceCoordinatorController, facilitatorChanged - darGruposXFacilitador");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("AttendanceCoordinatorController, facilitatorChanged - darGruposXFacilitador");
                    });
                }
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

            function initializeForm(f) {
                self.form = f;
            }

            function submit() {
                if (self.form.$valid && self.selectedGroup && self.selectedWorkshops.length > 0 && self.selectedFacilitator) {
                    var talleresString = "";
                    self.selectedWorkshops.map(function (o) {
                        if (talleresString == "")
                            talleresString = o;
                        else
                            talleresString += "," + o;
                    });
                    var req = {
                        IdFacilitador: self.selectedFacilitator,
                        IdGrupo: self.selectedGroup,
                        IdPeriodo: parseInt(self.selectedSeason),
                        Taller: talleresString
                    };
                    loading.startLoading("AttendanceCoordinatorController, submit - generarListaAsistencia");
                    var promesa = AttendanceService.generarListaAsistencia(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            // Mientras no se devuelva un pdf se delega la función de descargar al browser
                            //$window.open(CommonsConstants.factory.API_BASE_URL() + o.UrlDescarga,"_blank");
                            loading.startLoading("AttendanceCoordinatorController, submit - $http");
                            $http({
                                method: 'POST',
                                data: req,
                                url: CommonsConstants.factory.API_BASE_URL() + o.UrlDescarga,
                                responseType: "arraybuffer"
                            }).success(function (data) {
                                var file = new Blob([data], { type: 'application/pdf' });
                                var fileURL = URL.createObjectURL(file);
                                window.open(fileURL, "_blank");
                                loading.stopLoading("AttendanceCoordinatorController, submit - $http");
                            }).error(function (data, status, headers, config) {
                                if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                                if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                                loading.stopLoading("AttendanceCoordinatorController, submit - $http");
                            });
                        }
                        loading.stopLoading("AttendanceCoordinatorController, submit - generarListaAsistencia");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("AttendanceCoordinatorController, submit - generarListaAsistencia");
                    });
                } else {
                    if (!self.selectedGroup)
                        growl.error("Debe seleccionar un grupo.")
                    if (self.selectedWorkshops.length == 0)
                        growl.error("Debe seleccionar al menos un taller.")
                }
            }

            function toggleList(idWorkshop) {
                console.log("idWorkshop", idWorkshop)
                var i = self.selectedWorkshops.indexOf(idWorkshop);
                if (i >= 0) {
                    self.selectedWorkshops.splice(i, 1);
                } else {
                    self.selectedWorkshops.push(idWorkshop);
                }
                console.log("Cambiaron los talleres", self.selectedWorkshops);
            }

            function selectAllWorkshops(flag) {
                console.log("selectAll", flag)
                if (flag)
                    self.workshops.map(function (o) { if (self.selectedWorkshops.indexOf(o.IdTaller) < 0) self.selectedWorkshops.push(o.IdTaller) });
                else
                    self.selectedWorkshops = [];
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
