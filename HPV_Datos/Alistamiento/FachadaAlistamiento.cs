using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.Alistamiento;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;
using System.Data.Objects;
using System.Data;
using HPV_Entidades.AlistamientoWS;
using HPV_Datos.Alistamiento.Modelo;
using HPV_Entidades.General;
using HPV_Datos.General.Entidad;
using HPV_Datos.Alistamiento.Entidad;

namespace HPV_Datos.Alistamiento
{
    public class FachadaAlistamiento : InterfaceAlistamiento
    {
        public OS_DarDepartamentos DarDepartamentos()
        {
            OS_DarDepartamentos os = new OS_DarDepartamentos();

            try
            {


                DepartamentoEntidad entidad = new DepartamentoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_DarDepartamentos",
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
                        DepartamentoEntidad item = (DepartamentoEntidad)objeto;
                        os.ListaDepartamento.Add(item.Departamento);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarDepartamentos :" + e.Message;
            }

            return os;
        }

        public OS_DarDiasDisponibles DarDiasDisponibles(OE_DarDiasDisponibles oe)
        {
            OS_DarDiasDisponibles os = new OS_DarDiasDisponibles();
            try
            {

                DiaEntidad entidad = new DiaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_DarDiasDisponibles",
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMunicipio },
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
                        DiaEntidad item = (DiaEntidad)objeto;
                        os.ListaDia.Add(item.Dia);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarDiasDisponibles :" + e.Message;
            }

            return os;
        }

