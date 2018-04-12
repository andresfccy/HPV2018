using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmUsuario.Entidad
{
    class AutenticarEntidad : EntidadOracle
    {
        public AutenticarEntidad(string connectionStringName) : base(connectionStringName)
        {

        }

        public AutenticarEntidad()
        {

        }
        
        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            return null;
        }
    }
}
