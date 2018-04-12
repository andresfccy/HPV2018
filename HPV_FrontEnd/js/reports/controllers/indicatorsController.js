'use strict';

angular.module('prosperidad.reports')
    .controller('IndicatorsController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$http', 'Connection',
        'ReportsService', '$state', 'SessionsBusiness', 'RolesService', 'AttendanceService', 'CommonsUbicaciones', 'AllocationService', 'UsersService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $http, Connection,
        ReportsService, $state, SessionsBusiness, RolesService, AttendanceService, CommonsUbicaciones, AllocationService, UsersService) {
        if (!SessionsBusiness.authorized(53)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.seasons = [];
            self.identifySeasson = identifySeasson;

            self.indicators = [];
            self.filter = {};

            self.generateIndicator = generateIndicator;
            self.selectIndicator = selectIndicator;

            // Cantidad de columnas en la vista de reportes
            self.cols = 3;

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                var req = { Categoria: "PERIODO" };
                loading.startLoading("IndicatorsController, initialize - darLista(PERIODO)");
                var promesa = CommonsListasService.darLista(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.seasons = o.ListaValor;
                        self.filter.IdPeriodo = self.identifySeasson();

                        self.seasons.map(function (i) {
                            i.Mostrar = i.Valor;
                        });
                        self.seasons.push({ Descripcion: "TODOS", Valor: "0", Mostrar: "00000000000" });
                    }
                    loading.stopLoading("IndicatorsController, initialize - darLista(PERIODO)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("IndicatorsController, initialize - darLista(PERIODO)");
                });

                var req = {
                    IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
                };
                loading.startLoading("IndicatorsController, initialize - darReportes");
                var promesa = ReportsService.darReportes(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.indicators = o.ListaReporte;

                        self.indicators = self.indicators.reduce(function (o, l) {
                            o[l.Categoria] = o[l.Categoria] || [];
                            o[l.Categoria].push(l);
                            return o;
                        }, {});

                        var array = [];
                        // Se eliminan todos los objetos que no contentan la cadena "/Indicadores/", descartando así los índices.
                        for (var key in self.indicators) {
                            if (self.indicators.hasOwnProperty(key)) {
                                var obj = { Categoria: key, Lista: self.indicators[key] };
                                for (var i = 0; i < self.indicators[key].length; i++) {
                                    var r = self.indicators[key][i];
                                    if (r.URL.indexOf('/Indicadores/') < 0) {
                                        self.indicators[key].splice(i, 1);
                                        i--;
                                    }
                                }
                                if (obj.Lista.length > 0)
                                    array.push(obj);

                            }
                        }

                        self.indicators = array;
                        console.log("LISTA DE INDICADORES", self.indicators);
                    }
                    
                    loading.stopLoading("IndicatorsController, initialize - darReportes");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("IndicatorsController, initialize - darReportes");
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

            function selectIndicator(obj) {
                self.filter.IdReporte = obj.IdReporte;
                self.filter.url = obj.URL;
                self.filter.name = obj.Descripcion;
            }

            function generateIndicator() {
                if (self.filter.IdReporte && self.filter.IdReporte != 0) {

                    window.open(Connection.API_BASE_URL() + self.filter.url
                                + '?IdPeriodo=' + (self.filter.IdPeriodo || 0)
                                + '&Nombre=' + (self.filter.name || ""));
                }
                else {
                    growl.warning("Se debe seleccionar un indicador.");
                }
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
    ]);
