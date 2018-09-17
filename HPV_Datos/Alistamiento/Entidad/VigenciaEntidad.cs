using HPV_Datos.General.Entidad;
using HPV_Entidades.Alistamiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Alistamiento.Entidad
{
    public class VigenciaEntidad : EntidadOracle
    {
        public Vigencia Vigencia { get; set; }

        public VigenciaEntidad()
        {
            Vigencia = new Vigencia();
        }


        public VigenciaEntidad(string connectionStringName) : base(connectionStringName)
        {

        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            VigenciaEntidad entidad = new VigenciaEntidad();
            String x = row["vigencia"].ToString();

            entidad.Vigencia.ano = Int32.Parse(row["vigencia"].ToString());
            
            return entidad;
        }
    }
}
