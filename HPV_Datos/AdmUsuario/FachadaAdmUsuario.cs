using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.AdmUsuarioWS;
using HPV_Datos.AdmUsuario.Entidad;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Datos.Alistamiento.Entidad;

namespace HPV_Datos.AdmUsuario
{
    public class FachadaAdmUsuario : InterfaceAdmUsuario
    {
        public OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe)
        {
            OS_ActualizarRol os = new OS_ActualizarRol();
            try
            {

                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_ActualizarRol",
                    new OracleParameter { ParameterName = "p_idRol", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Rol.IdRol },
                    new OracleParameter { ParameterName = "p_nombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Rol.Nombre },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Rol.CodEstado },
                    new OracleParameter { ParameterName = "p_opciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Rol.Opciones },
                    new OracleParameter { ParameterName = "p_usuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuarioRegistra },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.ActualizarRol :" + e.Message;
            }

            return os;
        }

        public OS_ActualizarUsuario ActualizarUsuario(OE_ActualizarUsuario oe)
        {
            OS_ActualizarUsuario os = new OS_ActualizarUsuario();
            try
            {

                UsuarioEntidad entidad = new UsuarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_ActualizarUsuario",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Usuario.IdUsuario },
                    new OracleParameter { ParameterName = "p_rol", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Usuario.IdRol },
                    new OracleParameter { ParameterName = "p_coordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Usuario.IdCoordinador },
                    new OracleParameter { ParameterName = "p_login", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.AliasUsuario },
                    new OracleParameter { ParameterName = "p_clave", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.Clave },
                    new OracleParameter { ParameterName = "p_tipodocumento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.TipoDocumento },
                    new OracleParameter { ParameterName = "p_identificacion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.Identificacion },
                    new OracleParameter { ParameterName = "p_primernombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.PrimerNombre },
                    new OracleParameter { ParameterName = "p_segundonombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.SegundoNombre },
                    new OracleParameter { ParameterName = "p_primerapellido", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.PrimerApellido },
                    new OracleParameter { ParameterName = "p_segundoapellido", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.SegundoApellido },
                    new OracleParameter { ParameterName = "p_fechanacimiento", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.FechaNacimiento },
                    new OracleParameter { ParameterName = "p_email", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.Correo },
                    new OracleParameter { ParameterName = "p_telefono", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.Telefono },
                    new OracleParameter { ParameterName = "p_movil", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.Movil },
                    new OracleParameter { ParameterName = "p_movilalt", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.MovilAlt },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.CodEstado },
                    new OracleParameter { ParameterName = "p_perfil", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Usuario.IdPerfil },
                    new OracleParameter { ParameterName = "p_ciudades", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Usuario.Ciudad },
                    new OracleParameter { ParameterName = "p_usuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuarioRegistra },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.ActualizarUsuario :" + e.Message;
            }

            return os;
        }

        public OS_AsignarContrasenaXToken AsignarContrasenaXToken(OE_AsignarContrasenaXToken oe)
        {
            OS_AsignarContrasenaXToken os = new OS_AsignarContrasenaXToken();
            try
            {
                UsuarioEntidad entidad = new UsuarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_AsignarContrasenaXToken",
                    new OracleParameter { ParameterName = "p_token", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Token },
                    new OracleParameter { ParameterName = "p_contrasena", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Contrasena },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );
                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");


                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.Pr_AsignarContrasenaXToken :" + e.Message;
            }

            return os;

        }

        public OS_AutenticarUsuario AutenticarUsuario(OE_AutenticarUsuario oe)
        {
            OS_AutenticarUsuario os = new OS_AutenticarUsuario();
            try
            {

                AutenticarEntidad entidad = new AutenticarEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_AutenticarUsuario",
                    new OracleParameter { ParameterName = "p_usuario", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.AliasUsuario },
                    new OracleParameter { ParameterName = "p_clave", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Clave },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                    os.IdUsuario = entidad.GetParameterLong("p_idUsuario");

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.AutenticarUsuario :" + e.Message;
            }



            return os;

        }

        public OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe)
        {
            OS_ConsultarRol os = new OS_ConsultarRol();
            try
            {

                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_ConsultarRol",
                    new OracleParameter { ParameterName = "p_idrol", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdRol },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        RolEntidad item = (RolEntidad)objeto;
                        os.Rol = item.Rol;
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.OS_ConsultarRol :" + e.Message;
            }


            return os;
        }

        public OS_ConsultarUsuario ConsultarUsuario(OE_ConsultarUsuario oe)
        {
            OS_ConsultarUsuario os = new OS_ConsultarUsuario();
            try
            {

                UsuarioEntidad entidad = new UsuarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_ConsultarUsuario",
                    new OracleParameter { ParameterName = "p_idusuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario},
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        UsuarioEntidad item = (UsuarioEntidad)objeto;
                        os.Usuario = item.Usuario;
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.ConsultarUsuario :" + e.Message;
            }

            return os;

        }

        public OS_DarCoordinadores DarCoordinadores()
        {
            OS_DarCoordinadores os = new OS_DarCoordinadores();
            try
            {

                CoordinadorEntidad entidad = new CoordinadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarCoordinadores",
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        CoordinadorEntidad item = (CoordinadorEntidad)objeto;
                        os.ListaCoordinador.Add(item.Coordinador);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarCoordinadores :" + e.Message;
            }

            return os;
        }

        public OS_DarMenus DarMenus()
        {
            OS_DarMenus os = new OS_DarMenus();
            try
            {

                MenuEntidad entidad = new MenuEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarMenus",
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        MenuEntidad item = (MenuEntidad)objeto;
                        os.ListaMenu.Add(item.Menu);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarMenus :" + e.Message;
            }

            return os;
        }

        public OS_DarMunicipios DarMunicipios()
        {
            OS_DarMunicipios os = new OS_DarMunicipios();
            try
            {

                MunicipioEntidad entidad = new MunicipioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarMunicipios",
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        MunicipioEntidad item = (MunicipioEntidad)objeto;
                        os.ListaMunicipio.Add(item.Municipio);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarMunicipios :" + e.Message;
            }

            return os;
        }

        public OS_DarOpcionesMenu DarOpcionesMenu(OE_DarOpcionesMenu oe)
        {
            OS_DarOpcionesMenu os = new OS_DarOpcionesMenu();
            try
            {

                OpcionEntidad entidad = new OpcionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarOpcionesMenu",
                    new OracleParameter { ParameterName = "p_idmenu", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMenu }, 
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        OpcionEntidad item = (OpcionEntidad)objeto;
                        os.ListaOpcion.Add(item.Opcion);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarOpcionesMenu :" + e.Message;
            }

            return os;

        }

        public OS_DarOpcionesMenuRol DarOpcionesMenuRol(OE_DarOpcionesMenuRol oe)
        {
            OS_DarOpcionesMenuRol os = new OS_DarOpcionesMenuRol();
            try
            {

                OpcionEntidad entidad = new OpcionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarOpcionesMenuRol",
                    new OracleParameter { ParameterName = "p_idrol", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdRol },
                    new OracleParameter { ParameterName = "p_idMenu", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMenu },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        OpcionEntidad item = (OpcionEntidad)objeto;
                        os.ListaOpcion.Add(item.Opcion);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarOpcionesMenuRol :" + e.Message;
            }


            return os;

        }

        public OS_DarRoles DarRoles(OE_DarRoles oe)
        {
            OS_DarRoles os = new OS_DarRoles();
            try
            {

                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarRoles",
                    new OracleParameter { ParameterName = "p_consulta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        RolEntidad item = (RolEntidad)objeto;
                        os.ListaRol.Add(item.Rol);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarUsuarios :" + e.Message;
            }


            return os;
        }

        public OS_DarUsuarios DarUsuarios(OE_DarUsuarios oe)
        {
            OS_DarUsuarios os = new OS_DarUsuarios();
            try
            {

                UsuarioEntidad entidad = new UsuarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_DarUsuarios",
                    new OracleParameter { ParameterName = "p_consulta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
                    new OracleParameter { ParameterName = "p_resultado", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    List<EntidadOracle> lista = entidad.CursorToList("p_resultado");

                    foreach (EntidadOracle objeto in lista)
                    {
                        UsuarioEntidad item = (UsuarioEntidad)objeto;
                        os.ListaUsuario.Add(item.Usuario);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.DarUsuarios :" + e.Message;
            }


            return os;
        }

        public OS_RecuperarContrasena RecuperarContrasena(OE_RecuperarContrasena oe)
        {
            OS_RecuperarContrasena os = new OS_RecuperarContrasena();
            try
            {
                UsuarioEntidad entidad = new UsuarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);
                
                entidad.ExecuteStoreProcedure("PK_OPE_ADMUSUARIO.Pr_RestaurarContrasena",
                    new OracleParameter { ParameterName = "p_email", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Email },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );
                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");
                

                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmUsuario.FachadaAdmUsuario.RecuperarContraseña :" + e.Message;
            }

            return os;
        }
    }
}
