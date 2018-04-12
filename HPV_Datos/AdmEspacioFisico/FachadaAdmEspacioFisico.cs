using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPV_Entidades.AdmEspacioFisicoWS;
using HPV_Datos.AdmEspacioFisico.Entidad;
using HPV_Entidades.General;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using HPV_Datos.General.Entidad;
using HPV_Datos.Alistamiento.Entidad;

namespace HPV_Datos.AdmEspacioFisico
{
    public class FachadaAdmEspacioFisico : InterfaceAdmEspacioFisico
    {
        public OS_ActualizarEspacioFisico ActualizarEspacioFisico(OE_ActualizarEspacioFisico oe)
        {
            OS_ActualizarEspacioFisico os = new OS_ActualizarEspacioFisico();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_ActualizarEspacio",
                    new OracleParameter { ParameterName = "p_idEspacioFisico", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.IdEspacioFisico },
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.IdMunicipio },
                    new OracleParameter { ParameterName = "p_idDia", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.IdDia },
                    new OracleParameter { ParameterName = "p_idHorario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.IdHorario },
                    new OracleParameter { ParameterName = "p_idGrupo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.IdGrupo },
                    new OracleParameter { ParameterName = "p_nombreLugar", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.Lugar },
                    new OracleParameter { ParameterName = "p_direccion", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.EspacioFisico.Direccion },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.ActualizarEspacioFisico :" + e.Message;
            }

            return os;


        }

        public OS_AprobarEspacioFisico AprobarEspacioFisico(OE_AprobarEspacioFisico oe)
        {
            OS_AprobarEspacioFisico os = new OS_AprobarEspacioFisico();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.pr_AprobarEspacioFisico",
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idEspacioFisico", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEspacioFisico },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.AprobarEspacioFisico :" + e.Message;
            }

            return os;



        }

        public OS_ConsultarEspacioFisico ConsultarEspacioFisico(OE_ConsultarEspacioFisico oe)
        {
            OS_ConsultarEspacioFisico os = new OS_ConsultarEspacioFisico();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_ConsultarEspacio",
                    new OracleParameter { ParameterName = "p_idEspacioFisico", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEspacioFisico },
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
                        EspacioFisicoEntidad item = (EspacioFisicoEntidad)objeto;
                        os.EspacioFisico = item.EspacioFisico;
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.ConsultarEspacioFisico :" + e.Message;
            }

