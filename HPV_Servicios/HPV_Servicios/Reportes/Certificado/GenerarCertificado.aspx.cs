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
using HPV_Entidades.ConsultaWS;
using HPV_Datos.Consulta;

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

                String idInscrito = Request.QueryString["idInscrito"];

                if (idInscrito == null)
                    throw new Exception("NO HAY INFORMACION");


                InitDocument();

                var oe = new OE_ConsultarInscrito();
                oe.IdInscrito = long.Parse(idInscrito);
                oe.IdPeriodo = 0;

                var os = new FachadaConsulta().ConsultarInscrito(oe);

                if (os.Respuesta.Codigo == 0)
                {
                        CreateSection();
                        ConfigHeader(os);
                        
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
                Response.Clear();
                Response.Write("Genero error " + err.Message + err.StackTrace);

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

        private void ConfigHeader(OS_ConsultarInscrito os)
        {
            String rootPath = Server.MapPath("~");

            
            Paragraph paragraph = section.Headers.Primary.AddParagraph();
            var image = paragraph.AddImage(rootPath + "Img/pdfheader2018-1.png");
            image.Height = XUnit.FromCentimeter(3).Point;
            image.Width = XUnit.FromCentimeter(25).Point;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddLineBreak();

            paragraph = section.Headers.Primary.AddParagraph("El Programa Jóvenes en Acción de Prosperidad Social en alianza con la Organización");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("de Estados Iberoamericanos y la Universidad de Santander.");
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

            paragraph = section.Headers.Primary.AddParagraph(os.Inscrito.NomTipoDocumento + " No. "  + os.Inscrito.Documento );
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddLineBreak();

            paragraph = section.Headers.Primary.AddParagraph("");

            paragraph = section.Headers.Primary.AddParagraph("Asistió y participó activamente en el Módulo Presencial – “Construyendo Mi Camino”, como proceso de formación de veintiséis (26) horas,");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("desarrollando ocho (8) talleres bajo la metodología de aprendizaje experiencial, en los cuales se fortalecieron las siguientes Habilidades");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("para la Vida: LIDERAZGO, AUTOCONOCIMIENTO, RESILIENCIA, EMPATÍA, TRABAJO EN EQUIPO, GESTIÓN DE CONFLICTOS, ");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("TOMA DE DECISIONES Y COMUNICACIÓN ASERTIVA, contribuyendo así a su bienestar personal, familiar, social y productivo.");
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
                + " en el mes de "  + meses[Int32.Parse(fcer[1])-1] 
                + " de " + fcer[0]
            );
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Italic = false;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            var image2 = paragraph.AddImage(rootPath + "Img/firmaCertificado2018-1.png");
            image2.Height = XUnit.FromCentimeter(5).Point;
            image2.Width = XUnit.FromCentimeter(25).Point;

            

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