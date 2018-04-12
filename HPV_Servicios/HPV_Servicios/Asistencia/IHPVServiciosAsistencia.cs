using HPV_Entidades.AsistenciaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HPV_Servicios.Asistencia
{
    // IHPVServiciosAsistencia
    [ServiceContract]
    public interface IHPVServiciosAsistencia
    {

        [OperationContract]
        OS_DarInscritos DarInscritos(OE_DarInscritos oe);

        [OperationContract]
        OS_DarGruposXFacilitador DarGruposXFacilitador(OE_DarGruposXFacilitador oe);

        [OperationContract]
        OS_DarTalleres DarTalleres();

        [OperationContract]
        OS_GenerarListaAsistencia GenerarListaAsistencia(OE_GenerarListaAsistencia oe);

        [OperationContract]
        OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinador(OE_DarFacilitadorXCoordinador oe);

        [OperationContract]
        OS_DarEntregableEstado DarEntregableEstado(OE_DarEntregableEstado oe);

        [OperationContract]
        OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalle(OE_DarEntregableEstadoDetalle oe);

        [OperationContract]
        OS_DarAsistenciaEstado DarAsistenciaEstado(OE_DarAsistenciaEstado oe);

        [OperationContract]
        OS_DarAsistenteXFacilitador DarAsistenteXFacilitador(OE_DarAsistenteXFacilitador oe);

        [OperationContract]
        OS_RegistrarAsistencia RegistrarAsistencia(OE_RegistrarAsistencia oe);

        [OperationContract]
        OS_RegistrarAprobacion RegistrarAprobacion(OE_RegistrarAprobacion oe);
    }
}
