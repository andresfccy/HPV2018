using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmParametro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmParametro.Entidad
{
    public class PeriodoEntidad : EntidadOracle
    {
        public Periodo Periodo { get; set; }


        public PeriodoEntidad(string connectionStringName) : base(connectionStringName)
        {
           Periodo = new Periodo();
        }

        public PeriodoEntidad()
        {
           Periodo = new Periodo();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            PeriodoEntidad entidad = new PeriodoEntidad();

            entidad.Periodo.Codigo = Int64.Parse(row["Codigo"].ToString());
            entidad.Periodo.Nombre = row["Nombre"].ToString();
            entidad.Periodo.FechaInicio = row["FechaInicio"].ToString();
            entidad.Periodo.FechaFin = row["FechaFin"].ToString();
            entidad.Periodo.Numero = Int64.Parse(row["Numero"].ToString());
            entidad.Periodo.Estado = row["Estado"].ToString();
            entidad.Periodo.FechaIniTaller = row["FechaIniTaller"].ToString();

            return entidad;
        }
    }
}