        public OS_DarEncuesta DarEncuesta(OE_DarEncuesta oe)
        {
            OS_DarEncuesta os = new OS_DarEncuesta();
            try
            {

                EncuestaEntidad entidad = new EncuestaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_DarEncuesta",
                    new OracleParameter { ParameterName = "idTipoEncuesta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.IdTipoEncuesta },
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
                        EncuestaEntidad item = (EncuestaEntidad)objeto;
                        os.ListaEncuesta.Add(item.Encuesta);
                    }

                }

                entidad.Dispose();
            }

            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarEncuesta :" + e.Message;
            }

            return os;
        }

        public OS_DarHorariosDisponibles DarHorariosDisponibles(OE_DarHorariosDisponibles oe)
        {
            OS_DarHorariosDisponibles os = new OS_DarHorariosDisponibles();
            try
            {

                HorarioEntidad entidad = new HorarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_DarHorariosDisponibles",
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMunicipio },
                    new OracleParameter { ParameterName = "p_dia", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdDia },
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
                        HorarioEntidad item = (HorarioEntidad)objeto;
                        os.ListaHorario.Add(item.Horario);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarHorariosDisponibles :" + e.Message;
            }

            return os;
        }

        public OS_DarLugaresDisponibles DarLugaresDisponibles(OE_DarLugaresDisponibles oe)
        {
            OS_DarLugaresDisponibles os = new OS_DarLugaresDisponibles();
            try
            {

                LugarEntidad entidad = new LugarEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_DarLugaresDisponibles",
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMunicipio },
                    new OracleParameter { ParameterName = "p_dia", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdDia },
                    new OracleParameter { ParameterName = "p_horario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdHorario },
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
                        LugarEntidad item = (LugarEntidad)objeto;
                        os.ListaLugar.Add(item.Lugar);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarLugaresDisponibles :" + e.Message;
            }

            return os;
        }

        public OS_DarMunicipiosXDepto DarMunicipiosXDepto(OE_DarMunicipiosXDepto oe)
        {
            OS_DarMunicipiosXDepto os = new OS_DarMunicipiosXDepto();
            try
            {

                MunicipioEntidad entidad = new MunicipioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_DarMunicipiosXDepto",
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
                        MunicipioEntidad item = (MunicipioEntidad)objeto;
                        os.ListaMunicipio.Add(item.Municipio);
                    }

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarMunicipiosXDepto :" + e.Message;
            }

            return os;


        }

        public OS_DarPeriodoVigente DarPeriodoVigente()
        {
            OS_DarPeriodoVigente os = new OS_DarPeriodoVigente();
            try
            {

                PeriodoEntidad entidad = new PeriodoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_ObtenerPeriodoVigente",
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
                        os.Periodo = ((PeriodoEntidad)lista[0]).Periodo;

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.DarPeriodoVigente :" + e.Message;
            }

            return os;

        }

        public OS_GenerarCertificado GenerarCertificado(OE_GenerarCertificado oe)
        {
            OS_GenerarCertificado os = new OS_GenerarCertificado();
            try
            {

                PeriodoEntidad entidad = new PeriodoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_GenerarCertificado",
                    new OracleParameter { ParameterName = "p_tipoDocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.TipoDocumento },
                    new OracleParameter { ParameterName = "p_documento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Documento },
                    new OracleParameter { ParameterName = "p_codigoBeneficiario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CodigoBeneficiario },
                    new OracleParameter { ParameterName = "p_fechaNacimiento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FechaNacimiento },
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output},
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );


                os.IdInscrito = entidad.GetParameterLong("p_idInscrito");
                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.GenerarCertificado :" + e.Message;
            }

            return os;
        }


        public OS_GuardarInscripcion GuardarInscripcion(OE_GuardarInscripcion oe)
        {
            OS_GuardarInscripcion os = new OS_GuardarInscripcion();
            try
            {

                InscripcionEntidad entidad = new InscripcionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_GuardarInscripcion",
                    new OracleParameter { ParameterName = "p_esBeneficiario", OracleDbType = OracleDbType.Int16, Direction = ParameterDirection.Input, Value = oe.EsBeneficiario },
                    new OracleParameter { ParameterName = "p_grupofacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.GrupoFacilitador },
                    new OracleParameter { ParameterName = "p_tipoDocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.TipoDocumento },
                    new OracleParameter { ParameterName = "p_documento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.Documento },
                    new OracleParameter { ParameterName = "p_codigoBeneficiario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Beneficiario.CodigoBeneficiario },
                    new OracleParameter { ParameterName = "p_primernombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.PrimerNombre },
                    new OracleParameter { ParameterName = "p_segundonombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.SegundoNombre },
                    new OracleParameter { ParameterName = "p_primerapellido", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.PrimerApellido },
                    new OracleParameter { ParameterName = "p_segundoapellido", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.SegundoApellido },
                    new OracleParameter { ParameterName = "p_fechaNacimiento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.FechaNacimiento },
                    new OracleParameter { ParameterName = "p_email", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.Correo },
                    new OracleParameter { ParameterName = "p_telefono", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.Telefono },
                    new OracleParameter { ParameterName = "p_movil", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.Movil },
                    new OracleParameter { ParameterName = "p_movilalt", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Beneficiario.MovilAlt },
                    new OracleParameter { ParameterName = "p_condicionEspecial", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Beneficiario.CondicionEspecial },
                    new OracleParameter { ParameterName = "p_encuesta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.RespuestaEncuesta },
                    new OracleParameter { ParameterName = "p_codigoInscripcion", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                    os.CodigoInscripcion = entidad.GetParameterLong("p_codigoInscripcion");

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.GuardarInscripcion :" + e.Message;
            }

            return os;
        }

        public OS_RegistrarEncuestaFinal RegistrarEncuestaFinal(OE_RegistrarEncuestaFinal oe)
        {
            OS_RegistrarEncuestaFinal os = new OS_RegistrarEncuestaFinal();
            try
            {

                InscripcionEntidad entidad = new InscripcionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_RegistrarEncuestaFinal",
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdInscrito },
                    new OracleParameter { ParameterName = "p_encuesta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Encuesta },
                    new OracleParameter { ParameterName = "p_observaciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Observaciones},
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.RegistrarEncuestaFinal :" + e.Message;
            }

            return os;
        }

        public OS_ValidarBeneficiario ValidarBeneficiario(OE_ValidarBeneficiario oe)
        {
            OS_ValidarBeneficiario os = new OS_ValidarBeneficiario();
            try
            {

                BeneficiarioEntidad entidad = new BeneficiarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_FlujoInscripcion",
                    new OracleParameter { ParameterName = "p_tipoDocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.TipoDocumento },
                    new OracleParameter { ParameterName = "p_documento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Documento },
                    new OracleParameter { ParameterName = "p_codigoBeneficiario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CodigoBeneficiario },
                    new OracleParameter { ParameterName = "p_fechaNacimiento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FechaNacimiento },
                    new OracleParameter { ParameterName = "p_consultaEnLinea", OracleDbType = OracleDbType.Int16, Direction = ParameterDirection.Input, Value = oe.ConsultaEnLinea },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );



                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0 
                    || os.Respuesta.Codigo == 60 
                    || os.Respuesta.Codigo == 61 
                    || os.Respuesta.Codigo == 62 
                    || os.Respuesta.Codigo == 63 
                    || os.Respuesta.Codigo == 105 
                    || os.Respuesta.Codigo == 106
                    || os.Respuesta.Codigo == 107)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    if (lista.Count > 0)
                        os.Beneficiario = ((BeneficiarioEntidad)lista[0]).Beneficiario;

                }

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.GuardarInscripcion :" + e.Message;
            }

            return os;
        }

        public OS_ValidarCorreo ValidarCorreo(OE_ValidarCorreo oe)
        {
            OS_ValidarCorreo os = new OS_ValidarCorreo();
            try
            {

                CorreoEntidad entidad = new CorreoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("pk_ali_inscripcion.Pr_ValidarCorreo",
                    new OracleParameter { ParameterName = "p_correo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Correo },
                    new OracleParameter { ParameterName = "p_tipoDocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.TipoDocumento },
                    new OracleParameter { ParameterName = "p_documento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Documento },
                    new OracleParameter { ParameterName = "p_codigoBeneficiario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.CodigoBeneficiario },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.Alistamiento.FachadaAlistamiento.ValidarCorreo :" + e.Message;
            }

            return os;
        }
    }
}
