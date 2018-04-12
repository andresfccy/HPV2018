using HPV_Datos.General.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HPV_Entidades.FechaCorte;

namespace HPV_Datos.FechaCorte.Entidad
{
    public class FechaCorteEntidad : EntidadOracle
    {
        public HPV_Entidades.FechaCorte.FechaCorte FechaCorte { get; set; }

        public FechaCorteEntidad(string connectionStringName) : base(connectionStringName)
        {
            FechaCorte = new HPV_Entidades.FechaCorte.FechaCorte();
        }

        public FechaCorteEntidad()
        {
            FechaCorte = new HPV_Entidades.FechaCorte.FechaCorte();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            FechaCorteEntidad entidad = new FechaCorteEntidad();
            entidad.FechaCorte.Id = Int64.Parse(row["Codigo"].ToString());
            entidad.FechaCorte.Descripcion = row["Descripcion"].ToString();
            entidad.FechaCorte.Fecha = row["FechaCorte"].ToString();

            return entidad;
        }
    }
}
