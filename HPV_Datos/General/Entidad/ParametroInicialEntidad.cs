using HPV_Entidades.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.General.Entidad
{
    public class ParametroInicialEntidad : EntidadOracle
    {
        public ParametroInicial ParametroInicial { get; set; }


        public ParametroInicialEntidad(string connectionStringName) : base(connectionStringName)
        {
            ParametroInicial = new ParametroInicial();
        }

        public ParametroInicialEntidad()
        {
            ParametroInicial = new ParametroInicial();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            ParametroInicialEntidad entidad = new ParametroInicialEntidad();
            entidad.ParametroInicial.FechaIniPreaviso = row["FechaIniPreaviso"].ToString();
            entidad.ParametroInicial.FechaFinPreaviso = row["FechaFinPreaviso"].ToString();
            entidad.ParametroInicial.FechaIniInscripcion = row["FechaIniInscripcion"].ToString();
            entidad.ParametroInicial.FechaFinInscripcion = row["FechaFinInscripcion"].ToString();
            entidad.ParametroInicial.IdPeriodo = Int64.Parse(row["IdPeriodo"].ToString());
            entidad.ParametroInicial.NombrePeriodo = row["NombrePeriodo"].ToString();

            return entidad;
        }

    }
}
