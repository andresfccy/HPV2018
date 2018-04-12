using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HPV_Entidades.AsistenciaWS;
using HPV_Datos.Asistencia;

namespace HPV_Servicios.Asistencia
{
    // HPVServiciosAsistencia
    // El servicio se publica en HPVServiciosAsistencia.svc or HPVServiciosAsistencia.svc.cs
    public class HPVServiciosAsistencia : IHPVServiciosAsistencia
    {
        public OS_DarAsistenciaEstado DarAsistenciaEstado(OE_DarAsistenciaEstado oe)
        {
            return (new FachadaAsistencia().DarAsistenciaEstado(oe));
        }

        public OS_DarAsistenteXFacilitador DarAsistenteXFacilitador(OE_DarAsistenteXFacilitador oe)
        {
            return (new FachadaAsistencia().DarAsistenteXFacilitador(oe));
        }

        public OS_DarEntregableEstado DarEntregableEstado(OE_DarEntregableEstado oe)
        {
            return (new FachadaAsistencia().DarEntregableEstado(oe));
        }

        public OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalle(OE_DarEntregableEstadoDetalle oe)
        {
            return (new FachadaAsistencia().DarEntregableEstadoDetalle(oe));
        }

        public OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinador(OE_DarFacilitadorXCoordinador oe)
        {
            return (new FachadaAsistencia().DarFacilitadorXCoordinador(oe));
        }

        public OS_DarGruposXFacilitador DarGruposXFacilitador(OE_DarGruposXFacilitador oe)
        {
            return (new FachadaAsistencia().DarGruposXFacilitador(oe));
        }

        public OS_DarInscritos DarInscritos(OE_DarInscritos oe)
        {
            return (new FachadaAsistencia().DarInscritos(oe));
        }

        public OS_DarTalleres DarTalleres()
        {
            return (new FachadaAsistencia().DarTalleres());
        }

        public OS_GenerarListaAsistencia GenerarListaAsistencia(OE_GenerarListaAsistencia oe)
        {
            return (new FachadaAsistencia().GenerarListaAsistencia(oe));
        }

        public OS_RegistrarAprobacion RegistrarAprobacion(OE_RegistrarAprobacion oe)
        {
            return (new FachadaAsistencia().RegistrarAprobacion(oe));
        }

        public OS_RegistrarAsistencia RegistrarAsistencia(OE_RegistrarAsistencia oe)
        {
            return (new FachadaAsistencia().RegistrarAsistencia(oe));
        }
    }
}
