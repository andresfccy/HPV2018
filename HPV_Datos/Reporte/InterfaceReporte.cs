using HPV_Entidades.FechaCorteWS;
using HPV_Entidades.Reporte;
using HPV_Entidades.ReporteWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Reporte
{
    interface InterfaceReporte
    {
        OS_DarReportes DarReportes(OE_DarReportes oe);

        OS_RptTrazabilidad RptTrazabilidad(OE_RptTrazabilidad oe);

        OS_RptSatisfaccion RptEncuestaSatisfaccion(OE_RptSatisfaccion oe);

        OS_RptSatisfaccionObservaciones RptEncuestaSatisfaccionObservaciones(OE_RptSatisfaccionObservaciones oe);

        OS_RptCronograma RptCronogramaTaller(OE_RptCronograma oe);

        OS_RptEntradaSalida RptEncuestaEntradaSalida(OE_RptEntradaSalida oe);

        OS_RptCandidatos RptCandidatos(OE_RptCandidatos oe);

        OS_RptCasosExito RptCasosExito(OE_RptCasosExito oe);

        OS_RptEjemplosLiderazgo RptEjemplosLiderazgo(OE_RptEjemplosLiderazgo oe);

        OS_RptHistoriasVida RptHistoriasVida(OE_RptHistoriasVida oe);

        OS_RptUsuariosSistema RptUsuariosSistema(OE_RptUsuariosSistema oe);

        OS_RptEspaciosFisicos RptEspaciosFisicos(OE_RptEspaciosFisicos oe);

        OS_ConsultarAuditoria ConsultarAuditoria(OE_ConsultarAuditoria oe);

        OS_RptRechazoGrupos RptRechazoGrupos(OE_RptRechazoGrupos oe);

        OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesional(OE_DarCoordinadoresXProfesional oe);

        OS_DarDepartamentosXProfesional DarDepartamentosXProfesional(OE_DarDepartamentosXProfesional oe);

        OS_DarMunicipiosXProfesional DarMunicipiosXProfesional(OE_DarMunicipiosXProfesional oe);
    }
}
