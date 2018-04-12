'use strict';

angular.module('prosperidad.allocation')
    .controller('AllocationAdminController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'filterFilter',
        'CommonsConstants', '$log', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'AllocationService', 'loading',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, filterFilter,
        CommonsConstants, $log, CommonsUbicaciones, $state, SessionsBusiness, AllocationService, loading, AttendanceService) {

        if (!SessionsBusiness.authorized(23)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.departments = [];
            self.cities = [];
            self.days = [];
            self.placeDirections = [];
            self.physicalSpace = [];
            self.groups = [];
            self.schedules = [];
            self.cityPlaces = [];
            self.approveAllocation = approveAllocation;
            self.rejectAllocation = rejectAllocation;

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
            self.rejectReasons = [];
            self.physicalStates = [];
            self.searchFilter = "T";
            self.filterChange = filterChange;
            self.loadAllocation = loadAllocation;

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.physicalSpace, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            self.rolId = SessionsBusiness.getRolFromLocalStorage();
            self.identifySeasson = identifySeasson;

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                if (self.rolId == 6 || self.rolId == 7) {
                    self.supervisor = true;
                    self.searchFilter = "A";
                }
                else {
                    self.searchFilter = "T";
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

                        self.loadAllocation();
                    }
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AttendanceCoordinatorController, initialize - darLista(PERIODO)");
                });

                loading.startLoading("allocationAdminController, initialize - darLista(MOTIVORECHAZO)");
                var req = { Categoria: "MOTIVORECHAZO" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.rejectReasons = o.ListaValor;
                        console.log(self.rejectReasons);
                    }
                    loading.stopLoading("allocationAdminController, initialize - darLista(MOTIVORECHAZO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("allocationAdminController, initialize - darLista(MOTIVORECHAZO)");
                });

                loading.startLoading("allocationAdminController, initialize - darLista(ESTADOSESPACIOFISICO)");
                var req = { Categoria: "ESTADOSESPACIOFISICO" }
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.physicalStates = o.ListaValor;
                    }
                    loading.stopLoading("allocationAdminController, initialize - darLista(ESTADOSESPACIOFISICO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("allocationAdminController, initialize - darLista(ESTADOSESPACIOFISICO)");
                });
            }

            function loadAllocation() {
                loading.startLoading("AllocationAdminController, loadAllocation - getUserIdFromLocalStorage");
                var req = {
                    IdCoordinador: SessionsBusiness.getUserIdFromLocalStorage(),
                    EstadoxConsultar: self.searchFilter,
                    IdPeriodo: self.selectedSeason
                };
                var promesa = AllocationService.darLugaresPorAprobar(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.physicalSpace = o.ListaEspacioFisico;
                        self.total = self.physicalSpace.length;
                        self.filtered = self.physicalSpace;
                        self.noOfPages = Math.ceil(self.physicalSpace.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("AllocationAdminController, loadAllocation - getUserIdFromLocalStorage");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AllocationAdminController, loadAllocation - getUserIdFromLocalStorage");
                });
            }

            function identifySeasson() {
                var value = -1;
                if (self.seasons && getSeason()) {
                    self.seasons.map(function (o) {
                        if (getSeason().NombrePeriodo == o.Descripcion) {
                            value = o.Valor;
                        }
                    })
                }
                return value;
            }

            function filterChange() {
                if (self.searchFilter) {
                    loading.startLoading("allocationAdminController, filterChange - darLugaresPorAprobar");
                    var req = {
                        IdCoordinador: SessionsBusiness.getUserIdFromLocalStorage(),
                        EstadoxConsultar: self.searchFilter
                    };
                    var promesa = AllocationService.darLugaresPorAprobar(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.physicalSpace = o.ListaEspacioFisico;
                            self.total = self.physicalSpace.length;
                            self.filtered = self.physicalSpace;
                            self.noOfPages = Math.ceil(self.physicalSpace.length / self.maxResultsPerPage);
                        }
                        loading.stopLoading("allocationAdminController, filterChange - darLugaresPorAprobar");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("allocationAdminController, filterChange - darLugaresPorAprobar");
                    });
                }
            }

            function approveAllocation(allocationId) {
                loading.startLoading("allocationAdminController, approveAllocation - aprobarEspacioFisico");
                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage(),
                    IdEspacioFisico: allocationId
                };
                var promesa = AllocationService.aprobarEspacioFisico(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success(o.Respuesta.Mensaje);
                        self.physicalSpace.map(function (i) {
                            if (i.IdEspacioFisico == allocationId) {
                                i.CodEstado = "A";
                                self.physicalStates.some(function (e) {
                                    if (e.Valor == 'A') {
                                        i.Estado = e.Descripcion;
                                        return;
                                    }
                                });
                            }
                        })
                    }
                    loading.stopLoading("allocationAdminController, approveAllocation - aprobarEspacioFisico");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("allocationAdminController, approveAllocation - aprobarEspacioFisico");
                });
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }

            function rejectAllocation(allocationId, reason) {
                reason = reason.Descripcion;
                if (reason && reason != "") {
                    loading.startLoading("allocationAdminController, rejectAllocation - rechazarEspacioFisico");
                    var req = {
                        IdCoordinador: SessionsBusiness.getUserIdFromLocalStorage(),
                        IdEspacioFisico: allocationId,
                        MotivoRechazo: reason
                    };
                    var promesa = AllocationService.rechazarEspacioFisico(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success(o.Respuesta.Mensaje)
                            self.physicalSpace.map(function (i) {
                                if (i.IdEspacioFisico == allocationId) {
                                    i.CodEstado = "R";
                                    self.physicalStates.some(function (e) {
                                        if (e.Valor == 'R') {
                                            i.Estado = e.Descripcion;
                                            return;
                                        }
                                    });
                                }
                            })
                        }
                        loading.stopLoading("allocationAdminController, rejectAllocation - rechazarEspacioFisico");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("allocationAdminController, rejectAllocation - rechazarEspacioFisico");
                    });
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }
            }

            function getSeason() {
                return $rootScope.initialParam;
            }
        }
    }
    ]);
