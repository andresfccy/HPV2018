'use strict';

angular.module('prosperidad.reports')
    .controller('ReportsTerritorialProfController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$http', 'Connection',
        'ReportsService', '$state', 'SessionsBusiness', 'RolesService', 'AttendanceService', 'CommonsUbicaciones', 'AllocationService', 'UsersService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $http, Connection,
        ReportsService, $state, SessionsBusiness, RolesService, AttendanceService, CommonsUbicaciones, AllocationService, UsersService) {
        if (!SessionsBusiness.authorized(54)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.seasons = [];
            self.cutDates = [];
            self.identifySeasson = identifySeasson;

            self.reports = [];
            self.filter = {};
            self.roles = [];
            self.departments = [];
            self.cities = [];
            self.rolId = SessionsBusiness.getRolFromLocalStorage();

            self.generateReport = generateReport;
            self.loadDepartments = loadDepartments;
            self.citiesByDepartment = citiesByDepartment;
            self.selectReport = selectReport;
            self.loadCoordinatorsByProfessional = loadCoordinatorsByProfessional;
            self.cargarFechaCorte = cargarFechaCorte;

            // Cantidad de columnas en la vista de reportes
            self.cols = 3;

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                self.filter.IdDepartamento = -1;
                self.filter.IdMunicipio = -1;
                self.filter.IdCoordinador = -1;

                var req = { Categoria: "PERIODO" };
                loading.startLoading("ReportsTerritorialProfController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.filter.IdPeriodo = self.identifySeasson();

                        self.seasons.map(function (i) {
                            i.Mostrar = i.Valor;
                        });
                        //self.seasons.push({ Descripcion: "TODOS", Valor: "0", Mostrar: "00000000000" });

                        console.log(self.seasons);

                        self.cargarFechaCorte();
                    }
                    loading.stopLoading("ReportsTerritorialProfController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsTerritorialProfController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("ReportsTerritorialProfController, initialize - darReportes");
                var promesa = ReportsService.darReportes(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.reports = o.ListaReporte;

                        self.reports = self.reports.reduce(function (o, l) {
                            o[l.Categoria] = o[l.Categoria] || [];
                            o[l.Categoria].push(l);
                            return o;
                        }, {});

                        var array = [];
                        // Se eliminan todos los objetos que no contentan la cadena "/Reportes/", descartando así los índices.
                        for (var key in self.reports) {
                            if (self.reports.hasOwnProperty(key)) {
                                var obj = { Categoria: key, Lista: self.reports[key] };
                                for (var i = 0; i < self.reports[key].length; i++) {
                                    var r = self.reports[key][i];
                                    if (r.URL.indexOf('/Reportes/') < 0) {
                                        self.reports[key].splice(i, 1);
                                        i--;
                                    }
                                }
                                if (obj.Lista.length > 0)
                                    array.push(obj);

                            }
                        }

                        self.reports = array;
                        console.log("LISTA DE REPORTES", self.reports);
                    }
                    loading.stopLoading("ReportsTerritorialProfController, initialize - darReportes");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsTerritorialProfController, initialize - darReportes");
                });

                loadDepartments();
                loadCoordinatorsByProfessional();
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

            function cargarFechaCorte() {
                var req = {
                    IdPeriodo: self.filter.IdPeriodo
                };
                loading.startLoading("ReportsTerritorialProfController, initialize - darFechaCorte()");
                var promesa = ReportsService.darFechaCorte(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        self.cutDates = [];
                    } else {
                        self.cutDates = o.ListaFechaCorte;
                        self.cutDates.map(function (i) {
                            i.Mostrar = moment(i.Descripcion);
                        });

                        if (self.cutDates && self.cutDates.length > 0) {
                            console.log(self.cutDates);
                            self.filter.FechaCorte = self.cutDates[0].Fecha;
                        }
                    }
                    loading.stopLoading("ReportsTerritorialProfController, initialize - darFechaCorte()");
                }).catch(function (error) {
                    self.cutDates = [];
                    console.log(error);
                    loading.stopLoading("ReportsTerritorialProfController, initialize - darFechaCorte()");
                });
            }

            function loadCoordinatorsByProfessional() {
                var req = {};
                req.IdProfesional = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
                loading.startLoading("ReportsTerritorialProfController, loadCoordinatorsByProfessional - darCoordinadores");

                var promesa = ReportsService.darCoordinadoresProfesional(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.coordinators = o.ListaCoordinador;
                        self.coordinators.map(function (i) {
                            i.Mostrar = i.Nombre;
                        });
                        //self.coordinators.push({ Nombre: "TODOS", IdCoordinador: 0, Mostrar: "00000000000" });
                    }
                    loading.stopLoading("ReportsTerritorialProfController, loadCoordinatorsByProfessional - darCoordinadores");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsTerritorialProfController, loadCoordinatorsByProfessional - darCoordinadores");
                });
            }

            function loadDepartments() {
                self.cities = [];
                loading.startLoading("ReportsTerritorialProfController, loadDepartments - darDeptosProfesional()");
                var req = {};

                /*if (self.filter.IdCoordinador > 0)
                    req.IdProfesional = self.filter.IdCoordinador;
                else*/
                    req.IdProfesional = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
                var promesa = ReportsService.darDeptosProfesional(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        self.departments = [];
                        self.filter.IdDepartamento = -1;
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.departments = o.ListaDepartamento;
                        self.departments.map(function (i) {
                            i.Mostrar = i.Nombre;
                        });
                        //self.departments.push({ Nombre: "TODOS", Codigo: 0, Mostrar: "00000000000" });
                    }
                    loading.stopLoading("ReportsTerritorialProfController, loadDepartments - darDeptosProfesional()");
                }).catch(function (error) {
                    self.departments = [];
                    self.filter.IdDepartamento = -1;
                    console.log(error);
                    loading.stopLoading("ReportsTerritorialProfController, loadDepartments - darDeptosProfesional()");
                });
            }

            function citiesByDepartment() {
                self.cities = [];
                if (self.filter.IdDepartamento) {
                    loading.startLoading("ReportsTerritorialProfController, citiesByDepartment - darMunicipiosProfesional()");
                    var req = {};
                    req.IdDepartamento = angular.copy(self.filter.IdDepartamento);
                    req.IdProfesional = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());

                    var promesa = ReportsService.darMunicipiosProfesional(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            self.cities = [];
                            self.filter.IdMunicipio = -1;
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.cities = o.ListaMunicipio;
                            self.cities.map(function (i) {
                                i.Mostrar = i.Nombre;
                            });
                            //self.cities.push({ Nombre: "TODOS", Codigo: 0, Mostrar: "00000000000" });
                        }
                        loading.stopLoading("ReportsTerritorialProfController, citiesByDepartment - darMunicipiosProfesional()");
                    }).catch(function (error) {
                        self.cities = [];
                        self.filter.IdMunicipio = -1;
                        console.log(error);
                        loading.stopLoading("ReportsTerritorialProfController, citiesByDepartment - darMunicipiosProfesional()");
                    });
                }
            }

            function selectReport(obj) {
                self.filter.IdReporte = obj.IdReporte;
                self.filter.url = obj.URL;
            }

            function generateReport() {
                if (self.filter.IdReporte && self.filter.IdReporte != 0) {

                    window.open(Connection.API_BASE_URL() + self.filter.url
                                + '?IdPeriodo=' + (self.filter.IdPeriodo || 0)
                                + '&IdUsuario=' + SessionsBusiness.getUserIdFromLocalStorage()
                                + '&IdCoordinador=' + (self.filter.IdCoordinador || 0)
                                + '&IdFacilitador=' + (self.filter.IdFacilitador || 0)
                                + '&IdDepartamento=' + (self.filter.IdDepartamento || 0)
                                + '&IdMunicipio=' + (self.filter.IdMunicipio || 0)
                                + '&IdGrupo=' + (self.filter.IdGrupo || 0)
                                + '&FechaCorte=' + (self.filter.FechaCorte || 0)
                               + '&IdReporte=' + (self.filter.IdReporte || 0), "_self");
                }
                else {
                    growl.warning("Se debe seleccionar un reporte.");
                }
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
