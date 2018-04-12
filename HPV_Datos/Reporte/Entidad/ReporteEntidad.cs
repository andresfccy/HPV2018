using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Reporte.Entidad
{
    public class ReporteEntidad : EntidadOracle
    {
        public HPV_Entidades.Reporte.Reporte Reporte { get; set; }

        public ReporteEntidad(string connectionStringName) : base(connectionStringName)
        {
            Reporte = new HPV_Entidades.Reporte.Reporte();
        }

        public ReporteEntidad()
        {
            Reporte = new HPV_Entidades.Reporte.Reporte();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            ReporteEntidad entidad = new ReporteEntidad();
            try
            {
                entidad.Reporte.IdReporte = Int64.Parse(row["IdReporte"].ToString());
                entidad.Reporte.Categoria = row["Categoria"].ToString();
                entidad.Reporte.Nombre = row["Nombre"].ToString();
                entidad.Reporte.Descripcion = row["Descripcion"].ToString();
                entidad.Reporte.URL = row["URL"].ToString();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en " + entidad.Reporte.IdReporte + " " + e.Message);
            }
            return entidad;
        }
    }
}
