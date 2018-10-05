PROCEDURE Pr_DarRptSistematizacion(p_idPeriodo      IN NUMBER,
                              p_idUsuario      IN NUMBER,
                              p_idFacilitador  IN NUMBER,
                              p_idCoordinador  IN NUMBER,
                              p_idGrupo        IN NUMBER,
                              p_idDepartamento IN NUMBER,
                              p_idMunicipio    IN NUMBER,
                              p_fec_corte      IN varchar2,
                              p_idInstrumento  IN NUMBER,
                              p_resultado      OUT PK_UTILS.TY_CURSOR,
                              p_codError       OUT NUMBER,
                              p_msjError       OUT VARCHAR2) IS
  
    codExcepcion         number;
    NOMBRE_PROCEDIMIENTO varchar2(50) := 'Pr_DarRptSistematizacion';
    numReg               NUMBER;
  BEGIN
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Entro Pr_DarRptSistematizacion');
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
    
  IF p_idInstrumento = 1 THEN
    OPEN p_resultado FOR
      select periodo "Id Período",
             T12_PERIODOS.A12NOMBRE "Nombre Período",
             divipola "Id División Política",
             nombre_departamento "Nombre Departamento",
             nombre_municipio "Nombre Muncipio",
             tipo_instrumento "Tipo Instrumento",
             estado_instrumento "Estado Instrumento",
             sigla_grupo "Sigla Grupo",
             cedula_facilitador "Cédula Facilitador",
             nombre_facilitador "Nombre Faicilitador",
             primernombre_participante "Primer Nombre Participante",
             segundonombre_participante "Segundo Nombre Participante",
             primerapellido_participante "Primer Apellido Participante",
             segundoapellido_participante "Segundo Apellido Participante",
             tipo_documento_participante "Tipo Documento Participante",
             documento_participante "Documento Participante",
             codigo_beneficiario "Código Beneficiario",
             ruta_archivo "Ruta Archivo"
              ,''	"	C	1	S	1	"
              ,''	"	C	1	S	2	"
              ,''	"	C	1	S	3	"
              ,''	"	C	1	S	4	"
              ,''	"	C	1	S	5	"
              ,''	"	C	1	S	6	"
              ,''	"	C	1	S	7	"
              ,''	"	C	1	S	8	"
              ,''	"	C	1	S	9	"
              ,''	"	C	1	S	10	"
              ,''	"	C	1	S	11	"
              ,''	"	C	1	S	12	"
              ,''	"	C	1	S	13	"
              ,''	"	C	1	S	14	"
              ,''	"	C	1	S	15	"
              ,''	"	C	1	S	16	"
              ,''	"	C	1	S	17	"
              ,''	"	C	2	S	1	"
              ,''	"	C	2	S	2	"
              ,''	"	C	2	S	3	"
              ,''	"	C	2	S	4	"
              ,''	"	C	2	S	5	"
              ,''	"	C	2	S	6	"
              ,''	"	C	2	S	7	"
              ,''	"	C	2	S	8	"
              ,''	"	C	3	S	1	"
              ,''	"	C	3	S	2	"
              ,''	"	C	3	S	3	"
              ,''	"	C	4	S	1	"
              ,''	"	C	5	S	1	"
              ,''	"	C	6	S	1	"
              ,'' "Adicionales"
        from T116_RPT_SISTEMATIZACION, T12_PERIODOS
       where periodo = decode(p_idPeriodo, 0, periodo, p_idPeriodo)
         and T12_PERIODOS.A12CODIGO = PERIODO
         and FEC_CORTE = p_fec_corte;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta  Pr_DarRptSistematizacion OK ');
  ELSIF p_idInstrumento = 2 THEN
  OPEN p_resultado FOR
      select periodo "Id Período",
             T12_PERIODOS.A12NOMBRE "Nombre Período",
             divipola "Id División Política",
             nombre_departamento "Nombre Departamento",
             nombre_municipio "Nombre Muncipio",
             tipo_instrumento "Tipo Instrumento",
             estado_instrumento "Estado Instrumento",
             sigla_grupo "Sigla Grupo",
             cedula_facilitador "Cédula Facilitador",
             nombre_facilitador "Nombre Faicilitador",
             primernombre_participante "Primer Nombre Participante",
             segundonombre_participante "Segundo Nombre Participante",
             primerapellido_participante "Primer Apellido Participante",
             segundoapellido_participante "Segundo Apellido Participante",
             tipo_documento_participante "Tipo Documento Participante",
             documento_participante "Documento Participante",
             codigo_beneficiario "Código Beneficiario",
             ruta_archivo "Ruta Archivo"
             ,''	"	C	7	S	1	"
              ,''	"	C	7	S	2	"
              ,''	"	C	7	S	3	"
              ,''	"	C	7	S	4	"
              ,''	"	C	7	S	5	"
              ,''	"	C	7	S	6	"
              ,''	"	C	7	S	7	"
              ,''	"	C	8	S	1	"
              ,''	"	C	8	S	2	"
              ,''	"	C	8	S	3	"
              ,''	"	C	8	S	4	"
              ,''	"	C	8	S	5	"
              ,''	"	C	8	S	6	"
              ,''	"	C	8	S	7	"
              ,''	"	C	8	S	8	"
              ,''	"	C	9	S	1	"
              ,''	"	C	9	S	2	"
              ,''	"	C	9	S	3	"
              ,''	"	C	9	S	4	"
              ,''	"	C	9	S	5	"
              ,''	"	C	10	S	1	"
              ,''	"	C	11	S	1	"
              ,''	"	C	11	S	2	"
              ,''	"	C	12	S	1	"
              ,''	"	C	12	S	2	"
              ,''	"	C	12	S	3	"
              ,''	"	C	12	S	4	"
              ,''	"	C	12	S	5	"
              ,''	"	C	13	S	1	"
              ,''	"	C	13	S	2	"
              ,''	"	C	13	S	3	"
              ,''	"	C	13	S	4	"
              ,'' " Adicionales "
        from T116_RPT_SISTEMATIZACION, T12_PERIODOS
       where periodo = decode(p_idPeriodo, 0, periodo, p_idPeriodo)
         and T12_PERIODOS.A12CODIGO = PERIODO
         and FEC_CORTE = p_fec_corte;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta  Pr_DarRptSistematizacion OK ');
  ELSIF p_idInstrumento = 3 THEN
  OPEN p_resultado FOR
      select periodo "Id Período",
             T12_PERIODOS.A12NOMBRE "Nombre Período",
             divipola "Id División Política",
             nombre_departamento "Nombre Departamento",
             nombre_municipio "Nombre Muncipio",
             tipo_instrumento "Tipo Instrumento",
             estado_instrumento "Estado Instrumento",
             sigla_grupo "Sigla Grupo",
             cedula_facilitador "Cédula Facilitador",
             nombre_facilitador "Nombre Faicilitador",
             primernombre_participante "Primer Nombre Participante",
             segundonombre_participante "Segundo Nombre Participante",
             primerapellido_participante "Primer Apellido Participante",
             segundoapellido_participante "Segundo Apellido Participante",
             tipo_documento_participante "Tipo Documento Participante",
             documento_participante "Documento Participante",
             codigo_beneficiario "Código Beneficiario",
             ruta_archivo "Ruta Archivo"
             ,''	"	C	14	S	1	"
              ,''	"	C	14	S	2	"
              ,''	"	C	14	S	3	"
              ,''	"	C	14	S	4	"
              ,''	"	C	14	S	5	"
              ,''	"	C	15	S	1	"
              ,''	"	C	15	S	2	"
              ,''	"	C	16	S	1	"
              ,''	"	C	16	S	2	"
              ,''	"	C	17	S	1	"
              ,''	"	C	17	S	2	"
              ,''	"	C	18	S	1	"
              ,''	"	C	18	S	2	"
              ,''	"	C	19	S	1	"
              ,''	"	C	19	S	2	"
              ,''	"	C	19	S	3	"
              ,''	"	C	19	S	4	"
              ,''	"	C	19	S	5	"
              ,''	"	C	19	S	6	"
              ,''	"	C	19	S	7	"
              ,''	"	C	19	S	8	"
              ,''	"	C	20	S	1	"
              ,''	"	C	20	S	2	"
              ,''	"	C	20	S	3	"
              ,''	"	C	20	S	4	"
              ,''	"	C	20	S	5	"
              ,''	"	C	20	S	6	"
              ,''	"	C	20	S	7	"
              ,''	"	C	21	S	1	"
              ,''	"	C	21	S	2	"
              ,''	"	C	21	S	3	"
              ,''	"	C	21	S	4	"
              ,''	"	C	21	S	5	"
              ,''	"	C	21	S	6	"
              ,''	"	C	21	S	7	"
              ,''	"	C	22	S	1	"
              ,''	"	C	22	S	2	"
              ,''	"	C	22	S	3	"
              ,''	"	C	23	S	1	"
              ,''	"	C	23	S	2	"
              ,''	"	C	23	S	3	"
              ,''	"	C	23	S	4	"
              ,''	"	C	23	S	5	"
              ,''	"	C	23	S	6	"
              ,''	"	C	23	S	7	"
              ,''	"	C	23	S	8	"
              ,''	"	C	23	S	9	"
              ,''	"	C	24	S	1	"
              ,''	"	C	24	S	2	"
              ,''	"	C	24	S	3	"
              ,''	"	C	24	S	4	"
              ,''	"	C	24	S	5	"
              ,''	"	C	25	S	1	"
              ,''	"	C	25	S	2	"
              ,''	"	C	25	S	3	"
              ,''	"	C	26	S	1	"
              ,''	"	C	26	S	2	"
              ,''	"	C	27	S	1	"
            ,'' " Adicionales "
        from T116_RPT_SISTEMATIZACION, T12_PERIODOS
       where periodo = decode(p_idPeriodo, 0, periodo, p_idPeriodo)
         and T12_PERIODOS.A12CODIGO = PERIODO
         and FEC_CORTE = p_fec_corte;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta  Pr_DarRptSistematizacion OK ');
  END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);
    
  END Pr_DarRptSistematizacion;