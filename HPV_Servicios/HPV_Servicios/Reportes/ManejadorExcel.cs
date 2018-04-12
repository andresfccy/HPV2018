using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;

using System.IO;


namespace HPV_Servicios.Reportes
{
    public class ManejadorExcel
    {

        private ExcelPackage package = null;

        public ManejadorExcel()
        {
        }

        public void Abrir(String archivoPlantilla)
        {


            FileInfo plantilla = new FileInfo(archivoPlantilla);

            package = new ExcelPackage(plantilla);
        }

        public void Escribir(string hoja, string celda, Object valor)
        {

            ExcelWorkbook workBook = package.Workbook;

            if (workBook != null)
            {

                if (workBook.Worksheets.Count > 0)
                {

                    ExcelWorksheet worksheet = workBook.Worksheets[hoja];
                    worksheet.Cells[celda].Value = valor;
                }
            }

        }

        public void Escribir(string hoja, int row, int col, Object valor)
        {

            ExcelWorksheet worksheet = package.Workbook.Worksheets[hoja];
            worksheet.Cells[row, col].Value = valor;

        }

        public void SalvarComo(System.IO.Stream salida)
        {
            package.SaveAs(salida);
        }

        public void SalvarComo(String archivoSalida)
        {
            FileInfo salida = new FileInfo(archivoSalida);

            if (salida.Exists)
                salida.Delete();

            package.SaveAs(salida);
        }

        public void Cerrar()
        {
            package.Dispose();
        }

    }
}