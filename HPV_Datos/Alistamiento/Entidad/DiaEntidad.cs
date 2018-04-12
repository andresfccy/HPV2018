using HPV_Datos.General.Entidad;
using HPV_Entidades.Alistamiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Alistamiento.Entidad
{
    public class DiaEntidad : EntidadOracle
    {
        public Dia Dia { get; set; }

        public DiaEntidad()
        {
            Dia = new Dia();
        }


        public DiaEntidad(string connectionStringName) : base(connectionStringName)
        {
            Dia = new Dia();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            DiaEntidad entidad = new DiaEntidad();
            entidad.Dia.Codigo = Int64.Parse(row["A23CODIGO"].ToString());
            entidad.Dia.Nombre = row["A23NOMBRE"].ToString();

            return entidad;
        }

    }
}