            return os;
        }

        public OS_DarDeptosFacilitador DarDeptosFacilitador(OE_DarDeptosFacilitador oe)
        {
            OS_DarDeptosFacilitador os = new OS_DarDeptosFacilitador();
            try
            {

                DptoFacilitadorEntidad entidad = new DptoFacilitadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarDeptosFacilitador",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
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
                        DptoFacilitadorEntidad item = (DptoFacilitadorEntidad)objeto;
                        os.ListaDepartamento.Add(item.Departamento);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarDeptosFacilitador :" + e.Message;
            }

            return os;

        }

        public OS_DarDias DarDias()
        {
            OS_DarDias os = new OS_DarDias();
            try
            {

                DiaEntidad entidad = new DiaEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarDias",
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
                        DiaEntidad item = (DiaEntidad)objeto;
                        os.ListaDia.Add(item.Dia);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarDias :" + e.Message;
            }

            return os;

        }

        public OS_DarDireccionesLugar DarDireccionesLugar(OE_DarDireccionesLugar oe)
        {
            OS_DarDireccionesLugar os = new OS_DarDireccionesLugar();
            try
            {

                DireccionEntidad entidad = new DireccionEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarDireccionesLugar",
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMunicipio },
                    new OracleParameter { ParameterName = "p_lugar", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.Lugar },
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
                        DireccionEntidad item = (DireccionEntidad)objeto;
                        os.ListaDireccion.Add(item.Direccion);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarDireccionesLugar :" + e.Message;
            }

            return os;

        }

        public OS_DarEspacioFisico DarEspacioFisico(OE_DarEspacioFisico oe)
        {
            OS_DarEspacioFisico os = new OS_DarEspacioFisico();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarEspacioFisico",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
                    new OracleParameter { ParameterName = "p_filtroBusqueda", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroBusqueda },
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
                        EspacioFisicoEntidad item = (EspacioFisicoEntidad)objeto;
                        os.ListaEspacioFisico.Add(item.EspacioFisico);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarEspacioFisico :" + e.Message;
            }

            return os;
        }

        public OS_DarEspacioFisicoXAprobar DarEspacioFisicoXAprobar(OE_DarEspacioFisicoXAprobar oe)
        {
            OS_DarEspacioFisicoXAprobar os = new OS_DarEspacioFisicoXAprobar();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.pr_darEspacioFisicoXAprobar",
                    new OracleParameter { ParameterName = "p_idcoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCoordinador},
                    new OracleParameter { ParameterName = "p_consulta", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.FiltroConsulta },
                    new OracleParameter { ParameterName = "p_estado", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.EstadoxConsultar },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
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
                        EspacioFisicoEntidad item = (EspacioFisicoEntidad)objeto;
                        os.ListaEspacioFisico.Add(item.EspacioFisico);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarEspacioFisicoXAprobar :" + e.Message;
            }

            return os;
        }

        public OS_DarGrupos DarGrupos()
        {
            OS_DarGrupos os = new OS_DarGrupos();
            try
            {

                GrupoEntidad entidad = new GrupoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarGrupos",
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
                        GrupoEntidad item = (GrupoEntidad)objeto;
                        os.ListaGrupo.Add(item.Grupo);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarGrupos :" + e.Message;
            }

            return os;

        }

        public OS_DarHorarios DarHorarios()
        {
            OS_DarHorarios os = new OS_DarHorarios();
            try
            {

                HorarioEntidad entidad = new HorarioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarHorarios",
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
                        HorarioEntidad item = (HorarioEntidad)objeto;
                        os.ListaHorario.Add(item.Horario);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarHorarios :" + e.Message;
            }

            return os;

        }

        public OS_DarLugaresMunicipio DarLugaresMunicipio(OE_DarLugaresMunicipio oe)
        {
            OS_DarLugaresMunicipio os = new OS_DarLugaresMunicipio();
            try
            {

                LugarMunicipioEntidad entidad = new LugarMunicipioEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarLugaresMunicipio",
                    new OracleParameter { ParameterName = "p_idMunicipio", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdMunicipio },
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
                        LugarMunicipioEntidad item = (LugarMunicipioEntidad)objeto;
                        os.ListaLugarMunicipio.Add(item.LugarMunicipio);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarLugaresMunicipio :" + e.Message;
            }

            return os;
        }

        public OS_DarMunicipiosFacilitador DarMunicipiosFacilitador(OE_DarMunicipiosFacilitador oe)
        {
            OS_DarMunicipiosFacilitador os = new OS_DarMunicipiosFacilitador();
            try
            {

                MunicipioFacilitadorEntidad entidad = new MunicipioFacilitadorEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_DarMunicipiosFacilitador",
                    new OracleParameter { ParameterName = "p_idFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdFacilitador },
                    new OracleParameter { ParameterName = "p_idDepartamento", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdDepartamento },
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
                        MunicipioFacilitadorEntidad item = (MunicipioFacilitadorEntidad)objeto;
                        os.ListaMunicipio.Add(item.Municipio);
                    }

                }

                entidad.Dispose();

            }
            catch (Exception e)
            {
                os.Respuesta.Codigo = Constantes.COD_ERROR_GENERAL;
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.DarDeptosFacilitador :" + e.Message;
            }

            return os;
        }

        public OS_ReasignarEspacio ReasignarEspacio(OE_ReasignarEspacio oe)
        {
            OS_ReasignarEspacio os = new OS_ReasignarEspacio();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.Pr_ReasignarEspacio",
                    new OracleParameter { ParameterName = "p_idInscrito", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdInscrito },
                    new OracleParameter { ParameterName = "p_idGrupoFacilitador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdGrupoFacilitador },
                    new OracleParameter { ParameterName = "p_idUsuario", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdUsuarioRegistra },
                    new OracleParameter { ParameterName = "p_idPeriodo", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdPeriodo },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.ReasignarEspacio :" + e.Message;
            }

            return os;
        }

        public OS_RechazarEspacioFisico RechazarEspacioFisico(OE_RechazarEspacioFisico oe)
        {
            OS_RechazarEspacioFisico os = new OS_RechazarEspacioFisico();
            try
            {

                EspacioFisicoEntidad entidad = new EspacioFisicoEntidad(Constantes.HPV_NOM_CONNECTIONSTRING);

                entidad.ExecuteStoreProcedure("PK_OPE_AdmEspacioFisico.pr_RechazarEspacioFisico",
                    new OracleParameter { ParameterName = "p_idCoordinador", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdCoordinador },
                    new OracleParameter { ParameterName = "p_idEspacioFisico", OracleDbType = OracleDbType.Int64, Direction = ParameterDirection.Input, Value = oe.IdEspacioFisico },
                    new OracleParameter { ParameterName = "p_motivoRechazo", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = oe.MotivoRechazo },
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
                os.Respuesta.Mensaje = "Fallo en HPV_Datos.AdmEspacioFisico.RechazarEspacioFisico :" + e.Message;
            }

            return os;
        }
    }
}
