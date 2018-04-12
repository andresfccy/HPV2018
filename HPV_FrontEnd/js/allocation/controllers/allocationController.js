'use strict';

angular.module('prosperidad.allocation')
    .controller('AllocationController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl','loading',
        'CommonsConstants', '$log', 'AllocationService', 'CommonsUbicaciones', '$state', 'SessionsBusiness', 'filterFilter',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl,loading,
        CommonsConstants, $log, AllocationService, CommonsUbicaciones, $state, SessionsBusiness, filterFilter) {

        if (!SessionsBusiness.authorized(22)) {
            $state.go("home");
            growl.warning("Permisos insuficientes.");
        }
        else {
            var self = this;

            self.initialize = initialize;
            self.getSeason = getSeason;
            self.departments = [];
            self.cities = [];
            self.days = [];
            self.placeDirections = [];
            self.physicalSpace = [];
            self.groups = [];
            self.schedules = [];
            self.cityPlaces = [];

            self.opcLimit = [
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
            $scope.search;

            $scope.$watch('search', function (term) {
                self.filtered = filterFilter(self.physicalSpace, term);
                self.total = self.filtered.length;
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });

            $scope.$watch('maxResultsPerPage', function () {
                self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
            });


            // ******************************
            // Métodos internos
            // ******************************
            function initialize() {
                var req = {
                    IdFacilitador: SessionsBusiness.getUserIdFromLocalStorage(),
                    FiltroBusqueda: ""
                };
                loading.startLoading("allocationController, initialize - darEspacio");
                var promesa = AllocationService.darEspacio(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.physicalSpace = o.ListaEspacioFisico;
                        self.total = self.physicalSpace.length;
                        self.filtered = self.physicalSpace;
                        self.noOfPages = Math.ceil(self.physicalSpace.length / $scope.maxResultsPerPage);
                    }
                    loading.stopLoading("allocationController, initialize - darEspacio");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("allocationController, initialize - darEspacio");
                });
            }

            function getSeason() {
                return $rootScope.actualSeason;
            }
        }
    }
]);
