using HPV_Datos.General.Entidad;
using HPV_Entidades.Asistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Asistencia.Entidad
{
    public class FacilitadorEntidad : EntidadOracle
    {
        public Facilitador Facilitador { get; set; }


        public FacilitadorEntidad(string connectionStringName) : base(connectionStringName)
        {
            Facilitador = new Facilitador();
        }

        public FacilitadorEntidad()
        {
            Facilitador = new Facilitador();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            FacilitadorEntidad entidad = new FacilitadorEntidad();
            entidad.Facilitador.IdFacilitador = Int64.Parse(row["idFacilitador"].ToString());
            entidad.Facilitador.Nombre = row["nomFacilitador"].ToString();

            return entidad;
        }

    }
}
