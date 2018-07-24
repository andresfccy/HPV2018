using HPV_Datos.Sistematizacion;
using HPV_Entidades.SistematizacionWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPV_Servicios.Sistematizacion
{
    public partial class AdjuntarInstrumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String pathLog = System.Configuration.ConfigurationManager.AppSettings["pathLog"];
            String nameLog = pathLog + @"/logHPV.txt";

            try
            {
                if (!Directory.Exists(pathLog))
                {
                    Directory.CreateDirectory(pathLog);
                    File.WriteAllText(nameLog, "Creo log ok \r\n");
                }

                String pathDocument = System.Configuration.ConfigurationManager.AppSettings["pathDocument"];

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta documentos \r\n");
                }

                String idUsuario = Request.QueryString["IdUsuario"];
                String idPeriodo = Request.QueryString["IdPeriodo"]; // Opcional
                String idGrupoFacilitador = Request.QueryString["IdGrupoFacilitador"];
                String idFacilitador = Request.QueryString["IdFacilitador"];
                String idInstrumento = Request.QueryString["IdInstrumento"];
                String linkVideo = Request.QueryString["LinkVideo"];

                if (idFacilitador == null || idInstrumento == null || idGrupoFacilitador == null || idUsuario == null)
                {
                    File.WriteAllText(nameLog, "HAY PARAMETROS FALTANTES \r\n");
                    Response.Write("HAY PARAMETROS FALTANTES" + "\r\n");
                    return;
                }

                File.AppendAllText(nameLog, "Entro a cargar documento \r\n");
                var Contents = new byte[Request.InputStream.Length];
                Request.InputStream.Read(Contents, 0, (int)Request.InputStream.Length);

                File.AppendAllText(nameLog, "Tamano del archivo: " + Contents.Length);

                pathDocument += "/" + idPeriodo + "/SIS";

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                }

                pathDocument += "/" + idInstrumento;

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                }

                pathDocument += "/" + idGrupoFacilitador;

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                }

                String outFile = "";
                outFile = pathDocument + "/" + idInstrumento + "-" + idGrupoFacilitador + ".pdf";

                if (File.Exists(outFile))
                    File.Delete(outFile);

                System.IO.FileStream _FileStream = new System.IO.FileStream(outFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                _FileStream.Write(Contents, 0, Contents.Length);
                _FileStream.Close();

                // Creación del objeto en la BD
                OE_ActualizarSistematizacion oe = new OE_ActualizarSistematizacion()
                {
                    IdUsuario = Int64.Parse(idUsuario),
                    Sistematizacion = new HPV_Entidades.Sistematizacion.Sistematizacion()
                    {
                        IdFacilitador = Int64.Parse(idFacilitador),
                        IdGrupoFacilitador = Int64.Parse(idGrupoFacilitador),
                        LinkVideo = linkVideo ?? "",
                        IdInstrumento = Int64.Parse(idInstrumento)
                    }
                };
                new FachadaSistematizacion().ActualizarSistematizacion(oe);

                Response.Write("Archivo cargado correctamente");
            }
            catch (Exception err)
            {
                File.WriteAllText(nameLog, "Se genero error " + err.Message);
                File.WriteAllText(nameLog, "Se genero error " + err.StackTrace);

                Response.Write(err.Message + "\r\n");
                Response.Write(err.StackTrace + "\r\n");
            }
        }
    }
}