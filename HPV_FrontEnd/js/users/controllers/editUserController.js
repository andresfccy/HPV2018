'use strict';

angular.module('prosperidad.users')
    .controller('EditUsersController',
    ['$rootScope', '$scope', '$location', '$translate', 'growl', 'UsersService','loading', 'md5',
        'CommonsConstants', '$log', '$state', '$stateParams', 'CommonsUbicaciones', 'RolesService', 'CommonsListasService', 'SessionsBusiness',
    function ($rootScope, $scope, $location, $translate, growl, UsersService,loading, md5,
        CommonsConstants, $log, $state, $stateParams, CommonsUbicaciones, RolesService, CommonsListasService, SessionsBusiness) {

        if (!SessionsBusiness.authorized(11) && !SessionsBusiness.authorized(13)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;

            self.getSeason = getSeason;
            self.roles = [];
            self.municipios = [];
            self.tiposDocumentos = [];
            self.user = { IdUsuario: $stateParams.IdUsuario, CiudadesList: [] };
            self.isEdition = isEdition;
            self.toggleSelection = toggleSelection;
            self.userStates = [];
            self.coords = [];
            self.profiles = [];
            self.submit = submit;
            self.goBack = goBack;
            self.selectedCities = [];

            self.initializeForm = initializeForm;
            self.validForm = validForm;
            self.userForm = {};

            function initialize() {
                if (self.isEdition()) {
                    if (isProfileEdition()) {
                        if (!SessionsBusiness.getUserIdFromLocalStorage() || SessionsBusiness.getUserIdFromLocalStorage() < 0) {
                            $state.go("home");
                        }
                        console.log(SessionsBusiness.getUserIdFromLocalStorage())
                        self.user = { IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(), CiudadesList: [] };
                    }
                    var req = angular.copy(self.user);
                    loading.startLoading("EditUsersController, initialize - consultarUsuario");
                    var promesa = UsersService.consultarUsuario(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.user = angular.copy(o.Usuario);
                            self.user.ClaveConfirmacion = "";
                            self.user.Clave = "";
                            if (!self.user.FechaNacimiento || self.user.FechaNacimiento == "") {
                                delete self.user.FechaNacimiento;
                                delete self.user.FechaNacimientoFormat;
                            }
                            else {
                                self.user.FechaNacimientoFormat = new Date(self.user.FechaNacimiento);
                            }
                            self.user.AliasUsuario = parseInt(self.user.AliasUsuario);
                            self.user.Identificacion = parseInt(self.user.Identificacion);
                            self.user.IdPerfil = self.user.IdPerfil + "";
                            self.user.CiudadesList = self.user.Ciudad.split(',');

                            loading.startLoading("EditUsersController, initialize - darTodosMunicipios");
                            var req;
                            var promesa = CommonsUbicaciones.darTodosMunicipios().$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    self.municipios = angular.copy(o.ListaMunicipio);
                                    var finish = false;
                                    self.municipios.some(function (o) {
                                        if (self.user.CiudadesList.indexOf(o.Codigo + '') >= 0) {
                                            self.selectedCities.push(o)
                                        }
                                        return self.selectedCities.length == self.user.CiudadesList.length;
                                    });
                                }
                                loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                            });
                        }
                        loading.stopLoading("EditUsersController, initialize - consultarUsuario");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditUsersController, initialize - consultarUsuario");
                    });
                }

                var req = angular.copy(self.user);
                loading.startLoading("EditUsersController, initialize - darCoordinadores");
                var promesa = UsersService.darCoordinadores().$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.coords = angular.copy(o.ListaCoordinador);
                    }
                    loading.stopLoading("EditUsersController, initialize - darCoordinadores");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditUsersController, initialize - darCoordinadores");
                });

                if (!self.isEdition()) {
                    var req;
                    loading.startLoading("EditUsersController, initialize - darTodosMunicipios");
                    var promesa = CommonsUbicaciones.darTodosMunicipios().$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.municipios = angular.copy(o.ListaMunicipio);
                        }
                        loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditUsersController, initialize - darTodosMunicipios");
                    });
                }

                var req = { FiltroBusqueda: "" };
                loading.startLoading("EditUsersController, initialize - darRoles");
                var promesa = RolesService.darRolesUsuario(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.roles = angular.copy(o.ListaRol);
                    }
                    loading.stopLoading("EditUsersController, initialize - darRoles");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditUsersController, initialize - darRoles");
                });

                var categoriaLista = { Categoria: "TIPODOCUMENTO" };
                loading.startLoading("EditUsersController, initialize - darLista(TIPODOCUMENTO)");
                var promesa = CommonsListasService.darLista(categoriaLista).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.tiposDocumentos = o.ListaValor.map(function (tipoDocumento) {
                            return {
                                Descripcion: tipoDocumento.Descripcion,
                                Valor: tipoDocumento.Valor,
                                mostrar: tipoDocumento.Descripcion.toLowerCase()
                            }
                        })
                    }
                    loading.stopLoading("EditUsersController, initialize - darLista(TIPODOCUMENTO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditUsersController, initialize - darLista(TIPODOCUMENTO)");
                });

                var pedirLista = { Categoria: "ESTADOSUSUARIO" };
                loading.startLoading("EditUsersController, initialize - darLista(ESTADOSUSUARIO)");
                var promesa = CommonsListasService.darLista(pedirLista).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.userStates = o.ListaValor.map(function (item) {
                            return {
                                Descripcion: item.Descripcion,
                                Valor: item.Valor,
                                mostrar: item.Descripcion.toLowerCase()
                            }
                        })
                    }
                    loading.stopLoading("EditUsersController, initialize - darLista(ESTADOSUSUARIO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditUsersController, initialize - darLista(ESTADOSUSUARIO)");
                });

                var pedirLista = { Categoria: "PERFILESUSUARIO" };
                loading.startLoading("EditUsersController, initialize - darLista(PERFILESUSUARIO)");
                var promesa = CommonsListasService.darLista(pedirLista).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.profiles = o.ListaValor.map(function (item) {
                            return {
                                Descripcion: item.Descripcion,
                                Valor: item.Valor,
                                mostrar: item.Descripcion.toLowerCase()
                            }
                        })
                    }
                    loading.stopLoading("EditUsersController, initialize - darLista(PERFILESUSUARIO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("EditUsersController, initialize - darLista(PERFILESUSUARIO)");
                });
            }

            function submit() {
                if (self.userForm.$valid) {
                    var id = $stateParams.IdUsuario ? $stateParams.IdUsuario : 0;
                    if (isProfileEdition()) {
                        id = SessionsBusiness.getUserIdFromLocalStorage();
                    }
                    self.user.IdUsuario = id;
                    var req = {
                        IdUsuarioRegistra: SessionsBusiness.getUserIdFromLocalStorage(),
                        Usuario: angular.copy(self.user)
                    };
                    loading.startLoading("EditUsersController, submit - actualizarUsuario");
                    req.Usuario.FechaNacimiento = moment(self.user.FechaNacimientoFormat).format('YYYY-MM-DD');
                    req.Usuario.Estado = self.userStates.map(function (o) { if (o.Valor === self.user.CodEstado) return o.Descripcion; })[0];
                    var ciudadesString = "";
                    self.selectedCities.map(function (o) { if (ciudadesString.length == 0) { ciudadesString += o.Codigo } else { ciudadesString += "," + o.Codigo } })
                    req.Usuario.Ciudad = ciudadesString;
                    req.Usuario.Clave = md5.createHash(angular.copy(self.user.Clave));
                    var promesa = UsersService.actualizarUsuario(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {

                            if (!isProfileEdition()) {
                                growl.success("Usuario actualizado/creado correctamente.");
                                $state.go("users");
                            }
                            else {
                                growl.success("Perfil actualizado correctamente.");
                            }
                        }
                        loading.stopLoading("EditUsersController, submit - actualizarUsuario");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("EditUsersController, submit - actualizarUsuario");
                    });
                }
            }

            function toggleSelection(o) {
                console.log(o)
                var idx = self.user.CiudadesList.indexOf(o);

                // is currently selected
                if (idx > -1) {
                    self.user.CiudadesList.splice(idx, 1);
                }

                    // is newly selected
                else {
                    self.user.CiudadesList.push(o);
                }
            }

            function initializeForm(obj) {
                self.userForm = obj;
            }

            function validForm() {
                return self.userForm.$valid && self.selectedCities.length > 0;
            }

            function isEdition() {
                if ($stateParams.IdUsuario && $stateParams.IdUsuario) {
                    return true;
                }
                if ($location.path().indexOf("EditarUsuario") >= 0) {
                    $state.go("newUser")
                }
                return isProfileEdition() || false;
            }

            function isProfileEdition() {
                if ($location.path().indexOf("MiCuenta") >= 0) {
                    return true
                }
                return false;
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }

            function goBack() {
                $state.go("users");
            }
        }
    }]);
