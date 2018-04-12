using HPV_Datos.General.Entidad;
using HPV_Datos.EncuestasSena.Entidad;
using HPV_Entidades.General;
using HPV_Entidades.IncripcionEncuestasWS;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.EncuestasSena;
using System.Configuration;
using System.IO;

namespace HPV_Datos.EncuestasSena
{
    public class FachadaInscripcion : InterfaceInscripcion
    {
        string EncuEntrada = ConfigurationManager.AppSettings["EncuEntrada"];

        public OS_ConsultarTiposDocumento ConsultarTiposDocumento()
        {
            OS_ConsultarTiposDocumento os = new OS_ConsultarTiposDocumento();
            try
            {
                TiposDocumentoEntidad entidad = new TiposDocumentoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_ALI_INSCRIPCION.pr_ObtenerTiposDocumento",
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        TiposDocumentoEntidad item = (TiposDocumentoEntidad)objeto;
                        os.ListaTiposDocumento.Add(item.TiposDocumento);
                    }
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.ConssultarTiposDocumento :" + e.Message;
            }

            return os;
        }

        public OS_CrearDatosBasicos ValidarExistenciaUsuario(OS_CrearDatosBasicos UsuarioSena)
        {
            OS_CrearDatosBasicos os = new OS_CrearDatosBasicos();
            try
            {
                UsuariosSenaEntidad entidad = new UsuariosSenaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                entidad.ExecuteStoreProcedure("PK_ALI_INSCRIPCION.pr_ValidarUsarioSENA",
                new OracleParameter { ParameterName = "p_documento", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.Documento },
                 new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        UsuariosSenaEntidad item = (UsuariosSenaEntidad)objeto;
                        os.UsuarioEncuestaSena = item.UsusarioEncuesta;
                    }
                }

                entidad.Dispose();


            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.ValidarExistenciaUsuario :" + e.Message;
            }

