using HPV_Datos.Reporte;
using HPV_Entidades.ReporteWS;
using HPV_Servicios.Reportes;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace HPV_Servicios.Reportes
{
    // HPVServiciosReporte
    // El servicio se publica en HPVServiciosReporte.svc or HPVServiciosReporte.svc.cs
    public class HPVServiciosReportes : IHPVServiciosReportes
    {
        public OS_DarReportes DarReportes(OE_DarReportes oe)
        {
            return (new FachadaReporte().DarReportes(oe));
        }

        public OS_ConsultarAuditoria ConsultarAuditoria(OE_ConsultarAuditoria oe)
        {
            return (new FachadaReporte().ConsultarAuditoria(oe));
        }

        public OS_DarDepartamentosXProfesional DarDepartamentosXProfesional(OE_DarDepartamentosXProfesional oe)
        {
            return (new FachadaReporte().DarDepartamentosXProfesional(oe));
        }

        public OS_DarMunicipiosXProfesional DarMunicipiosXProfesional(OE_DarMunicipiosXProfesional oe)
        {
            return (new FachadaReporte().DarMunicipiosXProfesional(oe));
        }

        public OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesional(OE_DarCoordinadoresXProfesional oe)
        {
            return (new FachadaReporte().DarCoordinadoresXProfesional(oe));
        }
    }
}
