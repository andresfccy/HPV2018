using HPV_Datos.General.Entidad;
using HPV_Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Reporte.Entidad
{
    public class AuditoriaEntidad : EntidadOracle
    {
        public Auditoria Auditoria { get; set; }
        public AuditoriaEntidad(string connectionStringName) : base(connectionStringName)
        {
            Auditoria = new Auditoria();
        }

        public AuditoriaEntidad()
        {
            Auditoria = new Auditoria();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            AuditoriaEntidad entidad = new AuditoriaEntidad();
            try
            {
                entidad.Auditoria.IdFuncion = Int64.Parse(row["IDTRX"].ToString());
                entidad.Auditoria.Fecha = row["FECHA"].ToString();
                entidad.Auditoria.Funcion = row["TX"].ToString();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error en AuditoriaEntidad" + e.Message);
            }
            return entidad;
        }

    }
}
