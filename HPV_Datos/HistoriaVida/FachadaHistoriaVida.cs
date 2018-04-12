using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.HistoriasDeVidaWS;
using HPV_Datos.HistoriaVida.Entidad;
using HPV_Entidades.General;
using HPV_Entidades.HistoriasDeVida;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;

namespace HPV_Datos.HistoriaVida
{
    public class FachadaHistoriaVida : InterfaceHistoriaVida
    {
        public OS_ActualizarHistoriaVida ActualizarHistoriaVida(OE_ActualizarHistoriaVida oe)
        {
            OS_ActualizarHistoriaVida os = new OS_ActualizarHistoriaVida();
            try
            {

                HistoriaVidaEntidad entidad = new HistoriaVidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_HISTORIAVIDA.Pr_ActualizarHistoriaVida",
                    new OracleParameter { ParameterName = "p_idHistoriaDeVida", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.IdHistoriaDeVida },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idTipoHistoriaDeVida", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.IdTipoHistoriaDeVida },
                    new OracleParameter { ParameterName = "p_razonDeConsideracion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.RazonDeConsideracion },
                    new OracleParameter { ParameterName = "p_circunstanciasSuperadas", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.CircunstanciasSuperadas },
                    new OracleParameter { ParameterName = "p_habilidadesPracticadas", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.HabilidadesPracticadas },
                    new OracleParameter { ParameterName = "p_leccion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.Leccion },
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.HistoriaDeVida.IdInscrito },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.HistoriaVida.FachadaHistoriaVida.ActualizarHistoriaVida :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarHistoriaVida ConsultarHistoriaVida(OE_ConsultarHistoriaVida oe)
        {
            OS_ConsultarHistoriaVida os = new OS_ConsultarHistoriaVida();
            try
            {

                HistoriaVidaEntidad entidad = new HistoriaVidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_HISTORIAVIDA.Pr_consultarHistoriaVida",
                    new OracleParameter { ParameterName = "p_idHistoriaDeVida", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdHistoriaDeVida },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    if (lista.Count > 0)
                        os.HistoriaDeVida = ((HistoriaVidaEntidad)lista[0]).HistoriaDeVida;

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.HistoriaVida.FachadaHistoriaVida.ConsultarHistoriaVida :" + e.Message;
            }

            return os;
        }

        public OS_DarHistoriasDeVida DarHistoriasDeVida(OE_DarHistoriasDeVida oe)
        {
            OS_DarHistoriasDeVida os = new OS_DarHistoriasDeVida();
            try
            {

                HistoriaVidaEntidad entidad = new HistoriaVidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_HISTORIAVIDA.Pr_DarHistoriasDeVida",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_filtroBusqueda", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
                    new OracleParameter { ParameterName = "p_estadoXConsultar", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.EstadoXConsultar },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
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
                        HistoriaVidaEntidad item = (HistoriaVidaEntidad)objeto;
                        os.ListaHistoriaDeVida.Add(item.HistoriaDeVida);
                    }

                }


                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.HistoriaVida.FachadaHistoriaVida.OS_DarHistoriasDeVida :" + e.Message;
            }

            return os;
        }

        public OS_RegistrarEstadoHistoriaVida RegistrarEstadoHistoriaVida(OE_RegistrarEstadoHistoriaVida oe)
        {
            OS_RegistrarEstadoHistoriaVida os = new OS_RegistrarEstadoHistoriaVida();
            try
            {
                HistoriaVidaEntidad entidad = new HistoriaVidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_HISTORIAVIDA.Pr_RegistrarEstadoHistoriaVida",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_idHistoriaVida", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdHistoriaVida },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Estado },
                    new OracleParameter { ParameterName = "p_motivoRechazo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.MotivoRechazo },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.HistoriaVida.FachadaHistoriaVida.RechazarHistoriaVida :" + e.Message;
            }

            return os;
        }

        public OS_ValidarCantidadHistorias ValidarCantidadHistorias(OE_ValidarCantidadHistorias oe)
        {
            OS_ValidarCantidadHistorias os = new OS_ValidarCantidadHistorias();
            try
            {

                HistoriaVidaEntidad entidad = new HistoriaVidaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_HISTORIAVIDA.Pr_ValidarCantidadHistorias",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.HistoriaVida.FachadaHistoriaVida.ValidarCantidadHistorias :" + e.Message;
            }

            return os;
        }
    }
}
