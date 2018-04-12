using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.AsistenciaWS;
using HPV_Datos.Asistencia.Entidad;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Entidades.General;
using System.IO;

namespace HPV_Datos.Asistencia
{
    public class FachadaAsistencia : InterfaceAsistencia
    {
        public OS_DarAsistenciaDetalle DarAsistenciaDetalle(OE_DarAsistenciaDetalle oe)
        {
            OS_DarAsistenciaDetalle os = new OS_DarAsistenciaDetalle();

            try
            {
                AsistenciaDetalleEntidad entidad = new AsistenciaDetalleEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarAsistenciaDetalle",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupo },
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
                        AsistenciaDetalleEntidad item = (AsistenciaDetalleEntidad)objeto;
                        os.ListaAsistenciaDetalle.Add(item.AsistenciaDetalle);
                    }

                }

                entidad.Dispose();

            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarAsistenciaEncabezado :" + e.Message;
            }
            return os;
        }

        public OS_DarAsistenciaEncabezado DarAsistenciaEncabezado(OE_DarAsistenciaEncabezado oe)
        {

            OS_DarAsistenciaEncabezado os = new OS_DarAsistenciaEncabezado();

            try
            {
                AsistenciaEncabezadoEntidad entidad = new AsistenciaEncabezadoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarAsistenciaEncabezado",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupo },
                    new OracleParameter { ParameterName = "p_idTaller", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTaller },
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

                    if (lista.Count > 0)
                        os.AsistenciaEncabezado= ((AsistenciaEncabezadoEntidad)lista[0]).AsistenciaEncabezado;

                }

                entidad.Dispose();

            }

           catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarAsistenciaEncabezado :" + e.Message;
            }
            return os;
        }

        public OS_DarAsistenciaEstado DarAsistenciaEstado(OE_DarAsistenciaEstado oe)
        {
            OS_DarAsistenciaEstado os = new OS_DarAsistenciaEstado();

            try
            {
                AsistenciaEstadoEntidad entidad = new AsistenciaEstadoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarAsistenciaEstado",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idEntregable", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEntregable },
                    new OracleParameter { ParameterName = "p_idTaller", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTaller },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupoFacilitador },
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
                        AsistenciaEstadoEntidad item = (AsistenciaEstadoEntidad)objeto;
                        os.ListaAsistenciaEstado.Add(item.AsistenciaEstado);
                    }

                }

                entidad.Dispose();

            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarAsistenciaEstado :" + e.Message;
            }
            return os;
        }

        public OS_DarAsistenteXFacilitador DarAsistenteXFacilitador(OE_DarAsistenteXFacilitador oe)
        {
            OS_DarAsistenteXFacilitador os = new OS_DarAsistenteXFacilitador();

            try
            {
                AsistenteEntidad entidad = new AsistenteEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarAsistenteXFacilitador",
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idFacilitdor", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador},
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
                        AsistenteEntidad item = (AsistenteEntidad)objeto;
                        os.ListaAsistente.Add(item.Asistente);
                    }

                }

                entidad.Dispose();

            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarAsistenteXFacilitador :" + e.Message;
            }
            return os;
        }

        public OS_DarEntregableEstado DarEntregableEstado(OE_DarEntregableEstado oe)
        {
            OS_DarEntregableEstado os = new OS_DarEntregableEstado();

            try
            {
                EntregableEstadoEntidad entidad = new EntregableEstadoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarEntregableEstado",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_idTaller", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTaller },
                    new OracleParameter { ParameterName = "p_idEntregable", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEntregable },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idEstadoDocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.IdEstadoDocumento },
                    new OracleParameter { ParameterName = "p_idEstadoAsistencia", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.IdEstadoAsistencia },
                    new OracleParameter { ParameterName = "p_filtroBusqueda", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
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
                        EntregableEstadoEntidad item = (EntregableEstadoEntidad)objeto;
                        os.ListaEntregableEstado.Add(item.EntregableEstado);
                    }

                }

                entidad.Dispose();

            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarEntregableEstado :" + e.Message;
            }
            return os;
        }

        public OS_DarEntregableEstadoDetalle DarEntregableEstadoDetalle(OE_DarEntregableEstadoDetalle oe)
        {
            OS_DarEntregableEstadoDetalle os = new OS_DarEntregableEstadoDetalle();

            try
            {
                EntregableEstadoEntidad entidad = new EntregableEstadoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarEntregableEstadoDetalle",
                    new OracleParameter { ParameterName = "p_idEntregable", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEntregable },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idTaller", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTaller },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupoFacilitador},
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
                        os.EntregableEstado = ((EntregableEstadoEntidad)lista[0]).EntregableEstado;

                }

                entidad.Dispose();

            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarEntregableEstadoDetalle :" + e.Message;
            }
            return os;
        }

        public OS_DarFacilitadorXCoordinador DarFacilitadorXCoordinador(OE_DarFacilitadorXCoordinador oe)
        {
            OS_DarFacilitadorXCoordinador os = new OS_DarFacilitadorXCoordinador();

            try
            {

                
                FacilitadorEntidad entidad = new FacilitadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarFacilitadorXCoordinador",
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCoordinador },
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
                        FacilitadorEntidad item = (FacilitadorEntidad)objeto;
                        os.ListaFacilitador.Add(item.Facilitador);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarFacilitadorXCoordinador :" + e.Message;
            }
            return os;
        }

        public OS_DarGruposXFacilitador DarGruposXFacilitador(OE_DarGruposXFacilitador oe)
        {
            OS_DarGruposXFacilitador os = new OS_DarGruposXFacilitador();

            try
            {


            GrupoFacilitadorEntidad entidad = new GrupoFacilitadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

            entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarGruposXFacilitador",
                new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
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
                    GrupoFacilitadorEntidad item = (GrupoFacilitadorEntidad)objeto;
                    os.ListaGrupo.Add(item.GrupoFacilitador);
                }

            }

            entidad.Dispose();

            return os;
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarGruposXFacilitador :" + e.Message;
            }
            return os;

        }

        public OS_DarInscritos DarInscritos(OE_DarInscritos oe)
        {
            OS_DarInscritos os = new OS_DarInscritos();

            try
            {
                AsistenciaEstadoEntidad entidad = new AsistenciaEstadoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarInscritos",
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupoFacilitador },
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
                        AsistenciaEstadoEntidad item = (AsistenciaEstadoEntidad)objeto;
                        os.ListaInscrito.Add(item.AsistenciaEstado);
                    }

                }

                entidad.Dispose();

            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarAsistenciaEstado :" + e.Message;
            }
            return os;
        }

        public OS_DarTalleres DarTalleres()
        {
            OS_DarTalleres os = new OS_DarTalleres();

            try
            {


                TallerEntidad entidad = new TallerEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_DarTalleres",
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
                        TallerEntidad item = (TallerEntidad)objeto;
                        os.ListaTaller.Add(item.Taller);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.DarTalleres :" + e.Message;
            }
            return os;
        }

        public OS_GenerarListaAsistencia GenerarListaAsistencia(OE_GenerarListaAsistencia oe)
        {
            OS_GenerarListaAsistencia os = new OS_GenerarListaAsistencia();

            os.Respuesta.Codigo = 0;
            os.Respuesta.Mensaje = "Generacion correcta";
            os.UrlDescarga = "/Reportes/Asistencia/ListaAsistencia.aspx";

            return os;

        }

        public OS_RegistrarAprobacion RegistrarAprobacion(OE_RegistrarAprobacion oe)
        {
            OS_RegistrarAprobacion os = new OS_RegistrarAprobacion();

            try
            {

                String pathDocument = System.Configuration.ConfigurationManager.AppSettings["pathDocument"];
                pathDocument += "/" + oe.IdPeriodo;
                pathDocument += "/" + oe.IdEntregable;
                pathDocument += "/" + oe.IdTaller;
                pathDocument += "/" + oe.IdGrupoFacilitador;

                String outFile = pathDocument + "/Asistencia-" + oe.IdTaller + "-" + oe.IdGrupoFacilitador + ".pdf";

                if (oe.Categoria.Equals("D"))
                if (!File.Exists(outFile))
                {
                    os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                    os.Respuesta.Mensaje = "ARCHIVO NO HA SIDO CARGADO. POR FAVOR VOLVER A CARGAR";
                    return os;
                }

                AsistenciaDetalleEntidad entidad = new AsistenciaDetalleEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_RegistrarAprobacion",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idEntregable", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEntregable },
                    new OracleParameter { ParameterName = "p_idTaller", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTaller },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_categoria", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Categoria },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.RegistrarAprobacion :" + e.Message;
            }
            return os;

        }

        public OS_RegistrarAsistencia RegistrarAsistencia(OE_RegistrarAsistencia oe)
        {
            OS_RegistrarAsistencia os = new OS_RegistrarAsistencia();

            try
            {

                String arrAsistencia = "";

                foreach (var asistente in oe.ListaAsistenciaInfo)
                {
                    arrAsistencia += asistente.IdInscrito.ToString() + "," + asistente.IndAsistencia.ToString() + ";";
                    
                }

                if (arrAsistencia.Length > 0)
                    arrAsistencia = arrAsistencia.Substring(0, arrAsistencia.Length - 1);

                AsistenciaDetalleEntidad entidad = new AsistenciaDetalleEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ASISTENCIA.Pr_RegistrarAsistencia",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
                    new OracleParameter { ParameterName = "p_idEntregable", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEntregable },
                    new OracleParameter { ParameterName = "p_idTaller", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdTaller },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_fechaRealizacion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FechaRealizacion },
                    new OracleParameter { ParameterName = "p_arrAsistencia", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = arrAsistencia },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Asistencia.FachadaAsistencia.RegistrarAsistencia :" + e.Message;
            }
            return os;
        }
    }
}
