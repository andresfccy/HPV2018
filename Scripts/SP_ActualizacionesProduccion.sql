PROCEDURE Pr_GenerarRptSistematizacion(p_periodo  IN NUMBER,
                                       p_codError OUT NUMBER,
                                       p_msjError OUT VARCHAR2) IS
  
    codExcepcion         number;
    NOMBRE_PROCEDIMIENTO varchar2(50) := 'Pr_GenerarRptEjemLiderazgo';
    numReg               NUMBER;
    vrPorc               NUMBER := 12.5;
  
    idPeriodoAct number;
    idEncuesta   number;
    p_fec_corte  VARCHAR2(10);
    p_tipo_corte VARCHAR2(10);
  
  BEGIN
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Entro:' || NOMBRE_PROCEDIMIENTO);
  
    if p_periodo is null then
      idPeriodoAct := PK_UTILS.Fn_ObtenerPeriodoVigente;
      pk_prc_batch.pr_tipofechacorte(p_fec_corte,
                                     p_tipo_corte,
                                     p_coderror,
                                     p_msjerror);
    ELSE
      idPeriodoAct := p_periodo;
      SELECT MAX(FEC_CORTE)
        INTO p_fec_corte
        FROM T101_RPT_TRAZABILIDAD T
       WHERE T.PERIODO = idPeriodoAct;
      p_tipo_corte := 'P';
    
    end if;
  
    -- Por si se reprocesa.
    delete from T116_RPT_SISTEMATIZACION t
     where t.periodo = idPeriodoAct
       and t.tipo_corte = p_tipo_corte
       and t.fec_corte = p_fec_corte;
  
    insert into T116_RPT_SISTEMATIZACION
      (periodo,
       divipola,
       nombre_departamento,
       nombre_municipio,
       sigla_grupo,
       tipo_instrumento,
       estado_instrumento,
       cedula_facilitador,
       nombre_facilitador,
       primernombre_participante,
       segundonombre_participante,
       primerapellido_participante,
       segundoapellido_participante,
       genero,
       tipo_documento_participante,
       documento_participante,
       codigo_beneficiario,
       ruta_archivo,
       tipo_corte,
       fec_corte)
      select e.a64periodo,
             A03DANE,
             a02nombre,
             a03nombre,
             a26sigla,
             ins.A76NOMBRE,
             es.A77NOMBRE,
             a07identificacion,
             (a07primernombre || ' ' || a07segundonombre || ' ' ||
             a07primerapellido || ' ' || a07segundoapellido) nombrefacilitador,
             i.a15primernombre,
             i.a15segundonombre,
             i.a15primerapellido,
             i.a15segundoapellido,
             i.A15GENERO,
             i.a15tipodocumento,
             i.a15documento,
             i.a15codigobeneficiario,
             (e.A64PERIODO || '/SIS/' || e.A64GRUPOFACILITADOR || '/' || 
             e.A64INSTRUMENTO) rutaarchivo,
             p_tipo_corte,
             p_fec_corte
        from t15_inscritos              i,
             T64_SISTEMATIZACION        e,
             t26_gruposfacilitador      g,
             t03_municipios             m,
             t02_departamentos          d,
             T06_USUARIOS               u,
             T77_ESTADOSSISTEMATIZACION es,
             T76_INSTRUMENTOSSIS        ins,
             T07_PERSONAS
       where i.a15estado = PK_UTILS.COD_ACTIVO
         and a26codigo = a15grupofacilitador
         and a03codigo = a26ciudad
         and a02codigo = a03departamento
         AND a26facilitador = a06codigo
         AND a06codigo = a07usuario
         and e.a64periodo = idPeriodoAct
         and es.A77CODIGO = e.A64ESTADO
         and ins.A76CODIGO = e.A64INSTRUMENTO
         and e.a64inscrito = i.a15codigo;
  
    commit;
  
    -- Se eliminan los datos de calculos de dias anteriores.
    delete from T116_RPT_SISTEMATIZACION t
     where t.periodo = idPeriodoAct
       and t.tipo_corte = 'A'
       and t.fec_corte <> p_fec_corte;
  
    commit;
  
    select count(0)
      into numReg
      from T116_RPT_SISTEMATIZACION t
     where t.periodo = idPeriodoAct
       and t.fec_corte = p_fec_corte;
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA || '. Total registros: ' ||
                  to_char(numReg);
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Salio' || NOMBRE_PROCEDIMIENTO);
  EXCEPTION
    WHEN OTHERS THEN
      rollback;
    
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := SQLERRM;
    
  END Pr_GenerarRptSistematizacion;

PROCEDURE Pr_DarRptSistematizacion(p_idPeriodo      IN NUMBER,
                              p_idUsuario      IN NUMBER,
                              p_idFacilitador  IN NUMBER,
                              p_idCoordinador  IN NUMBER,
                              p_idGrupo        IN NUMBER,
                              p_idDepartamento IN NUMBER,
                              p_idMunicipio    IN NUMBER,
                              p_fec_corte      IN varchar2,
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
  
    OPEN p_resultado FOR
      select periodo "Id Período",
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
        from T116_RPT_SISTEMATIZACION
       where periodo = decode(p_idPeriodo, 0, periodo, p_idPeriodo)
         and FEC_CORTE = p_fec_corte;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta  Pr_DarRptSistematizacion OK ');
  
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