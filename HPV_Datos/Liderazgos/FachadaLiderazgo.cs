using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.LiderazgoWS;
using HPV_Datos.Liderazgos.Entidad;
using Oracle.ManagedDataAccess.Client;
using HPV_Entidades.General;
using System.Data;
using HPV_Datos.General.Entidad;

namespace HPV_Datos.Liderazgos
{
    public class FachadaLiderazgo : InterfaceLiderazgo
    {
        public OS_ActualizarLiderazgo ActualizarLiderazgo(OE_ActualizarLiderazgo oe)
        {
            OS_ActualizarLiderazgo os = new OS_ActualizarLiderazgo();
            try
            {

                LiderazgoEntidad entidad = new LiderazgoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_LIDERAZGO.Pr_ActualizarLiderazgo",
                    new OracleParameter { ParameterName = "p_idLiderazgo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Liderazgo.IdLiderazgo },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Liderazgo.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Liderazgo.IdFacilitador },
                    new OracleParameter { ParameterName = "p_criterios", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Liderazgo.Criterios },
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Liderazgo.IdInscrito },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Liderazgos.FachadaLiderazgo.ActualizarLiderazgo :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarLiderazgo ConsultarLiderazgo(OE_ConsultarLiderazgo oe)
        {
            OS_ConsultarLiderazgo os = new OS_ConsultarLiderazgo();
            try
            {

                LiderazgoEntidad entidad = new LiderazgoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);


                entidad.ExecuteStoreProcedure("PK_OPE_LIDERAZGO.Pr_consultarLiderazgo",
                    new OracleParameter { ParameterName = "p_idLiderazgo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdLiderazgo },
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
                        os.Liderazgo = ((LiderazgoEntidad)lista[0]).Liderazgo;

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Liderazgos.FachadaLiderazgo.ConsultarLiderazgo :" + e.Message;
            }

            return os;
        }

        public OS_DarLiderazgos DarLiderazgos(OE_DarLiderazgos oe)
        {
            OS_DarLiderazgos os = new OS_DarLiderazgos();
            try
            {

                LiderazgoEntidad entidad = new LiderazgoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);


                entidad.ExecuteStoreProcedure("PK_OPE_LIDERAZGO.Pr_DarLiderazgos",
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
                        LiderazgoEntidad item = (LiderazgoEntidad)objeto;
                        os.ListaLiderazgo.Add(item.Liderazgo);
                    }

                }


                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Liderazgos.FachadaLiderazgo.ConsultarLiderazgo :" + e.Message;
            }

            return os;
        }

        public OS_RegistrarEstadoLiderazgo RegistrarEstadoLiderazgo(OE_RegistrarEstadoLiderazgo oe)
        {
            OS_RegistrarEstadoLiderazgo os = new OS_RegistrarEstadoLiderazgo();
            try
            {
                LiderazgoEntidad entidad = new LiderazgoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_LIDERAZGO.Pr_RegistrarEstadoLiderazgo",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_idLiderazgo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdLiderazgo },
                    new OracleParameter { ParameterName = "p_motivoRechazo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.MotivoRechazo },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Estado },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Liderazgos.FachadaLiderazgo.ConsultarLiderazgo :" + e.Message;
            }

            return os;
        }

        public OS_ValidarCantidadLiderazgo ValidarCantidadLiderazgo(OE_ValidarCantidadLiderazgo oe)
        {
            OS_ValidarCantidadLiderazgo os = new OS_ValidarCantidadLiderazgo();
            try
            {
                LiderazgoEntidad entidad = new LiderazgoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);


                entidad.ExecuteStoreProcedure("PK_OPE_LIDERAZGO.Pr_ValidarCantidadLiderazgo",
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Liderazgos.FachadaLiderazgo.ValidarLiderazgo :" + e.Message;
            }

            return os;
        }
    }
}
