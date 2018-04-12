using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Entidades.ReporteWS;
using HPV_Datos.Reporte.Entidad;
using HPV_Entidades.General;
using HPV_Datos.General.Entidad;
using HPV_Entidades.Reporte;
using HPV_Datos.AdmUsuario.Entidad;
using HPV_Datos.AdmEspacioFisico.Entidad;

namespace HPV_Datos.Reporte
{
    public class FachadaReporte : InterfaceReporte
    {
        public OS_DarReportes DarReportes(OE_DarReportes oe)
        {
            OS_DarReportes os = new OS_DarReportes();
            try
            {
                ReporteEntidad entidad = new ReporteEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarReportes",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        ReporteEntidad item = (ReporteEntidad)objeto;
                        os.ListaReporte.Add(item.Reporte);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.DarReportes :" + e.Message;
            }

            return os;
        }

        public OS_RptTrazabilidad RptTrazabilidad(OE_RptTrazabilidad oe)
        {
            OS_RptTrazabilidad os = new OS_RptTrazabilidad();
            try
            {
                TrazabilidadEntidad entidad = new TrazabilidadEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptTrazabilidad",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },                    
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        TrazabilidadEntidad item = (TrazabilidadEntidad)objeto;
                        os.ListaTrazabilidad.Add(item.Trazabilidad);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptTrazabilidad :" + e.Message;
            }

            return os;
        }

        public OS_RptSatisfaccion RptEncuestaSatisfaccion(OE_RptSatisfaccion oe)
        {
            OS_RptSatisfaccion os = new OS_RptSatisfaccion();
            try
            {
                EncuestaSatisfaccionEntidad entidad = new EncuestaSatisfaccionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptSatisfaccion",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        EncuestaSatisfaccionEntidad item = (EncuestaSatisfaccionEntidad)objeto;
                        os.ListaSatisfaccion.Add(item.EncuestaSatisfaccion);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptSatisfaccion :" + e.Message;
            }

            return os;
        }

        public OS_RptSatisfaccionObservaciones RptEncuestaSatisfaccionObservaciones(OE_RptSatisfaccionObservaciones oe)
        {
            OS_RptSatisfaccionObservaciones os = new OS_RptSatisfaccionObservaciones();
            try
            {
                EncuestaSatisfaccionObservacionesEntidad entidad = new EncuestaSatisfaccionObservacionesEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptObsEncSatisf",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        EncuestaSatisfaccionObservacionesEntidad item = (EncuestaSatisfaccionObservacionesEntidad)objeto;
                        os.ListaSatisfaccionObservaciones.Add(item.EncuestaSatisfaccionObservaciones);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptSatisfaccionObservaciones :" + e.Message;
            }

            return os;
        }

        public OS_RptCronograma RptCronogramaTaller(OE_RptCronograma oe)
        {
            OS_RptCronograma os = new OS_RptCronograma();
            try
            {
                CronogramaTallerEntidad entidad = new CronogramaTallerEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptCronograma",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        CronogramaTallerEntidad item = (CronogramaTallerEntidad)objeto;
                        os.ListaCronograma.Add(item.CronogramaTaller);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptCronograma :" + e.Message;
            }

            return os;
        }

        public OS_RptEntradaSalida RptEncuestaEntradaSalida(OE_RptEntradaSalida oe)
        {
            OS_RptEntradaSalida os = new OS_RptEntradaSalida();
            try
            {
                EncuestaEntradaSalidaEntidad entidad = new EncuestaEntradaSalidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptEntradaSalida",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        EncuestaEntradaSalidaEntidad item = (EncuestaEntradaSalidaEntidad)objeto;
                        os.ListaEntradaSalida.Add(item.EncuestaEntradaSalida);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptEntradaSalida :" + e.Message;
            }

            return os;
        }

        public OS_RptCandidatos RptCandidatos(OE_RptCandidatos oe)
        {
            OS_RptCandidatos os = new OS_RptCandidatos();
            try
            {
                CandidatosEntidad entidad = new CandidatosEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptCandidatos",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        CandidatosEntidad item = (CandidatosEntidad)objeto;
                        os.ListaCandidatos.Add(item.Candidatos);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptCandidatos :" + e.Message;
            }

            return os;
        }

        public OS_RptCasosExito RptCasosExito(OE_RptCasosExito oe)
        {
            OS_RptCasosExito os = new OS_RptCasosExito();
            try
            {
                CasosExitoEntidad entidad = new CasosExitoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptCasosExito",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        CasosExitoEntidad item = (CasosExitoEntidad)objeto;
                        os.ListaCasosExito.Add(item.CasosExito);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptCasosExito :" + e.Message;
            }

            return os;
        }

        public OS_RptEjemplosLiderazgo RptEjemplosLiderazgo(OE_RptEjemplosLiderazgo oe)
        {
            OS_RptEjemplosLiderazgo os = new OS_RptEjemplosLiderazgo();
            try
            {
                EjemplosLiderazgoEntidad entidad = new EjemplosLiderazgoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptEjemLider",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        EjemplosLiderazgoEntidad item = (EjemplosLiderazgoEntidad)objeto;
                        os.ListaEjemplosLiderazgo.Add(item.EjemplosLiderazgo);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptEjemplosLiderazgo :" + e.Message;
            }

            return os;
        }

