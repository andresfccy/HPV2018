'use strict';

angular.module('prosperidad.reports')
    .controller('AuditController',
    ['$rootScope', '$scope', '$location', '$translate', 'CommonsListasService', 'growl', 'loading', '$http', 'Connection', 'filterFilter',
        'ReportsService', '$state', 'SessionsBusiness', 'RolesService', 'AttendanceService', 'CommonsUbicaciones', 'UsersService',
    function ($rootScope, $scope, $location, $translate, CommonsListasService, growl, loading, $http, Connection, filterFilter,
        ReportsService, $state, SessionsBusiness, RolesService, AttendanceService, CommonsUbicaciones, UsersService) {
        if (!SessionsBusiness.authorized(52)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;

            self.audits;
            self.filter = {};
            self.functions = [];
            self.users = [];

            self.generateAudits = generateAudits;

            self.opcLimit = [
                { cant: 5 },
                { cant: 10 },
                { cant: 15 },
                { cant: 20 },
            ];
            self.listaCambiados = [];
            $scope.maxResultsPerPage = 20;
            self.maxSize = 5;
            self.currentPage = 1;
            self.noOfPages = 0;
            self.total = 0;
            self.filtered = [];
            $scope.search;

            $scope.$watch('search', function (term) {
                if( self.audits ){
                    self.filtered = filterFilter(self.audits, term);
                    self.total = self.filtered.length;
                    self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
                }
            });

            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                self.filter.IdFuncion = 0;
                self.filter.IdUsuario = -1;

                var req = { FiltroBusqueda: "" };
                loading.startLoading("AuditController, initialize - darUsuarios");
                var promesa = UsersService.darUsuarios(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.users = o.ListaUsuario;
                    }
                    loading.stopLoading("AuditController, initialize - darUsuarios");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AuditController, initialize - darUsuarios");
                });

                var categoriaLista = { Categoria: "FUNCIONES" };
                loading.startLoading("AuditController, initialize - darLista(FUNCIONES)");
                var promesa = CommonsListasService.darLista(categoriaLista).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.functions = o.ListaValor;
                    }
                    loading.stopLoading("AuditController, initialize - darLista(FUNCIONES)");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("AuditController, initialize - darLista(FUNCIONES)");
                });
            }
            

            function generateAudits() {
                if (self.filter.IdUsuario != 0 && self.filter.IdFuncion != 0 && self.filter.FecIniNum && self.filter.FecFinNum) {
                    self.filter.Fec_Ini = moment(self.filter.FecIniNum).format('YYYY-MM-DD');
                    self.filter.Fec_Fin = moment(self.filter.FecFinNum).format('YYYY-MM-DD');
                    var req = self.filter;
                    loading.startLoading("AuditController, generateAudits - darLista(TIPODOCUMENTO)");
                    var promesa = ReportsService.consultarAuditoria(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.audits = o.ListaAuditorias;

                            self.total = self.audits.length;
                            self.filtered = self.audits;
                            self.noOfPages = Math.ceil(self.audits.length / $scope.maxResultsPerPage);
                        }
                        loading.stopLoading("AuditController, generateAudits - darLista(TIPODOCUMENTO)");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("AuditController, generateAudits - darLista(TIPODOCUMENTO)");
                    });
                } else {
                    growl.warning("Todos los filtros son requeridos.");
                }
            }
        }
    }
    ]);
