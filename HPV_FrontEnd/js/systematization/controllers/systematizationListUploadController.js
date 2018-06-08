appSystematization
    .controller('SystematizationListUploadController',
    ['$rootScope', '$scope', '$location', '$state', '$translate',
        'growl', 'moment', 'loading',
        'CommonsConstants', 'CommonsListasService', 'SystematizationService',
        function ($rootScope, $scope, $location, $translate, $state, $translate,
            growl, moment, loading,
            CommonsConstants, CommonListasService, SystematizationService) {

            //if (!SessionsBusiness.authorized(43)) {
            if (false) {
                $state.go("home");
                growl.warning("Permisos insuficientes.");
            }
            else {
                var self = this;

                //Funciones
                self.$onInit = init;
                self.goToSystematization = goToSystematization;

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

                self.sysList;

                function init() {

                }

                function goToSystematization(id) {
                    if (id) {
                        $state.go(id);
                    } else {
                        $state.go(1);
                    }
                }
            }
    ]);