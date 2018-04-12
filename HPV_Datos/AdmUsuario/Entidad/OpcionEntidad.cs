using HPV_Datos.General.Entidad;
using HPV_Entidades.AdmUsuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.AdmUsuario.Entidad
{
    public class OpcionEntidad : EntidadOracle
    {
        public Opcion Opcion { get; set; }


        public OpcionEntidad(string connectionStringName) : base(connectionStringName)
        {
            Opcion = new Opcion();
        }

        public OpcionEntidad()
        {
            Opcion = new Opcion();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            OpcionEntidad entidad = new OpcionEntidad();

            entidad.Opcion.IdMenu = Int64.Parse(row["a11menu"].ToString());
            entidad.Opcion.IdOpcion = Int64.Parse(row["a11opcion"].ToString());
            entidad.Opcion.Nombre = row["a10nombre"].ToString();
            entidad.Opcion.Tag = row["a10tag"].ToString();
            entidad.Opcion.Orden = Int32.Parse(row["a10orden"].ToString());

            return entidad;
        }
    }
}
