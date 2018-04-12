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
    public class EncuestaEntradaSalidaEntidad : EntidadOracle
    {
        public EncuestaEntradaSalida EncuestaEntradaSalida { get; set; }
        public EncuestaEntradaSalidaEntidad(string connectionStringName) : base(connectionStringName)
        {
            EncuestaEntradaSalida = new EncuestaEntradaSalida();
        }

        public EncuestaEntradaSalidaEntidad()
        {
            EncuestaEntradaSalida = new EncuestaEntradaSalida();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            

            if (row == null)
                return null;

            EncuestaEntradaSalidaEntidad entidad = new EncuestaEntradaSalidaEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.EncuestaEntradaSalida.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.EncuestaEntradaSalida.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en EncuestaEntradaSalida" + e.Message);
            }
            return entidad;
        }

    }
}
