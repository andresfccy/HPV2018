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
    public partial class ConsultarInstrumento : System.Web.UI.Page
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

                String idSistematizacion = Request.QueryString["IdSistematizacion"];

                if (idSistematizacion == null)
                {
                    File.WriteAllText(nameLog, "HAY PARAMETROS FALTANTES \r\n");
                    Response.Write("HAY PARAMETROS FALTANTES" + "\r\n");
                    return;
                }

                OE_ConsultarSistematizacion oe = new OE_ConsultarSistematizacion()
                {
                    IdSistematizacion = Int64.Parse(idSistematizacion)
                };
                OS_ConsultarSistematizacion buscada = new FachadaSistematizacion().ConsultarSistematizacion(oe);

                if (buscada == null)
                {
                    File.WriteAllText(nameLog, "No se encontró un instrumento con el Id buscado \r\n");
                    Response.Write("No se encontró un instrumento con el Id buscado" + "\r\n");
                    return;
                }

                File.AppendAllText(nameLog, "Entro a consultar documento \r\n");

                pathDocument += "/" + buscada.Sistematizacion.IdPeriodo + "/SIS";
                pathDocument += "/" + buscada.Sistematizacion.IdInstrumento;
                pathDocument += "/" + buscada.Sistematizacion.IdGrupoFacilitador;

                String outFile = "";
                String fileName = "";

                outFile = pathDocument + "/" + buscada.Sistematizacion.IdInstrumento + "-" + buscada.Sistematizacion.IdGrupoFacilitador + ".pdf";
                fileName = buscada.Sistematizacion.IdInstrumento + "-" + buscada.Sistematizacion.IdInstrumento + "-" + buscada.Sistematizacion.IdGrupoFacilitador + ".pdf";

                if (!File.Exists(outFile))
                {
                    File.WriteAllText(nameLog, "NO EXISTE ARCHIVO \r\n");
                    Response.Write("NO EXISTE ARCHIVO" + "\r\n");
                    return;
                }

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content–Disposition", "attachment; filename =" + fileName);
                Response.WriteFile(outFile);
                Response.Flush();
            }
            catch (Exception err)
            {
                File.WriteAllText(nameLog, "Se genero error " + err.Message);
                File.WriteAllText(nameLog, "Se genero error " + err.StackTrace);

                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(err.Message + "\r\n");
                Response.Write(err.StackTrace + "\r\n");
                Response.Flush();
            }
        }
    }
}