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
    public class TrazabilidadEntidad : EntidadOracle
    {
        public Trazabilidad Trazabilidad { get; set; }
        public TrazabilidadEntidad(string connectionStringName) : base(connectionStringName)
        {
            Trazabilidad = new Trazabilidad();
        }

        public TrazabilidadEntidad()
        {
            Trazabilidad = new Trazabilidad();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
           
            if (row == null)
                return null;

            TrazabilidadEntidad entidad = new TrazabilidadEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.Trazabilidad.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.Trazabilidad.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en Trazabilidad" + e.Message);
            }
            return entidad;
        }

    }
}
