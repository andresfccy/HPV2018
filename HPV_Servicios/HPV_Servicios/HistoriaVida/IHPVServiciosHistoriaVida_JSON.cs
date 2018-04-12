using HPV_Entidades.HistoriasDeVidaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.HistoriaVida
{
    // IHPVServiciosHistoriaVida_JSON
    [ServiceContract]
    public interface IHPVServiciosHistoriaVida_JSON
    {

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarHistoriaVida")]
        OS_ActualizarHistoriaVida ActualizarHistoriaVida(OE_ActualizarHistoriaVida oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarHistoriaVida")]
        OS_ActualizarHistoriaVida ActualizarHistoriaVidaOptions(OE_ActualizarHistoriaVida oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarHistoriaVida")]
        OS_ConsultarHistoriaVida ConsultarHistoriaVida(OE_ConsultarHistoriaVida oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarHistoriaVida")]
        OS_ConsultarHistoriaVida ConsultarHistoriaVidaOptions(OE_ConsultarHistoriaVida oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarHistoriasDeVida")]
        OS_DarHistoriasDeVida DarHistoriasDeVida(OE_DarHistoriasDeVida oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarHistoriasDeVida")]
        OS_DarHistoriasDeVida DarHistoriasDeVidaOptions(OE_DarHistoriasDeVida oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCantidadHistorias")]
        OS_ValidarCantidadHistorias ValidarCantidadHistorias(OE_ValidarCantidadHistorias oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidarCantidadHistorias")]
        OS_ValidarCantidadHistorias ValidarCantidadHistoriasOptions(OE_ValidarCantidadHistorias oe);
        
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEstadoHistoriaVida")]
        OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVida(OE_RegistrarEstadoHistoriaVida oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegistrarEstadoHistoriaVida")]
        OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVidaOptions(OE_RegistrarEstadoHistoriaVida oe);
    }
}
