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
    // HPVServiciosAsistencia_JSON
    // El servicio se publica en HPVServiciosAsistencia_JSON.svc or HPVServiciosAsistencia_JSON.svc.cs
    public class HPVServiciosAsistencia_JSON : IHPVServiciosAsistencia_JSON
    {
        public OS_DarAsistenciaEstado DarAsistenciaEstado(OE_DarAsistenciaEstado oe)
        {
            return (new FachadaAsistencia().DarAsistenciaEstado(oe));
        }

        public OS_DarAsistenciaEstado DarAsistenciaEstadoOptions(OE_DarAsistenciaEstado oe)
        {
            return null;
        }

        public OS_DarAsistenteXFacilitador DarAsistenteXFacilitador(OE_DarAsistenteXFacilitador oe)
        {
            return (new FachadaAsistencia().DarAsistenteXFacilitador(oe));
        }

        public OS_DarAsistenteXFacilitador DarAsistenteXFacilitadorOptions(OE_DarAsistenteXFacilitador oe)
        {
            return null;
        }

        public OS_DarEntregableEstado DarEntregableEstado(OE_DarEntregableEstado oe)
        {
            return (new FachadaAsistencia().DarEntregableEstado(oe));
        }

        public OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalle(OE_DarEntregableEstadoDetalle oe)
        {
            return (new FachadaAsistencia().DarEntregableEstadoDetalle(oe));
        }

        public OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalleOptions(OE_DarEntregableEstadoDetalle oe)
        {
            return null;
        }

        public OS_DarEntregableEstado DarEntregableEstadoOptions(OE_DarEntregableEstado oe)
        {
            return null;
        }

        public OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinador(OE_DarFacilitadorXCoordinador oe)
        {
            return (new FachadaAsistencia().DarFacilitadorXCoordinador(oe));
        }

        public OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinadorOptions(OE_DarFacilitadorXCoordinador oe)
        {
            return null;
        }

        public OS_DarGruposXFacilitador DarGruposXFacilitador(OE_DarGruposXFacilitador oe)
        {
            return (new FachadaAsistencia().DarGruposXFacilitador(oe));
        }

        public OS_DarGruposXFacilitador DarGruposXFacilitadorOptions(OE_DarGruposXFacilitador oe)
        {
            return null;
        }

        public OS_DarInscritos DarInscritos(OE_DarInscritos oe)
        {
            return (new FachadaAsistencia().DarInscritos(oe));

        }

        public OS_DarInscritos DarInscritosOptions(OE_DarInscritos oe)
        {
            return null;
        }

        public OS_DarTalleres DarTalleres()
        {
            return (new FachadaAsistencia().DarTalleres());
        }

        public OS_DarTalleres DarTalleresOptions()
        {
            return null;
        }

        public OS_GenerarListaAsistencia GenerarListaAsistencia(OE_GenerarListaAsistencia oe)
        {
            return (new FachadaAsistencia().GenerarListaAsistencia(oe));
        }

        public OS_GenerarListaAsistencia GenerarListaAsistenciaOptions(OE_GenerarListaAsistencia oe)
        {
            return null;
        }

        public OS_RegistrarAprobacion RegistrarAprobacion(OE_RegistrarAprobacion oe)
        {
            return (new FachadaAsistencia().RegistrarAprobacion(oe));
        }

        public OS_RegistrarAprobacion RegistrarAprobacionOptions(OE_RegistrarAprobacion oe)
        {
            return null;
        }

        public OS_RegistrarAsistencia RegistrarAsistencia(OE_RegistrarAsistencia oe)
        {
            return (new FachadaAsistencia().RegistrarAsistencia(oe));
        }

        public OS_RegistrarAsistencia RegistrarAsistenciaOptions(OE_RegistrarAsistencia oe)
        {
            return null;
        }
    }
}
