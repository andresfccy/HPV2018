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
    public class TallerEntidad : EntidadOracle
    {
        public Taller Taller { get; set; }


        public TallerEntidad(string connectionStringName) : base(connectionStringName)
        {
            Taller = new Taller();
        }

        public TallerEntidad()
        {
            Taller = new Taller();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            TallerEntidad entidad = new TallerEntidad();
            entidad.Taller.Descripcion = row["descripcion"].ToString();
            entidad.Taller.IdTaller = Int64.Parse(row["idTaller"].ToString());
            entidad.Taller.Nombre = row["nombre"].ToString();
            entidad.Taller.Orden = Int64.Parse(row["orden"].ToString());

            return entidad;
        }
    }
}
