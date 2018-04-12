using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPV_Servicios.Indicadores
{
    public partial class Indicadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String pathDocument = System.Configuration.ConfigurationManager.AppSettings["pathDocument"];

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                }

                String idPeriodo = Request.QueryString["IdPeriodo"];
                String nombre = Request.QueryString["Nombre"];

                if (nombre == null || idPeriodo == null)
                {
                    Response.Write("PARÁMETROS INCORRECTOS O VACÍOS" + "\r\n");
                    return;
                }
                pathDocument += "/" + idPeriodo;
                pathDocument += "/" + 4;

                String outFile = "";
                String fileName = "";
                
                fileName = nombre;
                outFile = pathDocument + "/" + fileName;
                
                if (!File.Exists(outFile))
                {
                    Response.Write("EL ARCHIVO NO EXISTE" + "\r\n");
                    return;
                }

                byte[] binaryRpt = File.ReadAllBytes(outFile);

                Response.Clear();
                
                if (nombre.ToUpper().Contains("XLS"))
                    Response.ContentType = "application/vnd.ms-excel";
                if (nombre.ToUpper().Contains("PDF"))
                    Response.ContentType = "application/pdf";

                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.Charset = "";
                Response.BinaryWrite(binaryRpt);
                
            }
            catch (Exception err)
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(err.Message + "\r\n");
                Response.Write(err.StackTrace + "\r\n");
                Response.Flush();
            }
        }
    }
}