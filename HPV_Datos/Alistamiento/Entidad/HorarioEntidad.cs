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
    public class HorarioEntidad : EntidadOracle
    {
        public Horario Horario { get; set; }

        public HorarioEntidad()
        {
            Horario = new Horario();
        }


        public HorarioEntidad(string connectionStringName) : base(connectionStringName)
        {
            Horario = new Horario();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            HorarioEntidad entidad = new HorarioEntidad();
            entidad.Horario.Codigo = Int64.Parse(row["a24codigo"].ToString());
            entidad.Horario.Nombre = row["a24horario"].ToString();

            return entidad;
        }

    }
}