            return os;
        }

        public bool ValidarEncuestaUsuario(OS_CrearDatosBasicos UsuarioSena)
        {
            bool result = false;
            OS_CrearDatosBasicos os = new OS_CrearDatosBasicos();
            try
            {
                UsuariosSenaEntidad entidad = new UsuariosSenaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                entidad.ExecuteStoreProcedure("PK_ENCUESTASSENA.pr_ValidarEncuestaUsuario",
                new OracleParameter { ParameterName = "p_usuariosena", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.IdUsusarioEncuestaSena },
                new OracleParameter { ParameterName = "p_Encuesta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.IdEncuestaPresentar },
                 new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        UsuariosSenaEntidad item = (UsuariosSenaEntidad)objeto;
                        os.UsuarioEncuestaSena = item.UsusarioEncuesta;
                    }
                }

                entidad.Dispose();

                if (os.UsuarioEncuestaSena.IdUsusarioEncuestaSena > 0)
                    result = true;

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.ValidarEncuestaUsuario :" + e.Message;
                return result;
            }

            return result;

        }

        public OS_CrearDatosBasicos CrearUsuariosEncuestasSena(OS_CrearDatosBasicos UsuarioSena)
        {
            OS_CrearDatosBasicos os = new OS_CrearDatosBasicos();
            try
            {
                UsuariosSenaEntidad entidad = new UsuariosSenaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_ALI_INSCRIPCION.pr_CrearUsuarioSena",
                    new OracleParameter { ParameterName = "p_tipoDocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.TipoDocumento },
                    new OracleParameter { ParameterName = "p_documento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.Documento },
                    new OracleParameter { ParameterName = "p_nombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.Nombre },
                    new OracleParameter { ParameterName = "p_primerApellido", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.PrimerApellido },
                    new OracleParameter { ParameterName = "P_sedundoApellido", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.SegundoApellido },
                    new OracleParameter { ParameterName = "p_correo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.Email },
                    new OracleParameter { ParameterName = "p_celular", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.Celular },
                    new OracleParameter { ParameterName = "P_esJovenEnAccion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = UsuarioSena.UsuarioEncuestaSena.EsJovenEnAccion },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        UsuariosSenaEntidad item = (UsuariosSenaEntidad)objeto;
                        os.UsuarioEncuestaSena = item.UsusarioEncuesta;
                        os.UsuarioEncuestaSena.IdEncuestaPresentar = UsuarioSena.UsuarioEncuestaSena.IdEncuestaPresentar;
                    }
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.CrearUsuariosEncuestasSena :" + e.Message;
            }

            return os;
        }


        public OS_Consultar_Preguntas ConsultarPreguntas(int IdEncuesta)
        {
            OS_Consultar_Preguntas os = new OS_Consultar_Preguntas();
            try
            {
                PreguntasEntidad entidad = new PreguntasEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_T13_ENCUESTAPREGUNTAS.Pr_ConsultarPregXEncuesta",
                     new OracleParameter { ParameterName = "p_a13encuesta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = IdEncuesta },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        PreguntasEntidad item = (PreguntasEntidad)objeto;
                        item.Pregunta.Respuestas = ObtenerRespuestas(item.Pregunta.IdPregunta);
                        os.Preguntas.Add(item.Pregunta);
                    }
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.ConsultarPreguntas :" + e.Message;
            }

            return os;
        }

        private List<OE_Respuesta> ObtenerRespuestas(int IdPregunta)
        {
            OS_Consultar_Respuestas os = new OS_Consultar_Respuestas();
            try
            {
                RespuestaEntidad entidad = new RespuestaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_T14_ENCUESTARESPUESTAS.Pr_ConsultarRespXPregunta",
                     new OracleParameter { ParameterName = "p_a14pregunta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = IdPregunta },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });


                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        RespuestaEntidad item = (RespuestaEntidad)objeto;
                        os.Respuestas.Add(item.Respuesta);
                    }
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.ObtenerRespuestas :" + e.Message;
            }
            return os.Respuestas;
        }


        public OS_CrearDatosBasicos GuardarRespuestasEncuesta(OS_Guardar_Encuesta repuestasEncuesta)
        {
            OS_CrearDatosBasicos os = new OS_CrearDatosBasicos();
            try
            {
                int _codUsuarioSena = 0;
                int _codEncuesta = 0;
                RespuestasEncuestaEntidad entidad = new RespuestasEncuestaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                foreach (OE_RespuestasEncuesta respuesta in repuestasEncuesta.RespuestasEncuesta)
                {
                    _codUsuarioSena = respuesta.CodUsuarioSena;
                    _codEncuesta = respuesta.CodEncuesta;
                    entidad.ExecuteStoreProcedure("PK_ENCUESTASSENA.pr_guardar_encuestasena",
                        new OracleParameter { ParameterName = "p_usuariosena", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = respuesta.CodUsuarioSena },
                        new OracleParameter { ParameterName = "p_encuesta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = respuesta.CodEncuesta },
                        new OracleParameter { ParameterName = "p_pregunta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = respuesta.CodPregunta },
                        new OracleParameter { ParameterName = "p_respuesta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = respuesta.CodRespuesta },
                        new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                        new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });
                    os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                    os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");
                    if (os.Respuesta.Codigo > 0)
                    {
                        break;
                    }
                }
                
                if (os.Respuesta.Codigo == 0)
                {
                    int esEntrada = 0;
                    if (EncuEntrada.Equals(_codEncuesta.ToString()))
                        esEntrada = 1;

                    
                    UsuarioSenaEncuestaEntidad entidadUsu = new UsuarioSenaEncuestaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                    entidadUsu.ExecuteStoreProcedure("PK_ENCUESTASSENA.pr_guardar_Consecutivo",
                    new OracleParameter { ParameterName = "pusuarioSena", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = _codUsuarioSena },
                     new OracleParameter { ParameterName = "pencuesta", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = _codEncuesta },
                     new OracleParameter { ParameterName = "esEntrada", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = esEntrada },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                    os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                    os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                    
                    if (os.Respuesta.Codigo == 0)
                    {
                        entidadUsu = new UsuarioSenaEncuestaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                        entidadUsu.ExecuteStoreProcedure("PK_ENCUESTASSENA.pr_ObtenenerUsuarioSena",
                        new OracleParameter { ParameterName = "p_usuariosena", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = _codUsuarioSena },
                         new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                        new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                        new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 });

                        os.Respuesta.Codigo = entidadUsu.GetParameterLong("p_codError");
                        os.Respuesta.Mensaje = entidadUsu.GetParameterString("p_msjError");

                        if (os.Respuesta.Codigo == 0)
                        {
                            List<EntidadOracle> lista = entidadUsu.CursorToList("p_resultado");

                            foreach (EntidadOracle objeto in lista)
                            {
                                UsuariosSenaEntidad item = (UsuariosSenaEntidad)objeto;
                                os.UsuarioEncuestaSena.IdUsusarioEncuestaSena = item.UsusarioEncuesta.IdUsusarioEncuestaSena;
                                os.UsuarioEncuestaSena.TipoDocumento = item.UsusarioEncuesta.TipoDocumento;
                                os.UsuarioEncuestaSena.Documento = item.UsusarioEncuesta.Documento;
                                os.UsuarioEncuestaSena.Nombre = item.UsusarioEncuesta.Nombre;
                                os.UsuarioEncuestaSena.PrimerApellido = item.UsusarioEncuesta.PrimerApellido;
                                os.UsuarioEncuestaSena.SegundoApellido = item.UsusarioEncuesta.SegundoApellido;
                                os.UsuarioEncuestaSena.Email = item.UsusarioEncuesta.Email;
                                os.UsuarioEncuestaSena.Celular = item.UsusarioEncuesta.Celular;
                                os.UsuarioEncuestaSena.EsJovenEnAccion = item.UsusarioEncuesta.EsJovenEnAccion;
                                os.UsuarioEncuestaSena.Consecutivo = item.UsusarioEncuesta.Consecutivo;
                            }
                        }
                    }
                }
                entidad.Dispose();


            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.InscripcionEncuestas.GuardarRespuestasEncuesta :" + e.Message;
                
            }

            return os;
               

        }
    }
}
