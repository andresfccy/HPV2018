using HPV_EncuestasSena.Models;
using HPV_EncuestasSena.ServicioEncuestasSena;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HPV_EncuestasSena.Controllers
{
    public class CertificadoController : Controller
    {
        string codEncuestaEntrada = ConfigurationManager.AppSettings["codencuestaentrada"];
        string codEncuestaSalida = ConfigurationManager.AppSettings["codencuestasalida"];
        string MsjEncuestaEntrada = ConfigurationManager.AppSettings["msjEntrada"];
        string MsjEncuestaSalida = ConfigurationManager.AppSettings["msjSalida"];
        string urlLogoPs = ConfigurationManager.AppSettings["urlLogoPs"];
        string urlLogoNuevoPais = ConfigurationManager.AppSettings["urlLogoNuevoPais"];
        string urlLogojovenes = ConfigurationManager.AppSettings["urlLogojovenes"];
        string urlLogoCursoV = ConfigurationManager.AppSettings["urlLogoCursoV"];
        string urlJovenok = ConfigurationManager.AppSettings["urlJovenok"];
        string cuestionarioEntrada = ConfigurationManager.AppSettings["cuestionarioEntrada"];
        string cuestionarioSalida = ConfigurationManager.AppSettings["cuestionarioSalida"];
        string rutaHtml = ConfigurationManager.AppSettings["rutaHtml"];
        string rutacss = ConfigurationManager.AppSettings["rutacss"];
        string nombreArchivoSalida = ConfigurationManager.AppSettings["nombreArchivoSalida"];
        string nombreArchivoEntrada = ConfigurationManager.AppSettings["nombreArchivoEntrada"];
        
        // GET: Certificado
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerarCertificado()
        {
            InscripcionModel datos = new InscripcionModel();
            if (TempData["EncuestaModel"] != null)
            {
                OS_CrearDatosBasicos datosUsuario = TempData["EncuestaModel"] as OS_CrearDatosBasicos;

                Session["Nombre"] = datosUsuario.UsuarioEncuestaSena.Nombre;
                Session["PrimerApellido"] = datosUsuario.UsuarioEncuestaSena.PrimerApellido;
                Session["SegundoApellido"] = datosUsuario.UsuarioEncuestaSena.SegundoApellido;
                Session["Documento"] = datosUsuario.UsuarioEncuestaSena.Documento;
                Session["idEncuesta"] = datosUsuario.UsuarioEncuestaSena.IdEncuestaPresentar;
                Session["Consecutivo"] = datosUsuario.UsuarioEncuestaSena.Consecutivo;
                if (Session["idEncuesta"].ToString().Equals(codEncuestaEntrada))
                    Session["nombreEncuesta"] = MsjEncuestaEntrada;
                else
                    Session["nombreEncuesta"] = MsjEncuestaSalida;                
            }

            datos.Nombre = Session["Nombre"].ToString ();
            datos.PrimerApellido = Session["PrimerApellido"].ToString ();
            datos.SegundoApellido = Session["SegundoApellido"].ToString ();
            datos.NumeroDocumento = Session["Documento"].ToString ();
            if (Session["idEncuesta"].ToString().Equals(codEncuestaEntrada))
            {
                datos.Mensaje = Session["nombreEncuesta"].ToString();
                datos.NombreEncuesta = cuestionarioEntrada;
            }
            else
            {
                datos.Mensaje = Session["nombreEncuesta"].ToString();
                datos.NombreEncuesta = cuestionarioSalida;
            }

            return View(datos);
        }

        [HttpPost]
        public ActionResult GenerarCertificado(InscripcionModel usuario)
        {
            string nomArchivo = string.Empty;
            WebClient wc = new WebClient();
            string htmlText = wc.DownloadString(rutaHtml);
            string cssText = wc.DownloadString(rutacss);

            string fecha = "{0} de {1} de {2}";
            fecha = string.Format(fecha, DateTime.Now.Day, Obtenermes(), DateTime.Now.Year);

            htmlText = htmlText.Replace("#urlLogoPs#", urlLogoPs);
            htmlText = htmlText.Replace("#urlLogoNuevoPais#", urlLogoNuevoPais);
            htmlText = htmlText.Replace("#urlLogojovenes#", urlLogojovenes);
            htmlText = htmlText.Replace("#urlLogoCursoV#", urlLogoCursoV);
            htmlText = htmlText.Replace("#urlJovenok#", urlJovenok);
            htmlText = htmlText.Replace("#nombreCompleto#", usuario.Nombre +" "+ usuario.PrimerApellido +" "+ usuario.SegundoApellido);
            htmlText = htmlText.Replace("#documento#", usuario.NumeroDocumento);
            htmlText = htmlText.Replace("#fecha#", fecha);
            htmlText = htmlText.Replace("#encuesta# ", usuario.Mensaje);
            htmlText = htmlText.Replace("#Conse#", Session["Consecutivo"].ToString ());
            if (usuario.Mensaje.Equals(MsjEncuestaEntrada))
            {
                htmlText = htmlText.Replace("#cuestionario#", cuestionarioEntrada);
                nomArchivo = nombreArchivoEntrada;
            }
            if (usuario.Mensaje.Equals(MsjEncuestaSalida))
            {
                htmlText = htmlText.Replace("#cuestionario#", cuestionarioSalida);
                nomArchivo = nombreArchivoSalida;
            }

            Response.Clear();
            Response.ContentType = "pdf/application";
            Response.AddHeader("content-disposition", "attachment;filename=" + nomArchivo + usuario.NumeroDocumento+".pdf");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ObtenerBytesPDF(htmlText, cssText));
            Response.End();
                         
            InscripcionModel datos = new InscripcionModel();
            datos.Nombre = usuario.Nombre;
            datos.PrimerApellido = usuario.PrimerApellido;
            datos.SegundoApellido = usuario.SegundoApellido;
            datos.NumeroDocumento = usuario.NumeroDocumento;
            datos.Mensaje = usuario.Mensaje;
            datos.NombreEncuesta = usuario.NombreEncuesta;


            return View(datos);
            
        }

        private string Obtenermes()
        {
            string mes=string.Empty;

            if (DateTime.Now.Month == 1)
                mes = "Enero";
            if (DateTime.Now.Month == 2)
                mes = "Febrero";
            if (DateTime.Now.Month == 3)
                mes = "Marzo";
            if (DateTime.Now.Month == 4)
                mes = "Abril";
            if (DateTime.Now.Month == 5)
                mes = "Mayo";
            if (DateTime.Now.Month == 6)
                mes = "Junio";
            if (DateTime.Now.Month == 7)
                mes = "Julio";
            if (DateTime.Now.Month == 8)
                mes = "Agosto";
            if (DateTime.Now.Month == 9)
                mes = "Septiembre";
            if (DateTime.Now.Month == 10)
                mes = "Octubre";
            if (DateTime.Now.Month == 11)
                mes = "Noviembre";
            if (DateTime.Now.Month == 12)
                mes = "Diciembre";

            return mes;
        }

        private void DescargarArchivoPdf(string RutaServer)
        {
            System.IO.Stream iStream = null;
            byte[] buffer = new Byte[10000];
            int length;
            long dataToRead;

            string filepath = string.Empty;
            string filename = System.IO.Path.GetFileName(RutaServer);

            try
            {
                iStream = new System.IO.FileStream(RutaServer, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

                dataToRead = iStream.Length;
                Response.ContentType = "pdf/application";
                Response.AddHeader("Content-Disposition", "attachment; filename=PDFfile.pdf");
                Response.Buffer = true;
                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();
                        buffer = new Byte[10000]; dataToRead = dataToRead - length;
                    }
                    else
                    {
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    iStream.Close();
                    Response.End();
                }
            }
        }

        private static byte[] ObtenerBytesPDF(string html, string cssText)
        {
            Byte[] bytes = new Byte[] { };
                       
            using (var ms = new MemoryStream())
            {
                //TextReader txtReader = new StringReader(html);
                //Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                //PdfWriter oPdfWriter = PdfWriter.GetInstance(document, ms);

                //HTMLWorker htmlWorker = new HTMLWorker(document);
                
                //document.Open();
                //htmlWorker.StartDocument();
                //htmlWorker.Parse(txtReader);

               

                //htmlWorker.EndDocument();
                //htmlWorker.Close();
                //document.Close();

                //bytes = ms.ToArray();

                var document = new Document(PageSize.A4, 50, 50, 60, 60);
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();


                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                {
                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                    }
                }
                document.Close();

                bytes = ms.ToArray();
                                            
            }
            return bytes;
        }
    }
}