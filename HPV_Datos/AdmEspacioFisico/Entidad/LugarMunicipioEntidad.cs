using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmEspacioFisico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmEspacioFisico.Entidad
{
    public class LugarMunicipioEntidad : EntidadOracle
    {
        public LugarMunicipio LugarMunicipio { get; set; }

        public LugarMunicipioEntidad(string connectionStringName) : base(connectionStringName)
        {
            LugarMunicipio = new LugarMunicipio();
        }

        public LugarMunicipioEntidad()
        {
            LugarMunicipio = new LugarMunicipio();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            LugarMunicipioEntidad entidad = new LugarMunicipioEntidad();
            entidad.LugarMunicipio.Nombre = row["A26LUGAR"].ToString();


            return entidad;

        }

    }
}
