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
    public class CoordinadorEntidad : EntidadOracle
    {
        public Coordinador Coordinador { get; set; }


        public CoordinadorEntidad(string connectionStringName) : base(connectionStringName)
        {
            Coordinador = new Coordinador();
        }

        public CoordinadorEntidad()
        {
            Coordinador = new Coordinador();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            CoordinadorEntidad entidad = new CoordinadorEntidad();
            entidad.Coordinador.IdCoordinador = Int64.Parse(row["a06codigo"].ToString());
            entidad.Coordinador.Nombre = row["nombre"].ToString();

            return entidad;
        }

    }
}
