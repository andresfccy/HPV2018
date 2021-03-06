﻿using System;
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
using HPV_Entidades.ConsultaWS;
using HPV_Datos.Consulta;
using HPV_Datos.Alistamiento;

namespace HPV_Servicios.Reportes.Certificado
{
    public partial class GenerarCertificado : System.Web.UI.Page
    {
        Document document = null;

        MigraDoc.DocumentObjectModel.Unit width, height;
        MigraDoc.DocumentObjectModel.Unit ancho, alto;

        Section section = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                String pathLog = System.Configuration.ConfigurationManager.AppSettings["pathLog"];
                String nameLog = pathLog + @"/logHPVCerti.txt";

                if (!Directory.Exists(pathLog))
                {
                    Directory.CreateDirectory(pathLog);
                    File.AppendAllText(nameLog, "Creo log ok \r\n");
                }


                String idInscrito = Request.QueryString["idInscrito"];
                String vigencia = Request.QueryString["Vigencia"];

                //File.AppendAllText(nameLog, "fecha : " + DateTime.Now + "\r\n");

                //File.AppendAllText(nameLog, "id inscrito: " + idInscrito + "\r\n");
                //File.AppendAllText(nameLog, "vigencia: " + vigencia + "\r\n");

                if (string.IsNullOrEmpty(idInscrito))
                {
                    File.AppendAllText(nameLog, "Hay parámetros faltantes en la petición" + "\r\n");
                    throw new Exception("Hay parámetros faltantes en la petición.");

                }


                InitDocument();

                var oe = new OE_ConsultarInscrito();
                oe.IdInscrito = long.Parse(idInscrito);
                oe.IdPeriodo = 0;

                var os = new FachadaConsulta().ConsultarInscrito(oe);

                //File.AppendAllText(nameLog, "consultar inscrito cod:" + os.Respuesta.Codigo + "\r\n");
                //File.AppendAllText(nameLog, "consultar inscrito msg:" + os.Respuesta.Mensaje + "\r\n");
                //File.AppendAllText(nameLog, "consultar inscrito doc:" + os.Inscrito.Documento + "\r\n");
                //File.AppendAllText(nameLog, "consultar inscrito vig:" + os.Inscrito.Vigencia + "\r\n");


                var parametrosCert = new FachadaAlistamiento().DarParametrosCertificado(new HPV_Entidades.AlistamientoWS.OE_DarParametrosCertificado { Documento = os.Inscrito.Documento });

                /*File.AppendAllText(nameLog, "consultar par cod:" + parametrosCert.Respuesta.Codigo + "\r\n");
                File.AppendAllText(nameLog, "consultar par msg:" + parametrosCert.Respuesta.Mensaje + "\r\n");
                File.AppendAllText(nameLog, "consultar par enc:" + parametrosCert.Parametros.Encabezado + "\r\n");
                File.AppendAllText(nameLog, "consultar par ali:" + parametrosCert.Parametros.NomAliado + "\r\n");
                File.AppendAllText(nameLog, "consultar par per:" + parametrosCert.Parametros.Periodo + "\r\n");
                File.AppendAllText(nameLog, "consultar par r1:" + parametrosCert.Parametros.Renglon1 + "\r\n");
                File.AppendAllText(nameLog, "consultar par r2:" + parametrosCert.Parametros.Renglon2 + "\r\n");
                File.AppendAllText(nameLog, "consultar par r3:" + parametrosCert.Parametros.Renglon3 + "\r\n");
                File.AppendAllText(nameLog, "consultar par r4:" + parametrosCert.Parametros.Renglon4 + "\r\n");
                File.AppendAllText(nameLog, "consultar par r5:" + parametrosCert.Parametros.Renglon5 + "\r\n");
                File.AppendAllText(nameLog, "consultar par r6:" + parametrosCert.Parametros.Renglon6 + "\r\n");
                */

                if (os.Respuesta.Codigo == 0)
                {
                    CreateSection();
                    vigencia = vigencia == null ? os.Inscrito.Vigencia : vigencia;
                    ConfigHeader(os, vigencia, parametrosCert);
                }


                // Crea un render
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

                // Asocia el documento MigraDoc con un render
                pdfRenderer.Document = document;

                // Capas y el documento rendereizado a PDF
                pdfRenderer.RenderDocument();

