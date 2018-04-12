'use strict';

angular.module('prosperidad.sessions')
    .controller('RetrievePasswordController',
    ['$rootScope', '$scope', 'SessionsBusiness', '$state', 'SessionsService', 'growl',
        'UsersService', 'loading', '$stateParams', 'md5',
        function ($rootScope, $scope, SessionsBusiness, $state, SessionsService, growl,
            UsersService, loading, $stateParams, md5) {
            var self = this;
            self.email = null;
            self.form = null;
            self.formChange = null;
            self.changePass = {
                pass:null,
                confirmPass:null
            }

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.initializeForm = initializeForm;
            self.initializeFormChangePass = initializeFormChangePass;
            self.sendingToken = sendingToken;
            self.sendToken = sendToken;
            self.submit = submit;

            function initialize() {
                // Nothing to do
            }

            function initializeForm(f) {
                self.form = f;
            }

            function initializeFormChangePass(fc) {
                console.log("init", fc)
                self.formChange = fc;
            }

            function sendToken() {
                if (self.sendingToken() && self.formChange.$valid) {
                    loading.stopLoading("RetrievePasswordController, sendToken - asignarContrasenaXToken");
                    var req = {
                        Token: $stateParams.token,
                        Contrasena: md5.createHash(self.changePass.pass)
                    }
                    var promise = SessionsService.asignarContrasenaXToken(req).$promise;
                    promise.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success(o.Respuesta.Mensaje);
                            $state.go("login");
                        }
                        loading.stopLoading("RetrievePasswordController, sendToken - asignarContrasenaXToken");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("RetrievePasswordController, sendToken - asignarContrasenaXToken");
                    });
                }
            }

            function sendingToken() {
                if ($stateParams.token && $stateParams.token != "") {
                    return true;
                }
                return false;
            }

            function submit() {
                loading.startLoading("RetrievePasswordController, submit - retrievePassword");
                var req = {
                    Email: self.email
                }
                var promise = SessionsService.retrievePassword(req).$promise;
                promise.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("El correo de reestablecimiento ha sido enviado correctamente.\nPor favor revisa tu bandeja de entrada.");
                        console.log(o);
                    }
                    loading.stopLoading("RetrievePasswordController, submit - retrievePassword");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("RetrievePasswordController, submit - retrievePassword");
                });
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    ]
);