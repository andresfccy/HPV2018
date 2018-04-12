using HPV_Datos.General.Entidad;
using HPV_Entidades.Alistamiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmEspacioFisico.Entidad
{
    public class MunicipioFacilitadorEntidad : EntidadOracle
    {
        public Municipio Municipio { get; set; }

        public MunicipioFacilitadorEntidad(string connectionStringName) : base(connectionStringName)
        {
            Municipio = new Municipio();
        }

        public MunicipioFacilitadorEntidad()
        {
            Municipio = new Municipio();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            MunicipioFacilitadorEntidad entidad = new MunicipioFacilitadorEntidad();
            entidad.Municipio.Codigo = Int64.Parse(row["A03CODIGO"].ToString());
            entidad.Municipio.Dane = row["A03DANE"].ToString();
            entidad.Municipio.IdDepartamento = Int64.Parse(row["A03DEPARTAMENTO"].ToString());
            entidad.Municipio.Nombre = row["A03NOMBRE"].ToString();

            return entidad;

        }

    }
}
