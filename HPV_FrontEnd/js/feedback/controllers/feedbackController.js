'use strict';

angular.module('prosperidad.users')
    .controller('FeedbackController',
    ['$rootScope', 'growl', 'CommonsConstants', '$state', '$stateParams', 'loading',
        'FeedbackService',
    function ($rootScope, growl, CommonsConstants, $state, $stateParams, loading,
        FeedbackService) {

        // Variables y funciones generales
        var self = this;
        self.initialize = initialize;
        self.getSeason = getSeason;

        // Variables
        self.showSurvey = false;
        self.survey;
        self.form;
        self.surveysAnswers = [];
        self.response;
        self.comments;

        // Funciones
        self.initializeForm = initializeForm;
        self.isValidForm = isValidForm;
        self.saveAnswer = saveAnswer;
        self.submit = submit;

        function initialize() {
            if (existsToken()) {
                var req = {
                    Categoria: "ENCUESTASATISFACION",
                    Token: $stateParams.token
                }
                loading.startLoading("FeedbackController, initialize - validarToken");
                var promesa = FeedbackService.validarToken(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && (o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "1" && o.Respuesta.Codigo != "2" && o.Respuesta.Codigo != "3")) {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        switch (o.Respuesta.Codigo) {
                            case 0:
                                self.response = 0;
                                self.showSurvey = true;
                                querySurvey();
                                break;
                            case 1:
                                self.response = 1;
                                break;
                            case 2:
                                self.response = 2;
                                break;
                            case 3:
                                $state.go("home");
                                break;
                        }
                    }
                    loading.stopLoading("FeedbackController, initialize - validarToken");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("FeedbackController, initialize - validarToken");
                });
            }
        }

        function querySurvey() {
            var req = {
                IdTipoEncuesta: "S"
            }
            loading.startLoading("FeedbackController, querySurvey - darEncuesta");
            var promesa = FeedbackService.darEncuesta(req).$promise;
            promesa.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    var enc = o.ListaEncuesta.reverse();
                    enc = enc.reduce(function (o, l) {
                        o[l.CodigoPregunta] = o[l.CodigoPregunta] || [];
                        o[l.CodigoPregunta].push(l);
                        return o;
                    }, []).filter(function (o) { return o ? true : false; });
                    self.survey = angular.copy(enc);
                }
                loading.stopLoading("FeedbackController, querySurvey - darEncuesta");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("FeedbackController, querySurvey - darEncuesta");
            });
        }

        function initializeForm(f) {
            self.form = f;
        }

        function isValidForm() {
            //if(self.survey && self.form && self.surveysAnswers)
            //    return self.survey && self.surveysAnswers && (Object.keys(self.survey).length == Object.keys(self.surveysAnswers).length);
            //return false;
            return self.survey && self.surveysAnswers && (self.survey.length == Object.keys(self.surveysAnswers).length);
        }

        function saveAnswer(qst, answr) {
            self.surveysAnswers[qst] = answr;
        }

        function submit() {
            if (isValidForm()) {
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
                    Token: $stateParams.token,
                    RespuestaEncuesta: surveyString,
                    Observaciones: self.comments + " / " + self.comments2
                };
                var promesa = FeedbackService.registrarEncuestaSatisfaccion(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Se enviaron tus respuestas correctamente, ¡Gracias por participar!.");
                        $state.go("home");
                    }
                    loading.stopLoading("FeedbackController, initialize - validarToken");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("FeedbackController, initialize - validarToken");
                });
            }
        }


        function existsToken() {
            if ($stateParams.token && $stateParams.token != "") {
                return true;
            }
            return false;
        }

        function getSeason() {
            return $rootScope.actualSeason;
        }

    }]);
