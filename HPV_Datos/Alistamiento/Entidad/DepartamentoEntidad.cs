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
    public class DepartamentoEntidad : EntidadOracle
    {
        public Departamento Departamento { get; set; }


        public DepartamentoEntidad(string connectionStringName) : base(connectionStringName)
        {
            Departamento = new Departamento();
        }

        public DepartamentoEntidad()
        {
            Departamento = new Departamento();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            DepartamentoEntidad entidad = new DepartamentoEntidad();
            entidad.Departamento.Codigo = Int64.Parse(row["A02CODIGO"].ToString());
            entidad.Departamento.Dane = row["A02DANE"].ToString();
            entidad.Departamento.Nombre = row["A02NOMBRE"].ToString();
            entidad.Departamento.Indicativo = Int16.Parse(row["A02INDICATIVO"].ToString());

            return entidad;
        }
    }
}
