'use strict';

angular.module('prosperidad.commons')
    .controller('CertificateController',
    ['$location', '$state', 'growl', 'loading', '$http', 'CommonsConstants',
        'InscriptionService', 'CommonsListasService',
    function ($location, $state, growl, loading, $http, CommonsConstants,
        InscriptionService, CommonsListasService) {
        var self = this;
        self.initialize = initialize;
        self.form;

        self.participant;
        self.initializeForm = initializeForm;
        self.generateCertificate = generateCertificate;

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
        }

        function initializeForm(f){
            self.form = f;
        }

        function generateCertificate() {
            if(self.form.$valid){
                var req = angular.copy(self.participant);
                if (self.participant.TipoDocumento) {
                    req.TipoDocumento = self.participant.TipoDocumento.Valor;
                }
                if (!req.CodigoBeneficiario) {
                    req.CodigoBeneficiario = 0;
                }
                req.FechaNacimiento = moment(req.FechaNum).format('YYYY-MM-DD');
                loading.startLoading("CertificateController, generateCertificate - generarCertificado");
                var promesa = InscriptionService.generarCertificado(req).$promise;
                promesa.then(function (o) {
                    //Pregunta si se recibe la respuesta del WS con error, de lo contrario procesa la respuesta.
                    if (o.Respuesta.Codigo && o.Respuesta.Codigo != "0" && o.Respuesta.Codigo != "85") {
                        growl.error("Ha ocurrido un error:\n" + o.Respuesta.Mensaje);
                    } else {
                        if(o.Respuesta.Codigo == "0"){
                            growl.success("Ya se había enviado la encuesta de salida, por lo tanto estamos generando tu certificado. ¡Gracias por participar!.");
                            loading.startLoading("CertificateController, generateCertificate - $http");
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
                    loading.stopLoading("CertificateController, generateCertificate - generarCertificado");
                }).catch(function (error) {
                    console.log(error);
                    loading.stopLoading("CertificateController, generateCertificate - generarCertificado");
                });
            }
        }
    }
    ]);
