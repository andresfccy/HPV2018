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
                              p_msjError       OUT VARCHAR2);
							  
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
      select periodo,
             divipola,
             nombre_departamento,
             nombre_municipio,
             sigla_grupo,
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
             ruta_archivo "Archivo"
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
  
  /**
  declare 
  p_coderror number;
  p_msjerror varchar2 (500);
  begin
  pk_prc_batch.Pr_GenerarRptSistematizacion(p_coderror, p_msjerror);
  end;*/