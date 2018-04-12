'use strict';

angular.module('prosperidad.reallocation')
    .controller('ReallocationController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading',
        'ReallocationService', '$state', 'SessionsBusiness',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading,
        ReallocationService, $state, SessionsBusiness) {

        if (!SessionsBusiness.authorized(26)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.seasons = [];
            self.identifySeasson = identifySeasson;
            self.reallocation = {};

            self.scheduleSearch = {
                IdPeriodo: "",
                FiltroBusqueda: ""
            };
            self.schedules = [];

            self.activeTab = 0;
            self.scheduleParameters = [{}];
            self.scheduleParamsString = [];
            self.scheduleMaxParameters = 3;
            self.scheduleOpcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            $scope.scheduleMaxResultsPerPage = 5;
            self.scheduleMaxSize = 5;
            self.scheduleCurrentPage = 1;
            self.scheduleNoOfPages = 0;
            self.scheduleTotal = 0;
            self.scheduleFiltered = [];

            self.submitScheduleSearch = submitScheduleSearch;
            self.scheduleAddParameter = scheduleAddParameter;
            self.scheduleRemoveParameter = scheduleRemoveParameter;
            self.scheduleVerifyIfEmpty = scheduleVerifyIfEmpty;
            self.scheduleSendSearch = scheduleSendSearch;

            $scope.$watch('scheduleMaxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.scheduleFiltered.length / $scope.scheduleMaxResultsPerPage);
            });

            self.userSearch = {
                IdPeriodo: "",
                FiltroBusqueda: ""
            };

            self.users = null;
            self.userOpcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            $scope.userMaxResultsPerPageus = 5;
            self.userMaxSize = 5;
            self.userCurrentPage = 1;
            self.userNoOfPages = 0;
            self.userTotal = 0;
            self.userFiltered = [];

            self.submitUserSearch = submitUserSearch;

            $scope.$watch('userMaxResultsPerPageus', function (newv, oldv) {
                self.userNoOfPages = Math.ceil(self.userFiltered.length / $scope.userMaxResultsPerPageus);
            });

            self.selectSchedule = selectSchedule;
            self.selectUser = selectUser;
            self.submit = submit;

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("ReallocationController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.scheduleSearch.IdPeriodo = self.identifySeasson();
                        //self.userSearch.IdPeriodo = self.identifySeasson();
                        self.userSearch.IdPeriodo = self.scheduleSearch.IdPeriodo;
                        self.submitScheduleSearch();
                        self.submitUserSearch();
                    }
                    loading.stopLoading("ReallocationController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReallocationController, initialize - darLista(PERIODO)");
                });
            }

            function submitScheduleSearch() {
                console.log(self.scheduleSearch)
                var req = angular.copy(self.scheduleSearch);
                req.IdCoordinador = SessionsBusiness.getUserIdFromLocalStorage();
                loading.startLoading("ReallocationController, submitScheduleSearch - ConsultarHorarios");
                var promesa = ReallocationService.ConsultarHorarios(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.schedules = angular.copy(o.ListaHorario);
                        self.scheduleTotal = self.schedules.length;
                        self.scheduleFiltered = self.schedules;
                        self.scheduleNoOfPages = Math.ceil(self.schedules.length / $scope.scheduleMaxResultsPerPage);
                        self.submitUserSearch();
                    }
                    loading.stopLoading("ReallocationController, submitScheduleSearch - ConsultarHorarios");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReallocationController, submitScheduleSearch - ConsultarHorarios");
                });
            }

            function submitUserSearch() {
                self.userSearch.IdPeriodo = self.scheduleSearch.IdPeriodo;
                console.log(self.userSearch)
                loading.startLoading("ReallocationController, submitUserSearch - ConsultarInscritos");
                var req = angular.copy(self.userSearch);
                var promesa = ReallocationService.ConsultarInscritos(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.users = angular.copy(o.ListaInscrito);
                        self.userTotal = self.users.length;
                        self.userFiltered = self.users;
                        self.userNoOfPages = Math.ceil(self.users.length / $scope.userMaxResultsPerPageus);
                    }
                    loading.stopLoading("ReallocationController, submitUserSearch - ConsultarInscritos");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReallocationController, submitUserSearch - ConsultarInscritos");
                });
            }

            function submit() {
                var req = {
                    IdInscrito: self.reallocation.user.IdInscrito,
                    IdPeriodo: self.userSearch.IdPeriodo,
                    IdGrupoFacilitador: self.reallocation.schedule.IdGrupoFacilitador,
                    IdUsuarioRegistra: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("ReallocationController, submit - ReasignarEspacio");
                var promesa = ReallocationService.ReasignarEspacio(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success(o.Respuesta.Mensaje);
                        $state.go($state.current, {}, { reload: true });
                    }
                    loading.stopLoading("ReallocationController, submit - ReasignarEspacio");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReallocationController, submit - ReasignarEspacio");
                });
            }

            function selectSchedule(schedule) {
                self.reallocation.schedule = schedule;
                self.activeTab = 1;
            }

            function selectUser(user) {
                self.reallocation.user = user;
                var req = {
                    IdInscrito: self.reallocation.user.IdInscrito,
                    IdPeriodo: self.userSearch.IdPeriodo
                };
                loading.startLoading("ReallocationController, selectUser - ConsultarInscrito");
                var promesa = ReallocationService.ConsultarInscrito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.reallocation.user = angular.copy(o.Inscrito);
                        self.activeTab = 2;
                    }
                    loading.stopLoading("ReallocationController, selectUser - ConsultarInscrito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("ReallocationController, selectUser - ConsultarInscrito");
                });
            }

            function scheduleRemoveParameter(i) {
                self.scheduleParameters.splice(i, 1);
                self.scheduleParamsString.splice(i, 1);
            }

            function scheduleVerifyIfEmpty(i) {
                if (self.scheduleParamsString[i] == "") {
                    self.scheduleRemoveParameter(i);
                }
            }

            function scheduleAddParameter() {
                var i = {};
                if (self.scheduleParameters.length < self.scheduleMaxParameters &&
                    self.scheduleParameters.length == self.scheduleParamsString.length) {
                    self.scheduleParameters.push(i);
                }
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

            function scheduleSendSearch() {
                self.scheduleSearch.FiltroBusqueda = "";
                self.scheduleParamsString.map(function (o) {
                    self.scheduleSearch.FiltroBusqueda += "," + o;
                })
                self.scheduleSearch.FiltroBusqueda = self.scheduleSearch.FiltroBusqueda.substr(1);
                self.submitScheduleSearch();
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
