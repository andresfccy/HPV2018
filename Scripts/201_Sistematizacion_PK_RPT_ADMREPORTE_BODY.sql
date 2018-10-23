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
    inst           number;
    NOMBRE_PROCEDIMIENTO varchar2(50) := 'Pr_DarRptSistematizacion';
    numReg               NUMBER;
    sqlqry               CLOB;
    cols                 CLOB;
    colsPost             CLOB;
  BEGIN
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Entro Pr_DarRptSistematizacion');
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
    
    inst := p_idInstrumento;
    
    select 
      LISTAGG('''' || sigCats.cat || ''' as "' || sigCats.cat || '"' ,',') 
      within group (order by sigCats.A66CODIGO)
    into cols
    from(
      select 'C' || cprn."NCP" || 'S' || (ROW_NUMBER() OVER(PARTITION BY cprn.A65CODIGO ORDER BY sc.A66CODIGO)) cat, sc.A66CODIGO,
        A65TIPOINST TIPO
      from T66_CAT_SUB_SIST sc
      join(
        select rownum "NCP", cp.A65CODIGO, cp.A65TIPOINST
        from T65_CAT_PRIM_SIST cp
        where cp.A65PERIODO = p_idPeriodo
      ) cprn on sc.A66CATPRIM = cprn.A65CODIGO
    ) sigCats
    where tipo = inst
    ;
      
    select 
      LISTAGG(sigCats.cat||'_CANT '||sigCats.cat,',') 
      within group (order by sigCats.A66CODIGO)
    into colsPost
    from(
      select 'C' || cprn."NCP" || 'S' || (ROW_NUMBER() OVER(PARTITION BY cprn.A65CODIGO ORDER BY sc.A66CODIGO)) cat, sc.A66CODIGO,
        A65TIPOINST TIPO
      from T66_CAT_SUB_SIST sc
      join(
        select rownum "NCP", cp.A65CODIGO, cp.A65TIPOINST
        from T65_CAT_PRIM_SIST cp
        where cp.A65PERIODO = p_idPeriodo
      ) cprn on sc.A66CATPRIM = cprn.A65CODIGO
    ) sigCats
    where tipo = inst
    ;
    
    numreg := 0;
    select count(*) into numReg from user_tables where table_name = upper('tmp_sistem');
   if numReg = 1 then
    sqlqry := 'drop table tmp_sistem';
    execute immediate sqlqry;
   end if;
      
    sqlqry := '
      create table tmp_sistem as
      select '||colsPost||', cmt Adicionales, inst ID_INST
      from (
        select cats.A67INSTRUMENTO INST, SIGLAS.SIGLA, SIGLAS.TIPO,
          case when cats.A67CODIGO is not null then 1 else 0 end CANT
        from(
          select sc.A66CODIGO, sc.A66DESCRIPCION,
            ''C'' || cprn.NCP || ''S'' || (ROW_NUMBER() OVER(PARTITION BY cprn.A65CODIGO ORDER BY sc.A66CODIGO)) SIGLA,
            A65TIPOINST TIPO
          from T66_CAT_SUB_SIST sc
          join(
            select rownum NCP, cp.A65CODIGO, cp.A65TIPOINST
            from T65_CAT_PRIM_SIST cp
            where cp.A65PERIODO = '||p_idPeriodo||'
        ) cprn on sc.A66CATPRIM = cprn.A65CODIGO) SIGLAS
        join T67_SIST_CAT cats on cats.A67SUBCATEGORIA = SIGLAS.A66CODIGO
        where tipo = '|| inst ||'
        order by SIGLAS.A66CODIGO
      )
      pivot(sum(cant) as cant for (sigla) in ('||cols||'))
      join (
        select t67.A67INSTRUMENTO idinst, t67.A67DESCOTRA cmt
        from T67_SIST_CAT t67
        where t67.A67SUBCATEGORIA = 0
      ) cmts on cmts.idinst = inst
      order by INST';
    
    execute immediate sqlqry;
    
    open p_resultado for
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
             ruta_archivo "Ruta Archivo",
             tmp_sistem.*
        from T116_RPT_SISTEMATIZACION, T12_PERIODOS, tmp_sistem
       where periodo = decode(p_idPeriodo, 0, periodo, p_idPeriodo)
         and T12_PERIODOS.A12CODIGO = PERIODO
         and FEC_CORTE = p_fec_corte
         and tmp_sistem.ID_INST = T116_RPT_SISTEMATIZACION.ID_SISTEMATIZACION;
  
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
