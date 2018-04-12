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
    public class EncuestaSatisfaccionEntidad : EntidadOracle
    {
        public EncuestaSatisfaccion EncuestaSatisfaccion { get; set; }
        public EncuestaSatisfaccionEntidad(string connectionStringName) : base(connectionStringName)
        {
            EncuestaSatisfaccion = new EncuestaSatisfaccion();
        }

        public EncuestaSatisfaccionEntidad()
        {
            EncuestaSatisfaccion = new EncuestaSatisfaccion();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            
            if (row == null)
                return null;

            EncuestaSatisfaccionEntidad entidad = new EncuestaSatisfaccionEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.EncuestaSatisfaccion.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.EncuestaSatisfaccion.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en EncuestaSatisfaccion" + e.Message);
            }
            return entidad;
        }

    }
}
