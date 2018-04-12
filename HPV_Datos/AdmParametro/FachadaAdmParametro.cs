using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.AdmParametroWS;
using HPV_Datos.AdmParametro.Entidad;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Datos.Alistamiento.Entidad;

namespace HPV_Datos.AdmParametro
{
    public class FachadaAdmParametro : InterfaceAdmParametro
    {
        public OS_ActualizarParametro ActualizarParametro(OE_ActualizarParametro oe)
        {
            OS_ActualizarParametro os = new OS_ActualizarParametro();
            try
            {

                ParametroEntidad entidad = new ParametroEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_ActualizarParametro",
                    new OracleParameter { ParameterName = "p_codigo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Parametro.Codigo },
                    new OracleParameter { ParameterName = "p_nombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Parametro.Nombre },
                    new OracleParameter { ParameterName = "p_valor", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Parametro.Valor },
                    new OracleParameter { ParameterName = "p_descripcion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Parametro.Descripcion },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Parametro.Estado },
                    new OracleParameter { ParameterName = "p_usuarioregistra", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmParametro.FachadaAdmParametro.ActualizarParametro :" + e.Message;
            }

            return os;
        }

        public OS_ActualizarPeriodo ActualizarPeriodo(OE_ActualizarPeriodo oe)
        {
            OS_ActualizarPeriodo os = new OS_ActualizarPeriodo();
            try
            {
                PeriodoEntidad entidad = new PeriodoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_ActualizarPeriodo",
                    new OracleParameter { ParameterName = "p_codigo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Periodo.Codigo },
                    new OracleParameter { ParameterName = "p_nombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Periodo.Nombre },
                    new OracleParameter { ParameterName = "p_fechainicio", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Periodo.FechaInicio },
                    new OracleParameter { ParameterName = "p_fechafin", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Periodo.FechaFin },
                    new OracleParameter { ParameterName = "p_noperiodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Periodo.Numero },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Periodo.Estado },
                    new OracleParameter { ParameterName = "p_fechaIniTaller", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Periodo.FechaIniTaller },
                    new OracleParameter { ParameterName = "p_usuarioregistra", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmParametro.FachadaAdmParametro.ActualizarPeriodo :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarParametro ConsultarParametro(OE_ConsultarParametro oe)
        {
            OS_ConsultarParametro os = new OS_ConsultarParametro();
            try
            {
                ParametroEntidad entidad = new ParametroEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_ConsultarParametro",
                    new OracleParameter { ParameterName = "p_codigo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Codigo},
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
                        ParametroEntidad item = (ParametroEntidad)objeto;
                        os.Parametro = item.Parametro;
                    }

                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmParametro.FachadaAdmParametro.ConsultarParametro :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarPeriodo ConsultarPeriodo(OE_ConsultarPeriodo oe)
        {
            OS_ConsultarPeriodo os = new OS_ConsultarPeriodo();
            try
            {
                PeriodoEntidad entidad = new PeriodoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_ConsultarPeriodo",
                    new OracleParameter { ParameterName = "p_codigo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Codigo },
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
                        PeriodoEntidad item = (PeriodoEntidad)objeto;
                        os.Periodo = item.Periodo;
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmParametro.FachadaAdmParametro.ConsultarPeriodo :" + e.Message;
            }

            return os;
        }

        public OS_DarParametros DarParametros()
        {
            OS_DarParametros os = new OS_DarParametros();
            try
            {

                ParametroEntidad entidad = new ParametroEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_DarParametros",
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
                        ParametroEntidad item = (ParametroEntidad)objeto;
                        os.ListaParametro.Add(item.Parametro);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmParametro.FachadaAdmParametro.DarParametros :" + e.Message;
            }

            return os;
        }

        public OS_DarPeriodos DarPeriodos()
        {
            OS_DarPeriodos os = new OS_DarPeriodos();
            try
            {
                PeriodoEntidad entidad = new PeriodoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMPARAMETRO.Pr_DarPeriodos",
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
                        PeriodoEntidad item = (PeriodoEntidad)objeto;
                        os.ListaPeriodo.Add(item.Periodo);
                    }
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmParametro.FachadaAdmParametro.DarPeriodos :" + e.Message;
            }

            return os;
        }
    }
}
