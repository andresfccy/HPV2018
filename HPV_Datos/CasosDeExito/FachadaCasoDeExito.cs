using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Datos.CasosDeExito.Entidad;
using HPV_Entidades.CasosDeExitoWS;

namespace HPV_Datos.CasosDeExito
{
    public class FachadaCasoDeExito : InterfaceCasoDeExito
    {
        public OS_ActualizarCasoDeExito ActualizarCasoDeExito(OE_ActualizarCasoDeExito oe)
        {
            OS_ActualizarCasoDeExito os = new OS_ActualizarCasoDeExito();
            try
            {

                CasoDeExitoEntidad entidad = new CasoDeExitoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
               
                entidad.ExecuteStoreProcedure("PK_OPE_CASOEXITO.Pr_ActualizarCasoDeExito",
                    new OracleParameter { ParameterName = "p_idCasoExito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.IdCasoDeExito },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idLogro", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.IdLogro },
                    new OracleParameter { ParameterName = "p_logros", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.Logros },
                    new OracleParameter { ParameterName = "p_criterios", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.Criterios },
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.IdInscrito },
                    new OracleParameter { ParameterName = "p_observaciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.CasoDeExito.Observaciones },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.CasosDeExito.FachadaCasoDeExito.ActualizarCasoDeExito :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarCasoDeExito ConsultarCasoDeExito(OE_ConsultarCasoDeExito oe)
        {
            OS_ConsultarCasoDeExito os = new OS_ConsultarCasoDeExito();
            try
            {

                CasoDeExitoEntidad entidad = new CasoDeExitoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                
                entidad.ExecuteStoreProcedure("PK_OPE_CASOEXITO.Pr_consultarCasoDeExito",
                    new OracleParameter { ParameterName = "p_idCasoDeExito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCasoDeExito },
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
                        os.CasoDeExito = ((CasoDeExitoEntidad)lista[0]).CasoDeExito;

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.CasosDeExito.FachadaCasoDeExito.ConsultarCasoDeExito :" + e.Message;
            }

            return os;
        }

        public OS_DarCasosDeExito DarCasosDeExito(OE_DarCasosDeExito oe)
        {
            OS_DarCasosDeExito os = new OS_DarCasosDeExito();
            try
            {

                CasoDeExitoEntidad entidad = new CasoDeExitoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                
                entidad.ExecuteStoreProcedure("PK_OPE_CASOEXITO.Pr_DarCasosDeExito",
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
                        CasoDeExitoEntidad item = (CasoDeExitoEntidad)objeto;
                        os.ListaCasoDeExito.Add(item.CasoDeExito);
                    }

                }


                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.CasosDeExito.FachadaCasoDeExito.DarCasosDeExito :" + e.Message;
            }

            return os;
        }

        public OS_DarLogros DarLogros()
        {
            OS_DarLogros os = new OS_DarLogros();
            try
            {

                LogroEntidad entidad = new LogroEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                
                entidad.ExecuteStoreProcedure("PK_OPE_CASOEXITO.Pr_DarLogros",
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
                        LogroEntidad item = (LogroEntidad)objeto;
                        os.ListaLogro.Add(item.Logro);
                    }

                }


                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.CasosDeExito.FachadaCasoDeExito.DarLogros :" + e.Message;
            }

            return os;
        }

        public OS_RegistrarEstadoCasoDeExito RegistrarEstadoCasoDeExito(OE_RegistrarEstadoCasoDeExito oe)
        {
            OS_RegistrarEstadoCasoDeExito os = new OS_RegistrarEstadoCasoDeExito();
            try
            {
                CasoDeExitoEntidad entidad = new CasoDeExitoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_CASOEXITO.Pr_RegistrarEstadoCasoDeExito",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_idCasoDeExito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCasoDeExito },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.CasosDeExito.FachadaCasoDeExito.RechazarCasoDeExito :" + e.Message;
            }

            return os;
        }

        public OS_ValidarCasoDeExito ValidarCasoDeExito(OE_ValidarCasoDeExito oe)
        {
            OS_ValidarCasoDeExito os = new OS_ValidarCasoDeExito();
            try
            {
                CasoDeExitoEntidad entidad = new CasoDeExitoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                
                entidad.ExecuteStoreProcedure("PK_OPE_CASOEXITO.Pr_ValidarCantidadCasoDeExito",
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.CasosDeExito.FachadaCasoDeExito.ValidarCasoDeExito :" + e.Message;
            }

            return os;
        }
    }
}
