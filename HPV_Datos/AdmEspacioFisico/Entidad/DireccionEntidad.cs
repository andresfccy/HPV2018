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
    public class DireccionEntidad : EntidadOracle
    {
        public Direccion Direccion { get; set; }

        public DireccionEntidad(string connectionStringName) : base(connectionStringName)
        {
            Direccion = new Direccion();
        }

        public DireccionEntidad()
        {
            Direccion = new Direccion();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            DireccionEntidad entidad = new DireccionEntidad();
            entidad.Direccion.Nombre = row["A26DIRECCION"].ToString();


            return entidad;

        }
    }

}
