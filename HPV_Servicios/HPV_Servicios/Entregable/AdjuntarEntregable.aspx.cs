using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPV_Servicios.Entregable
{
    public partial class AdjuntarEntregable : System.Web.UI.Page
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

                File.AppendAllText(nameLog, "Entro a cargar documento \r\n");
                var Contents = new byte[Request.InputStream.Length];
                Request.InputStream.Read(Contents, 0, (int)Request.InputStream.Length);

                File.AppendAllText(nameLog, "Tamano del archivo: " + Contents.Length);

                pathDocument += "/" + idPeriodo;

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                }

                pathDocument += "/" + idEntregable;

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                }

                if (idEntregable.Equals("1") )
                {
                    pathDocument += "/" + idTaller;

                    if (!Directory.Exists(pathDocument))
                    {
                        Directory.CreateDirectory(pathDocument);
                        File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                    }

                }

                pathDocument += "/" + idGrupoFacilitador;

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                    File.WriteAllText(nameLog, "Creo ruta " + pathDocument + "\r\n");
                }

                String outFile = "";
                if (idEntregable.Equals("1"))
                    outFile = pathDocument + "/Asistencia-" + idTaller + "-" + idGrupoFacilitador + ".pdf";

                if (idEntregable.Equals("3"))
                    outFile = pathDocument + "/HistoriaVida-" + idGrupoFacilitador + ".pdf";

                if (File.Exists(outFile))
                    File.Delete(outFile);

                
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(outFile, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                
                
                _FileStream.Write(Contents, 0, Contents.Length);

                
                _FileStream.Close();

                
                
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