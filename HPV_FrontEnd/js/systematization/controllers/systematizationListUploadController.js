appSystematization
    .controller('SystematizationListUploadController',
    ['$rootScope', '$scope', '$location', '$state', '$translate',
        'growl', 'moment', 'loading', 'filterFilter',
        'CommonsConstants', 'CommonsListasService', 'SystematizationService', 'SessionsBusiness',
        function ($rootScope, $scope, $location, $state, $translate,
            growl, moment, loading, filterFilter,
            CommonsConstants, CommonListasService, SystematizationService, SessionsBusiness) {

            if (!SessionsBusiness.authorized(410)) {
                //if (false) {
                $state.go("home");
                growl.warning("Permisos insuficientes.");
            }
            else {
                $scope.search;

                var self = this;

                //Funciones
                self.init = init;
                self.goToSystematization = goToSystematization;
                self.createSystematization = createSystematization;
                self.submitSearch = submitSearch;

                // Variables
                self.opcLimit = [
                    { cant: 10 },
                    { cant: 15 },
                    { cant: 20 },
                ];
                $scope.maxResultsPerPage = 10;
                self.maxSize = 5;
                self.currentPage = 1;
                self.noOfPages = 2;
                self.total = 20;
                self.filtered = [];

                $scope.$watch('search', function (term) {
                    self.filtered = filterFilter(self.sysList, term) || [];
                    self.total = self.filtered.length;
                    self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
                });

                $scope.$watch('maxResultsPerPage', function () {
                    self.noOfPages = Math.ceil(self.filtered.length / $scope.maxResultsPerPage);
                });

                self.sysList;

                function init() {
                    self.submitSearch();
                }

                function submitSearch() {
                    var action = getCtrlName() + ", submitSearch - darCasosDeExito()";
                    loading.startLoading(action);
                    var req = {
                        IdUsuario: SessionsBusiness.getUserIdFromLocalStorage(),
                        FiltroBusqueda: $scope.search,
                        EstadoXConsultar: self.selectedState
                    }
                    var promesa = SystematizationService.darSistematizacion(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "111") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.sysList = o.ListaSistematizacion;
                            self.total = self.sysList.length;
                            self.filtered = self.sysList;
                            self.noOfPages = Math.ceil(self.sysList.length / $scope.maxResultsPerPage);
                        }
                        loading.stopLoading(action);
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading(action);
                    });
                }

                function createSystematization() {
                    // Do something before create
                    $state.go("systematizationDetailCreateUpload");
                }

                function goToSystematization(id) {
                    $state.go("systematizationDetailUpload", { id: id });
                }

                function getCtrlName() {
                    return "SystematizationListUploadController";
                }

                function getSeason() {
                    return $rootScope.actualSeason;
                }
            }
        }
    ]);