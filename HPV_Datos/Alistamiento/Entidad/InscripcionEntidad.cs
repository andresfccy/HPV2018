using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Alistamiento.Entidad
{
    public class InscripcionEntidad : EntidadOracle
    {
        public InscripcionEntidad(string connectionStringName) : base(connectionStringName)
        {
        }

        public InscripcionEntidad()
        {
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            return null;
        }

    }
}
