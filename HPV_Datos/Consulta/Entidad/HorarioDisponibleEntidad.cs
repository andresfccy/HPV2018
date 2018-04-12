using HPV_Datos.General.Entidad;
using HPV_Entidades.Consulta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Consulta.Entidad
{
    public class HorarioDisponibleEntidad : EntidadOracle
    {
        public HorarioDisponible HorarioDisponible { get; set; }

        public HorarioDisponibleEntidad(string connectionStringName) : base(connectionStringName)
        {
            HorarioDisponible = new HorarioDisponible();
        }

        public HorarioDisponibleEntidad()
        {
            HorarioDisponible = new HorarioDisponible();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            HorarioDisponibleEntidad entidad = new HorarioDisponibleEntidad();

            entidad.HorarioDisponible.NombreFacilitador = row["NombreFacilitador"].ToString();
            entidad.HorarioDisponible.NomDepartamento = row["NomDepartamento"].ToString();
            entidad.HorarioDisponible.NomMunicipio = row["NomMunicipio"].ToString();
            entidad.HorarioDisponible.Dia = row["Dia"].ToString();
            entidad.HorarioDisponible.Horario = row["Horario"].ToString();
            entidad.HorarioDisponible.SiglaGrupo = row["SiglaGrupo"].ToString();
            entidad.HorarioDisponible.Lugar = row["Lugar"].ToString();
            entidad.HorarioDisponible.Direccion = row["Direccion"].ToString();
            entidad.HorarioDisponible.CupoDisponible = Int64.Parse(row["CupoDisponible"].ToString());
            entidad.HorarioDisponible.IdGrupoFacilitador = Int64.Parse(row["IdGrupoFacilitador"].ToString());

            return entidad;
        }
    }
}
