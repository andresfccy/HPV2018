using HPV_Datos.General.Entidad;
using HPV_Entidades.CasosDeExito;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.CasosDeExito.Entidad
{
    public class CasoDeExitoEntidad : EntidadOracle
    {
        public CasoDeExito CasoDeExito { get; set; }

        public CasoDeExitoEntidad(string connectionStringName) : base(connectionStringName)
        {
            CasoDeExito = new CasoDeExito();
        }

        public CasoDeExitoEntidad()
        {
            CasoDeExito = new CasoDeExito();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            CasoDeExitoEntidad entidad = new CasoDeExitoEntidad();

            entidad.CasoDeExito.IdCasoDeExito = Int64.Parse(row["IdCasoDeExito"].ToString());
            entidad.CasoDeExito.IdPeriodo = Int64.Parse(row["IdPeriodo"].ToString());
            entidad.CasoDeExito.IdGrupoFacilitador = Int64.Parse(row["IdGrupoFacilitador"].ToString());
            entidad.CasoDeExito.SiglaGrupo = row["SiglaGrupo"].ToString();
            entidad.CasoDeExito.NomGrupo = row["NomGrupo"].ToString();

            entidad.CasoDeExito.IdFacilitador = Int64.Parse(row["IdFacilitador"].ToString());
            entidad.CasoDeExito.NomFacilitador = row["NomFacilitador"].ToString();
            entidad.CasoDeExito.IdCoordinador = Int64.Parse(row["IdCoordinador"].ToString());
            entidad.CasoDeExito.NomCoordinador = row["NomCoordinador"].ToString();

            entidad.CasoDeExito.IdMunicipio = Int64.Parse(row["IdMunicipio"].ToString());
            entidad.CasoDeExito.NomMunicipio = row["NomMunicipio"].ToString();

            entidad.CasoDeExito.IdDepartamento = Int64.Parse(row["IdDepartamento"].ToString());
            entidad.CasoDeExito.NomDepartamento = row["NomDepartamento"].ToString();

            entidad.CasoDeExito.IdEstado = row["IdEstado"].ToString();
            entidad.CasoDeExito.NomEstado = row["NomEstado"].ToString();

            entidad.CasoDeExito.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
            entidad.CasoDeExito.NomInscrito = row["NomInscrito"].ToString();

            entidad.CasoDeExito.IdLogro = Int64.Parse(row["IdLogro"].ToString());
            entidad.CasoDeExito.NomLogro = row["NomLogro"].ToString();

            entidad.CasoDeExito.Criterios = row["Criterios"].ToString();
            entidad.CasoDeExito.NomVerbo = row["NomVerbo"].ToString();
            entidad.CasoDeExito.NomAdjetivo = row["NomAdjetivo"].ToString();
            entidad.CasoDeExito.NomMedio = row["NomMedio"].ToString();
            entidad.CasoDeExito.Observaciones = row["Observaciones"].ToString();
            entidad.CasoDeExito.MotivoRechazo = row["MotivoRechazo"].ToString();

            entidad.CasoDeExito.Logros = row["Logros"] == null ? "" : row["Logros"].ToString();
            return entidad;
        }
    }
}
