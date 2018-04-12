'use strict';

angular.module('prosperidad.commons')
    .controller('HomeController', ['$rootScope', '$scope', '$location', '$translate', 'moment', '$window',
        'CommonsPeriodoActual', 'CommonsConstants', 'growl', 'loading', 'CommonsListasService',
        function ($rootScope, $scope, $location, $translate, moment, $window,
            CommonsPeriodoActual, CommonsConstants, growl, loading, CommonsListasService) {

            // Para ver una prueba de growl, activar el siguiente código
            /*
            growl.warning("Se agrega un mensaje de alerta", { title: 'Alerta!' });
            growl.info("Se agrega un mensaje de información", { title: 'Información Aleatoria' });
            growl.success("Se agrega un mensaje de éxito"); //no title here
            growl.error("Se agrega un mensaje de error", { title: 'TUVIMOS UN ERROR' });
            */
            var self = this;
            var alreadyInit = false;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.getParameter = getParameter;
            self.version = CommonsConstants.factory.VERSION;
            self.goToTermsPdf = goToTermsPdf;

            function initialize() {
                alreadyInit = true;

                loading.startLoading("HomeController, initialize - get");
                var promise = CommonsPeriodoActual.get().$promise;
                promise.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        //console.log("periopdo actual: ", o);
                        $rootScope.actualSeason = o.Periodo;
                        $rootScope.actualSeason.NomPeriodo = o.Periodo.NomPeriodo;
                        $rootScope.actualSeason.ready = true;
                    }
                    loading.stopLoading("HomeController, initialize - get");
                }).catch(function (er) {
                    growl.error("Ha ocurrido un error:\n" + JSON.stringify(er));
                    loading.stopLoading("HomeController, initialize - get");
                });

                var req = { Categoria: "NOTICIA" };
                loading.startLoading("HomeController, initialize - darLista(NOTICIA)");
                var promise = CommonsListasService.darLista(req).$promise;
                promise.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        $rootScope.news = o.ListaValor;
                    }
                    loading.stopLoading("HomeController, initialize - darLista(NOTICIA)");
                }).catch(function (er) {
                    growl.error("Ha ocurrido un error:\n" + JSON.stringify(er));
                    loading.stopLoading("HomeController, initialize - darLista(NOTICIA)");
                });

                var req = {};
                loading.startLoading("HomeController, initialize - darParametroInicial");
                var promise = CommonsPeriodoActual.darParametroInicial(req).$promise;
                promise.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        //console.log("Parametro inicial: ",o.ParametroInicial);
                        $rootScope.initialParam = o.ParametroInicial;
                        /*$rootScope.actualSeason = o.ParametroInicial;
                        $rootScope.actualSeason.NomPeriodo = o.ParametroInicial.NombrePeriodo;
                        $rootScope.actualSeason.ready = true;*/
                    }
                    loading.stopLoading("HomeController, initialize - darParametroInicial");
                }).catch(function (er) {
                    growl.error("Ha ocurrido un error:\n" + JSON.stringify(er));
                    loading.stopLoading("HomeController, initialize - darParametroInicial");
                });
            }

            function getParameter() {
                if ($rootScope.initialParam && $rootScope.initialParam.FechaIniPreaviso != "") {
                    var now = moment().startOf('day');
                    var preIni = moment($rootScope.initialParam.FechaIniPreaviso);
                    var preEnd = moment($rootScope.initialParam.FechaFinPreaviso);
                    var insIni = moment($rootScope.initialParam.FechaIniInscripcion);
                    var insEnd = moment($rootScope.initialParam.FechaFinInscripcion);

                    //console.log("entro if", { now, preIni, preEnd, insIni, insEnd});
                    if (now >= preIni && now <= preEnd) {
                        //console.log("true")
                        return {
                            code: 0,
                            iniDate: $rootScope.initialParam.FechaIniInscripcion,
                            endDate: $rootScope.initialParam.FechaFinInscripcion
                        };
                    } else if (now >= insIni && now <= insEnd) {
                        return { code: 1 };
                        //console.log("false",1)
                    } else {
                        return { code: 2 };
                        //console.log("false", 2)
                    }
                }
            }

            function isIndex() {
                if (!$location.path().split('/')[1] && $location.path().split('/')[1] == '') {
                    return true;
                }
                return false;
            };

            function goToTermsPdf() {
                $window.open(CommonsConstants.factory.API_BASE_URL() + '/Reportes/PDF/TerminosCondiciones.pdf', '_blank');
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    ]);
