using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmEspacioFisico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmEspacioFisico.Entidad
{
    public class GrupoEntidad : EntidadOracle
    {
        public Grupo Grupo { get; set; }

        public GrupoEntidad(string connectionStringName) : base(connectionStringName)
        {
            Grupo = new Grupo();
        }

        public GrupoEntidad()
        {
            Grupo = new Grupo();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            GrupoEntidad entidad = new GrupoEntidad();
            entidad.Grupo.IdGrupo  = Int64.Parse(row["A25CODIGO"].ToString());
            entidad.Grupo.Nombre = row["A25NOMBRE"].ToString();

            return entidad;

        }
    }
}
