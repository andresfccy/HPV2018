'use strict';

angular.module('prosperidad.commons')
    .controller('HeaderController',
    ['$rootScope', '$scope', '$location', '$translate', 'SessionsBusiness', '$state', 'growl', 'loading', '$window', 'RolesService',
        'InscriptionService', 'CommonsConstants',
    function ($rootScope, $scope, $location, $translate, SessionsBusiness, $state, growl, loading, $window, RolesService,
        InscriptionService, CommonsConstants) {
        var self = this;

        self.initialize = initialize;
        self.isIndex = isIndex;
        self.getSeason = getSeason;
        self.actualRoute = actualRoute;
        self.isActive = isActive;
        self.isLoggedIn = isLoggedIn;
        self.logout = logout;
        self.getNameRole = getNameRole;
        self.getIdRole = getIdRole;
        self.getNameUser = getNameUser;
        self.authorization = authorization;
        self.getRoleUser = getRoleUser;
        self.getNews = getNews;
        self.getParameter = getParameter;
        self.goToSchedulePdf = goToSchedulePdf;
        $rootScope.permissions = [];

        function initialize() {

            loading.startLoading("HeaderController, initialize - darOpcionesUsuario");
            var req = {
                IdUsuario: SessionsBusiness.getUserIdFromLocalStorage()
            };
            if (SessionsBusiness.getUserIdFromLocalStorage() == undefined) {
                req.IdUsuario = -1;
            }
            var promesa = RolesService.darOpcionesUsuario(req).$promise;
            promesa.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    //console.log("respuesta:", o.Rol)
                    var lista = o.Rol.Opciones.split(',');
                    // En caso de que el String venga con una última coma, la última posición del arreglo quedará vacía, por lo tanto se elimina.
                    if (lista[lista.length - 1] == "") {
                        lista.splice(lista.length - 1, 1);
                    }
                    $rootScope.permissions[o.Rol.Codigo] = lista;
                    $rootScope.opcDef.resolve("Permisos listos");
                }
                loading.stopLoading("HeaderController, initialize - darOpcionesUsuario");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("HeaderController, initialize - darOpcionesUsuario");
            });
        }

        function isIndex() {
            if (!$location.path().split('/')[1] && $location.path().split('/')[1] == '') {
                return true;
            }
            return false;
        };

        function getNews() {
            return $rootScope.news;
        }

        function getParameter() {
            if ($rootScope.initialParam && $rootScope.initialParam.FechaIniPreaviso != "") {
                var now = moment().startOf('day');
                var preIni = moment($rootScope.initialParam.FechaIniPreaviso);
                var preEnd = moment($rootScope.initialParam.FechaFinPreaviso);
                var insIni = moment($rootScope.initialParam.FechaIniInscripcion);
                var insEnd = moment($rootScope.initialParam.FechaFinInscripcion);
                if (now >= preIni && now <= preEnd) {
                    return {
                        code: 0,
                        iniDate: $rootScope.initialParam.FechaIniInscripcion,
                        endDate: $rootScope.initialParam.FechaFinInscripcion
                    };
                } else if (now >= insIni && now <= insEnd) {
                    return { code: 1 };
                } else {
                    return { code: 2 };
                }
            }
        }

        function getNameRole() {
            var roleValue = SessionsBusiness.getRolFromLocalStorage();
            var role;
            switch (roleValue) {
                case 1:
                    role = "FACILITADOR";
                    break;
                case 2:
                    role = "COORDINADOR TERRITORIAL";
                    break;
                case 3:
                    role = "INTERVENTOR";
                    break;
                case 4:
                    role = "MESA DE AYUDA";
                    break;
                case 6:
                    role = "SUPERVISOR";
                    break;
                case 7:
                    role = "ADMIN";
                    break;
            }
            return role;
        }


        function getIdRole() {
            return SessionsBusiness.getRolFromLocalStorage();
        }

        function getNameUser() {
            return SessionsBusiness.getNameFromLocalStorage();
        }

        function getRoleUser() {
            return "¡Bienvenido! " + (getNameRole() || "") + (getNameRole() ? ":" : "") + (getNameUser() || "");
        }
        /*
        $rootScope.permissions[1] = [//Rol con id = 1 Facilitador
            1, 13,
            2, 22, 21, 28,
            3, 31, 32,
            4, 41, 42, 47,
            5, 51
        ];
        $rootScope.permissions[2] = [//Rol con id = 2 Coordinador
            1, 13,
            2, 27, 23, 26, 29,
            3, 31, 32,
            4, 43, 44, 48,
            5, 51
        ];
        $rootScope.permissions[3] = [//Rol con id = 3 Interventor
            1, 13,
            3, 31, 32
        ];
        $rootScope.permissions[4] = [//Rol con id = 4 Callcenter
            1, 13,
            3, 31, 32
        ];
        $rootScope.permissions[6] = [//Rol con id = 6 Supervisor
            1, 13,
            2, 26, 27, 210,
            3, 31, 32,
            4, 45, 46, 49,
            5, 51
        ];
        $rootScope.permissions[7] = [//Rol con id = 7 Admin
            1, 11, 12, 13, 14,
            2, 21, 22, 23, 24, 25, 26, 27, 28, 29, 210,
            3, 31, 32,
            4, 41, 42, 43, 44, 45, 46, 47, 48, 49,
            5, 51
        ];
        */
        function authorization(numOpcion) {
            var roleActual = SessionsBusiness.getRolFromLocalStorage();
            if ($rootScope.permissions && $rootScope.permissions[roleActual]) {
                if (roleActual) {
                    return $rootScope.permissions[roleActual].indexOf(numOpcion+"") >= 0;
                }
            }
        }

        function actualRoute() {
            return self.isIndex() ? "Inicio" : $location.path().split('/')[1];
        }

        function isActive(r) {
            if ($location.path().indexOf(r) >= 0 && !self.isIndex()) {
                return true;
            }
            return false;
        }

        function isLoggedIn() {
            return SessionsBusiness.isLoggedIn();
        }

        function logout() {
            SessionsBusiness.removeAllInfoFromLocalStorage();
            $state.go('home');
        }

        function getSeason() {
            return $rootScope.actualSeason;
        }

        function goToSchedulePdf() {
            $window.open(CommonsConstants.factory.API_BASE_URL() + '/Reportes/PDF/160706CronogramaContingencia.pdf', '_blank');
        }
    }
    ]);
