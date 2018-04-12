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
    public class EjemplosLiderazgoEntidad : EntidadOracle
    {
        public EjemplosLiderazgo EjemplosLiderazgo { get; set; }
        public EjemplosLiderazgoEntidad(string connectionStringName) : base(connectionStringName)
        {
            EjemplosLiderazgo = new EjemplosLiderazgo();
        }

        public EjemplosLiderazgoEntidad()
        {
            EjemplosLiderazgo = new EjemplosLiderazgo();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            

            if (row == null)
                return null;

            EjemplosLiderazgoEntidad entidad = new EjemplosLiderazgoEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.EjemplosLiderazgo.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.EjemplosLiderazgo.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en EjemplosLiderazgo" + e.Message);
            }
            return entidad;
        }

    }
}
