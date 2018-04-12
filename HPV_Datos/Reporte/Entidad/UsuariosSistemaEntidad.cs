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
    public class UsuariosSistemaEntidad : EntidadOracle
    {
        public UsuariosSistema UsuariosSistema { get; set; }
        public UsuariosSistemaEntidad(string connectionStringName) : base(connectionStringName)
        {
            UsuariosSistema = new UsuariosSistema();
        }

        public UsuariosSistemaEntidad()
        {
            UsuariosSistema = new UsuariosSistema();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
           
            if (row == null)
                return null;

            UsuariosSistemaEntidad entidad = new UsuariosSistemaEntidad();
            try
            {
                //Se asignan los atributos según las filas en la variable row
                foreach (var att in row.ItemArray)
                {
                    String test = att.ToString();
                    entidad.UsuariosSistema.Atributos.Add(att.ToString());
                }

                foreach (var r in row.Table.Columns)
                {
                    entidad.UsuariosSistema.NomAtributos.Add(r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en UsuariosSistema" + e.Message);
            }
            return entidad;
        }

    }
}
