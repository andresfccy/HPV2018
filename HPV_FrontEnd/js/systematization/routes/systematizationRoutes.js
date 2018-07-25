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
                        $title: function () { return 'Sistematización de experiencias'; }
                    }
                })
                .state('systematizationListAdmin', {
                    url: '/SistematizacionListaAdmin',
                    templateUrl: 'views/systematization/systematizationListAdmin.html',
                    controller: 'SystematizationListAdminController as sysListAdmCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias administración'; }
                    }
                })
                .state('systematizationListSuper', {
                    url: '/SistematizacionListaSuper',
                    templateUrl: 'views/systematization/systematizationListSuper.html',
                    controller: 'SystematizationListSuperController as sysListSprCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias supervisión'; }
                    }
                })
                .state('systematizationDetailCreateUpload', {
                    url: '/Sistematizacion/Crear',
                    templateUrl: 'views/systematization/systematizationDetailUpload.html',
                    controller: 'SystematizationDetailUploadController as sysDetUpCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias creación'; }
                    }
                })
                .state('systematizationDetailUpload', {
                    url: '/Sistematizacion/:id',
                    templateUrl: 'views/systematization/systematizationDetailUpload.html',
                    controller: 'SystematizationDetailUploadController as sysDetUpCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias detalle'; }
                    }
                })
                .state('systematizationDetailAdmin', {
                    url: '/SistematizacionAdmin/:id',
                    templateUrl: 'views/systematization/systematizationDetailAdmin.html',
                    controller: 'SystematizationDetailAdminController as sysDetAdmCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias detalle administración'; }
                    }
                })
                .state('systematizationDetailSuper', {
                    url: '/SistematizacionSuper/:id',
                    templateUrl: 'views/systematization/systematizationDetailSuper.html',
                    controller: 'SystematizationDetailSuperController as sysDetSprCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias detalle supervisión'; }
                    }
                })
                ;
        }
    ]);