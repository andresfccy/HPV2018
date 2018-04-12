using HPV_Entidades.ReporteWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Reportes
{
    // IHPVServiciosReporte
    [ServiceContract]
    public interface IHPVServiciosReportes
    {
        [OperationContract]
        OS_DarReportes DarReportes(OE_DarReportes oe);

        [OperationContract]
        OS_ConsultarAuditoria ConsultarAuditoria(OE_ConsultarAuditoria oe);

        [OperationContract]
        OS_DarDepartamentosXProfesional DarDepartamentosXProfesional(OE_DarDepartamentosXProfesional oe);

        [OperationContract]
        OS_DarMunicipiosXProfesional DarMunicipiosXProfesional(OE_DarMunicipiosXProfesional oe);

        [OperationContract]
        OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesional(OE_DarCoordinadoresXProfesional oe);
    }
}
