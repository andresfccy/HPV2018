using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;


namespace HPV_Servicios.Reportes
{
    public class ManejadorExcelOffice
    {
        private Excel.Workbook MyBook = null;
        private Excel.Application MyApp = null;
        private Excel.Worksheet MySheet = null;

        public void Abrir(String archivoPlantilla)
        {

            MyApp = new Excel.Application();
            MyApp.Visible = false;

            String e = archivoPlantilla.Replace("/", "\\");
            MyBook = MyApp.Workbooks.Open(e);
      
        }

        public void Escribir(string hoja, string celda, Object valor)
        {

            Excel.Worksheet MySheet = MyBook.Sheets[1];
            Excel.Range sampleCell = MySheet.get_Range(celda);
            sampleCell.Value = valor;
      
        }

        public void Escribir(string hoja, int row, int col, Object valor)
        {
            Excel.Worksheet MySheet = MyBook.Sheets[1];
            Excel.Range sampleCell = MySheet.Cells[row,col];
            sampleCell.Value = valor;

        }

        public void SalvarComo(System.IO.Stream salida)
        {
            //     sampleWorkbook.SaveAs("Output.xlsx");
            //MyBook.Close();
            //MyApp.Quit();

        }

        public void SalvarComo(String archivoSalida)
        {
            FileInfo salida = new FileInfo(archivoSalida);

            if (salida.Exists)
                salida.Delete();

            //MyBook.SaveAs(@archivoSalida);
            object misValue = System.Reflection.Missing.Value;

            String s = archivoSalida.Replace("/", "\\");
       
            MyBook.SaveAs(s, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
    misValue, false, false, Excel.XlSaveAsAccessMode.xlShared,
    Excel.XlSaveConflictResolution.xlUserResolution, true,
    misValue, misValue, misValue);

          //  MyBook.Close(true, misValue, misValue);
          //  MyApp.Quit();

        }

        public void Cerrar()
        {
            //MyBook.Close();

            /* if (MyBook!= null)
              {
                  MyBook.Close();
                  Marshal.ReleaseComObject(MyBook);
              }


              //MyApp.Quit();*/
            //Marshal.ReleaseComObject(MyApp);
        }

        private Excel.Worksheet darHoja (string hoja)
        {
            int numSheets = MyBook.Sheets.Count;

            //
            // Iterate through the sheets. They are indexed starting at 1.
            //
            for (int sheetNum = 1; sheetNum < numSheets + 1; sheetNum++)
            {
                Excel.Worksheet sheet = (Excel.Worksheet)MyBook.Sheets[sheetNum];
                if (sheet.Name.Equals(hoja))
                    return sheet;
            }

            return null;
        }

    }
}