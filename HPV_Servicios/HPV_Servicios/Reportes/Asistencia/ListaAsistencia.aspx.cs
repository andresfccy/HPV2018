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

namespace HPV_Servicios.Reportes.Asistencia
{
    public partial class ListaAsistencia : System.Web.UI.Page
    {
        Document document = null;
        bool nivelacion = false;
        bool contingencia = false;

        MigraDoc.DocumentObjectModel.Unit width, height;
        MigraDoc.DocumentObjectModel.Unit ancho, alto;

        Section section = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var Contents = new byte[Request.InputStream.Length];
                Request.InputStream.Read(Contents, 0, (int)Request.InputStream.Length);
                var str = System.Text.Encoding.Default.GetString(Contents);


                int posIni = str.IndexOf("IdFacilitador");
                int posFin = str.IndexOf(",");

                if (posIni < 0)
                {
                    return;
                }


                String strIdFacilitador = str.Substring(posIni + 15, posFin - posIni - 15);

                posIni = str.IndexOf("IdGrupo", posIni);
                posFin = str.IndexOf(",", posIni);

                String strIdGrupo = str.Substring(posIni + 9, posFin - posIni - 9);

                if (strIdGrupo.Contains("11"))
                {
                    nivelacion = true;
                }
                else if (strIdGrupo.Contains("12"))
                {
                    contingencia = true;
                }

                posIni = str.IndexOf("IdPeriodo", posIni);
                posFin = str.IndexOf(",", posIni);

                String strIdPeriodo = str.Substring(posIni + 11, posFin - posIni - 11);


                posIni = str.IndexOf("Taller", posIni);
                String talleres = "";

                posFin = str.IndexOf("}", posIni);

                if ((posFin - 1 - posIni - 9) > 0)
                    talleres = str.Substring(posIni + 9, posFin - 1 - posIni - 9);
                else
                    talleres = str.Substring(posIni + 8, posFin - posIni - 8);



                // Obtener talleres a generar
                String[] tallerArray = talleres.Split(',');

                // Para cada taller generar listado
                InitDocument();

                foreach (String strIdTaller in tallerArray)
                {

                    var oe = new OE_DarAsistenciaEncabezado();
                    oe.IdFacilitador = Int64.Parse(strIdFacilitador);
                    oe.IdGrupo = Int64.Parse(strIdGrupo);

                    oe.IdTaller = Int64.Parse(strIdTaller);
                    oe.IdPeriodo = Int64.Parse(strIdPeriodo);

                    var os = new FachadaAsistencia().DarAsistenciaEncabezado(oe);

                    if (os.Respuesta.Codigo == 0)
                    {
                        CreateSection();
                        ConfigHeader(os);
                        ConfigFooter();

                        FillTableHeader(os);

                        var table = ConfigTableDetail();

                        var oeDetalle = new OE_DarAsistenciaDetalle();
                        oeDetalle.IdFacilitador = Int64.Parse(strIdFacilitador);
                        oeDetalle.IdGrupo = Int64.Parse(strIdGrupo);
                        oeDetalle.IdPeriodo = Int64.Parse(strIdPeriodo);

                        var osDetalle = new FachadaAsistencia().DarAsistenciaDetalle(oeDetalle);


                        if (osDetalle.Respuesta.Codigo == 0)
                        {
                            foreach (AsistenciaDetalle asistencia in osDetalle.ListaAsistenciaDetalle)
                            {
                                FillTableDetail(table, asistencia);
                            }


                        }

                        if (os.AsistenciaEncabezado.NomGrupo.ToUpper().Contains("NIVEL"))
                            FillEmpyDetail(table, true);
                        else
                            FillEmpyDetail(table, false);
                        CreateObservation();


                    }

                }


                // Crear un renderer
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

                // Asociar el documento MigraDoc con el renderer
                pdfRenderer.Document = document;

                // Capas y el documento renderizado a PDF
                pdfRenderer.RenderDocument();

