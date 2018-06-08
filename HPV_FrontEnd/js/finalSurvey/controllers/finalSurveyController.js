'use strict';

angular.module('prosperidad.finalSurvey')
    .controller('FinalSurveyController',
    ['$rootScope', 'growl', 'CommonsConstants', '$state', '$stateParams', 'loading', '$http',
        'FinalSurveyService',
    function ($rootScope, growl, CommonsConstants, $state, $stateParams, loading,$http,
        FinalSurveyService) {

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
            querySurvey();
        }

        function querySurvey() {
            var req = {
                IdTipoEncuesta: "F"
            }
            loading.startLoading("FinalSurveyService, querySurvey - darEncuesta");
            var promesa = FinalSurveyService.darEncuesta(req).$promise;
            promesa.then(function (o) {
                //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                    growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                } else {
                    var enc = o.ListaEncuesta;
                    enc = enc.reduce(function (o, l) {
                        o[l.CodigoPregunta] = o[l.CodigoPregunta] || [];
                        o[l.CodigoPregunta].push(l);
                        return o;
                    }, []).filter(function (o) { return o ? true : false; });
                    self.survey = angular.copy(enc);
                    console.log(self.survey)
                }
                loading.stopLoading("FinalSurveyService, querySurvey - darEncuesta");
            }).catch(function (error) {
                console.log(error);
                loading.stopLoading("FinalSurveyService, querySurvey - darEncuesta");
            });
        }

        function initializeForm(f) {
            self.form = f;
        }

        function isValidForm() {
            //if (self.survey && self.form && self.surveysAnswers)
            //    return self.survey && self.surveysAnswers && (Object.keys(self.survey).length == Object.keys(self.surveysAnswers).length);
            return self.survey && self.surveysAnswers && (self.survey.length == Object.keys(self.surveysAnswers).length);
            //return false;
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
                loading.startLoading("FinalSurveyService, submit - registrarEncuestaFinal");
                var req = {
                    IdInscrito: $stateParams.id,
                    Encuesta: surveyString,
                    Observaciones: self.comments
                };
                var promesa = FinalSurveyService.registrarEncuestaFinal(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        growl.success("Se enviaron tus respuestas correctamente, estamos generando tu certificado. ¡Gracias por participar!.");
                            loading.startLoading("FinalSurveyService, submit - $http");
                            $http({
                                method: 'POST',
                                data: req,
                                url: CommonsConstants.factory.API_BASE_URL() + "/Reportes/Certificado/GenerarCertificado.aspx"
                                    + "?idInscrito=" + $stateParams.id,
                                responseType: "arraybuffer"
                            }).success(function (data) {
                                var file = new Blob([data], { type: 'application/pdf' });
                                var fileURL = URL.createObjectURL(file);
                                window.open(fileURL, "_blank");
                                loading.stopLoading("FinalSurveyService, submit - $http");
                            }).error(function (data, status, headers, config) {
                                if (status == 404) growl.error("Ha ocurrido un error al realizar la descarga: \n" + data.toUpperCase());
                                if (status == 500) growl.error("Error del servidor, por favor consulte con el administrador.");
                                loading.stopLoading("FinalSurveyService, submit - $http");
                            });
                        $state.go("home");
                    }
                    loading.stopLoading("FinalSurveyService, submit - registrarEncuestaFinal");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("FinalSurveyService, submit - registrarEncuestaFinal");
                });
            }
        }


        function existsId() {
            if ($stateParams.id && $stateParams.id != "") {
                return true;
            }
            return false;
        }

        function getSeason() {
            return $rootScope.actualSeason;
        }

    }]);
