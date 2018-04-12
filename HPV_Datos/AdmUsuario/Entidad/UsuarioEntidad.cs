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
    public class UsuarioEntidad : EntidadOracle
    {
        public Usuario Usuario { get; set; }


        public UsuarioEntidad(string connectionStringName) : base(connectionStringName)
        {
            Usuario = new Usuario();
        }

        public UsuarioEntidad()
        {
            Usuario = new Usuario();
        }

        public override EntidadOracle ParseFromDataRow(DataRow row)
        {
            if (row == null)
                return null;

            UsuarioEntidad entidad = new UsuarioEntidad();
            entidad.Usuario.IdUsuario = Int64.Parse(row["a06codigo"].ToString());
            entidad.Usuario.IdRol = Int64.Parse(row["a06rol"].ToString());
            entidad.Usuario.AliasUsuario = row["a06usuario"].ToString();
            entidad.Usuario.TipoDocumento = row["a07tipodocumento"].ToString();
            entidad.Usuario.Identificacion = row["a07identificacion"].ToString();

            entidad.Usuario.PrimerNombre = row["a07primernombre"].ToString();
            entidad.Usuario.SegundoNombre = row["a07segundonombre"].ToString();
            entidad.Usuario.PrimerApellido = row["a07primerapellido"].ToString();
            entidad.Usuario.SegundoApellido = row["a07segundoapellido"].ToString();
            entidad.Usuario.FechaNacimiento = row["a07fechanacimiento"].ToString();

            entidad.Usuario.Correo = row["a07email"].ToString();
            entidad.Usuario.Telefono = row["a07telefono"].ToString();
            entidad.Usuario.Movil = row["a07movil"].ToString();
            entidad.Usuario.MovilAlt = row["a07movilalt"].ToString();
            entidad.Usuario.CodEstado = row["a06estado"].ToString();
            entidad.Usuario.Estado = row["a93nombre"].ToString();
            entidad.Usuario.Ciudad = row["ciudades"].ToString();
            entidad.Usuario.IdPerfil = Int64.Parse(row["a06perfil"].ToString());
            entidad.Usuario.Clave = row["a06clave"].ToString();
            entidad.Usuario.IdCoordinador = Int64.Parse(row["a06coordinador"].ToString());
            entidad.Usuario.NomRol = row["a05nombre"].ToString();

            return entidad;
        }
    }
}
