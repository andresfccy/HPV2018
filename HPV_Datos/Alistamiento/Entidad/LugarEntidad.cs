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
    public class LugarEntidad : EntidadOracle
    {
        public Lugar Lugar { get; set; }

        public LugarEntidad()
        {
            Lugar = new Lugar();
        }


        public LugarEntidad(string connectionStringName) : base(connectionStringName)
        {
            Lugar = new Lugar();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            LugarEntidad entidad = new LugarEntidad();
            entidad.Lugar.Grupofacilitador = Int64.Parse(row["a26codigo"].ToString());
            entidad.Lugar.Nombre = row["a26lugar"].ToString();
            entidad.Lugar.Direccion = row["a26direccion"].ToString();

            return entidad;
        }


    }
}