                // Enviar el PDF al browser
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
                Response.Write("Genero error " + err.Message);

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

            ancho = width - XUnit.FromCentimeter(2).Point;
            alto = height - XUnit.FromCentimeter(6).Point;

            section.PageSetup.LeftMargin = XUnit.FromCentimeter(1).Point;
            section.PageSetup.RightMargin = XUnit.FromCentimeter(1).Point;
            section.PageSetup.TopMargin = XUnit.FromCentimeter(4).Point;
            section.PageSetup.BottomMargin = XUnit.FromCentimeter(2).Point;
        }

        private void ConfigHeader(OS_DarAsistenciaEncabezado os)
        {
            String rootPath = Server.MapPath("~");


            Paragraph paragraph = section.Headers.Primary.AddParagraph();
            var image = paragraph.AddImage(rootPath + "Img/pdfheader.png");
            image.Height = XUnit.FromCentimeter(1).Point;
            image.Width = XUnit.FromCentimeter(20).Point;

            paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddLineBreak();

            paragraph = section.Headers.Primary.AddParagraph("MÓDULO PRESENCIAL DE HABILIDADES PARA LA VIDA/COMPETENCIAS TRANSVERSALES - CONTRATO 503");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Left;

            MigraDoc.DocumentObjectModel.Tables.Table table = section.Headers.Primary.AddTable();

            table.Borders.Width = 0;
            var column = table.AddColumn(ancho * 0.70);
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(ancho * 0.30);
            column.Format.Alignment = ParagraphAlignment.Right;

            Row row = table.AddRow();
            Cell cell = row.Cells[0];
            paragraph = cell.AddParagraph("LISTADO DE ASISTENCIA A TALLERES");
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            cell = row.Cells[1];

            if (os.AsistenciaEncabezado.NomGrupo.ToUpper().Contains("NIVEL") || nivelacion || contingencia)
            {
                if (nivelacion)
                    paragraph = cell.AddParagraph(os.AsistenciaEncabezado.SiglaGrupo + " - NIVELACIÓN");
                if (contingencia)
                    paragraph = cell.AddParagraph(os.AsistenciaEncabezado.SiglaGrupo + " - CONTINGENCIA");
            }
            else
                paragraph = cell.AddParagraph(os.AsistenciaEncabezado.SiglaGrupo);
            paragraph.Format.Font.Name = "helvetica";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

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
            if (os.AsistenciaEncabezado.NomGrupo.ToUpper().Contains("NIVEL") || nivelacion || contingencia)
            {
                cell.Borders.Left.Width = 1;
                cell.Borders.Right.Width = 1;
                cell.Borders.Top.Width = 1;
                cell.Borders.Bottom.Width = 1;
                paragraph = cell.AddParagraph("");
            }
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
            if (nivelacion || contingencia)
            {
                cell.Borders.Left.Width = 1;
                cell.Borders.Right.Width = 1;
                cell.Borders.Top.Width = 1;
                cell.Borders.Bottom.Width = 1;
                paragraph = cell.AddParagraph("");
            }
            else
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
            if (nivelacion || contingencia)
            {
                cell.Borders.Left.Width = 1;
                cell.Borders.Right.Width = 1;
                cell.Borders.Top.Width = 1;
                cell.Borders.Bottom.Width = 1;
                paragraph = cell.AddParagraph("");
            }
            else
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
            row.TopPadding = XUnit.FromCentimeter(2).Point;

            cell = row.Cells[0];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("");

            cell = row.Cells[1];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            paragraph = cell.AddParagraph("");

            cell = row.Cells[2];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("");

            cell = row.Cells[3];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            paragraph = cell.AddParagraph("");

            cell = row.Cells[4];
            cell.Borders.Top.Clear();
            cell.Borders.Left.Clear();
            cell.Borders.Right.Clear();
            cell.Borders.Bottom.Clear();
            paragraph = cell.AddParagraph("");

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