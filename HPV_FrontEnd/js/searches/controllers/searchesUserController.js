'use strict';

angular.module('prosperidad.searches')
    .controller('SearchesUserController',
    ['$rootScope', '$scope', '$location', '$translate', 'growl', 'SearchesService',
        'CommonsConstants', '$log', '$state', 'filterFilter', 'loading', 'SessionsBusiness', '$stateParams',
    function ($rootScope, $scope, $location, $translate, growl, SearchesService,
        CommonsConstants, $log, $state, filterFilter, loading, SessionsBusiness, $stateParams) {

        if (!SessionsBusiness.authorized(31)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;
            self.initialize = initialize;
            self.getSeason = getSeason;
            self.user = {};

            self.goBack = goBack;

            function initialize() {
                var req = {
                    IdInscrito: $stateParams.Id,
                    IdPeriodo: $stateParams.Season
                };
                loading.startLoading("SearchesUserController, initialize - ConsultarInscrito");
                var promesa = SearchesService.ConsultarInscrito(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.user = angular.copy(o.Inscrito);
                    }
                    loading.stopLoading("SearchesUserController, initialize - ConsultarInscrito");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("SearchesUserController, initialize - ConsultarInscrito");
                });
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }

            function goBack() {
                $state.go("searches");
            }
        }
    }
    ]);
