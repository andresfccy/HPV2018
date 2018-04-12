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
    public class ParametroEntidad : EntidadOracle
    {
        public Parametro Parametro { get; set; }


        public ParametroEntidad(string connectionStringName) : base(connectionStringName)
        {
            Parametro = new Parametro();
        }

        public ParametroEntidad()
        {
            Parametro = new Parametro();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            ParametroEntidad entidad = new ParametroEntidad();
            entidad.Parametro.Codigo = Int64.Parse(row["Codigo"].ToString());
            entidad.Parametro.Nombre = row["Nombre"].ToString();
            entidad.Parametro.Valor = row["Valor"].ToString();
            entidad.Parametro.Descripcion = row["Descripcion"].ToString();
            entidad.Parametro.Estado = row["Estado"].ToString();

            return entidad;
        }
    }
}
