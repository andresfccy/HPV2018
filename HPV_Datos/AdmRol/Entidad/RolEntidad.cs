using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmRol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmRol.Entidad
{
    public class RolEntidad : EntidadOracle
    {
        public Rol Rol { get; set; }

        public RolEntidad(string connectionStringName) : base(connectionStringName)
        {
            Rol = new Rol();
        }

        public RolEntidad()
        {
            Rol = new Rol();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            RolEntidad entidad = new RolEntidad();

            entidad.Rol.Codigo =  Int64.Parse(row["Codigo"].ToString());
            entidad.Rol.Nombre = row["Nombre"].ToString();
            entidad.Rol.Estado =  row["Estado"].ToString();
            entidad.Rol.Opciones = row["Opciones"].ToString();

            return entidad;
        }
    }
}
