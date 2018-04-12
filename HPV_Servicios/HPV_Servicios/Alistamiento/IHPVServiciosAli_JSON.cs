using HPV_Entidades.AdmUsuarioWS;
using HPV_Entidades.AlistamientoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.Alistamiento
{
    // IHPVServiciosAli_JSON
    [ServiceContract]
    public interface IHPVServiciosAli_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarPeriodoVigente")]
        OS_DarPeriodoVigente DarPeriodoVigente();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarPeriodoVigente")]
        OS_DarPeriodoVigente DarPeriodoVigenteOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarBeneficiario")]
        OS_ValidarBeneficiario ValidarBeneficiario(OE_ValidarBeneficiario oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarBeneficiario")]
        OS_ValidarBeneficiario ValidarBeneficiarioOptions(OE_ValidarBeneficiario oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCorreo")]
        OS_ValidarCorreo ValidarCorreo(OE_ValidarCorreo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCorreo")]
        OS_ValidarCorreo ValidarCorreoOptions(OE_ValidarCorreo oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDepartamentos")]
        OS_DarDepartamentos DarDepartamentos();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDepartamentos")]
        OS_DarDepartamentos DarDepartamentosOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipiosXDepto")]
        OS_DarMunicipiosXDepto DarMunicipiosXDepto(OE_DarMunicipiosXDepto oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipiosXDepto")]
        OS_DarMunicipiosXDepto DarMunicipiosXDeptoOptions(OE_DarMunicipiosXDepto oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDiasDisponibles")]
        OS_DarDiasDisponibles DarDiasDisponibles(OE_DarDiasDisponibles oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDiasDisponibles")]
        OS_DarDiasDisponibles DarDiasDisponiblesOptions(OE_DarDiasDisponibles oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarHorariosDisponibles")]
        OS_DarHorariosDisponibles DarHorariosDisponibles(OE_DarHorariosDisponibles oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarHorariosDisponibles")]
        OS_DarHorariosDisponibles DarHorariosDisponiblesOptions(OE_DarHorariosDisponibles oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLugaresDisponibles")]
        OS_DarLugaresDisponibles DarLugaresDisponibles(OE_DarLugaresDisponibles oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLugaresDisponibles")]
        OS_DarLugaresDisponibles DarLugaresDisponiblesOptions(OE_DarLugaresDisponibles oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GuardarInscripcion")]
        OS_GuardarInscripcion GuardarInscripcion(OE_GuardarInscripcion oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GuardarInscripcion")]
        OS_GuardarInscripcion GuardarInscripcionOptions(OE_GuardarInscripcion oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEncuesta")]
        OS_DarEncuesta DarEncuesta(OE_DarEncuesta oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEncuesta")]
        OS_DarEncuesta DarEncuestaOptions(OE_DarEncuesta oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerarCertificado")]
        OS_GenerarCertificado GenerarCertificado(OE_GenerarCertificado oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerarCertificado")]
        OS_GenerarCertificado GenerarCertificadoOptions(OE_GenerarCertificado oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEncuestaFinal")]
        OS_RegistrarEncuestaFinal RegistrarEncuestaFinal(OE_RegistrarEncuestaFinal oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEncuestaFinal")]
        OS_RegistrarEncuestaFinal RegistrarEncuestaFinalOptions(OE_RegistrarEncuestaFinal oe);

    }
}
