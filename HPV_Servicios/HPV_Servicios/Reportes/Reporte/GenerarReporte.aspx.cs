using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using MigraDoc.Rendering;
using PdfSharp;
using HPV_Datos.Asistencia;
using HPV_Entidades.AsistenciaWS;
using HPV_Entidades.Asistencia;

namespace HPV_Servicios.Reportes.Reporte
{
    public partial class Reporte : System.Web.UI.Page
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

                String idFacilitador = Request.QueryString["IdFacilitador"];
                String idTaller = Request.QueryString["IdTaller"];
                String idEntregable = Request.QueryString["IdEntregable"];
                String idPeriodo = Request.QueryString["IdPeriodo"];
                String idGrupoFacilitador = Request.QueryString["IdGrupoFacilitador"];

                if (idFacilitador == null || idTaller == null || idEntregable == null || idPeriodo == null || idGrupoFacilitador == null)
                {
                    File.WriteAllText(nameLog, "NO HAY PARAMETROS \r\n");
                    Response.Write("NO HAY PARAMETROS" + "\r\n");
                    return;
                }

                File.AppendAllText(nameLog, "Entro a consultar documento \r\n");


                pathDocument += "/" + idPeriodo;
                pathDocument += "/" + idEntregable;

                if (idEntregable.Equals("1"))
                    pathDocument += "/" + idTaller;

                pathDocument += "/" + idGrupoFacilitador;

                String outFile = "";
                String fileName = "";

                if (idEntregable.Equals("1"))
                {
                    outFile = pathDocument + "/Asistencia-" + idTaller + "-" + idGrupoFacilitador + ".pdf";
                    fileName = "asistencia-" + idTaller + "-" + idGrupoFacilitador + ".pdf";

                }

                if (idEntregable.Equals("3"))
                {
                    outFile = pathDocument + "/HistoriaVida-" + idGrupoFacilitador + ".pdf";
                    fileName = "HistoriaVida-" + idGrupoFacilitador + ".pdf";

                }
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