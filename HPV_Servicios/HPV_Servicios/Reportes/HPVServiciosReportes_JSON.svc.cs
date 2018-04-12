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
    // HPVServiciosReporte_JSON
    // El servicio se publica en HPVServiciosReporte_JSON.svc or HPVServiciosReporte_JSON.svc.cs
    public class HPVServiciosReportes_JSON : IHPVServiciosReportes_JSON
    {
        public OS_DarReportes DarReportesOptions(OE_DarReportes oe)
        {
            return null;
        }

        public OS_DarReportes DarReportes(OE_DarReportes oe)
        {
            return (new FachadaReporte().DarReportes(oe));
        }

        public OS_ConsultarAuditoria ConsultarAuditoriaOptions(OE_ConsultarAuditoria oe)
        {
            return null;
        }

        public OS_ConsultarAuditoria ConsultarAuditoria(OE_ConsultarAuditoria oe)
        {
            return (new FachadaReporte().ConsultarAuditoria(oe));
        }

        public OS_DarDepartamentosXProfesional DarDepartamentosXProfesionalOptions(OE_DarDepartamentosXProfesional oe)
        {
            return null;
        }

        public OS_DarDepartamentosXProfesional DarDepartamentosXProfesional(OE_DarDepartamentosXProfesional oe)
        {
            return (new FachadaReporte().DarDepartamentosXProfesional(oe));
        }

        public OS_DarMunicipiosXProfesional DarMunicipiosXProfesionalOptions(OE_DarMunicipiosXProfesional oe)
        {
            return null;
        }

        public OS_DarMunicipiosXProfesional DarMunicipiosXProfesional(OE_DarMunicipiosXProfesional oe)
        {
            return (new FachadaReporte().DarMunicipiosXProfesional(oe));
        }

        public OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesionalOptions(OE_DarCoordinadoresXProfesional oe)
        {
            return null;
        }

        public OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesional(OE_DarCoordinadoresXProfesional oe)
        {
            return (new FachadaReporte().DarCoordinadoresXProfesional(oe));
        }
    }
}
