'use strict';

angular.module('prosperidad.inscription')
    .controller('InscriptionController',
    ['$rootScope', '$scope', '$location', '$translate', 'InscriptionService', 'CommonsListasService', 'growl', '$window', '$http',
        'CommonsConstants', '$log', 'CommonsUbicaciones', '$state', 'moment', 'loading',
    function ($rootScope, $scope, $location, $translate, InscriptionService, CommonsListasService, growl, $window, $http,
        CommonsConstants, $log, CommonsUbicaciones, $state, moment, loading) {

        var self = this;

        self.urlBack = CommonsConstants.factory.API_BASE_URL();
        //console.log(self.urlBack)
        // ----------------------------------------------------------------------------
        // Declaración de variables
        // ----------------------------------------------------------------------------
        self.participant = {};
        self.isBenef = -1;
        self.docTypes = [];
        self.beneficiary = {
            CondicionEspecial: { Valor: 0 }
        };
        self.specialConditions = [];
        self.departments = [];
        self.cities = [];
        self.days = [];
        self.hours = [];
        self.places = [];
        self.selectedDpmt = null;
        self.selectedCty = null;
        self.selectedDay = null;
        self.selectedHr = null;
        self.selectedPlc = null;
        self.emailConfirmation = null;
        self.survey = [];
        self.surveysAnswers = [];
        self.surveySent = false;
        self.acceptedTerms = false;
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
            // Se ordenan objetos para las 5 pestañas de navegación según la 
            // posición en el arreglo y se definen dos atributos, uno para la
            // disponibilidad de la pestaña y uno para su visualización.
            { active: true, show: true }, // 0 - Validación
            { active: false, show: true }, // 1 - Datos Básicos
            { active: false, show: false }, // 2 - Disponibilidad
            { active: false, show: false }, // 3 - Encuesta
            { active: false, show: true }  // 4 - Conclusión
        ];
        self.basicDataAvailable = true;
        self.nextButtons = [
            // Se ordenan los 5 botones para avanzar según la posición en el arreglo .
            true,  // 0 - Validación
            false,  // 1 - Datos Básicos
            false,  // 2 - Disponibilidad
            false,  // 3 - Encuesta
            false   // 4 - Conclusión
        ];
        self.backButtons = [
            // Se ordenan los 4 botones para retroceder según la posición en el arreglo.
            false,  // 0 - Validación (Siempre oculto y sin función, pero se conserva para mantener orden)
            false,  // 1 - Datos Básicos
            false,  // 2 - Disponibilidad
            false,  // 3 - Encuesta
            false   // 4 - Conclusión
        ];
        // Declaración de los formularios.
        self.forms = [
            {}, // 0 - Formulario de Validación
            {}, // 1 - Formulario de Datos Básicos
            {}, // 2 - Formulario de Disponibilidad
            {}, // 3 - Formulario de Encuesta
            {}  // 4 - Formulario de confirmación
        ];
        self.initForms = [
            {}, // 0 - Formulario de Validación
            {}, // 1 - Formulario de Datos Básicos
            {}, // 2 - Formulario de Disponibilidad
            {}, // 3 - Formulario de Encuesta
            {}  // 4 - Formulario de confirmación
        ];
        // Declaración del caso en cuestión.
        self.case = -1;
        // Disponibilidad del combo de confirmación de email
        self.emailConfirmationEnable = true;

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
         * Función que retorna TRUE o FALSE acorde a la comparación de fechas para
         * identificar si la inscripción está activa o no.
         **/
        self.inscriptionIsActive = inscriptionIsActive;

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

        /**
         * Función que consulta a los WS los municipios de un departamento.
         **/
        self.citiesByDepartment = citiesByDepartment;

        /**
         * Función que consulta a los WS los días de un municipio.
         **/
        self.daysByCity = daysByCity;

        /**
         * Función que consulta a los WS los horarios de un día.
         **/
        self.schedulesByDay = schedulesByDay;

        /**
         * Función que consulta los lugares de un horario.
         **/
        self.placesBySchedule = placesBySchedule;

        /**
         * Función que captura el evento de cambio del correo de confirmación.
         **/
        self.emailChanged = emailChanged;

        /**
         * Función que guarda una respuesta seleccionada.
         **/
        self.saveAnswer = saveAnswer;

        /**
         * Función que confirma la inscripción del usuario.
         **/
        self.subscribe = subscribe;

        /**
         * Función que le permite a un usuario, dirigirse a la página de términos y condiciones.
         **/
        self.goToTermsPdf = goToTermsPdf;


        // ----------------------------------------------------------------------------
        // Lógica de las funciones declaradas
        // ----------------------------------------------------------------------------
        function initialize() {
            // Petición de la lista de tipos de documentos.
            var req = { Categoria: "TIPODOCUMENTO" };
            loading.startLoading("InscriptionController, initialize - darLista(TIPODOCUMENTO)");
            var promise = CommonsListasService.darLista(req).$promise;
            promise.then(function (o) {
                // Pregunta si se recibe la respuesta del WS con error, de lo contrario 
                // procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    self.docTypes = o.ListaValor;
                }
                loading.stopLoading("InscriptionController, initialize - darLista(TIPODOCUMENTO)");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("InscriptionController, initialize - darLista(TIPODOCUMENTO)");
            });

            // Petición de la lista de condiciones de discapacidad.
            var req = { Categoria: "CONDICIONESPECIAL" };
            loading.startLoading("InscriptionController, initialize - darLista(CONDICIONESPECIAL)");
            var promise = CommonsListasService.darLista(req).$promise;
            promise.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    self.specialConditions = o.ListaValor;
                }
                loading.stopLoading("InscriptionController, initialize - darLista(CONDICIONESPECIAL)");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("InscriptionController, initialize - darLista(CONDICIONESPECIAL)");
            });
        }

        function initializeForm(i, f) {
            self.forms[i] = f;
        }

        function activateView(index) {
            if (self.tabList[index].active && !self.sentSurvey) {
                var found = false;
                self.tabList[index].active = true;
                for (var i = 0; i < self.forms.length; i++) {
                    if (i > index) {
                        $(self.forms[i].$name).blur();
                        //$("#"+self.forms[i].$name)[0].reset()
                        self.tabList[i].active = false;
                        self.tabList[i].show = false;
                        self.nextButtons[i] = false;
                        self.backButtons[i] = false;
                    }
                }

                // Pestañas
                switch (index) {
                    case 0:
                        self.case = -1;
                        self.tabList[1].show = true;
                        self.tabList[4].show = true;
                        self.isBenef = -1;
                        self.beneficiary = {
                            CondicionEspecial: { Valor: 0 }
                        };
                        self.cities = [];
                        self.days = [];
                        self.hours = [];
                        self.places = [];
                        self.selectedDpmt = null;
                        self.selectedCty = null;
                        self.selectedDay = null;
                        self.selectedHr = null;
                        self.selectedPlc = null;
                        self.emailConfirmation = null;
                        self.survey = [];
                        self.surveysAnswers = [];
                        self.surveySent = false;
                        self.acceptedTerms = false;
                        break;
                    case 1:
                        self.cities = [];
                        self.days = [];
                        self.hours = [];
                        self.places = [];
                        self.selectedDpmt = null;
                        self.selectedCty = null;
                        self.selectedDay = null;
                        self.selectedHr = null;
                        self.selectedPlc = null;
                        self.emailConfirmation = null;
                        self.survey = [];
                        self.surveysAnswers = [];
                        self.surveySent = false;
                        self.acceptedTerms = false;
                        break;
                    case 2:
                        self.survey = [];
                        self.surveysAnswers = [];
                        self.surveySent = false;
                        self.acceptedTerms = false;
                        break;
                    case 3:
                        self.acceptedTerms = false;
                        break;
                    case 4:
                        self.acceptedTerms = false;
                        break;
                }
                // Casos
                switch (self.case) {
                    case 1:
                    case 2:
                        self.tabList[2].show = true;
                        break;
                    case 5:
                        self.tabList[2].show = true;
                        self.tabList[3].show = true;
                        break;
                }
                self.activeTab = index;
            }
        }

        function isValidForm(index) {
            if (index == 3) {
                //console.log(self.survey);
                //console.log(self.survey.length);
                //console.log(Object.keys(self.surveysAnswers).length);
                return self.survey && self.surveysAnswers && (self.survey.length == Object.keys(self.surveysAnswers).length);
            }
            /*if (index == 2) {
                return !!self.emailConfirmation && (self.emailConfirmation===self.beneficiary.Correo) && !!self.selectedPlc;
            }*/
            return self.forms[index].$valid;
        }

        function submitForm(index) {
            // Formulario del que vienen los datos
            self.forms[index].$setSubmitted();
            $(self.forms[index].$name).focusout();
            switch (index) {
                case 0:
                    if (self.isValidForm(index)) {
                        validation();
                    }
                    break;
                case 1:
                    if (self.isValidForm(index)) {
                        validateBasicData();
                    }
                    break;
                case 2:
                    if (self.isValidForm(index)) {
                        //if (true) {
                        validateAvailability();
                    }
                    break;
                case 3:
                    if (self.isValidForm(index)) {
                        self.tabList[4].active = true;
                        self.activateView(4);
                    }
                    break;
            }
        }

        function goBack(index) {
            // Vista de la que viene la instrucción
            switch (index) {
                case 1:
                    self.activateView(0);
                case 2:
                    self.activateView(1);
                    break;
                case 3:
                    self.activateView(2);
                    break;
                case 4:
                    // Casos de negocio
                    switch (self.case) {
                        case 0:
                            self.activateView(1);
                            break;
                        case 1:
                        case 2:
                            self.activateView(2);
                            break;
                        case 3:
                        case 4:
                            self.activateView(0);
                            break;
                        case 5:
                            self.activateView(3);
                            break;
                        case 6:
                            self.activateView(0);
                            break;
                    }
                    break;
            }
        }

        function emailChanged() {
            console.log(self.emailConfirmation)
            console.log(self.beneficiary.Correo)
            console.log(self.forms[2])
            if (!(self.emailConfirmation === self.beneficiary.Correo)) {
                self.forms[2]['inscription_text_email_confirm'].$setValidity("emailConfirm", false);
            } else {
                self.forms[2]['inscription_text_email_confirm'].$setValidity("emailConfirm", true);
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
            loading.startLoading("InscriptionController, validation - validarUsuario");
            var promise = InscriptionService.validarUsuario(req).$promise;
            promise.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo
                    && o.Respuesta.Codigo != "0"
                    && o.Respuesta.Codigo != "1"
                    && o.Respuesta.Codigo != "60"
                    && o.Respuesta.Codigo != "61"
                    && o.Respuesta.Codigo != "62"
                    && o.Respuesta.Codigo != "63"
                    && o.Respuesta.Codigo != "105"
                    && o.Respuesta.Codigo != "106"
                    && o.Respuesta.Codigo != "107") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else if (o.Respuesta.Codigo == "0") {
                    // ok
                    self.beneficiary = o.Beneficiario;
                    delete self.beneficiary.Correo;
                    self.basicDataAvailable = false;
                    self.case = 5;
                    self.isBenef = 1;
                    self.tabList[1].active = true;
                    self.activateView(1);
                    growl.info(o.Respuesta.Mensaje);
                } else if (o.Respuesta.Codigo == "1") {
                    // ok
                    self.case = 0;
                    self.isBenef = 0;
                    self.tabList[1].active = true;
                    self.activateView(1);
                } else if (o.Respuesta.Codigo == "60") {
                    // ok
                    self.beneficiary = o.Beneficiario;
                    delete self.beneficiary.Correo;
                    self.basicDataAvailable = false;
                    self.case = 1;
                    self.isBenef = 1;
                    self.tabList[1].active = true;
                    self.tabList[2].show = true;
                    self.activateView(1);
                    growl.info(o.Respuesta.Mensaje);
                } else if (o.Respuesta.Codigo == "61") {
                    // ok
                    self.beneficiary = o.Beneficiario;
                    delete self.beneficiary.Correo;
                    self.basicDataAvailable = false;
                    self.case = 2;
                    self.isBenef = 1;
                    self.tabList[1].active = true;
                    self.tabList[2].show = true;
                    self.activateView(1);
                    growl.info(o.Respuesta.Mensaje);
                } else if (o.Respuesta.Codigo == "62") {
                    // ok
                    self.case = 3;
                    self.isBenef = 1;
                    self.tabList[1].show = false;
                    self.tabList[4].active = true;
                    self.activateView(4);
                    growl.info(o.Respuesta.Mensaje);
                } else if (o.Respuesta.Codigo == "63") {
                    // ok
                    self.case = 4;
                    self.isBenef = 1;
                    self.tabList[1].show = false;
                    self.tabList[4].active = true;
                    self.activateView(4);
                    growl.info(o.Respuesta.Mensaje);
                } else if (o.Respuesta.Codigo == "105") {
                    generateCertificate();
                } else if (o.Respuesta.Codigo == "106") {
                    self.case = 6;
                    self.isBenef = 1;
                    self.tabList[1].show = false;
                    self.tabList[4].active = true;
                    self.activateView(4);
                    //growl.info(o.Respuesta.Mensaje);
                    self.confMessage = o.Respuesta.Mensaje;
                } else if (o.Respuesta.Codigo == "107") {
                    self.case = 7;
                    self.isBenef = 1;
                    self.tabList[1].show = false;
                    self.tabList[4].active = true;
                    self.activateView(4);
                    //growl.info(o.Respuesta.Mensaje);
                    self.confMessage = o.Respuesta.Mensaje;
                }

                if (self.beneficiary) {
                    self.beneficiary.CondicionEspecial = self.specialConditions[0].Valor;
                }
                loading.stopLoading("InscriptionController, validation - validarUsuario");
            }).catch(function (error) {
                console.log(error);
                self.validarButton = false;
                loading.stopLoading("InscriptionController, validation - validarUsuario");
            });
        }

        function generateCertificate() {
            var req = {};
            if (self.participant.TipoDocumento && self.participant.TipoDocumento.Valor) {
                req.TipoDocumento = self.participant.TipoDocumento.Valor;
                self.beneficiary.TipoDocumento = self.participant.TipoDocumento.Valor;
            }
            if (self.participant.Documento && self.participant.Documento != 0) {
                req.Documento = self.participant.Documento;
                self.beneficiary.Documento = self.participant.Documento;
            }
            if (self.participant.CodigoBeneficiario && self.participant.CodigoBeneficiario != 0) {
                req.CodigoBeneficiario = self.participant.CodigoBeneficiario;
                self.beneficiary.CodigoBeneficiario = self.participant.CodigoBeneficiario;
            }
            req.FechaNacimiento = self.participant.FechaNacimiento;
            loading.startLoading("InscriptionController, generateCertificate - generarCertificado");
            var promesa = InscriptionService.generarCertificado(req).$promise;
            promesa.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "85") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    if (o.Respuesta.Codigo == "0") {
                        growl.success("Ya se había enviado la encuesta de salida, por lo tanto estamos generando tu certificado. ¡Gracias por participar!.");
                        loading.startLoading("InscriptionController, generateCertificate - $http");
                        $http({
                            method: 'POST',
                            data: req,
                            url: CommonsConstants.factory.API_BASE_URL() + "/Reportes/Certificado/GenerarCertificado.aspx"
                                + "?idInscrito=" + o.IdInscrito,
                            responseType: "arraybuffer"
                        }).success(function (data) {
                            var file = new Blob([data], { type: 'application/pdf' });
                            var fileURL = URL.createObjectURL(file);
                            window.open(fileURL, "_blank");
                            loading.stopLoading("CertificateController, generateCertificate - $http");
                        }).error(function (data, status, headers, config) {
                            if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                            if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                            loading.stopLoading("CertificateController, generateCertificate - $http");
                        });
                        $state.go("home");
                    } else if (o.Respuesta.Codigo = "85") {
                        $state.go("finalSurvey", { id: o.IdInscrito })
                    }
                }
                loading.stopLoading("InscriptionController, generateCertificate - generarCertificado");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("InscriptionController, generateCertificate - generarCertificado");
            });
        }

        function validateBasicData() {
            var req = {};
            req.Correo = self.beneficiary.Correo;
            if (self.participant.TipoDocumento && self.participant.TipoDocumento.Valor) {
                req.TipoDocumento = self.participant.TipoDocumento.Valor;
                self.beneficiary.TipoDocumento = self.participant.TipoDocumento.Valor;
            }
            if (self.participant.Documento && self.participant.Documento != 0) {
                req.Documento = self.participant.Documento;
                self.beneficiary.Documento = self.participant.Documento;
            }
            if (self.participant.CodigoBeneficiario && self.participant.CodigoBeneficiario != 0) {
                req.CodigoBeneficiario = self.participant.CodigoBeneficiario;
                self.beneficiary.CodigoBeneficiario = self.participant.CodigoBeneficiario;
            }
            self.beneficiary.FechaNacimiento = self.participant.FechaNacimiento;
            loading.startLoading("InscriptionController, validateBasicData - validarCorreo");
            var promise = InscriptionService.validarCorreo(req).$promise;
            promise.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "1") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    // Casos de negocio
                    switch (self.case) {
                        case 0:
                            self.tabList[4].active = true;
                            self.activateView(4);
                            break;
                        case 1:
                        case 2:
                        case 5:
                            self.tabList[2].active = true;
                            loadDepartments();
                            self.activateView(2);
                            break;
                    }
                }
                loading.stopLoading("InscriptionController, validateBasicData - validarCorreo");
            }).catch(function (error) {
                console.log(error);
                self.datosButton = false;
                loading.stopLoading("InscriptionController, validateBasicData - validarCorreo");
            });
        }

        function loadDepartments() {
            self.cities = [];
            self.days = [];
            self.schedules = [];
            self.places = [];
            loading.startLoading("InscriptionController, loadDepartments - darDepartamentos");
            var promesa = CommonsUbicaciones.darDepartamentos().$promise;
            promesa.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    self.departments = o.ListaDepartamento.map(function (dpto) {
                        return {
                            Codigo: dpto.Codigo,
                            Dane: dpto.Dane,
                            Indicativo: dpto.Indicativo,
                            Nombre: dpto.Nombre
                        }
                    })
                }
                loading.stopLoading("InscriptionController, loadDepartments - darDepartamentos");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("InscriptionController, loadDepartments - darDepartamentos");
            });

        }

        function citiesByDepartment() {
            self.cities = [];
            self.days = [];
            self.schedules = [];
            self.places = [];
            if (self.selectedDpmt) {
                var req = angular.copy(self.selectedDpmt)
                req.IdDepartamento = req.Codigo;
                loading.startLoading("InscriptionController, citiesByDepartment - darMunicipios");
                var promesa = CommonsUbicaciones.darMunicipios(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.cities = o.ListaMunicipio.map(function (item) {
                            return {
                                Codigo: item.Codigo,
                                Dane: item.Dane,
                                IdDepartamento: item.IdDepartamento,
                                Nombre: item.Nombre
                            }
                        })
                    }
                    loading.stopLoading("InscriptionController, citiesByDepartment - darMunicipios");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("InscriptionController, citiesByDepartment - darMunicipios");
                });
            }
        }

        function daysByCity() {
            self.days = [];
            self.schedules = [];
            self.places = [];
            if (self.selectedCty) {
                var req = angular.copy(self.selectedCty)
                req.IdMunicipio = req.Codigo;
                loading.startLoading("InscriptionController, daysByCity - darDias");
                var promesa = CommonsUbicaciones.darDias(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.days = o.ListaDia.map(function (item) {
                            return {
                                Codigo: item.Codigo,
                                Nombre: item.Nombre
                            }
                        })
                    }
                    loading.stopLoading("InscriptionController, daysByCity - darDias");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("InscriptionController, daysByCity - darDias");
                });
            }
        }

        function schedulesByDay() {
            self.schedules = [];
            self.places = [];
            if (self.selectedDay) {
                var req = angular.copy(self.selectedDay)
                req.IdDia = req.Codigo;
                req.IdMunicipio = self.selectedCty.Codigo;
                loading.startLoading("InscriptionController, schedulesByDay - darHoras");
                var promesa = CommonsUbicaciones.darHoras(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.schedules = o.ListaHorario.map(function (item) {
                            return {
                                Codigo: item.Codigo,
                                Nombre: item.Nombre
                            }
                        })
                    }
                    loading.stopLoading("InscriptionController, schedulesByDay - darHoras");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("InscriptionController, schedulesByDay - darHoras");
                });
            }
        }

        function placesBySchedule() {
            self.places = [];
            if (self.selectedHr) {
                var req = angular.copy(self.selectedHr)
                req.IdHorario = req.Codigo;
                req.IdDia = self.selectedDay.Codigo;
                req.IdMunicipio = self.selectedCty.Codigo;
                loading.startLoading("InscriptionController, placesBySchedule - darLugares");
                var promesa = CommonsUbicaciones.darLugares(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        self.places = o.ListaLugar.map(function (item) {
                            return {
                                Nombre: item.Nombre,
                                GrupoFacilitador: item.Grupofacilitador
                            }
                        })
                    }
                    loading.stopLoading("InscriptionController, placesBySchedule - darLugares");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("InscriptionController, placesBySchedule - darLugares");
                });
            }
        }

        function validateAvailability() {
            var req = { IdTipoEncuesta: "I" };
            loading.startLoading("InscriptionController, validateAvailability - darEncuesta");
            var promise = InscriptionService.darEncuesta(req).$promise;
            promise.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    switch (self.case) {
                        case 1:
                        case 2:
                            self.tabList[4].active = true;
                            self.activateView(4);
                            break;
                        case 5:
                            var enc = o.ListaEncuesta;
                            enc = enc.reduce(function (o, l) {
                                o[l.CodigoPregunta] = o[l.CodigoPregunta] || [];
                                o[l.CodigoPregunta].push(l);
                                return o;
                            }, []).filter(function (o) { return o ? true : false; });
                            self.survey = angular.copy(enc);
                            self.tabList[3].active = true;
                            self.activateView(3);
                            break;
                    }
                }
                loading.stopLoading("InscriptionController, validateAvailability - darEncuesta");
            }).catch(function (error) {
                loading.stopLoading("InscriptionController, validateAvailability - darEncuesta");
                console.log(error);
            });
        }

        function subscribe() {
            loading.startLoading("InscriptionController, subscribe - guardarInscripcion");
            var surveyString = "";
            self.surveysAnswers.map(function (o) {
                if (surveyString.indexOf(',') < 0) {
                    surveyString += o.CodigoPregunta + "," + o.CodigoRespuesta;
                }
                else {
                    surveyString += "," + o.CodigoPregunta + "," + o.CodigoRespuesta;
                }
            })
            var req = {
                Beneficiario: self.beneficiary,
                RespuestaEncuesta: surveyString,
                EsBeneficiario: self.isBenef,
            };
            if (self.selectedPlc) {
                req.GrupoFacilitador = self.selectedPlc.GrupoFacilitador;
            }
            var promise = InscriptionService.guardarInscripcion(req).$promise;
            promise.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    growl.success(o.Respuesta.Mensaje);
                    self.tabList[0].active = false;
                    self.sentSurvey = true;
                }
                loading.stopLoading("InscriptionController, subscribe - guardarInscripcion");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("InscriptionController, subscribe - guardarInscripcion");
            });
        }

        function saveAnswer(qst, answr) {
            self.surveysAnswers[qst] = answr;
        }

        function getSeason() {
            //console.log($rootScope.actualSeason);
            return $rootScope.actualSeason;
        }

        function inscriptionIsActive() {
            if (getSeason()) {
                var today = moment();
                var initDate = moment(getSeason().FechaInicial);
                var finalDate = moment(getSeason().FechaFinal);
                if (initDate <= today && today <= finalDate) {
                    return true;
                } else {
                    // Cambiar a FALSE cuando se necesite la validación correcta.
                    // Siempre retorna TRUE para pruebas.
                    return false;
                }
            }
        }

        function goToTermsPdf() {
            $window.open(CommonsConstants.factory.API_BASE_URL() + '/Reportes/PDF/TerminosCondiciones.pdf', '_blank');
        }
    }
    ]);
