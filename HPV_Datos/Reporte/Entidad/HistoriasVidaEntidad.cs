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
    public class HistoriasVidaEntidad : EntidadOracle
    {
        public HistoriasVida HistoriasVida { get; set; }
        public HistoriasVidaEntidad(string connectionStringName) : base(connectionStringName)
        {
            HistoriasVida = new HistoriasVida();
        }

        public HistoriasVidaEntidad()
        {
            HistoriasVida = new HistoriasVida();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            
            if (row == null)
                return null;

            HistoriasVidaEntidad entidad = new HistoriasVidaEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.HistoriasVida.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.HistoriasVida.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en HistoriasVida" + e.Message);
            }
            return entidad;
        }

    }
}
