
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Entidades.Alistamiento;

namespace HPV_Datos.Alistamiento.Modelo
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
            entidad.Periodo.NomPeriodo = row["A12NOMBRE"].ToString();
            entidad.Periodo.FechaInicial = row["a12fechainicio"].ToString();
            entidad.Periodo.FechaFinal = row["a12fechafin"].ToString();

            return entidad;
        }
    }
}
