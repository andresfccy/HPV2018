using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmUsuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmUsuario.Entidad
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

            entidad.Rol.IdRol=  Int64.Parse(row["a05codigo"].ToString());
            entidad.Rol.Nombre = row["a05nombre"].ToString();
            entidad.Rol.CodEstado = row["a05estado"].ToString();
            entidad.Rol.Estado=  row["a91nombre"].ToString();

            return entidad;
        }
    }
}
