'use strict';

angular.module('prosperidad.roles')
    .controller('RolesController',
    ['$rootScope', '$scope', '$location', '$translate', 'growl', 'RolesService', 'loading', 'filterFilter', '$stateParams', 'SessionsBusiness',
        'CommonsConstants', '$state', 'CommonsListasService', 'UsersService',
    function ($rootScope, $scope, $location, $translate, growl, RolesService, loading, filterFilter, $stateParams, SessionsBusiness,
        CommonsConstants, $state, CommonsListasService, UsersService) {

        if (!SessionsBusiness.authorized(12)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.seasons = [];
            self.identifySeasson = identifySeasson;
            self.back = back;
            self.search = {
                IdPeriodo: "",
                FiltroBusqueda: ""
            };
            self.roles = [];
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
            self.optionsRelation = CommonsConstants.factory.OPTIONS_RELATION;
            self.roleOptions = [];
            self.creation = false;

            self.isEdition = isEdition;
            self.submit = submit;

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.roles, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            self.role;

            function initialize() {
                if (self.isEdition()) {
                    if ($stateParams.id != "Nuevo") {
                        //Es la edición de un rol
                        loading.startLoading("RolesService, initialize - DarRoles");
                        var req = {
                            Codigo: $stateParams.id
                        };
                        var promesa = RolesService.consultarRol(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                $state.go("rolesList");
                            } else {
                                self.role = o.Rol;
                                self.role.OpcionesLista = self.role.Opciones.split(',');
                                // En caso de que el String venga con una última coma, la última posición del arreglo quedará vacía, por lo tanto se elimina.
                                if (self.role.OpcionesLista[self.role.OpcionesLista.length - 1] == "") {
                                    self.role.OpcionesLista.splice(self.role.OpcionesLista.length - 1, 1)
                                }

                                self.role.OpcionesLista.map(function (k) {
                                    self.roleOptions.push(self.optionsRelation[k]);
                                })
                                console.log(self.role)
                            }
                            loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                        });
                    } else {
                        //Creación de un rol
                        self.creation = true;
                    }
                }
                else {
                    loading.startLoading("RolesService, initialize - DarRoles");
                    var req;
                    var promesa = RolesService.darRoles().$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.roles = o.ListaRol;

                            self.total = self.roles.length;
                            self.filtered = self.roles;
                            self.noOfPages = Math.ceil(self.roles.length / $scope.maxResultsPerPage);
                        }
                        loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                    });
                }
            }

            function submit() {
                loading.startLoading("RolesService, submit - actualizarRol");
                self.role.Opciones = "";
                self.roleOptions.map(function (o) {
                    self.optionsRelation.some(function (k, idx) {
                        if (o == k) {
                            self.role.Opciones += idx + ",";
                            return true;
                        }
                    })
                });
                self.role.Opciones = self.role.Opciones.substring(0, self.role.Opciones.length - 1);
                console.log(self.role)

                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                    Rol: self.role
                };
                var promesa = RolesService.actualizarRol(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Rol actualizado exitosamente.");
                        $state.go("rolesList");
                    }
                    loading.stopLoading("EditUsersController, submit - actualizarRol");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditUsersController, submit - actualizarRol");
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

            function isEdition() {
                if ($stateParams.id && $stateParams.id) {
                    return true;
                }
                if ($location.path().indexOf("Editar") >= 0) {
                    $state.go("newRol")
                }
                return false;
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }

            function back() {
                $state.go("rolesList");
            }
        }
    }]);
