using HPV_Entidades.AdmEspacioFisicoWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.AdmEspacioFisico
{
    [ServiceContract]
    public interface IHPVServiciosAdmEspacioFisico_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEspacioFisico")]
        OS_DarEspacioFisico DarEspacioFisico(OE_DarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEspacioFisico")]
        OS_DarEspacioFisico DarEspacioFisicoOptions(OE_DarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDias")]
        OS_DarDias DarDias();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDias")]
        OS_DarDias DarDiasOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarHorarios")]
        OS_DarHorarios DarHorarios();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarHorarios")]
        OS_DarDias DarHorariosOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarGrupos")]
        OS_DarGrupos DarGrupos();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarGrupos")]
        OS_DarGrupos DarGruposOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLugaresMunicipio")]
        OS_DarLugaresMunicipio DarLugaresMunicipio(OE_DarLugaresMunicipio oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarLugaresMunicipio")]
        OS_DarLugaresMunicipio DarLugaresMunicipioOptions(OE_DarLugaresMunicipio oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDireccionesLugar")]
        OS_DarDireccionesLugar DarDireccionesLugar(OE_DarDireccionesLugar oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDireccionesLugar")]
        OS_DarDireccionesLugar DarDireccionesLugarOptions(OE_DarDireccionesLugar oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarEspacioFisico")]
        OS_ActualizarEspacioFisico ActualizarEspacioFisico(OE_ActualizarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ActualizarEspacioFisico")]
        OS_ActualizarEspacioFisico ActualizarEspacioFisicoOptions(OE_ActualizarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarEspacioFisico")]
        OS_ConsultarEspacioFisico ConsultarEspacioFisico(OE_ConsultarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarEspacioFisico")]
        OS_ConsultarEspacioFisico ConsultarEspacioFisicoOptions(OE_ConsultarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEspacioFisicoXAprobar")]
        OS_DarEspacioFisicoXAprobar DarEspacioFisicoXAprobar(OE_DarEspacioFisicoXAprobar oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarEspacioFisicoXAprobar")]
        OS_DarEspacioFisicoXAprobar DarEspacioFisicoXAprobarOptions(OE_DarEspacioFisicoXAprobar oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AprobarEspacioFisico")]
        OS_AprobarEspacioFisico AprobarEspacioFisico(OE_AprobarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AprobarEspacioFisico")]
        OS_AprobarEspacioFisico AprobarEspacioFisicoOptions(OE_AprobarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RechazarEspacioFisico")]
        OS_RechazarEspacioFisico RechazarEspacioFisico(OE_RechazarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RechazarEspacioFisico")]
        OS_RechazarEspacioFisico RechazarEspacioFisicoOptions(OE_RechazarEspacioFisico oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDeptosFacilitador")]
        OS_DarDeptosFacilitador DarDeptosFacilitador(OE_DarDeptosFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDeptosFacilitador")]
        OS_DarDeptosFacilitador DarDeptosFacilitadorOptions(OE_DarDeptosFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipiosFacilitador")]
        OS_DarMunicipiosFacilitador DarMunicipiosFacilitador(OE_DarMunicipiosFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipiosFacilitador")]
        OS_DarMunicipiosFacilitador DarMunicipiosFacilitadorOptions(OE_DarMunicipiosFacilitador oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ReasignarEspacio")]
        OS_ReasignarEspacio ReasignarEspacio(OE_ReasignarEspacio oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ReasignarEspacio")]
        OS_ReasignarEspacio ReasignarEspacioOptions(OE_ReasignarEspacio oe);

    }
}
