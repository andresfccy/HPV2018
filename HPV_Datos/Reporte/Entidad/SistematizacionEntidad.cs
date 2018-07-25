using HPV_Datos.General.Entidad;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Reporte.Entidad
{
    public class SistematizacionEntidad : EntidadOracle
    {
        public HPV_Entidades.Reporte.Sistematizacion Sistematizaciones { get; set; }
        public SistematizacionEntidad(string connectionStringName) : base(connectionStringName)
        {
            Sistematizaciones = new HPV_Entidades.Reporte.Sistematizacion();
        }

        public SistematizacionEntidad()
        {
            Sistematizaciones = new HPV_Entidades.Reporte.Sistematizacion();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            
            if (row == null)
                return null;

            SistematizacionEntidad entidad = new SistematizacionEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.Sistematizaciones.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.Sistematizaciones.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en Sistematización" + e.Message);
            }
            return entidad;
        }

    }
}
