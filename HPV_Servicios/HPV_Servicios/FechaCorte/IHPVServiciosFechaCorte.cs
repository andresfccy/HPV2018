using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.FechaCorteWS;

namespace HPV_Servicios.FechaCorte
{
    // IHPVServiciosFechaCorte
    [ServiceContract]
    public interface IHPVServiciosFechaCorte
    {
        [OperationContract]
        OS_DarFechaCorte DarFechaCorte(OE_DarFechaCorte OE_DarFechaCorte);
    }
}
