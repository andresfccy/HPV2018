using HPV_Entidades.AsistenciaWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Asistencia
{
    interface InterfaceAsistencia
    {
        OS_DarInscritos DarInscritos(OE_DarInscritos oe);
        OS_DarGruposXFacilitador DarGruposXFacilitador(OE_DarGruposXFacilitador oe);
        OS_DarTalleres DarTalleres();
        OS_GenerarListaAsistencia GenerarListaAsistencia(OE_GenerarListaAsistencia oe);
        OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinador(OE_DarFacilitadorXCoordinador oe);
        OS_DarAsistenciaEncabezado DarAsistenciaEncabezado(OE_DarAsistenciaEncabezado oe);
        OS_DarAsistenciaDetalle DarAsistenciaDetalle(OE_DarAsistenciaDetalle oe);

        OS_DarEntregableEstado DarEntregableEstado(OE_DarEntregableEstado oe);
        OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalle(OE_DarEntregableEstadoDetalle oe);

        OS_DarAsistenciaEstado DarAsistenciaEstado(OE_DarAsistenciaEstado oe);
        OS_DarAsistenteXFacilitador DarAsistenteXFacilitador(OE_DarAsistenteXFacilitador oe);
        OS_RegistrarAsistencia RegistrarAsistencia(OE_RegistrarAsistencia oe);
        OS_RegistrarAprobacion RegistrarAprobacion(OE_RegistrarAprobacion oe);
    }
}
