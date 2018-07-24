using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Sistematizacion.Entidad
{
    public class SistematizacionEntidad : EntidadOracle
    {
        public HPV_Entidades.Sistematizacion.Sistematizacion Sistematizacion { get; set; }

        public SistematizacionEntidad(string connectionStringName) : base(connectionStringName)
        {
            Sistematizacion = new HPV_Entidades.Sistematizacion.Sistematizacion();
        }

        public SistematizacionEntidad()
        {
            Sistematizacion = new HPV_Entidades.Sistematizacion.Sistematizacion();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            SistematizacionEntidad entidad = new SistematizacionEntidad();

            entidad.Sistematizacion.Id = Int64.Parse(row["IdSistematizacion"].ToString());
            entidad.Sistematizacion.IdPeriodo = Int64.Parse(row["IdPeriodo"].ToString());
            entidad.Sistematizacion.IdGrupoFacilitador = Int64.Parse(row["IdGrupoFacilitador"].ToString());
            entidad.Sistematizacion.SiglaGrupo = row["SiglaGrupo"].ToString();
            entidad.Sistematizacion.NomGrupo = row["NomGrupo"].ToString();
            entidad.Sistematizacion.IdFacilitador = Int64.Parse(row["IdFacilitador"].ToString());
            entidad.Sistematizacion.NomFacilitador = row["NomFacilitador"].ToString();
            entidad.Sistematizacion.IdCoordinador = Int64.Parse(row["IdCoordinador"].ToString());
            entidad.Sistematizacion.NomCoordinador = row["NomCoordinador"].ToString();
            entidad.Sistematizacion.IdMunicipio = Int64.Parse(row["IdMunicipio"].ToString());
            entidad.Sistematizacion.NomMunicipio = row["NomMunicipio"].ToString();
            entidad.Sistematizacion.IdDepartamento = Int64.Parse(row["IdDepartamento"].ToString());
            entidad.Sistematizacion.NomDepartamento = row["NomDepartamento"].ToString();
            entidad.Sistematizacion.IdEstado = row["IdEstado"].ToString();
            entidad.Sistematizacion.NomEstado = row["NomEstado"].ToString();
            entidad.Sistematizacion.LinkVideo = row["LinkVideo"].ToString();
            entidad.Sistematizacion.IdInstrumento = Int64.Parse(row["IdInstrumento"].ToString());
            entidad.Sistematizacion.NomInstrumento = row["NomInstrumento"].ToString();

            return entidad;
        }
    }
}
