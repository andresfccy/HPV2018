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
    public class EspaciosFisicosEntidad : EntidadOracle
    {
        public EspaciosFisicos EspaciosFisicos { get; set; }
        public EspaciosFisicosEntidad(string connectionStringName) : base(connectionStringName)
        {
            EspaciosFisicos = new EspaciosFisicos();
        }

        public EspaciosFisicosEntidad()
        {
            EspaciosFisicos = new EspaciosFisicos();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            EspaciosFisicosEntidad entidad = new EspaciosFisicosEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach(var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.EspaciosFisicos.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.EspaciosFisicos.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en espacio físico " + e.Message);
            }
            return entidad;
        }

    }
}
