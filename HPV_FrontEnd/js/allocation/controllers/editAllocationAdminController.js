'use strict';

angular.module('prosperidad.allocation')
    .controller('EditAllocationAdminController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', 'SessionsBusiness',
        'CommonsConstants', '$log', 'CommonsUbicaciones', '$state', '$stateParams', 'AllocationService', 'loading', 'AttendanceService',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, SessionsBusiness,
        CommonsConstants, $log, CommonsUbicaciones, $state, $stateParams, AllocationService, loading, AttendanceService) {
        
        if (!SessionsBusiness.authorized(23)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;

            self.getSeason = getSeason;
            self.departments = [];
            self.departmentsOk = false;
            self.departmentsByFacilitator = departmentsByFacilitator;
            self.cities = [];
            self.citiesOk = false;
            self.days = [];
            self.allocation = { IdEspacioFisico: $stateParams.IdAllocation };
            self.groups = [];
            self.schedules = [];
            self.cityPlaces = [];
            self.addresses = [];
            self.placesOk = false;
            self.isEdition = isEdition;
            self.submit = submit;
            self.goBack = goBack;
            self.citiesByDepartment = citiesByDepartment;
            self.placesByCity = placesByCity;
            self.searched = null;
            self.searchPlaceName = searchPlaceName;
            self.addressByPlace = addressByPlace;
            self.searched2 = null;
            self.searchAddressName = searchAddressName;
            self.addressOk = false;
            self.facilitators = [];

            self.editAllocationAdminForm = {};
            self.initializeForm = initializeForm;
            self.validForm = validForm;
            self.rolId = SessionsBusiness.getRolFromLocalStorage();


            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                if (self.rolId == 6 || self.rolId == 7) {
                    self.supervisor = true;
                }

                loading.startLoading("editAllocationAdminController, initialize - darUnEspacio");
                if (self.isEdition()) {
                    var req = angular.copy(self.allocation);
                    var promesa = AllocationService.darUnEspacio(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.allocation = o.EspacioFisico;
                            self.departmentsByFacilitator();
                        }
                        loading.stopLoading("editAllocationAdminController, initialize - darUnEspacio");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("editAllocationAdminController, initialize - darUnEspacio");
                    });
                }

                loading.startLoading("editAllocationController, initialize - darDias");
                var req = {};
                var promesa = AllocationService.darDias().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.days = o.ListaDia;
                    }
                    loading.stopLoading("editAllocationController, initialize - darDias");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("editAllocationController, initialize - darDias");
                });

                loading.startLoading("editAllocationController, initialize - darGrupos");
                var promesa = AllocationService.darGrupos().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.groups = o.ListaGrupo;
                    }
                    loading.stopLoading("editAllocationController, initialize - darGrupos");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("editAllocationController, initialize - darGrupos");
                });

                loading.startLoading("editAllocationController, initialize - darHorarios");
                var promesa = AllocationService.darHorarios().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.schedules = o.ListaHorario;
                    }
                    loading.stopLoading("editAllocationController, initialize - darHorarios");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("editAllocationController, initialize - darHorarios");
                });

                var req = {
                    IdCoordinador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("editAllocationController, initialize - darFacilitadorXCoordinador");
                var promesa = AttendanceService.darFacilitadorXCoordinador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.facilitators = o.ListaFacilitador;
                    }
                    loading.stopLoading("editAllocationController, initialize - darFacilitadorXCoordinador");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("editAllocationController, initialize - darFacilitadorXCoordinador");
                });
            }

            function departmentsByFacilitator() {
                self.departments = []; self.departmentsOk = false;
                if (self.allocation.IdFacilitador) {
                    loading.startLoading("editAllocationController, departmentsByFacilitator - darDeptosFacilitador");
                    var req = {
                        IdFacilitador: self.allocation.IdFacilitador
                    };
                    var promesa = AllocationService.darDeptosFacilitador(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.departments = o.ListaDepartamento;
                            self.departmentsOk = true;
                            self.citiesByDepartment();
                        }
                        loading.stopLoading("editAllocationController, departmentsByFacilitator - darDeptosFacilitador");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("editAllocationController, departmentsByFacilitator - darDeptosFacilitador");
                    });
                }
            }

            function citiesByDepartment() {
                self.cities = []; self.citiesOk = false;
                if (self.allocation.IdDepartamento) {
                    loading.startLoading("editAllocationController, citiesByDepartment - darMunicipiosFacilitador");
                    var req = {};
                    req.IdDepartamento = angular.copy(self.allocation.IdDepartamento);
                    req.IdFacilitador = angular.copy(self.allocation.IdFacilitador);
                    var promesa = AllocationService.darMunicipiosFacilitador(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.citiesOk = true;
                            self.cities = o.ListaMunicipio.map(function (item) {
                                return {
                                    Codigo: item.Codigo,
                                    Dane: item.Dane,
                                    IdDepartamento: item.IdDepartamento,
                                    Nombre: item.Nombre
                                }
                            });
                            self.placesByCity();
                        }
                        loading.stopLoading("editAllocationController, citiesByDepartment - darMunicipiosFacilitador");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("editAllocationController, citiesByDepartment - darMunicipiosFacilitador");
                    });
                }
            }

            function placesByCity() {
                self.cityPlaces = []; self.placesOk = false;
                if (self.allocation.IdMunicipio) {
                    loading.startLoading("editAllocationController, placesByCity - darLugaresMunicipio");
                    var city = {};
                    city.IdMunicipio = angular.copy(self.allocation.IdMunicipio);
                    var promesa = AllocationService.darLugaresMunicipio(city).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "42") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.placesOk = true;
                            self.cityPlaces = o.ListaLugarMunicipio.map(function (item) {
                                return {
                                    Nombre: item.Nombre
                                }
                            })
                            self.addressByPlace();
                        }
                        loading.stopLoading("editAllocationController, placesByCity - darLugaresMunicipio");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("editAllocationController, placesByCity - darLugaresMunicipio");
                    });
                }
            }

            function addressByPlace() {
                self.addresses = []; self.addressOk = false;
                if (self.searched) {
                    self.allocation.Lugar = self.searched;
                }
                if (self.allocation.Lugar) {
                    loading.startLoading("editAllocationController, addressByPlace - darDireccionesLugar");
                    var req = {};
                    if (!self.searched) {
                        req.Lugar = angular.copy(self.allocation.Lugar);
                    } else {
                        req.Lugar = angular.copy(self.searched);
                    }
                    req.IdMunicipio = angular.copy(self.allocation.IdMunicipio);
                    var promesa = AllocationService.darDireccionesLugar(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "43") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.addressOk = true;
                            self.addresses = o.ListaDireccion.map(function (item) {
                                return {
                                    Nombre: item.Nombre
                                }
                            })
                        }
                        loading.stopLoading("editAllocationController, addressByPlace - darDireccionesLugar");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("editAllocationController, addressByPlace - darDireccionesLugar");
                    });
                }
            }

            function searchPlaceName(obj) {
                console.log(obj)
                var response = obj ? self.cityPlaces.filter(createFilter(obj)) : self.cityPlaces.filter(function (o) { if (o.Nombre) return o.Nombre; });
                return response;
            }

            function searchAddressName(obj) {
                var response = obj ? self.addresses.filter(createFilter(obj)) : self.addresses.filter(function (o) { if (o.Nombre) return o.Nombre; });
                return response;
            }

            function createFilter(obj) {
                var lowerCase = angular.lowercase(obj);
                return function filterFn(obj_) {
                    return (angular.lowercase(obj_.Nombre).indexOf(lowerCase) >= 0);
                };
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }

            function submit() {
                if (self.editAllocationAdminForm.$valid) {
                    loading.startLoading("editAllocationController, submit - actualizarEspacioFisico");
                    var id = $stateParams.IdAllocation ? $stateParams.IdAllocation : 0;
                    self.allocation.IdEspacioFisico = id;
                    var req = {
                        IdUsuarioRegistra: SessionsBusiness.getUserIdFromLocalStorage(),
                        EspacioFisico: angular.copy(self.allocation)
                    };
                    req.EspacioFisico.IdFacilitador = self.allocation.IdFacilitador;

                    var promesa = AllocationService.actualizarEspacioFisico(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Espacio actualizado/creado correctamente.");
                            $state.go("allocationAdmin");
                        }
                        loading.stopLoading("editAllocationController, submit - actualizarEspacioFisico");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("editAllocationController, submit - actualizarEspacioFisico");
                    });
                }
            }

            function initializeForm(obj) {
                self.editAllocationAdminForm = obj;
            }

            function validForm() {
                console.log("validación form", self.editAllocationAdminForm.$valid, self.editAllocationAdminForm)
                return self.editAllocationAdminForm.$valid;
            }

            function isEdition() {
                if ($stateParams.IdAllocation) {
                    return true;
                }
                if ($location.path().indexOf("Ubicaciones/Editar") >= 0) {
                    $state.go("newAllocation")
                }
                return false;
            }

            function goBack() {
                $state.go("allocationAdmin");
            }
        }
    }
]);
