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
                    templateUrl: 'views/systematization/systematizationListUpload.html',
                    controller: 'SystematizationDetailAdminController as sysDetAdmCtrl',
                    resolve: {
                        $title: function () { return 'Sistematización de experiencias detalle administración'; }
                    }
                })
                ;
        }
    ]);