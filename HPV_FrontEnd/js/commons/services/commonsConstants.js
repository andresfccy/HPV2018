angular.module('prosperidad.commons')
    .factory('CommonsConstants', [
        'Connection',
        function (Connection) {
            function constructOptionsRelation() {
                var list = [];
                list[1] = "Administración";
                list[11] = "Usuarios";
                list[12] = "Roles";
                list[13] = "Mi cuenta";
                list[14] = "Parámetros";
                list[2] = "Operativo";
                list[21] = "Generar lista de asistencia";
                list[22] = "Asignación de espacios físicos";
                list[23] = "Aprobación de espacios físicos";
                list[25] = "Aprobación de asistencia";
                list[26] = "Reasignación de participantes";
                list[27] = "Generar lista de asistencia - Coordinador";
                list[28] = "Marcación de asistencia";
                list[29] = "Aprobación marcación de asistencia";
                list[210] = "Ver marcación de asistencia";
                list[3] = "Consulta",
                list[31] = "Inscritos",
                list[32] = "Horarios",
                list[4] = "Sistematización de experiencias",
                list[41] = "Historias de vida",
                list[42] = "Casos de éxito",
                list[43] = "Aprobación de historias de vida",
                list[44] = "Aprobación de casos de éxito",
                list[45] = "Selección de historias de vida",
                list[46] = "Selección de casos de éxito",
                list[47] = "Creación de ejemplos de liderazgo",
                list[48] = "Aprobación de ejemplos de liderazgo",
                list[49] = "Selección de ejemplos de liderazgo",
                list[410] = "Lista de sistematización de experiencias",
                list[411] = "Detalle sistematización de experiencias",
                list[420] = "Administración sistematización de experiencias",
                list[421] = "Administración detalle sistematización de experiencias",
                list[430] = "Supervisión sistematización de experiencias",
                list[431] = "Supervisión detalle sistematización de experiencias",
                list[5] = "Reportes",
                list[51] = "Lista de reportes",
                list[52] = "Auditoría",
                list[53] = "Lista de indicadores"
                list[54] = "Lista de reportes - Profesional Territorial"

                return list;
            }

            var factory = {
                API_HOSTNAME: Connection.API_HOSTNAME,
                API_PORT: Connection.API_PORT,
                API_BASE_URL: Connection.API_BASE_URL,

                TOKEN_HEADER: 'Authorization',
                TOKEN_KEY_LOCALSTORAGE: 'authData',
                USER_ID_LOCALSTORAGE: 'user_id',
                ROL_LOCALSTORAGE: 'rol',
                NAME_LOCALSTORAGE: 'name',
                CONTENT_TYPE_HEADER: { 'Content-Type': 'application/json' },
                VERSION: '08.02.2019' // d.mm.aa#

                // URLs Web Services
                , URL_WS_GENERAL: '/General/HPVServiciosGen_JSON.svc'
                , URL_WS_ALISTAMIENTO: '/Alistamiento/HPVServiciosAli_JSON.svc'
                , URL_WS_USUARIOS: '/AdmUsuario/HPVServiciosAdmUsuario_JSON.svc'
                , URL_WS_UBICACIONES: '/AdmEspacioFisico/HPVServiciosAdmEspacioFisico_JSON.svc'
                , URL_WS_ASISTENCIA: '/Asistencia/HPVServiciosAsistencia_JSON.svc'
                , URL_WS_SEARCHES: '/Consulta/HPVServiciosConsulta_JSON.svc'
                , URL_WS_REASIGNACION: '/Consulta/HPVServiciosReasignacion_JSON.svc'
                , URL_WS_ENTREGABLES: 'Por generar'
                , URL_WS_HISTORIASDEVIDA: '/HistoriaVida/HPVServiciosHistoriaVida_JSON.svc'
                , URL_WS_CASOSDEEXITO: '/CasosDeExito/HPVServiciosCasosDeExito_JSON.svc'
                , URL_WS_LIDERAZGO: '/Liderazgo/HPVServiciosLiderazgo_JSON.svc'
                , URL_WS_PARAMETROS: '/AdmParametro/HPVServiciosAdmParametro_JSON.svc'
                , URL_WS_ROLES: '/AdmRol/HPVServiciosAdmRol_JSON.svc'
                , URL_WS_REPORTES: '/Reportes/HPVServiciosReportes_JSON.svc'
                , URL_WS_FECHA_CORTE: '/FechaCorte/HPVServiciosFechaCorte_JSON.svc'

                , URL_WS_SISTEMATIZACION: '/Sistematizacion/HPVServiciosSistematizacion_JSON.svc'

                , OPTIONS_RELATION: constructOptionsRelation()
            };

            var storage = localStorage;

            function guardar(clave, valor) {
                storage.setItem(clave, valor);
            }

            function eliminar(clave) {
                storage.removeItem(clave);
            }

            return {
                factory: factory
            };
        }
    ]);