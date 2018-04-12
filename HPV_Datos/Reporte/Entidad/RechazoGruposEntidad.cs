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
    public class RechazoGruposEntidad : EntidadOracle
    {
        public RechazoGrupos RechazoGrupos { get; set; }

        public RechazoGruposEntidad(string connectionStringName) : base(connectionStringName)
        {
            RechazoGrupos = new RechazoGrupos();
        }

        public RechazoGruposEntidad()
        {
            RechazoGrupos = new RechazoGrupos();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            RechazoGruposEntidad entidad = new RechazoGruposEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.RechazoGrupos.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.RechazoGrupos.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en RechazoGrupos" + e.Message);
            }
            return entidad;
        }

    }
}