        public OS_RptHistoriasVida RptHistoriasVida(OE_RptHistoriasVida oe)
        {
            OS_RptHistoriasVida os = new OS_RptHistoriasVida();
            try
            {
                HistoriasVidaEntidad entidad = new HistoriasVidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptHistVida",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        HistoriasVidaEntidad item = (HistoriasVidaEntidad)objeto;
                        os.ListaHistoriasVida.Add(item.HistoriasVida);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptHistoriasVida :" + e.Message;
            }

            return os;
        }

        public OS_RptUsuariosSistema RptUsuariosSistema(OE_RptUsuariosSistema oe)
        {
            OS_RptUsuariosSistema os = new OS_RptUsuariosSistema();
            try
            {
                UsuariosSistemaEntidad entidad = new UsuariosSistemaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptUsuarSistema",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        UsuariosSistemaEntidad item = (UsuariosSistemaEntidad)objeto;
                        os.ListaUsuariosSistema.Add(item.UsuariosSistema);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptUsuariosSistema :" + e.Message;
            }

            return os;
        }

        public OS_RptEspaciosFisicos RptEspaciosFisicos(OE_RptEspaciosFisicos oe)
        {
            OS_RptEspaciosFisicos os = new OS_RptEspaciosFisicos();
            try
            {
                EspaciosFisicosEntidad entidad = new EspaciosFisicosEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptEspaciosFisicos",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        EspaciosFisicosEntidad item = (EspaciosFisicosEntidad)objeto;
                        os.ListaEspaciosFisicos.Add(item.EspaciosFisicos);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptEspaciosFisicos :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarAuditoria ConsultarAuditoria(OE_ConsultarAuditoria oe)
        {
            OS_ConsultarAuditoria os = new OS_ConsultarAuditoria();
            try
            {
                AuditoriaEntidad entidad = new AuditoriaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AUDIT.Pr_DarTransac",
                    new OracleParameter { ParameterName = "p_idFuncion", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFuncion },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_fec_ini", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Fec_Ini },
                    new OracleParameter { ParameterName = "p_fec_fin", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Fec_Fin },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        AuditoriaEntidad item = (AuditoriaEntidad)objeto;
                        os.ListaAuditorias.Add(item.Auditoria);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.ConsultarAuditoria :" + e.Message;
            }

            return os;
        }

        public OS_RptRechazoGrupos RptRechazoGrupos(OE_RptRechazoGrupos oe)
        {

            OS_RptRechazoGrupos os = new OS_RptRechazoGrupos();
            try
            {
                RechazoGruposEntidad entidad = new RechazoGruposEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarRptRechazoGrupos",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdUsuario },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdGrupo },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdDepartamento },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.IdMunicipio },
                    new OracleParameter { ParameterName = "p_fechaCorte", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ParametroRpt.FechaCorte },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        RechazoGruposEntidad item = (RechazoGruposEntidad)objeto;
                        os.ListaRechazoGrupos.Add(item.RechazoGrupos);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.Reporte.FachadaReporte.RptRechazoGrupos :" + e.Message;
            }

            return os;
        }

        public OS_DarCoordinadoresXProfesional DarCoordinadoresXProfesional(OE_DarCoordinadoresXProfesional oe)
        {
            OS_DarCoordinadoresXProfesional os = new OS_DarCoordinadoresXProfesional();
            try
            {
                CoordinadorEntidad entidad = new CoordinadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarCoordinadoresXPrf",
                    new OracleParameter { ParameterName = "p_idProfesional", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdProfesional },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        CoordinadorEntidad item = (CoordinadorEntidad)objeto;
                        os.ListaCoordinador.Add(item.Coordinador);
                    }
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Reporte.FachadaReporte.DarCoordinadoresXProfesional :" + e.Message;
            }

            return os;
        }

        public OS_DarDepartamentosXProfesional DarDepartamentosXProfesional(OE_DarDepartamentosXProfesional oe)
        {
            OS_DarDepartamentosXProfesional os = new OS_DarDepartamentosXProfesional();
            try
            {
                DptoFacilitadorEntidad entidad = new DptoFacilitadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarDptosXProfesional",
                    new OracleParameter { ParameterName = "p_idProfesional", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdProfesional },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        DptoFacilitadorEntidad item = (DptoFacilitadorEntidad)objeto;
                        os.ListaDepartamento.Add(item.Departamento);
                    }
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Reporte.FachadaReporte.DarDepartamentosXProfesional :" + e.Message;
            }

            return os;
        }

        public OS_DarMunicipiosXProfesional DarMunicipiosXProfesional(OE_DarMunicipiosXProfesional oe)
        {
            OS_DarMunicipiosXProfesional os = new OS_DarMunicipiosXProfesional();
            try
            {
                MunicipioFacilitadorEntidad entidad = new MunicipioFacilitadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_RPT_ADMREPORTE.Pr_DarMunicipiosXProfesional",
                    new OracleParameter { ParameterName = "p_idProfesional", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdProfesional },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdDepartamento },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        MunicipioFacilitadorEntidad item = (MunicipioFacilitadorEntidad)objeto;
                        os.ListaMunicipio.Add(item.Municipio);
                    }
                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Reporte.FachadaReporte.DarMunicipiosXProfesional :" + e.Message;
            }

            return os;
        }
    }
}
