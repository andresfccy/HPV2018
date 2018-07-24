'use strict';

angular.module('prosperidad.sessions')
    .controller('LoginController',
    ['$rootScope', '$scope', 'SessionsBusiness', '$state', 'SessionsService', 'growl',
        'UsersService', 'loading', 'md5', 'RolesService',
        function ($rootScope, $scope, SessionsBusiness, $state, SessionsService, growl,
            UsersService, loading, md5, RolesService) {
            var self = this;

            self.user = null;
            self.login = login;
            self.getSeason = getSeason;

            self.validForm = validForm;
            self.initializeForm = initializeForm;
            self.loginForm = {};

            self.$onInit = initialize();

            function initialize() {
                if (SessionsBusiness.isLoggedIn()) {
                    $state.go("welcomeLoggedIn");
                }
            }

            function login() {
                if ($scope.loginForm.$valid) {
                    var req = angular.copy(self.user);
                    req.Clave = md5.createHash(req.Clave);
                    loading.startLoading("LoginController, login - authenticateUser");
                    var promesa = SessionsService.authenticateUser(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            console.log("Respuesta Login: ",o);
                            // do something with the session
                            growl.success("Ingreso exitoso");
                            // test token is set
                            SessionsBusiness.setTokenToLocalStorage("token_prueba");
                            SessionsBusiness.setUserIdToLocalStorage(o.IdUsuario);
                            var req = { IdUsuario: SessionsBusiness.getUserIdFromLocalStorage() };
                            loading.startLoading("LoginController, login - consultarUsuario");
                            var promesa = UsersService.consultarUsuario(req).$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    SessionsBusiness.setRolToLocalStorage(o.Usuario.IdRol);
                                    SessionsBusiness.setNameToLocalStorage(o.Usuario.PrimerNombre + " " + o.Usuario.SegundoNombre + " " + o.Usuario.PrimerApellido + " " + o.Usuario.SegundoApellido);
                                }
                                loading.stopLoading("LoginController, login - consultarUsuario");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("LoginController, login - consultarUsuario");
                            });

                            loading.startLoading("LoginController, login - darOpcionesUsuario");
                            var req = {
                                IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                            };
                            var promesa = RolesService.darOpcionesUsuario(req).$promise;
                            promesa.then(function (o) {
                                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                                } else {
                                    console.log("respuesta:", o.Rol)
                                    var lista = o.Rol.Opciones.split(',');
                                    // En caso de que el String venga con una última coma, la última posición del arreglo quedará vacía, por lo tanto se elimina.
                                    if (lista[lista.length - 1] == "") {
                                        lista.splice(lista.length - 1, 1);
                                    }
                                    $rootScope.permissions[o.Rol.Codigo] = lista;
                                }
                                loading.stopLoading("LoginController, login - darOpcionesUsuario");
                            }).catch(function (error) {
                                console.log(error);
                                loading.stopLoading("LoginController, login - darOpcionesUsuario");
                            });

                            $state.go("welcomeLoggedIn");
                        }
                        loading.stopLoading("LoginController, login - authenticateUser");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("LoginController, login - authenticateUser");
                    });
                }
            }

            function initializeForm(obj) {
                self.loginForm = obj;
            }

            function validForm() {
                return self.loginForm.$valid;
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    ]
);