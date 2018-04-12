'use strict';

angular.module('prosperidad.allocation')
    .controller('EditAllocationController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', '$stateParams', 'loading',
        'CommonsConstants', '$log', 'AllocationService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, $stateParams, loading,
        CommonsConstants, $log, AllocationService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter) {

        if (!SessionsBusiness.authorized(22)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;

            self.getSeason = getSeason;
            self.departments = [];
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
            self.approved = false;

            self.allocationForm = {};
            self.initializeForm = initializeForm;
            self.validForm = validForm;

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                if (self.isEdition()) {
                    loading.startLoading("EditAllocationController, initialize - darUnEspacio()");
                    var req = angular.copy(self.allocation);
                    var promesa = AllocationService.darUnEspacio(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            if (o.EspacioFisico.CodEstado == 'A') {
                                self.approved = true;
                                growl.error("No puede editar un espacio físico en estado APROBADO.");
                                $state.go("allocation");
                            }
                            else {
                                self.allocation = o.EspacioFisico;
                                self.citiesByDepartment();
                                self.placesByCity();
                                self.addressByPlace();
                            }
                        }
                        loading.stopLoading("EditAllocationController, initialize - darUnEspacio()");
                    }).catch(function (error) {
                        loading.stopLoading("EditAllocationController, initialize - darUnEspacio()");
                        console.log(error);
                    });
                }

                loading.startLoading("EditAllocationController, initialize - darDeptosFacilitador()");
                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage()
                };
                var promesa = AllocationService.darDeptosFacilitador(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.departments = o.ListaDepartamento;
                    }
                    loading.stopLoading("EditAllocationController, initialize - darDeptosFacilitador()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditAllocationController, initialize - darDeptosFacilitador()");
                });

                loading.startLoading("EditAllocationController, initialize - darDias()");
                var req = {};
                var promesa = AllocationService.darDias().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.days = o.ListaDia;
                    }
                    loading.stopLoading("EditAllocationController, initialize - darDias()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditAllocationController, initialize - darDias()");
                });

                loading.startLoading("EditAllocationController, initialize - darGrupos()");
                var promesa = AllocationService.darGrupos().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.groups = o.ListaGrupo;
                    }
                    loading.stopLoading("EditAllocationController, initialize - darGrupos()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditAllocationController, initialize - darGrupos()");
                });

                loading.startLoading("EditAllocationController, initialize - darHorarios()");
                var promesa = AllocationService.darHorarios().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.schedules = o.ListaHorario;
                    }
                    loading.stopLoading("EditAllocationController, initialize - darHorarios()");
                }).catch(function (error) {
                    loading.stopLoading("EditAllocationController, initialize - darHorarios()");
                    console.log(error);
                });
            }

            function citiesByDepartment() {
                self.cities = []; self.citiesOk = false;
                if (self.allocation.IdDepartamento) {
                    loading.startLoading("EditAllocationController, citiesByDepartment - darMunicipiosFacilitador()");
                    var req = {};
                    req.IdDepartamento = angular.copy(self.allocation.IdDepartamento);
                    req.IdFacilitador = angular.copy(SessionsBusiness.getUserIdFromLocalStorage());
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
                            })
                        }
                        loading.stopLoading("EditAllocationController, citiesByDepartment - darMunicipiosFacilitador()");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditAllocationController, citiesByDepartment - darMunicipiosFacilitador()");
                    });
                }
            }

            function placesByCity() {
                self.cityPlaces = []; self.placesOk = false;
                if (self.allocation.IdMunicipio) {
                    loading.startLoading("EditAllocationController, placesByCity - darLugaresMunicipio()");
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
                        }
                        loading.stopLoading("EditAllocationController, placesByCity - darLugaresMunicipio()");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditAllocationController, placesByCity - darLugaresMunicipio()");
                    });
                }
            }

            function addressByPlace() {
                self.addresses = []; self.addressOk = false;
                if (self.searched) {
                    self.allocation.Lugar = self.searched;
                }
                if (self.allocation.Lugar) {
                    loading.startLoading("EditAllocationController, addressByPlace - darDireccionesLugar()");
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
                        loading.stopLoading("EditAllocationController, addressByPlace - darDireccionesLugar()");
                    }).catch(function (error) {
                        loading.stopLoading("EditAllocationController, addressByPlace - darDireccionesLugar()");
                        console.log(error);
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
                console.log(obj)
                var lowerCase = angular.lowercase(obj);
                return function filterFn(obj_) {
                    return (angular.lowercase(obj_.Nombre).indexOf(lowerCase) >= 0);
                };
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }

            function submit() {
                if (self.allocationForm.$valid && !self.approved) {
                    loading.startLoading("EditAllocationController, submit - actualizarEspacioFisico()");
                    var id = $stateParams.IdAllocation ? $stateParams.IdAllocation : 0;
                    self.allocation.IdEspacioFisico = id;
                    var req = {
                        IdUsuarioRegistra: SessionsBusiness.getUserIdFromLocalStorage(),
                        EspacioFisico: angular.copy(self.allocation)
                    };
                    req.EspacioFisico.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();

                    var promesa = AllocationService.actualizarEspacioFisico(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Espacio actualizado/creado correctamente.");
                            $state.go("allocation");
                        }
                        loading.stopLoading("EditAllocationController, submit - actualizarEspacioFisico()");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditAllocationController, submit - actualizarEspacioFisico()");
                    });
                }
            }

            function initializeForm(obj) {
                self.allocationForm = obj;
            }

            function validForm() {
                return self.allocationForm.$valid;
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
                $state.go("allocation");
            }
        }
    }
]);
