appSystematization
    .controller('SystematizationDetailAdminController',
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
                self.$onInit = init;

                function init() {
                    
                }
            }
    ]);