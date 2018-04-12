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
    class LogroEntidad : EntidadOracle
    {
        public Logro Logro { get; set; }

        public LogroEntidad(string connectionStringName) : base(connectionStringName)
        {
            Logro = new Logro();
        }

        public LogroEntidad()
        {
            Logro = new Logro();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            LogroEntidad entidad = new LogroEntidad();

            entidad.Logro.IdLogro = Int64.Parse(row["IdLogro"].ToString());
            entidad.Logro.Verbo = row["Verbo"].ToString();
            entidad.Logro.Adjetivo = row["Adjetivo"].ToString();
            entidad.Logro.Medio = row["Medio"].ToString();
            entidad.Logro.NomLogro = row["NomLogro"].ToString();


            return entidad;
        }
    }
}
