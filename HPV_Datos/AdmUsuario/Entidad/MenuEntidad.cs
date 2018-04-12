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
    public class MenuEntidad : EntidadOracle
    {
        public Menu Menu { get; set; }


        public MenuEntidad(string connectionStringName) : base(connectionStringName)
        {
            Menu = new Menu();
        }

        public MenuEntidad()
        {
            Menu = new Menu();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            MenuEntidad entidad = new MenuEntidad();

            entidad.Menu.IdMenu = Int64.Parse(row["A09CODIGO"].ToString());
            entidad.Menu.Nombre = row["A09NOMBRE"].ToString();
            entidad.Menu.Tag = row["A09TAG"].ToString();
            entidad.Menu.Orden = Int32.Parse(row["A09ORDEN"].ToString());

            return entidad;
        }

    }
}
