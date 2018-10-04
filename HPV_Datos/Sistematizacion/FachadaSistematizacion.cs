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
        public OS_ActualizarCategorizacion ActualizarCategorizacion(OE_ActualizarCategorizacion oe)
        {
            OS_ActualizarCategorizacion os = new OS_ActualizarCategorizacion();
            try
            {
                SistematizacionEntidad entidad = new SistematizacionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_ActualizarCategorizacion",
                    new OracleParameter { ParameterName = "p_idSistematizacion", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdSistematizacion },
                    new OracleParameter { ParameterName = "p_listaCategorizaciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.ListaCategorizaciones },
                    new OracleParameter { ParameterName = "p_descotros", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.DescripcionOtros },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.ActualizarCategorizacion :" + e.Message;
            }

            return os;
        }

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
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.IdInscrito },
                    new OracleParameter { ParameterName = "p_motivoRechazo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Sistematizacion.MotivoRechazo },
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

        public OS_DarCategoriasXInstrumento DarCategoriasXInstrumento(OE_DarCategoriasXInstrumento oe)
        {
            OS_DarCategoriasXInstrumento os = new OS_DarCategoriasXInstrumento();
            try
            {

                CategoriaEntidad entidad = new CategoriaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_DarCategorias",
                    new OracleParameter { ParameterName = "p_instrumento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdSistematizacion },
                    new OracleParameter { ParameterName = "p_periodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
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
                        CategoriaEntidad item = (CategoriaEntidad)objeto;
                        os.ListaCategorias.Add(item.Categoria);
                    }
                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.DarCategoriasXInstrumento :" + e.Message;
            }

            return os;
        }

        public OS_DarCategorizacion DarCategorizacion(OE_DarCategorizacion oe)
        {
            OS_DarCategorizacion os = new OS_DarCategorizacion();
            try
            {
                SubcategoriaEntidad entidad = new SubcategoriaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_ConsultarCategorizacion",
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

                    foreach (EntidadOracle objeto in lista)
                    {
                        SubcategoriaEntidad item = (SubcategoriaEntidad)objeto;
                        os.ListaCategorizacion.Add(item.Subcategoria);
                    }
                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.DarCategorizacion :" + e.Message;
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

        public OS_DarSubcategoriasXInstrumento DarSubcategoriasXInstrumento(OE_DarSubcategoriasXInstrumento oe)
        {
            OS_DarSubcategoriasXInstrumento os = new OS_DarSubcategoriasXInstrumento();
            try
            {

                SubcategoriaEntidad entidad = new SubcategoriaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_SISTEMATIZACION.Pr_DarSubCategorias",
                    new OracleParameter { ParameterName = "p_idTipoInstrumento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTipoInstrumento },
                    new OracleParameter { ParameterName = "p_periodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
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
                        SubcategoriaEntidad item = (SubcategoriaEntidad)objeto;
                        os.ListaSubcategorias.Add(item.Subcategoria);
                    }
                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Sistematizacion.FachadaSistematizacion.DarSubcategoriasXInstrumento :" + e.Message;
            }

            return os;
        }
    }
}
