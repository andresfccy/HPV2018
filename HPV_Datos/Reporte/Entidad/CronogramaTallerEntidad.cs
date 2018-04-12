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
    public class CronogramaTallerEntidad : EntidadOracle
    {
        public CronogramaTaller CronogramaTaller { get; set; }
        public CronogramaTallerEntidad(string connectionStringName) : base(connectionStringName)
        {
            CronogramaTaller = new CronogramaTaller();
        }

        public CronogramaTallerEntidad()
        {
            CronogramaTaller = new CronogramaTaller();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            

            if (row == null)
                return null;

            CronogramaTallerEntidad entidad = new CronogramaTallerEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.CronogramaTaller.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.CronogramaTaller.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en CronogramaTaller" + e.Message);
            }
            return entidad;
        }

    }
}
