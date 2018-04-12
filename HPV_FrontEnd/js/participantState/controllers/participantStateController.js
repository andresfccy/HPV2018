'use strict';

angular.module('prosperidad.participantState')
    .controller('ParticipantStateController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl',
        'CommonsConstants', '$log', 'CommonsUbicaciones', '$state', 'moment', 'loading', 'SearchesService',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl,
        CommonsConstants, $log, CommonsUbicaciones, $state, moment, loading, SearchesService) {

        var self = this;

        // ----------------------------------------------------------------------------
        // Declaración de variables
        // ----------------------------------------------------------------------------
        self.participant = {};
        self.user;
        self.docTypes = [];

        self.dateOptions = {
            dateDisabled: false,
            formatYear: 'yyyy',
            maxDate: new Date(),
            startingDay: 1
        };
        self.altInputFormats = ['yyyy/mm/dd'];
        // Variables de control de la vista.
        self.activeTab = 0;
        self.tabList = [
            // Se ordenan objetos para las 2 pestañas de navegación según la 
            // posición en el arreglo y se definen dos atributos, uno para la
            // disponibilidad de la pestaña y uno para su visualización.
            { active: true, show: true }, // 0 - Validación
            { active: false, show: true } // 1 - Datos del participante
        ];
        self.nextButtons = [
            // Se ordenan los 1 botones para avanzar según la posición en el arreglo .
            true  // 0 - Validación
        ];
        self.backButtons = [
            // Se ordenan los 2 botones para retroceder según la posición en el arreglo.
            false,  // 0 - Validación (Siempre oculto y sin función, pero se conserva para mantener orden)
            false  // 1 - Datos del participante
        ];
        // Declaración de los formularios.
        self.forms = [
            {} // 0 - Formulario de Validación
        ];
        self.initForms = [
            {} // 0 - Formulario de Validación
        ];

        // ----------------------------------------------------------------------------
        // Declaración de funciones
        // ----------------------------------------------------------------------------
        /**
         * Función que se ejecuta al iniciar la vista de Inscripción.
         **/
        self.initialize = initialize;

        /**
         * Función que devuelve el objeto que modela el período.
         **/
        self.getSeason = getSeason;

        /**
         * Función que, dado un índice correspondiente a un formulario en la lista de
         * formularios y un un parámetro que modela un formulario, inicializa el de la
         * lista igualándolo al recibido por parámetro.
         **/
        self.initializeForm = initializeForm;

        /** 
         * Función que, dado un índice activa la pestaña, botones y formulario
         * correspondientes a dicho índice.
         **/
        self.activateView = activateView;

        /** 
         * Función que, dado un índice correspondiente a un formulario en la lista de
         * formularios, retorna TRUE o FALSE acorde a la validez del formulario.
         **/
        self.isValidForm = isValidForm;

        /**
         * Función que dado un índice de un formulario, envía la función de
         * envío del formulario según corresponda.
         **/
        self.submitForm = submitForm;

        /**
         * Función que dado un índice de un formulario, realiza las acciones
         * necesarias para volver a la vista anterior.
         **/
        self.goBack = goBack;

        // ----------------------------------------------------------------------------
        // Lógica de las funciones declaradas
        // ----------------------------------------------------------------------------
        function initialize() {
            // Petición de la lista de tipos de documentos.
            var req = { Categoria: "TIPODOCUMENTO" };
            loading.startLoading("ParticipantStateController, initialize - darLista(TIPODOCUMENTO)");
            var promise = CommonsListasService.darLista(req).$promise;
            promise.then(function (o) {
                // Pregunta si se recibe la respuesta del WS con error, de lo contrario 
                // procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    self.docTypes = o.ListaValor;
                }
                loading.stopLoading("ParticipantStateController, initialize - darLista(TIPODOCUMENTO)");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("ParticipantStateController, initialize - darLista(TIPODOCUMENTO)");
            });
        }

        function initializeForm(i, f) {
            self.forms[i] = f;
        }

        function activateView(index) {
            console.log(index)
            if (self.tabList[index].active) {
                // Pestañas
                switch (index) {
                    case 0:
                        self.participant = {};
                        self.tabList[1].show = true;
                        self.tabList[1].active = false;
                        break;
                }
                self.activeTab = index;
            }
        }

        function isValidForm(index) {
            return self.forms[index].$valid;
        }

        function submitForm(index) {
            // Formulario del que vienen los datos
            self.forms[index].$setSubmitted();
            switch (index) {
                case 0:
                    if (self.isValidForm(index)) {
                        validation();
                    }
                    break;
            }
        }

        function goBack(index) {
            // Vista de la que viene la instrucción
            switch (index) {
                case 1:
                    self.tabList[1].active = false;
                    self.user = {};
                    self.activateView(0);
            }
        }

        function validation() {
            var req = angular.copy(self.participant);
            if (self.participant.TipoDocumento) {
                req.TipoDocumento = self.participant.TipoDocumento.Valor;
            }
            if (!req.CodigoBeneficiario) {
                req.CodigoBeneficiario = 0;
            }
            req.FechaNacimiento = moment(req.FechaNum).format('YYYY-MM-DD');
            self.participant.FechaNacimiento = moment(req.FechaNum).format('YYYY-MM-DD');
            loading.startLoading("ParticipantStateController, validation - validarUsuario");
            req.ConsultaEnLinea = 1;
            var promise = InscriptionService.validarUsuario(req).$promise;
            promise.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo
                    && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    var req = {
                        IdInscrito: o.Beneficiario.IdInscrito,
                        IdPeriodo: 0
                    };
                    loading.startLoading("SearchesUserController, initialize - ConsultarInscrito");
                    var promesa = SearchesService.ConsultarInscrito(req).$promise;
                    promesa.then(function (o) {
                        //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                        if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                            growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                        } else {
                            self.tabList[1].active = true;
                            self.activateView(1);
                            console.log(o.Inscrito.ListaAsistenciaTaller.length == 0)

                            // *********************************************************************************************
                            // Descomentar este if para verificar las pruebas de la lista de asistencia
                            // *********************************************************************************************
                            /*
                            if (!o.Inscrito.ListaAsistenciaTaller || o.Inscrito.ListaAsistenciaTaller.length == 0) {
                                o.Inscrito.ListaAsistenciaTaller = [
                                    { FechaRealizacion: "fecha 1", IndAsistencia: 1 },
                                    { FechaRealizacion: "fecha 2", IndAsistencia: 0 },
                                    { FechaRealizacion: "fecha 3", IndAsistencia: 1 },
                                    { FechaRealizacion: "fecha 4", IndAsistencia: 1 },
                                    { FechaRealizacion: "fecha 5", IndAsistencia: 1 },
                                    { FechaRealizacion: "fecha 6", IndAsistencia: 0 },
                                    { FechaRealizacion: "fecha 7", IndAsistencia: 1 },
                                    { FechaRealizacion: "fecha 8", IndAsistencia: 1 }
                                ]
                            }
                            */

                            self.user = angular.copy(o.Inscrito);
                        }
                        loading.stopLoading("SearchesUserController, initialize - ConsultarInscrito");
                    }).catch(function (error) {
                        console.log(error);
                        loading.stopLoading("SearchesUserController, initialize - ConsultarInscrito");
                    });
                }
                loading.stopLoading("ParticipantStateController, validation - validarUsuario");
            }).catch(function (error) {
                console.log(error);
                self.validarButton = false;
                loading.stopLoading("ParticipantStateController, validation - validarUsuario");
            });
        }

        function getSeason() {
            return $rootScope.actualSeason;
        }
    }
    ]);
