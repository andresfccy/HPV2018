using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;
using System.Data.Objects;
using HPV_Entidades.General;
using HPV_Datos.General;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using HPV_Entidades.GeneralWS;
using HPV_Datos.General.Entidad;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace HPV_Datos.General
{
    public class FachadaGeneral : InterfaceGeneral
    {
        public OS_DarListaValor DarListaValor(OE_DarListaValor oe)
        {
            OS_DarListaValor os = new OS_DarListaValor();
            try
            {

                ListaValorEntidad entidad = new ListaValorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_UTILS.Pr_ConsultarLista",
                    new OracleParameter { ParameterName = "p_categoria", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Categoria },
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
                        ListaValorEntidad item = (ListaValorEntidad)objeto;
                        os.ListaValor.Add(item.ListaValor);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.General.DarListaValor :" + e.Message;
            }

            return os;
        }

        public OS_DarParametroInicial DarParametroInicial()
        {
            OS_DarParametroInicial os = new OS_DarParametroInicial();
            try
            {

                ParametroInicialEntidad entidad = new ParametroInicialEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_UTILS.Pr_DarParametroInicial",
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
                        os.ParametroInicial = ((ParametroInicialEntidad)lista[0]).ParametroInicial;
                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.General.DarParametroInicial :" + e.Message;
            }

            return os;
        }

        public OS_EnviarCorreo EnviarCorreo(OE_EnviarCorreo oe)
        {
            OS_EnviarCorreo os = new OS_EnviarCorreo();
            try
            {

                String[] campo = Regex.Split(oe.Body, "},{");

                if (campo.Length > 0)
                {


                    campo[0] = campo[0].Substring(1);
                    campo[campo.Length - 1] = campo[campo.Length - 1].Substring(0, campo[campo.Length - 1].Length - 1);


                    String pathEmail = System.Configuration.ConfigurationManager.AppSettings["pathEmail"];
                    String nameEmail = pathEmail + @"/" + campo[0] + ".html";

                    if (!File.Exists(nameEmail))
                        throw new Exception("No existe template correo " + nameEmail);

                    string readText = File.ReadAllText(nameEmail);

                    for (int i = 0; i < campo.Length; i++)
                    {
                        if (i > 0)
                        {
                            char[] delimiterChars = { ':' };

                            String[] parametro = campo[i].Split(delimiterChars, 2);
                            readText = readText.Replace("&" + parametro[0], parametro[1]);

                        }
                    }

                    oe.Body = readText;

                }




                MailMessage o = new MailMessage(oe.From, oe.To, oe.Subject, oe.Body);

                o.IsBodyHtml = true;

                if (oe.Cc.ToString().Length > 0)
                    o.CC.Add(oe.Cc);

                if (oe.Bcc.ToString().Length > 0)
                    o.Bcc.Add(oe.Bcc);

                NetworkCredential netCred = new NetworkCredential(oe.UserName, oe.Password);
                SmtpClient smtpobj = new SmtpClient(oe.Server, oe.Port);
                smtpobj.Credentials = netCred;
                smtpobj.EnableSsl = true;
            
                smtpobj.Send(o);

                os.Respuesta.Codigo = 0;
                os.Respuesta.Mensaje = "Envio correcto de correo";
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.General.EnviarCorreo :" + e.Message;
            }

            return os;
        }

        public OS_RegistrarEncuestaSatisfaccion RegistrarEncuestaSatisfaccion(OE_RegistrarEncuestaSatisfaccion oe)
        {
            OS_RegistrarEncuestaSatisfaccion os = new OS_RegistrarEncuestaSatisfaccion();
            try
            {

                ParametroInicialEntidad entidad = new ParametroInicialEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_ALI_INSCRIPCION.pr_GuardarEncuestaToken",
                    new OracleParameter { ParameterName = "p_token", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Token },
                    new OracleParameter { ParameterName = "p_encuesta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.RespuestaEncuesta },
                    new OracleParameter { ParameterName = "p_observaciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Observaciones },
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
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.General.RegistrarEncuestaSatisfaccion :" + e.Message;
            }

            return os;


        }

        public OS_ValidarToken ValidarToken(OE_ValidarToken oe)
        {
            OS_ValidarToken os = new OS_ValidarToken();
            try
            {

                ParametroInicialEntidad entidad = new ParametroInicialEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_UTILS.Pr_ValidarToken",
                    new OracleParameter { ParameterName = "p_categoria", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Categoria },
                    new OracleParameter { ParameterName = "p_token", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Token },
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
                os.Respuesta.Mensaje = "Fallo en  HPV_Datos.General.ValidarToken :" + e.Message;
            }

            return os;
        }
    }

}
