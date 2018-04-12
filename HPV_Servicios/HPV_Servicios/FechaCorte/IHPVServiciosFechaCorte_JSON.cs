using HPV_Entidades.FechaCorteWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HPV_Servicios.FechaCorte
{
    // IHPVServiciosFechaCorte_JSON
    [ServiceContract]
    public interface IHPVServiciosFechaCorte_JSON
    {
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarFechaCorte")]
        OS_DarFechaCorte DarFechaCorteOptions(OE_DarFechaCorte OE_DarFechaCorte);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DarFechaCorte")]
        OS_DarFechaCorte DarFechaCorte(OE_DarFechaCorte OE_DarFechaCorte);
    }
}
