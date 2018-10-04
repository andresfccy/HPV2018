using HPV_Datos.General.Entidad;
using HPV_Entidades.Sistematizacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Datos.Sistematizacion.Entidad
{
    public class CategoriaEntidad : EntidadOracle
    {
        public Categoria Categoria{ get; set; }

        public CategoriaEntidad(string connectionStringName) : base(connectionStringName)
        {
            Categoria = new Categoria();
        }

        public CategoriaEntidad()
        {
            Categoria = new Categoria();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            CategoriaEntidad entidad = new CategoriaEntidad();

            entidad.Categoria.Id = Int64.Parse(row["Id"].ToString());
            entidad.Categoria.NomCategoria = row["NomCategoria"].ToString();
            entidad.Categoria.IdInstrumento = Int64.Parse(row["IdInstrumento"].ToString());
            entidad.Categoria.NomInstrumento = row["NomInstrumento"].ToString();

            return entidad;
        }
    }
}
