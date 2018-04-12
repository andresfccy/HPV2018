using HPV_Entidades.ReporteWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace HPV_Servicios.Reportes
{
    // IHPVServiciosReporte_JSON
    [ServiceContract]
    public interface IHPVServiciosReportes_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarReportes")]
        OS_DarReportes DarReportesOptions(OE_DarReportes oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarReportes")]
        OS_DarReportes DarReportes(OE_DarReportes oe);
        
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarAuditoria")]
        OS_ConsultarAuditoria ConsultarAuditoriaOptions(OE_ConsultarAuditoria oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ConsultarAuditoria")]
        OS_ConsultarAuditoria ConsultarAuditoria(OE_ConsultarAuditoria oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDepartamentosXProfesional")]
        OS_DarDepartamentosXProfesional DarDepartamentosXProfesionalOptions(OE_DarDepartamentosXProfesional oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarDepartamentosXProfesional")]
        OS_DarDepartamentosXProfesional DarDepartamentosXProfesional(OE_DarDepartamentosXProfesional oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipiosXProfesional")]
        OS_DarMunicipiosXProfesional DarMunicipiosXProfesionalOptions(OE_DarMunicipiosXProfesional oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarMunicipiosXProfesional")]
        OS_DarMunicipiosXProfesional DarMunicipiosXProfesional(OE_DarMunicipiosXProfesional oe);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarCoordinadoresXProfesional")]
        OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesionalOptions(OE_DarCoordinadoresXProfesional oe);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarCoordinadoresXProfesional")]
        OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesional(OE_DarCoordinadoresXProfesional oe);
    }
}
