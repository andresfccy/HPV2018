using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Datos.General.Entidad;
using HPV_Datos.Sistematizacion.Entidad;
using HPV_Entidades.General;
using HPV_Entidades.SistematizacionWS;
using Oracle.ManagedDataAccess.Client;

namespace HPV_Datos.Sistematizacion
{
    public class FachadaSistematizacion : InterfaceSistematizacion
    {
        public OS_ActualizarSistematizacion ActualizarSistematizacion(OE_ActualizarSistematizacion oe)
        {
            OS_ActualizarSistematizacion os = new OS_ActualizarSistematizacion();
            try
            {
                SistematizacionEntidad entidad = new SistematizacionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_ActualizarSistematizacion",
                    new OracleParameter { ParameterName = "p_idSistematizacion", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.Id },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idInstrumento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.IdInstrumento },
                    new OracleParameter { ParameterName = "p_idEstado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.IdEstado },
                    new OracleParameter { ParameterName = "p_linkVideo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.LinkVideo },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.ActualizarSistematizacion :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarSistematizacion ConsultarSistematizacion(OE_ConsultarSistematizacion oe)
        {
            OS_ConsultarSistematizacion os = new OS_ConsultarSistematizacion();
            try
            {

                SistematizacionEntidad entidad = new SistematizacionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);


                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_consultarSistematizacion",
                    new OracleParameter { ParameterName = "p_idSistematizacion", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdSistematizacion },
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
                        os.Sistematizacion = ((SistematizacionEntidad)lista[0]).Sistematizacion;

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.ConsultarSistematizacion :" + e.Message;
            }

            return os;
        }

        public OS_DarSistematizacion DarSistematizacion(OE_DarSistematizacion oe)
        {
            OS_DarSistematizacion os = new OS_DarSistematizacion();
            try
            {

                SistematizacionEntidad entidad = new SistematizacionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);


                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_DarSistematizacion",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_filtroBusqueda", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
                    new OracleParameter { ParameterName = "p_instrumentoXConsultar", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.InstrumentoXConsultar},
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
                        SistematizacionEntidad item = (SistematizacionEntidad)objeto;
                        os.ListaSistematizacion.Add(item.Sistematizacion);
                    }

                }


                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.ConsultarSistematizacion :" + e.Message;
            }

            return os;
        }
    }
}
