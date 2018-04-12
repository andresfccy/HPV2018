using HPV_Entidades.GeneralWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace HPV_Servicios.General
{
    // IHPVServiciosGen_JSON
    [ServiceContract]
    public interface IHPVServiciosGen_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarListaValor")]
        OS_DarListaValor DarListaValor(OE_DarListaValor oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarListaValor")]
        OS_DarListaValor DarListaValorOptions(OE_DarListaValor oe);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "EnviarCorreo")]
        OS_EnviarCorreo EnviarCorreo(OE_EnviarCorreo oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "EnviarCorreo")]
        OS_EnviarCorreo EnviarCorreoOptions(OE_EnviarCorreo oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarParametroInicial")]
        OS_DarParametroInicial DarParametroInicial();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarParametroInicial")]
        OS_DarParametroInicial DarParametroInicialOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarToken")]
        OS_ValidarToken ValidarToken(OE_ValidarToken oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarToken")]
        OS_ValidarToken ValidarTokenOptions(OE_ValidarToken oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEncuestaSatisfaccion")]
        OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccion(OE_RegistrarEncuestaSatisfaccion oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEncuestaSatisfaccion")]
        OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccionOptions(OE_RegistrarEncuestaSatisfaccion oe);

    }
}
