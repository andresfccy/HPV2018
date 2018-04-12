'use strict';

angular.module('prosperidad.parameters')
    .controller('ParametersController',
    ['$rootScope', '$scope', '$location', '$translate', 'growl', 'ParametersService', 'loading', 'moment',
        'CommonsConstants', '$state', 'CommonsListasService', 'SessionsBusiness',
    function ($rootScope, $scope, $location, $translate, growl, ParametersService, loading, moment,
        CommonsConstants, $state, CommonsListasService, SessionsBusiness) {

        if (!SessionsBusiness.authorized(14)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.seasonForm;
            self.parameterForm;

            self.initialize = initialize;
            self.initializeForm = initializeForm;
            self.getSeason = getSeason;
            self.seasons = [];
            self.seasonStates = [];
            self.season;
            self.parameter;
            self.parameters = [];
            self.identifySeasson = identifySeasson;
            self.search = {
                IdPeriodo: "",
                FiltroBusqueda: ""
            };
            self.parameters = [];
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

            self.selectSeasonFromList = selectSeasonFromList;
            self.addSeason = addSeason;
            self.saveChangesSeason = saveChangesSeason;
            self.seasonInList = seasonInList;
            self.resetSeasonForm = resetSeasonForm;

            self.selectParameterFromList = selectParameterFromList;
            self.addParameter = addParameter;
            self.saveChangesParameter = saveChangesParameter;
            self.parameterInList = parameterInList;
            self.resetParameterForm = resetParameterForm;

            self.submit = submit;

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            function initialize() {
                getSeasonList();
                getParameterList();

                var req = {
                    Categoria: "ESTADOPERIODO"
                };
                loading.startLoading("ParametersController, initialize - darLista(ESTADOPERIODO)")
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasonStates = o.ListaValor;
                    }
                    loading.stopLoading("ParametersController, initialize - darLista(ESTADOPERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ParametersController, initialize - darLista(ESTADOPERIODO)");
                });
            }

            function getSeasonList() {
                var req = {};
                loading.startLoading("ParametersController, getSeasonList - darPeriodos()");
                var promesa = ParametersService.darPeriodos(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaPeriodo;
                        self.seasons.map(function (o) {
                            if (o.FechaInicio && o.FechaInicio != "")
                                o.FechaInicioNum = moment(o.FechaInicio).toDate();
                            if (o.FechaFin && o.FechaFin != "")
                                o.FechaFinNum = moment(o.FechaFin).toDate();
                            if (o.FechaIniTaller && o.FechaIniTaller != "")
                                o.FechaIniTallerNum = moment(o.FechaIniTaller).toDate();
                        })
                    }
                    loading.stopLoading("ParametersController, getSeasonList - darPeriodos()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ParametersController, getSeasonList - darPeriodos()");
                });
            }

            function getParameterList() {
                var req = {};
                loading.startLoading("ParametersController, getParameterList - darParametros()");
                var promesa = ParametersService.darParametros(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.parameters = o.ListaParametro;

                        self.total = self.parameters.length;
                        self.filtered = self.parameters;
                        self.noOfPages = Math.ceil(self.parameters.length / $scope.maxResultsPerPage);

                        self.parameters.map(function (o) {
                            if (o.Estado)
                                o.EstadoNum = 1;
                            else
                                o.EstadoNum = 0;
                        })
                    }
                    loading.stopLoading("ParametersController, getParameterList - darParametros()");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ParametersController, getParameterList - darParametros()");
                });
            }

            function initializeForm(f, i) {
                switch (i) {
                    case 1:
                        self.seasonForm = f;
                        break;
                    case 2:
                        self.parameterForm = f;
                        break;
                }
            }

            function saveChangesSeason() {
                if (self.seasonForm.$valid) {
                    formatDatesToSend();

                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        Periodo: self.season
                    };
                    loading.startLoading("ParametersController, saveChangesSeason - actualizarPeriodo()");
                    var promesa = ParametersService.actualizarPeriodo(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Se actualizó el periodo correctamente.");
                        }
                        loading.stopLoading("ParametersController, saveChangesSeason - actualizarPeriodo()");
                        getSeasonList();
                        self.season = null;
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("ParametersController, saveChangesSeason - actualizarPeriodo()");
                    });
                }
            }

            function saveChangesParameter() {
                if (self.parameterForm.$valid) {
                    formatStatesToSend();

                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        Parametro: self.parameter
                    };
                    loading.startLoading("ParametersController, saveChangesParameter - actualizarParametro()");
                    var promesa = ParametersService.actualizarParametro(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Se actualizó el parámetro correctamente.");
                        }
                        loading.stopLoading("ParametersController, saveChangesParameter - actualizarParametro()");
                        getParameterList();
                        self.parameter = null;
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("ParametersController, saveChangesParameter - actualizarParametro()");
                    });
                }
            }

            function addSeason(item) {
                if (self.seasonForm.$valid && !self.seasonInList()) {
                    formatDatesToSend();

                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        Periodo: self.season
                    };
                    loading.startLoading("ParametersController, addSeason - actualizarPeriodo()");
                    var promesa = ParametersService.actualizarPeriodo(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Se creó el periodo correctamente.");
                        }
                        loading.stopLoading("ParametersController, addSeason - actualizarPeriodo()");
                        getSeasonList();
                        self.season = null;
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("ParametersController, addSeason - actualizarPeriodo()");
                    });
                }
            }

            function addParameter(item) {
                if (self.parameterForm.$valid && !self.parameterInList()) {
                    formatStatesToSend();

                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        Parametro: self.parameter
                    };
                    loading.startLoading("ParametersController, addParameter - actualizarParametro()");
                    var promesa = ParametersService.actualizarParametro(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            growl.success("Se creó el parametro correctamente.");
                        }
                        loading.stopLoading("ParametersController, addParameter - actualizarParametro()");
                        getParameterList();
                        self.parameter = null;
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("ParametersController, addParameter - actualizarParametro()");
                    });
                }
            }

            function formatDatesToSend() {
                if (self.season.FechaInicioNum && self.season.FechaInicioNum != "")
                    self.season.FechaInicio = moment(self.season.FechaInicioNum).format('YYYY-MM-DD');
                if (self.season.FechaFinNum && self.season.FechaFinNum != "")
                    self.season.FechaFin = moment(self.season.FechaFinNum).format('YYYY-MM-DD');
                if (self.season.FechaIniTallerNum && self.season.FechaIniTallerNum != "")
                    self.season.FechaIniTaller = moment(self.season.FechaIniTallerNum).format('YYYY-MM-DD');
                else
                    self.season.FechaIniTaller = "";
            }

            function formatStatesToSend() {
                if (self.parameter.EstadoNum == 1)
                    self.parameter.Estado = "A";
                else
                    self.parameter.Estado = "I";
            }

            function resetSeasonForm() {
                self.season = null;
            }

            function resetParameterForm() {
                self.parameter = null;
            }

            function seasonInList() {
                if (self.season && self.season.Nombre)
                    return self.seasons.some(function (o) { return o.Nombre == self.season.Nombre; })
            }

            function parameterInList() {
                if (self.parameter && self.parameter.Nombre)
                    return self.parameters.some(function (o) { return o.Nombre == self.parameter.Nombre; })
            }

            function selectSeasonFromList(item) {
                self.season = item;
            }

            function selectParameterFromList(item) {
                self.parameter = item;
                if (item.Estado == "A")
                    self.parameter.EstadoNum = 1;
                else
                    self.parameter.EstadoNum = 0;
            }

            function submit() {

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
        }
    }
    ]);
