'use strict';

angular.module('prosperidad.attendance')
    .controller('AttendanceFacilitatorController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'loading', '$window','$http','$q',
        'CommonsConstants', '$log', 'AttendanceService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, loading, $window,$http,$q,
        CommonsConstants, $log, AttendanceService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter) {

        if (!SessionsBusiness.authorized(21)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.workshops = [];
            self.groups = [];
            self.selectedWorkshops = [];
            self.toggleList = toggleList;
            self.selectAllWorkshops = selectAllWorkshops;
            self.selectedGroup = null;
            self.form = {};
            self.initializeForm = initializeForm;
            self.submit = submit;

            self.identifySeasson = identifySeasson;

            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("AttendanceFacilitatorController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.selectedSeason = self.identifySeasson();
                        console.log(self.selectedSeason)
                    }
                    loading.stopLoading("AttendanceFacilitatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceFacilitatorController, initialize - darLista(PERIODO)");
                });

                var req = {};
                loading.startLoading("AttendanceFacilitatorController, initialize - darTalleres");
                var promesa = AttendanceService.darTalleres().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.workshops = o.ListaTaller;
                    }
                    loading.stopLoading("AttendanceFacilitatorController, initialize - darTalleres");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceFacilitatorController, initialize - darTalleres");
                });

                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("AttendanceFacilitatorController, initialize - darGruposXFacilitador");
                var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.groups = o.ListaGrupo;
                    }
                    loading.stopLoading("AttendanceFacilitatorController, initialize - darGruposXFacilitador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceFacilitatorController, initialize - darGruposXFacilitador");
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

            function initializeForm(f) {
                self.form = f;
            }

            function submit() {
                if (self.form.$valid && self.selectedGroup && self.selectedWorkshops.length > 0) {
                    var talleresString = "";
                    self.selectedWorkshops.map(function (o) {
                        if (talleresString == "")
                            talleresString = o;
                        else
                            talleresString += "," + o;
                    });
                    var req = {
                        IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdGrupo: self.selectedGroup,
                        IdPeriodo: parseInt(self.selectedSeason),
                        Taller: talleresString
                    };
                    loading.startLoading("AttendanceFacilitatorController, submit - generarListaAsistencia");
                    var promesa = AttendanceService.generarListaAsistencia(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            // Mientras no se devuelva un pdf se delega la función de descargar al browser
                            //$window.open(CommonsConstants.factory.API_BASE_URL() + o.UrlDescarga,"_blank");
                            loading.startLoading("AttendanceFacilitatorController, submit - $http");
                            $http({
                                method: 'POST',
                                data: req,
                                url: CommonsConstants.factory.API_BASE_URL() + o.UrlDescarga,
                                responseType: "arraybuffer"
                            }).success(function (data) {
                                var file = new Blob([data], { type: 'application/pdf' });
                                var fileURL = URL.createObjectURL(file);
                                window.open(fileURL, "_blank");
                                loading.stopLoading("AttendanceFacilitatorController, submit - $http");
                            }).error(function (data, status, headers, config) {
                                if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                                if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                                loading.stopLoading("AttendanceFacilitatorController, submit - $http");
                            });
                        }
                        loading.stopLoading("AttendanceFacilitatorController, submit - generarListaAsistencia");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("AttendanceFacilitatorController, submit - generarListaAsistencia");
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
