using HPV_Datos.General.Entidad;
using HPV_Entidades.HistoriasDeVida;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.HistoriaVida.Entidad
{
    public class HistoriaVidaEntidad : EntidadOracle
    {
        
    
        public HistoriaDeVida HistoriaDeVida { get; set; }


        public HistoriaVidaEntidad(string connectionStringName) : base(connectionStringName)
        {
            HistoriaDeVida = new HistoriaDeVida();
        }

        public HistoriaVidaEntidad()
        {
            HistoriaDeVida = new HistoriaDeVida();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            HistoriaVidaEntidad entidad = new HistoriaVidaEntidad();

            entidad.HistoriaDeVida.IdHistoriaDeVida = Int64.Parse(row["IdHistoriaDeVida"].ToString());
            entidad.HistoriaDeVida.IdPeriodo = Int64.Parse(row["IdPeriodo"].ToString());
            entidad.HistoriaDeVida.IdGrupoFacilitador = Int64.Parse(row["IdGrupoFacilitador"].ToString());
            entidad.HistoriaDeVida.SiglaGrupo = row["SiglaGrupo"].ToString();

            entidad.HistoriaDeVida.IdGrupo = Int64.Parse(row["IdGrupo"].ToString());
            entidad.HistoriaDeVida.NomGrupo = row["NomGrupo"].ToString();


            entidad.HistoriaDeVida.IdFacilitador = Int64.Parse(row["IdFacilitador"].ToString());
            entidad.HistoriaDeVida.NomFacilitador = row["NomFacilitador"].ToString();
            entidad.HistoriaDeVida.IdCoordinador = Int64.Parse(row["IdCoordinador"].ToString());
            entidad.HistoriaDeVida.NomCoordinador = row["NomCoordinador"].ToString();

            entidad.HistoriaDeVida.IdMunicipio = Int64.Parse(row["IdMunicipio"].ToString());
            entidad.HistoriaDeVida.NomMunicipio = row["NomMunicipio"].ToString();

            entidad.HistoriaDeVida.IdDepartamento = Int64.Parse(row["IdDepartamento"].ToString());
            entidad.HistoriaDeVida.NomDepartamento = row["NomDepartamento"].ToString();

            entidad.HistoriaDeVida.IdEstado = row["IdEstado"].ToString();
            entidad.HistoriaDeVida.NomEstado = row["NomEstado"].ToString();

            entidad.HistoriaDeVida.IdTipoHistoriaDeVida = Int64.Parse(row["IdTipoHistoriaDeVida"].ToString());
            entidad.HistoriaDeVida.NomTipoHistoriaDeVida = row["NomTipoHistoriaDeVida"].ToString();

            entidad.HistoriaDeVida.RazonDeConsideracion = row["RazonDeConsideracion"].ToString();
            entidad.HistoriaDeVida.CircunstanciasSuperadas = row["CircunstanciasSuperadas"].ToString();
            entidad.HistoriaDeVida.HabilidadesPracticadas = row["HabilidadesPracticadas"].ToString();
            entidad.HistoriaDeVida.Leccion = row["Leccion"].ToString();
            entidad.HistoriaDeVida.RazonDeConsideracion = row["RazonDeConsideracion"].ToString();

            entidad.HistoriaDeVida.IdInscrito = Int64.Parse(row["IdInscrito"].ToString());
            entidad.HistoriaDeVida.NomInscrito = row["NomInscrito"].ToString();
            entidad.HistoriaDeVida.MotivoRechazo = row["MotivoRechazo"].ToString();

            return entidad;
        }
    }
}
