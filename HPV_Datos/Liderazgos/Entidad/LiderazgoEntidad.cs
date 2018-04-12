using HPV_Datos.General.Entidad;
using HPV_Entidades.Liderazgos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Liderazgos.Entidad
{
    public class LiderazgoEntidad : EntidadOracle
    {
        public Liderazgo Liderazgo { get; set; }

        public LiderazgoEntidad(string connectionStringName) : base(connectionStringName)
        {
            Liderazgo = new Liderazgo();
        }

        public LiderazgoEntidad()
        {
            Liderazgo = new Liderazgo();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            LiderazgoEntidad entidad = new LiderazgoEntidad();

            entidad.Liderazgo.IdLiderazgo = Int64.Parse(row["IdLiderazgo"].ToString());
            entidad.Liderazgo.IdPeriodo = Int64.Parse(row["IdPeriodo"].ToString());
            entidad.Liderazgo.IdGrupoFacilitador = Int64.Parse(row["IdGrupoFacilitador"].ToString());
            entidad.Liderazgo.SiglaGrupo = row["SiglaGrupo"].ToString();
            entidad.Liderazgo.NomGrupo = row["NomGrupo"].ToString();

            entidad.Liderazgo.IdFacilitador = Int64.Parse(row["IdFacilitador"].ToString());
            entidad.Liderazgo.NomFacilitador = row["NomFacilitador"].ToString();
            entidad.Liderazgo.IdCoordinador = Int64.Parse(row["IdCoordinador"].ToString());
            entidad.Liderazgo.NomCoordinador = row["NomCoordinador"].ToString();

            entidad.Liderazgo.IdMunicipio = Int64.Parse(row["IdMunicipio"].ToString());
            entidad.Liderazgo.NomMunicipio = row["NomMunicipio"].ToString();

            entidad.Liderazgo.IdDepartamento = Int64.Parse(row["IdDepartamento"].ToString());
            entidad.Liderazgo.NomDepartamento = row["NomDepartamento"].ToString();

            entidad.Liderazgo.IdEstado = row["IdEstado"].ToString();
            entidad.Liderazgo.NomEstado = row["NomEstado"].ToString();

            entidad.Liderazgo.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
            entidad.Liderazgo.NomInscrito = row["NomInscrito"].ToString();

            entidad.Liderazgo.Criterios = row["Criterios"].ToString();
            entidad.Liderazgo.MotivoRechazo = row["MotivoRechazo"].ToString();

            return entidad;

        }
    }
}
