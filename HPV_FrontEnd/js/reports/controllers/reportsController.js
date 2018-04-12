'use strict';

angular.module('prosperidad.reports')
    .controller('ReportsController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$http', 'Connection',
        'ReportsService', '$state', 'SessionsBusiness', 'RolesService', 'AttendanceService', 'CommonsUbicaciones', 'AllocationService', 'UsersService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $http, Connection,
        ReportsService, $state, SessionsBusiness, RolesService, AttendanceService, CommonsUbicaciones, AllocationService, UsersService) {
        if (!SessionsBusiness.authorized(51)) {
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
            self.facilitators = [];
            self.departments = [];
            self.cities = [];
            self.rolId = SessionsBusiness.getRolFromLocalStorage();

            self.generateReport = generateReport;
            self.loadDepartments = loadDepartments;
            self.citiesByDepartment = citiesByDepartment;
            self.selectReport = selectReport;
            self.loadGroupsByFacilitator = loadGroupsByFacilitator;
            self.loadFacilitatorsByCoordinator = loadFacilitatorsByCoordinator;
            self.loadCoordinatorsBySupervisor = loadCoordinatorsBySupervisor;
            self.cargarFechaCorte = cargarFechaCorte;

            // Cantidad de columnas en la vista de reportes
            self.cols = 3;

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                self.filter.IdRol = 0;
                self.filter.IdGrupo = 0;
                self.filter.IdDepartamento = 0;
                self.filter.IdMunicipio = 0;
                self.filter.IdCoordinador = 0;

                var req = { Categoria: "PERIODO" };
                loading.startLoading("ReportsController, initialize - darLista(PERIODO)");
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
                        self.seasons.push({ Descripcion: "TODOS", Valor: "0", Mostrar: "00000000000" });
                        
                        console.log(self.seasons);

                        self.cargarFechaCorte();
                    }
                    loading.stopLoading("ReportsController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsController, initialize - darLista(PERIODO)");
                });

                var req = { FiltroBusqueda: "" };
                loading.startLoading("ReportsController, initialize - darRoles()");
                var promesa = RolesService.darRolesUsuario(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.roles = angular.copy(o.ListaRol);
                        self.roles.map(function (i) {
                            i.Mostrar = i.Nombre;
                        });
                        self.roles.push({ Nombre: "TODOS", IdRol: 0, Mostrar: "00000000000" });
                        for (var i = self.roles.length - 1; i > 0; i--) {
                            if (self.roles[i].IdRol >= self.rolId) {
                                self.roles.splice(i, 1);
                            }
                        }
                    }
                    loading.stopLoading("ReportsController, initialize - darRoles()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsController, initialize - darRoles()");
                });

                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("ReportsController, initialize - darReportes");
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
                                if(obj.Lista.length > 0)
                                    array.push(obj);

                            }
                        }

                        self.reports = array;
                        console.log("LISTA DE REPORTES", self.reports);
                    }
                    loading.stopLoading("ReportsController, initialize - darReportes");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsController, initialize - darReportes");
                });


                if (self.rolId == 1) {
                    self.filter.IdRol = 1;
                    self.filter.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();

                    loadGroupsByFacilitator();
                }
                else if (self.rolId == 2) {
                    self.filter.IdRol = 2;
                    self.filter.IdCoordinador = SessionsBusiness.getUserIdFromLocalStorage();
                    loadFacilitatorsByCoordinator();
                }
                else if (self.rolId == 6 || self.rolId == 7) {
                    self.filter.IdRol = SessionsBusiness.getRolFromLocalStorage();;
                    loadCoordinatorsBySupervisor();
                }

                loadDepartments();
                //loadGroupsByFacilitator();
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
                loading.startLoading("ReportsController, initialize - darFechaCorte()");
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
                    loading.stopLoading("ReportsController, initialize - darFechaCorte()");
                }).catch(function (error) {
                    self.cutDates = [];
                    console.log(error);
                    loading.stopLoading("ReportsController, initialize - darFechaCorte()");
                });
            }

            function loadCoordinatorsBySupervisor() {
                var req = {};
                // Para dar los coordinadores no necesita el ID del supervisor
                //req.IdCoordinador = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
                loading.startLoading("ReportsController, loadCoordinatorsBySupervisor - darCoordinadores");
                var promesa = UsersService.darCoordinadores(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.coordinators = o.ListaCoordinador;
                        self.coordinators.map(function (i) {
                            i.Mostrar = i.Nombre;
                        });
                        self.coordinators.push({ Nombre: "TODOS", IdCoordinador: 0, Mostrar: "00000000000" });
                    }
                    loading.stopLoading("ReportsController, loadCoordinatorsBySupervisor - darCoordinadores");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsController, loadCoordinatorsBySupervisor - darCoordinadores");
                });
            }

            function loadFacilitatorsByCoordinator() {
                console.log("cargar los facilitadores")
                var req = {};
                if (self.filter.IdCoordinador && self.filter.IdCoordinador != 0)
                    req.IdCoordinador = angular.copy(self.filter.IdCoordinador);
                else
                    req.IdCoordinador = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
                loading.startLoading("ReportsController, loadFacilitatorsByCoordinator - darFacilitadorXCoordinador");
                var promesa = AttendanceService.darFacilitadorXCoordinador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.facilitators = o.ListaFacilitador;
                        self.facilitators.map(function (i) {
                            i.Mostrar = i.Nombre;
                        });
                        self.facilitators.push({ Nombre: "TODOS", IdFacilitador: 0, Mostrar: "00000000000" });
                    }
                    loading.stopLoading("ReportsController, loadFacilitatorsByCoordinator - darFacilitadorXCoordinador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReportsController, loadFacilitatorsByCoordinator - darFacilitadorXCoordinador");
                });
            }

            function loadGroupsByFacilitator() {
                var req = {};
                if (self.filter.IdFacilitador && self.filter.IdFacilitador != 0)
                    req.IdFacilitador = angular.copy(self.filter.IdFacilitador);
                else if (self.filter.IdCoordinador && self.filter.IdCoordinador != 0)
                    req.IdFacilitador = angular.copy(self.filter.IdCoordinador);
                else
                    req.IdFacilitador = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
                loading.startLoading("ReportsController, loadGroupsByFacilitator - darGruposXFacilitador");
                var promesa = AttendanceService.darGruposXFacilitador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        self.groups = [];
                        self.filter.IdGrupo = 0;
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.groups = o.ListaGrupo;
                        self.groups.map(function (i) {
                            //var splt = i.Sigla.split("G");
                            //i.Mostrar = parseInt(splt[splt.length - 1]);
                            i.Mostrar = i.Sigla;
                        });
                        self.groups.push({ Sigla: "TODOS", IdGrupoFacilitador: 0, Mostrar: "00000000000000" });
                    }
                    loading.stopLoading("ReportsController, loadGroupsByFacilitator - darGruposXFacilitador");
                }).catch(function (error) {
                    self.groups = [];
                    self.filter.IdGrupo = 0;
                    console.log(error);
                    loading.stopLoading("ReportsController, loadGroupsByFacilitator - darGruposXFacilitador");
                });
            }

            function loadDepartments() {
                self.cities = [];
                loading.startLoading("ReportsController, loadDepartments - darDeptosFacilitador()");
                var req = {};

                if (self.rolId == 6 || self.rolId == 7) {
                    var promesa = CommonsUbicaciones.darDepartamentos().$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            self.departments = [];
                            self.filter.IdDepartamento = 0;
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.departments = o.ListaDepartamento;
                            self.departments.map(function (i) {
                                i.Mostrar = i.Nombre;
                            });
                            self.departments.push({ Nombre: "TODOS", Codigo: 0, Mostrar: "00000000000" });
                        }
                        loading.stopLoading("ReportsController, loadDepartments - darDeptosFacilitador");
                    }).catch(function (error) {
                        self.departments = [];
                        self.filter.IdDepartamento = 0;
                        console.log(error);
                        loading.stopLoading("ReportsController, loadDepartments - darDeptosFacilitador");
                    });
                }
                else {
                    if (self.filter.IdFacilitador && self.filter.IdFacilitador != 0)
                        req.IdFacilitador = angular.copy(self.filter.IdFacilitador);
                    else if (self.filter.IdCoordinador && self.filter.IdCoordinador != 0)
                        req.IdFacilitador = angular.copy(self.filter.IdCoordinador);
                    else
                        req.IdFacilitador = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
                    var promesa = AllocationService.darDeptosFacilitador(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            self.departments = [];
                            self.filter.IdDepartamento = 0;
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.departments = o.ListaDepartamento;
                            self.departments.map(function (i) {
                                i.Mostrar = i.Nombre;
                            });
                            self.departments.push({ Nombre: "TODOS", Codigo: 0, Mostrar: "00000000000" });
                        }
                        loading.stopLoading("ReportsController, loadDepartments - darDeptosFacilitador()");
                    }).catch(function (error) {
                        self.departments = [];
                        self.filter.IdDepartamento = 0;
                        console.log(error);
                        loading.stopLoading("ReportsController, loadDepartments - darDeptosFacilitador()");
                    });
                }
            }

            function citiesByDepartment() {
                self.cities = [];
                if (self.filter.IdDepartamento) {
                    loading.startLoading("ReportsController, citiesByDepartment - darMunicipiosFacilitador()");
                    var req = {};
                    req.IdDepartamento = angular.copy(self.filter.IdDepartamento);

                    if (self.rolId == 6 || self.rolId == 7) {
                        var promesa = CommonsUbicaciones.darMunicipios(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                self.cities = [];
                                self.filter.IdMunicipio = 0;
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.cities = o.ListaMunicipio;
                                self.cities.map(function (i) {
                                    i.Mostrar = i.Nombre;
                                });
                                self.cities.push({ Nombre: "TODOS", Codigo: 0, Mostrar: "00000000000" });
                            }
                            loading.stopLoading("ReportsController, citiesByDepartment - darMunicipiosFacilitador");
                        }).catch(function (error) {
                            self.cities = [];
                            self.filter.IdMunicipio = 0;
                            console.log(error);
                            loading.stopLoading("ReportsController, citiesByDepartment - darMunicipiosFacilitador");
                        });
                    }
                    else {
                        if (self.filter.IdFacilitador && self.filter.IdFacilitador != 0)
                            req.IdFacilitador = angular.copy(self.filter.IdFacilitador);
                        else if (self.filter.IdCoordinador && self.filter.IdCoordinador != 0)
                            req.IdFacilitador = angular.copy(self.filter.IdCoordinador);
                        else
                            req.IdFacilitador = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());

                        var promesa = AllocationService.darMunicipiosFacilitador(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                self.cities = [];
                                self.filter.IdMunicipio = 0;
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                self.cities = o.ListaMunicipio;
                                self.cities.map(function (i) {
                                    i.Mostrar = i.Nombre;
                                });
                                self.cities.push({ Nombre: "TODOS", Codigo: 0, Mostrar: "00000000000" });
                            }
                            loading.stopLoading("ReportsController, citiesByDepartment - darMunicipiosFacilitador()");
                        }).catch(function (error) {
                            self.cities = [];
                            self.filter.IdMunicipio = 0;
                            console.log(error);
                            loading.stopLoading("ReportsController, citiesByDepartment - darMunicipiosFacilitador()");
                        });
                    }
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
