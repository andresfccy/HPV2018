'use strict';

appSystematization
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('systematizationListUpload', {
                    url: '/SistematizacionLista',
                    templateUrl: 'views/systematization/systematizationListUpload.html',
                    controller: 'SystematizationListUploadController as sysListUpCtrl',
                    resolve: {
                        $title: function () { return 'Lista de instrumentos'; }
                    }
                })
                .state('systematizationListAdmin', {
                    url: '/SistematizacionListaAdmin',
                    templateUrl: 'views/systematization/systematizationListAdmin.html',
                    controller: 'SystematizationListAdminController as sysListAdmCtrl',
                    resolve: {
                        $title: function () { return 'Administración de instrumentos'; }
                    }
                })
                .state('systematizationListSuper', {
                    url: '/SistematizacionListaSuper',
                    templateUrl: 'views/systematization/systematizationListSuper.html',
                    controller: 'SystematizationListSuperController as sysListSprCtrl',
                    resolve: {
                        $title: function () { return 'Supervisión de instrumentos'; }
                    }
                })
                .state('systematizationDetailCreateUpload', {
                    url: '/Sistematizacion/Crear',
                    templateUrl: 'views/systematization/systematizationDetailUpload.html',
                    controller: 'SystematizationDetailUploadController as sysDetUpCtrl',
                    resolve: {
                        $title: function () { return 'Creación de instrumentos'; }
                    }
                })
                .state('systematizationDetailUpload', {
                    url: '/Sistematizacion/:id',
                    templateUrl: 'views/systematization/systematizationDetailUpload.html',
                    controller: 'SystematizationDetailUploadController as sysDetUpCtrl',
                    resolve: {
                        $title: function () { return 'Detalle de instrumentos'; }
                    }
                })
                .state('systematizationDetailAdmin', {
                    url: '/SistematizacionAdmin/:id',
                    templateUrl: 'views/systematization/systematizationDetailAdmin.html',
                    controller: 'SystematizationDetailAdminController as sysDetAdmCtrl',
                    resolve: {
                        $title: function () { return 'Aprobación de instrumentos'; }
                    }
                })
                .state('systematizationDetailSuper', {
                    url: '/SistematizacionSuper/:id',
                    templateUrl: 'views/systematization/systematizationDetailSuper.html',
                    controller: 'SystematizationDetailSuperController as sysDetSprCtrl',
                    resolve: {
                        $title: function () { return 'Supervición de instrumentos'; }
                    }
                })
                ;
        }
    ]);