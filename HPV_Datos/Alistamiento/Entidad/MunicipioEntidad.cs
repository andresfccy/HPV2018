using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Entidades.Alistamiento;

namespace HPV_Datos.Alistamiento.Entidad
{
    public class MunicipioEntidad : EntidadOracle
    {
        public Municipio Municipio { get; set; }

        public MunicipioEntidad()
        {
            Municipio = new Municipio();
        }


        public MunicipioEntidad(string connectionStringName) : base(connectionStringName)
        {
            Municipio = new Municipio();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            MunicipioEntidad entidad = new MunicipioEntidad();
            entidad.Municipio.Codigo = Int64.Parse(row["A03CODIGO"].ToString());
            entidad.Municipio.Dane = row["A03DANE"].ToString();
            entidad.Municipio.IdDepartamento = Int64.Parse(row["A03DEPARTAMENTO"].ToString());
            entidad.Municipio.Nombre = row["A03NOMBRE"].ToString();

            return entidad;
        }
    }
}
