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
    public class EncuestaSatisfaccionObservacionesEntidad : EntidadOracle
    {
        public EncuestaSatisfaccionObservaciones EncuestaSatisfaccionObservaciones { get; set; }
        public EncuestaSatisfaccionObservacionesEntidad(string connectionStringName) : base(connectionStringName)
        {
            EncuestaSatisfaccionObservaciones = new EncuestaSatisfaccionObservaciones();
        }

        public EncuestaSatisfaccionObservacionesEntidad()
        {
            EncuestaSatisfaccionObservaciones = new EncuestaSatisfaccionObservaciones();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            EncuestaSatisfaccionObservacionesEntidad entidad = new EncuestaSatisfaccionObservacionesEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.EncuestaSatisfaccionObservaciones.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.EncuestaSatisfaccionObservaciones.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en EncuestaSatisfaccionObservaciones" + e.Message);
            }
            return entidad;
        }

    }
}