                // Enviar al browser
                MemoryStream stream = new MemoryStream();
                pdfRenderer.PdfDocument.Save(stream, false);
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", stream.Length.ToString());
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                stream.Close();
                Response.End();


            }
            catch (Exception err)
            {
              //  Response.Clear();
              //  Response.Write("Genero error " + err.Message + err.StackTrace);

            }
        }

        private void InitDocument()
        {
            document = new Document();
            document.Info.Author = "HPV";
            PageSetup.GetPageSize(PageFormat.Letter, out height, out width);

        }

        private void CreateSection()
        {


            section = document.AddSection();
            section.PageSetup.PageHeight = height;
            section.PageSetup.PageWidth = width;

            ancho = width - XUnit.FromCentimeter(4).Point;
            alto = height - XUnit.FromCentimeter(6).Point;

            section.PageSetup.LeftMargin = XUnit.FromCentimeter(2).Point;
            section.PageSetup.RightMargin = XUnit.FromCentimeter(2).Point;
            section.PageSetup.TopMargin = XUnit.FromCentimeter(4).Point;
            section.PageSetup.BottomMargin = XUnit.FromCentimeter(2).Point;
        }

        private void ConfigHeader(OS_ConsultarInscrito os, string vigencia = "2018", HPV_Entidades.AlistamientoWS.OS_DarParametrosCertificado param = null)
        {

            String pathLog = System.Configuration.ConfigurationManager.AppSettings["pathLog"];
            String nameLog = pathLog + @"/logHPVCerti.txt";


            /*File.AppendAllText(nameLog, "ConfigHeader inscrito cod:" + os.Respuesta.Codigo + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader inscrito msg:" + os.Respuesta.Mensaje + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader inscrito doc:" + os.Inscrito.Documento + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader inscrito vig:" + os.Inscrito.Vigencia + "\r\n");

            File.AppendAllText(nameLog, "ConfigHeader par vig:" + vigencia + "\r\n");

            File.AppendAllText(nameLog, "ConfigHeader par cod:" + param.Respuesta.Codigo + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par msg:" + param.Respuesta.Mensaje + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par enc:" + param.Parametros.Encabezado + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par ali:" + param.Parametros.NomAliado + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par per:" + param.Parametros.Periodo + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par r1:" + param.Parametros.Renglon1 + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par r2:" + param.Parametros.Renglon2 + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par r3:" + param.Parametros.Renglon3 + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par r4:" + param.Parametros.Renglon4 + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par r5:" + param.Parametros.Renglon5 + "\r\n");
            File.AppendAllText(nameLog, "ConfigHeader par r6:" + param.Parametros.Renglon6 + "\r\n");
            */

            String rootPath = Server.MapPath("~");


            Paragraph paragraph = section.Headers.Primary.AddParagraph();
            var rutaImg = vigencia == "2016" ? "Img/pdfheader - 2016.png" : vigencia == "2018" && param.Parametros.Encabezado == null ? "Img/pdfheader2018-1.png" : vigencia == "2018" && param.Parametros.Encabezado != null ? param.Parametros.Encabezado : "Img/pdfheader2018-1.png";
            var image = paragraph.AddImage(rootPath + rutaImg);
            image.Height = XUnit.FromCentimeter(3).Point;
            image.Width = XUnit.FromCentimeter(25).Point;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddLineBreak();

            var renglon1 = vigencia == "2016" ? "Prosperidad Social y su programa" : vigencia == "2018" && param.Parametros.Renglon1 == null ? "El Programa Jóvenes en Acción de Prosperidad Social en alianza con la Organización" : vigencia == "2018" && param.Parametros.Renglon1 != null ? param.Parametros.Renglon1 : "El Programa Jóvenes en Acción de Prosperidad Social en alianza con la Organización";
            paragraph = section.Headers.Primary.AddParagraph(renglon1);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            var renglon2 = vigencia == "2016" ? "Jóvenes en Acción" : vigencia == "2018" && param.Parametros.Renglon2 == null ? "de Estados Iberoamericanos y la Universidad de Santander." : vigencia == "2018" && param.Parametros.Renglon2 != null ? param.Parametros.Renglon2 : "de Estados Iberoamericanos y la Universidad de Santander.";
            paragraph = section.Headers.Primary.AddParagraph(renglon2);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("");
            paragraph = section.Headers.Primary.AddParagraph("");

            paragraph = section.Headers.Primary.AddParagraph("Certifican que");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("");

            paragraph = section.Headers.Primary.AddParagraph(os.Inscrito.NombreInscrito);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("");

            paragraph = section.Headers.Primary.AddParagraph("Identificado con");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("");

            paragraph = section.Headers.Primary.AddParagraph(os.Inscrito.NomTipoDocumento + " No. " + os.Inscrito.Documento);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddLineBreak();

            paragraph = section.Headers.Primary.AddParagraph("");

            var renglon3 = vigencia == "2016" ? "Asistió y participó activamente a los talleres del Módulo Presencial del Componente de Habilidades para la Vida bajo la metodología" : vigencia == "2018" && param.Parametros.Renglon3 == null ? "Asistió y participó activamente en el Módulo Presencial – “Construyendo Mi Camino”, como proceso de formación de veintiséis (26) horas," : vigencia == "2018" && param.Parametros.Renglon3 != null ? param.Parametros.Renglon3 : "Asistió y participó activamente en el Módulo Presencial – “Construyendo Mi Camino”, como proceso de formación de veintiséis (26) horas,";
            paragraph = section.Headers.Primary.AddParagraph(renglon3);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            var renglon4 = vigencia == "2016" ? "de aprendizaje experiencial, desarrollando Competencias Laborales y las siguientes Competencias Transversales: Resiliencia," : vigencia == "2018" && param.Parametros.Renglon4 == null ? "desarrollando ocho (8) talleres bajo la metodología de aprendizaje experiencial, en los cuales se fortalecieron las siguientes Habilidades" : vigencia == "2018" && param.Parametros.Renglon4 != null ? param.Parametros.Renglon4 : "desarrollando ocho (8) talleres bajo la metodología de aprendizaje experiencial, en los cuales se fortalecieron las siguientes Habilidades";
            paragraph = section.Headers.Primary.AddParagraph(renglon4);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            var renglon5 = vigencia == "2016" ? "Perseverancia, Gestión de Conflictos, Comunicación Asertiva, Adaptabilidad, Empatía y Toma de Decisiones, realizados en 2 meses con" : vigencia == "2018" && param.Parametros.Renglon5 == null ? "para la Vida: LIDERAZGO, AUTOCONOCIMIENTO, RESILIENCIA, EMPATÍA, TRABAJO EN EQUIPO, GESTIÓN DE CONFLICTOS, " : vigencia == "2018" && param.Parametros.Renglon5 != null ? param.Parametros.Renglon5 : "para la Vida: LIDERAZGO, AUTOCONOCIMIENTO, RESILIENCIA, EMPATÍA, TRABAJO EN EQUIPO, GESTIÓN DE CONFLICTOS, ";
            paragraph = section.Headers.Primary.AddParagraph(renglon5);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            var renglon6 = vigencia == "2016" ? "una intensidad de 24 horas." : vigencia == "2018" && param.Parametros.Renglon6 == null ? "TOMA DE DECISIONES Y COMUNICACIÓN ASERTIVA, contribuyendo así a su bienestar personal, familiar, social y productivo." : vigencia == "2018" && param.Parametros.Renglon6 != null ? param.Parametros.Renglon6 : "TOMA DE DECISIONES Y COMUNICACIÓN ASERTIVA, contribuyendo así a su bienestar personal, familiar, social y productivo.";
            paragraph = section.Headers.Primary.AddParagraph(renglon6);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("");
            paragraph = section.Headers.Primary.AddParagraph("");
            paragraph = section.Headers.Primary.AddParagraph("");

            String[] fcer = os.Inscrito.FechaCertificado.Split('-');
            List<String> meses = new List<string>();
            meses.Add("Enero");
            meses.Add("Febrero");
            meses.Add("Marzo");
            meses.Add("Abril");
            meses.Add("Mayo");
            meses.Add("Junio");
            meses.Add("Julio");
            meses.Add("Agosto");
            meses.Add("Septiembre");
            meses.Add("Octubre");
            meses.Add("Noviembre");
            meses.Add("Diciembre");

            paragraph = section.Headers.Primary.AddParagraph(
                "En constancia se firma a los " + fcer[2]
                + " días, en " + os.Inscrito.NomMunicipio
                + " en el mes de " + meses[Int32.Parse(fcer[1]) - 1]
                + " de " + fcer[0]
            );
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            var rutaimg2 = vigencia == "2016" ? "Img/firmaCertificado.png" : vigencia == "2018" && param.Parametros.Firma == null ? "Img/firmaCertificado2018-1.png" : vigencia == "2018" && param.Parametros.Firma != null ? param.Parametros.Firma : "Img/firmaCertificado2018-1.png";
            var image2 = paragraph.AddImage(rootPath + rutaimg2);
            image2.Height = XUnit.FromCentimeter(5).Point;
            int ancho = vigencia == "2016" ? 10 : vigencia == "2018" && param == null ? 25 : vigencia == "2018" && param != null ? 25 : 25;
            image2.Width = XUnit.FromCentimeter(ancho).Point;



        }

        private void ConfigFooter()
        {




            Paragraph paragraph = section.Footers.Primary.AddParagraph("Página: ");
            paragraph.AddPageField();
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Alignment = ParagraphAlignment.Right;



        }

        private void FillTableHeader(OS_DarAsistenciaEncabezado os)
        {




            var table = section.AddTable();

            table.Borders.Width = 0;
            var column = table.AddColumn(ancho * 0.25);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.30);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.20);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.25);
            column.Format.Alignment = ParagraphAlignment.Left;


            var row = table.AddRow();
            var cell = row.Cells[0];
            var paragraph = cell.AddParagraph("FECHA");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            cell.Borders.Left.Width = 1;
            cell.Borders.Right.Width = 1;
            cell.Borders.Top.Width = 1;
            cell.Borders.Bottom.Width = 1;
            paragraph = cell.AddParagraph("");

            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;




            cell = row.Cells[2];
            paragraph = cell.AddParagraph("CÓDIGO DE GRUPO");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.SiglaGrupo);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph("DEPARTAMENTO");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomDepartamento);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            cell = row.Cells[2];
            paragraph = cell.AddParagraph("HORARIO DEL TALLER");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            if (os.AsistenciaEncabezado.NomGrupo.ToUpper().Contains("NIVEL"))
                paragraph = cell.AddParagraph("");
            else
                paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomHorario);

            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph("MUNICIPIO");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomMunicipio);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            cell = row.Cells[2];
            paragraph = cell.AddParagraph("LUGAR");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.Lugar);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph("FACILITADOR");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomFacilitador);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            cell = row.Cells[2];
            paragraph = cell.AddParagraph("DIRECCION");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.Direccion);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph("COORDINADOR TERRITORIAL");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomCoordinador);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            cell = row.Cells[2];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph("NOMBRE DEL TALLER");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomTaller + " - " + os.AsistenciaEncabezado.DescripcionTaller);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            cell = row.Cells[2];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph("PERIODO");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(os.AsistenciaEncabezado.NomPeriodoVigente);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;


            cell = row.Cells[2];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Bold = false;

        }

        private MigraDoc.DocumentObjectModel.Tables.Table ConfigTableDetail()
        {

            section.AddParagraph("");
            section.AddParagraph("");


            var table = section.AddTable();

            table.Borders.Width = 1;
            var column = table.AddColumn(ancho * 0.05);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.35);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.15);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.30);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.15);
            column.Format.Alignment = ParagraphAlignment.Left;

            var row = table.AddRow();

            var cell = row.Cells[0];
            var paragraph = cell.AddParagraph("#");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph("APELLIDOS Y NOMBRES");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[2];
            paragraph = cell.AddParagraph("DOCUMENTO");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph("OBSERVACIONES POR PARTICIPANTE");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[4];
            paragraph = cell.AddParagraph("FIRMA");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            return table;
        }

        private void FillTableDetail(MigraDoc.DocumentObjectModel.Tables.Table table, AsistenciaDetalle asistencia)
        {

            var row = table.AddRow();

            var cell = row.Cells[0];
            var paragraph = cell.AddParagraph(asistencia.IdRegistro.ToString());
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;

            cell = row.Cells[1];
            paragraph = cell.AddParagraph(asistencia.Nombre);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;

            cell = row.Cells[2];
            paragraph = cell.AddParagraph(asistencia.Documento);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;

            cell = row.Cells[3];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;

            cell = row.Cells[4];
            paragraph = cell.AddParagraph("");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;




        }

        private void FillEmpyDetail(MigraDoc.DocumentObjectModel.Tables.Table table, Boolean esNivelacion)
        {
            if (esNivelacion)
                for (int i = 0; i < 38; i++)
                {
                    var row = table.AddRow();
                }
            else
                for (int i = 0; i < 8; i++)
                {
                    var row = table.AddRow();
                }


        }

        private void CreateObservation()
        {



            section.AddParagraph("");

            var table = section.AddTable();

            table.Borders.Width = 1;
            var column = table.AddColumn(ancho * 1);
            column.Format.Alignment = ParagraphAlignment.Left;

            var row = table.AddRow();

            var cell = row.Cells[0];

            var paragraph = cell.AddParagraph("OBSERVACIONES");

            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = false;

            paragraph = cell.AddParagraph("");
            paragraph = cell.AddParagraph("");





            section.AddParagraph("");




            table = section.AddTable();

            table.Borders.Width = 1;


            column = table.AddColumn(ancho * 0.12);
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn(ancho * 0.30);
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn(ancho * 0.15);
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn(ancho * 0.30);
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn(ancho * 0.12);
            column.Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();

            cell = row.Cells[0];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("");

            cell = row.Cells[1];
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("FIRMA DEL FACILITADOR");

            cell = row.Cells[2];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("");

            cell = row.Cells[3];
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("FIRMA DEL COORDINADOR");

            cell = row.Cells[4];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("");

        }
    }
}