appSystematization
    .controller('SystematizationDetailAdminController',
    ['$rootScope', '$scope', '$location', '$state', '$translate', '$http', '$stateParams',
        'growl', 'moment', 'loading',
        'CommonsConstants', 'CommonsListasService', 'SystematizationService', 'SessionsBusiness',
        'CommonsListasService', 'Connection',
        function ($rootScope, $scope, $location, $state, $translate, $http, $stateParams,
            growl, moment, loading,
            CommonsConstants, CommonListasService, SystematizationService, SessionsBusiness,
            CommonsListasService, Connection) {

            if (!SessionsBusiness.authorized(421)) {
                //if (false) {
                $state.go("home");
                growl.warning("Permisos insuficientes.");
            }
            else {
                var self = this;
                self.init = init;
                self.initForm = initForm;
                self.accept = accept;
                self.reject = reject;
                self.seeDocument = seeDocument;
                self.goBack = goBack;
                self.identifySeasson = identifySeasson;
                self.goBack = goBack;

                function init() {
                    var req = { Categoria: "PERIODO" };
                    var action = getCtrlName() + ", initialize - darLista(PERIODO)";
                    loading.startLoading(action);
                    var promesa = CommonsListasService.darLista(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.seasons = o.ListaValor;
                            self.selectedSeason = self.identifySeasson();
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });

                    var req = {
                        IdSistematizacion: $stateParams.id
                    };
                    var action = getCtrlName() + ", initialize - consultarHistoriaVida";
                    loading.startLoading(action);
                    var promesa = SystematizationService.consultarSistematizacion(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.sys = o.Sistematizacion;
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });
                }

                function accept() {
                    var action = getCtrlName() + ", submit - ActualizarHistoriaVida()";
                    loading.startLoading(action);
                    self.sys.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                    self.sys.IdEstado = "A";
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        Sistematizacion: self.sys
                    }
                    var promesa = SystematizationService.actualizarSistematizacion(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Instrumento aceptado exitosamente.");
                            goBack();
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });
                }

                function reject() {
                    if (self.form.$valid) {
                        var action = getCtrlName() + ", submit - ActualizarHistoriaVida()";
                        loading.startLoading(action);
                        self.sys.IdFacilitador = SessionsBusiness.getUserIdFromLocalStorage();
                        self.sys.IdEstado = "R";
                        var req = {
                            IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                            Sistematizacion: self.sys
                        }
                        var promesa = SystematizationService.actualizarSistematizacion(req).$promise;
                        promesa.then(function (o) {
                            //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                            if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                                growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                            } else {
                                growl.success("Instrumento rechazado exitosamente.");
                                goBack();
                            }
                            loading.stopLoading(action);
                        }).catch(function (error) {
                            console.log(error);
                            loading.stopLoading(action);
                        });
                    }
                }

                function seeDocument() {
                    var action = getCtrlName() + ", seeDocument - $http";
                    loading.startLoading(action);
                    $http({
                        method: 'POST',
                        url: Connection.API_BASE_URL() + '/Sistematizacion/ConsultarInstrumento.aspx'
                            + '?IdSistematizacion=' + self.sys.Id,
                        responseType: "arraybuffer"
                    }).success(function (data) {
                        var file = new Blob([data], { type: 'application/pdf' });
                        var fileURL = URL.createObjectURL(file);
                        window.open(fileURL, "_blank");
                        loading.stopLoading(action);
                    }).error(function (data, status, headers, config) {
                        if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                        if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                        loading.stopLoading(action);
                    });

                }

                function initForm(f) {
                    self.form = f;
                }

                function goBack() {
                    $state.go("systematizationListAdmin");
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

                function getSeason() {
                    return $rootScope.actualSeason;
                }

                function getCtrlName() {
                    return "SystematizationDetailAdminController";
                }
            }
        }
    ]);