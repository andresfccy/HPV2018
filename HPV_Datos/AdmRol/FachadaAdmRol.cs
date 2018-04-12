using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.AdmRolWS;
using HPV_Datos.AdmRol.Entidad;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Datos.Alistamiento.Entidad;

namespace HPV_Datos.AdmRol
{
    public class FachadaAdmRol : InterfaceAdmRol
    {
        public OS_ActualizarRol ActualizarRol(OE_ActualizarRol oe)
        {
            OS_ActualizarRol os = new OS_ActualizarRol();
            try
            {

                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMROL.Pr_ActualizarRol",
                    new OracleParameter { ParameterName = "p_codigo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Rol.Codigo },
                    new OracleParameter { ParameterName = "p_nombre", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Rol.Nombre },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Rol.Estado },
                    new OracleParameter { ParameterName = "p_opciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Rol.Opciones },
                    new OracleParameter { ParameterName = "p_usuarioRegistra", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmRol.FachadaAdmRol.ActualizarRol :" + e.Message;
            }

            return os;
        }

        public OS_ConsultarRol ConsultarRol(OE_ConsultarRol oe)
        {
            OS_ConsultarRol os = new OS_ConsultarRol();
            try
            {

                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMROL.Pr_ConsultarRol",
                    new OracleParameter { ParameterName = "p_codigo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.Codigo },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmRol.FachadaAdmRol.ConsultarRol :" + e.Message;
            }
            return os;
        }

        public OS_DarOpcionesUsuario DarOpcionesUsuario(OE_DarOpcionesUsuario oe)
        {
            OS_DarOpcionesUsuario os = new OS_DarOpcionesUsuario();
            try
            {

                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMROL.Pr_DarOpcionesXUsuario",
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuario },
                    new OracleParameter { ParameterName = "p_idRol", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_nomRol", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 },
                    new OracleParameter { ParameterName = "p_opciones", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 },
                    new OracleParameter { ParameterName = "p_codError", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Output },
                    new OracleParameter { ParameterName = "p_msjError", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 1000 }
                    );

                os.Respuesta.Codigo = entidad.GetParameterLong("p_codError");
                os.Respuesta.Mensaje = entidad.GetParameterString("p_msjError");

                if (os.Respuesta.Codigo == 0)
                {
                    os.Rol = new HPV_Entidades.AdmRol.Rol();
                    os.Rol.Codigo = Int64.Parse(entidad.GetParameterString("p_idRol"));
                    os.Rol.Nombre = entidad.GetParameterString("p_nomRol");
                    os.Rol.Opciones = entidad.GetParameterString("p_opciones");
                }
                entidad.Dispose();
            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmRol.FachadaAdmRol.Pr_DarOpcionesXUsuario :" + e.Message;
            }
            return os;
        }

        public OS_DarRoles DarRoles()
        {
            OS_DarRoles os = new OS_DarRoles();
            try
            {
                RolEntidad entidad = new RolEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_PAR_ADMROL.Pr_DarRoles",
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmRol.FachadaAdmRol.DarRols :" + e.Message;
            }


            return os;
        }
    }
}
