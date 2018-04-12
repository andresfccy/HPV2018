using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HPV_Datos.Alistamiento.Entidad
{
    public class CorreoEntidad : EntidadOracle
    {
        public CorreoEntidad(string connectionStringName) : base(connectionStringName)
        {
        }

        public CorreoEntidad()
        {

        }
        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
