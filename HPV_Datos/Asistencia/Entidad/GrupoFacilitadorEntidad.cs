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
    public class GrupoFacilitadorEntidad : EntidadOracle
    {
        public GrupoFacilitador GrupoFacilitador { get; set; }


        public GrupoFacilitadorEntidad(string connectionStringName) : base(connectionStringName)
        {
            GrupoFacilitador = new GrupoFacilitador();
        }

        public GrupoFacilitadorEntidad()
        {
            GrupoFacilitador = new GrupoFacilitador();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            GrupoFacilitadorEntidad entidad = new GrupoFacilitadorEntidad();
            entidad.GrupoFacilitador.IdDia = Int64.Parse(row["iddia"].ToString());
            entidad.GrupoFacilitador.IdGrupo = Int64.Parse(row["idgrupo"].ToString());
            entidad.GrupoFacilitador.IdHorario = Int64.Parse(row["idhorario"].ToString());
            entidad.GrupoFacilitador.Nombre = row["nombre"].ToString();
            entidad.GrupoFacilitador.Sigla = row["sigla"].ToString();
            entidad.GrupoFacilitador.NomDia = row["nomDia"].ToString();
            entidad.GrupoFacilitador.NomHorario = row["nomHorario"].ToString();
            entidad.GrupoFacilitador.IdGrupoFacilitador = Int64.Parse(row["idGrupoFacilitador"].ToString());

            return entidad;
        }

    }
}
