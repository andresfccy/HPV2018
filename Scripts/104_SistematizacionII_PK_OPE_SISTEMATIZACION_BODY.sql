create or replace package body PK_OPE_SISTEMATIZACION is

  /********************************************************************************
  DESCRIPCION     : Procedimiento para consultar los instrumentos de sistematización de experiencias de un usuario dado
  PARAMETROS      :
    IN  :
      p_idUsuario
      p_filtroBusqueda
      p_instrumentoXConsultar
    OUT :
      p_resultado
      p_codError                Codigo de error generado
      p_msjError                Mensaje del error generado
  REALIZADO POR   : Andrés Felipe Cárdenas
  FECHA CREACION  : 04/07/2018
  MODIFICADO POR  :
  FECHA MODIFICA  :
  CAMBIOS         :
  *******************************************************************************/
  PROCEDURE Pr_DarSistematizacion(p_idUsuario             IN number,
                                  p_filtroBusqueda        IN varchar2,
                                  p_instrumentoXConsultar IN number,
                                  p_idPeriodo             IN number,
                                  p_resultado             OUT PK_UTILS.ty_cursor,
                                  p_codError              OUT number,
                                  p_msjError              OUT varchar2) IS
  
    codExcepcion          NUMBER;
    NOMBRE_PROCEDIMIENTO  VARCHAR2(50) := 'Pr_DarSistematizacion';
    numReg                NUMBER;
    codPeriodo            NUMBER;
    idRol                 NUMBER;
  
  BEGIN
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Entro Pr_DarSistematizacion');
  
    if (p_idPeriodo is null) then
      codPeriodo := PK_UTILS.Fn_ObtenerPeriodoVigente;
    else
      codPeriodo := p_idPeriodo;
    end if;
  
    select count(*)
      into numReg
      from t06_usuarios
     where a06codigo = p_idUsuario;
  
    if (numReg = 0) then
      p_codError := 22;
      p_msjError := Pk_Utils.Fn_Obtenermensaje(p_codError);
      return;
    end if;
  
    select a06rol
      into idRol
      from t06_usuarios
     where a06codigo = p_idUsuario;
  
    --es un facilitador
    if (idRol = 1) then
    
      select count(*)
        into numReg
        from t64_sistematizacion, t26_gruposfacilitador
       where a64periodo = codPeriodo
         and a64grupofacilitador = a26codigo
         and a26facilitador = p_idUsuario
         and ((p_instrumentoXConsultar <> 0 and A64INSTRUMENTO = p_instrumentoXConsultar) or 
              (p_instrumentoXConsultar = 0));
    
      if (numReg = 0) then
        p_codError := 111;
        p_msjError := Pk_Utils.Fn_Obtenermensaje(p_codError); -- 111 No hay INSTRUMENTOS DE SISTEMATIZACION registrados
        return;
      end if;
    
      OPEN p_resultado FOR
        select a64codigo  IdSistematizacion,
               a64periodo IdPeriodo,
               a26codigo IdGrupoFacilitador,
               a26sigla SiglaGrupo,
               a25nombre NomGrupo,
               a26facilitador IdFacilitador,
               (p1.a07primernombre || ' ' || p1.a07segundonombre || ' ' ||
               p1.a07primerapellido || ' ' || p1.a07segundoapellido) NomFacilitador,
               a06coordinador IdCoordinador,
               (p2.a07primernombre || ' ' || p2.a07segundonombre || ' ' ||
               p2.a07primerapellido || ' ' || p2.a07segundoapellido) NomCoordinador,
               a26ciudad IdMunicipio,
               a03nombre NomMunicipio,
               a03departamento IdDepartamento,
               a02nombre NomDepartamento,
               a64estado Idestado,
               a77nombre NomEstado,
               a64inscrito IdInscrito,
               (a15primernombre || ' ' || a15segundonombre || ' ' ||
               a15primerapellido || ' ' || a15segundoapellido) NomInscrito,
               a64motivoRechazo MotivoRechazo,
               A76CODIGO IdInstrumento,
               A76NOMBRE NomInstrumento
          from t64_sistematizacion,
               t26_gruposfacilitador,
               t25_grupos,
               t07_personas          p1,
               t07_personas          p2,
               t06_usuarios,
               t03_municipios,
               t02_departamentos,
               T77_ESTADOSSISTEMATIZACION,
               T76_INSTRUMENTOSSIS,
               t15_inscritos
         where a64periodo = codPeriodo
           and a64grupofacilitador = a26codigo
           AND a26grupo = a25codigo
           AND a26facilitador = p1.a07usuario
           and a06coordinador = p2.a07usuario
           and a06codigo = a26facilitador
           and a03departamento = a02codigo
           and a64estado = a77codigo
           and a26ciudad = a03codigo
           and A64INSTRUMENTO = A76CODIGO
           and a06codigo = p_idUsuario
           and a64inscrito = a15codigo
           and (UPPER(a26sigla) LIKE '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07primernombre) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07segundonombre) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07primerapellido) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07segundoapellido) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(a03nombre) LIKE '%' || UPPER(p_filtroBusqueda) || '%')
           and ((p_instrumentoXConsultar <> 0 and A64INSTRUMENTO = p_instrumentoXConsultar) or 
                (p_instrumentoXConsultar = 0));
    
    end if;
  
    --es un coordinador
    if (idRol = 2) then
    
      select count(*)
        into numReg
        from t64_sistematizacion, t26_gruposfacilitador, t06_usuarios
       where a64periodo = codPeriodo
         and a64grupofacilitador = a26codigo
         and a26facilitador = a06codigo
         and a06coordinador = p_idUsuario
         and ((p_instrumentoXConsultar <> 0 and A64INSTRUMENTO = p_instrumentoXConsultar) or 
              (p_instrumentoXConsultar = 0));
    
      if (numReg = 0) then
        p_codError := 111;
        p_msjError := Pk_Utils.Fn_Obtenermensaje(p_codError); -- 111 No hay INSTRUMENTOS DE SISTEMATIZACION registrados
        return;
      end if;
    
      OPEN p_resultado FOR
        select a64codigo  IdSistematizacion,
               a64periodo IdPeriodo,
               a26codigo IdGrupoFacilitador,
               a26sigla SiglaGrupo,
               a25nombre NomGrupo,
               a26facilitador IdFacilitador,
               (p1.a07primernombre || ' ' || p1.a07segundonombre || ' ' ||
               p1.a07primerapellido || ' ' || p1.a07segundoapellido) NomFacilitador,
               a06coordinador IdCoordinador,
               (p2.a07primernombre || ' ' || p2.a07segundonombre || ' ' ||
               p2.a07primerapellido || ' ' || p2.a07segundoapellido) NomCoordinador,
               a26ciudad IdMunicipio,
               a03nombre NomMunicipio,
               a03departamento IdDepartamento,
               a02nombre NomDepartamento,
               a64estado Idestado,
               a77nombre NomEstado,
               a64inscrito IdInscrito,
               (a15primernombre || ' ' || a15segundonombre || ' ' ||
               a15primerapellido || ' ' || a15segundoapellido) NomInscrito,
               a64motivoRechazo MotivoRechazo,
               A76CODIGO IdInstrumento,
               A76NOMBRE NomInstrumento
          from t64_sistematizacion,
               t26_gruposfacilitador,
               t25_grupos,
               t07_personas          p1,
               t07_personas          p2,
               t06_usuarios,
               t03_municipios,
               t02_departamentos,
               T77_ESTADOSSISTEMATIZACION,
               T76_INSTRUMENTOSSIS,
               t15_inscritos
         where a64periodo = codPeriodo
           and a64grupofacilitador = a26codigo
           AND a26grupo = a25codigo
           AND a26facilitador = p1.a07usuario
           and a06coordinador = p2.a07usuario
           and a06codigo = a26facilitador
           and a03departamento = a02codigo
           and a64estado = a77codigo
           and a26ciudad = a03codigo
           and A64INSTRUMENTO = A76CODIGO
           and a06coordinador = p_idUsuario
           and a64inscrito = a15codigo
           and (UPPER(a26sigla) LIKE '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07primernombre) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07segundonombre) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07primerapellido) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07segundoapellido) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(a03nombre) LIKE '%' || UPPER(p_filtroBusqueda) || '%')
           and ((p_instrumentoXConsultar <> 0 and A64INSTRUMENTO = p_instrumentoXConsultar) or 
                (p_instrumentoXConsultar = 0));
    
    end if;
  
    --es un supervisor o administrador
    if (idRol = 6 or idRol = 7) then
    
      select count(*)
        into numReg
        from t64_sistematizacion, t26_gruposfacilitador
       where a64periodo = codPeriodo
         and a64grupofacilitador = a26codigo
         and ((p_instrumentoXConsultar <> 0 and A64INSTRUMENTO = p_instrumentoXConsultar) or 
              (p_instrumentoXConsultar = 0));
    
      if (numReg = 0) then
        p_codError := 111;
        p_msjError := Pk_Utils.Fn_Obtenermensaje(p_codError); -- 111 No hay INSTRUMENTOS DE SISTEMATIZACION registrados
        return;
      end if;
    
      OPEN p_resultado FOR
        select a64codigo  IdSistematizacion,
               a64periodo IdPeriodo,
               a26codigo IdGrupoFacilitador,
               a26sigla SiglaGrupo,
               a25nombre NomGrupo,
               a26facilitador IdFacilitador,
               (p1.a07primernombre || ' ' || p1.a07segundonombre || ' ' ||
               p1.a07primerapellido || ' ' || p1.a07segundoapellido) NomFacilitador,
               a06coordinador IdCoordinador,
               (p2.a07primernombre || ' ' || p2.a07segundonombre || ' ' ||
               p2.a07primerapellido || ' ' || p2.a07segundoapellido) NomCoordinador,
               a26ciudad IdMunicipio,
               a03nombre NomMunicipio,
               a03departamento IdDepartamento,
               a02nombre NomDepartamento,
               a64estado Idestado,
               a77nombre NomEstado,
               a64inscrito IdInscrito,
               (a15primernombre || ' ' || a15segundonombre || ' ' ||
               a15primerapellido || ' ' || a15segundoapellido) NomInscrito,
               a64motivoRechazo MotivoRechazo,
               A76CODIGO IdInstrumento,
               A76NOMBRE NomInstrumento
          from t64_sistematizacion,
               t26_gruposfacilitador,
               t25_grupos,
               t07_personas          p1,
               t07_personas          p2,
               t06_usuarios,
               t03_municipios,
               t02_departamentos,
               T77_ESTADOSSISTEMATIZACION,
               T76_INSTRUMENTOSSIS,
               t15_inscritos
         where a64periodo = codPeriodo
           and a64grupofacilitador = a26codigo
           AND a26grupo = a25codigo
           AND a26facilitador = p1.a07usuario
           and a06coordinador = p2.a07usuario
           and a06codigo = a26facilitador
           and a03departamento = a02codigo
           and a64estado = a77codigo
           and a26ciudad = a03codigo
           and A64INSTRUMENTO = A76CODIGO
           and a64inscrito = a15codigo
           and (UPPER(a26sigla) LIKE '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07primernombre) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07segundonombre) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07primerapellido) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(p1.a07segundoapellido) LIKE
               '%' || UPPER(p_filtroBusqueda) || '%' or
               UPPER(a03nombre) LIKE '%' || UPPER(p_filtroBusqueda) || '%')
           and ((p_instrumentoXConsultar <> 0 and A64INSTRUMENTO = p_instrumentoXConsultar) or 
                (p_instrumentoXConsultar = 0));
    
    end if;
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta Pr_DarSistematizacion');
  
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);
    
  END Pr_DarSistematizacion;

  /********************************************************************************
  DESCRIPCION     : Procedimiento para crear o actualizar un instrumento de sistematización
  PARAMETROS      :
    IN  :
      p_IdLiderazgo
    OUT :
      p_resultado
      p_codError     Codigo de error generado
      p_msjError     Mensaje del error generado
  REALIZADO POR   : Andrés Felipe Cárdenas
  FECHA CREACION  : 04/07/2018
  MODIFICADO POR  :
  FECHA MODIFICA  :
  CAMBIOS         :
  *******************************************************************************/

  PROCEDURE Pr_ActualizarSistematizacion(p_idSistematizacion        IN number, -- 0 Si es nuevo
                                         p_idGrupoFacilitador       number,
                                         p_idFacilitador            number,
                                         p_idInstrumento            number,
                                         p_idEstado                 varchar2,
                                         p_idInscrito               number,
                                         p_motivoRechazo            varchar2,
                                         p_codError                 OUT NUMBER,
                                         p_msjError                 OUT VARCHAR2) IS
  
    codExcepcion         NUMBER;
    NOMBRE_PROCEDIMIENTO VARCHAR2(50) := 'Pr_ActualizarSistematizacion';
    numReg               NUMBER;
  
    codPeriodo  number;
    idSistematizacion number;
    
  BEGIN
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Entro Pr_ActualizarSistematizacion ');
  
    codPeriodo := PK_UTILS.Fn_ObtenerPeriodoVigente;
  
  
  
    if (p_idSistematizacion = 0) then
    
      select count(*)
        into numReg
        from t64_sistematizacion
       where a64periodo = codPeriodo
         and a64grupofacilitador = p_idGrupoFacilitador
         and ((p_idInstrumento <> 0 and A64INSTRUMENTO = p_idInstrumento) or 
              (p_idInstrumento = 0));
    
      if (numReg > 0) then
        p_codError := 112;
        p_msjError := Pk_Utils.Fn_Obtenermensaje(p_codError); -- 112 YA HAY UN INSTRUMENTO DE  SISTEMATIZACIÓN DEL MISMO TIPO, REGISTRADO PARA ESTE GRUPO
        return;
      end if;
    
      select t64_sistematizacion_seq.NEXTVAL into idSistematizacion from dual;
    
      insert into t64_sistematizacion
        (a64codigo,
         a64periodo,
         a64grupofacilitador,
         a64estado,
         a64fechacreacion,
         a64fechamodificacion,
         a64usuariocreacion,
         a64usuariomodificacion,
         a64inscrito,
         a64facilitador,
         a64instrumento,
         a64motivorechazo)
      values
        (idSistematizacion,
         codPeriodo,
         p_idGrupofacilitador,
         'P',
         sysdate,
         sysdate,
         p_idFacilitador,
         p_idFacilitador,
         p_idInscrito,
         p_idFacilitador,
         p_idInstrumento,
         p_motivoRechazo
         );
    else
      idSistematizacion := p_idSistematizacion;
    
      update t64_sistematizacion
         set --a64grupofacilitador    = p_idGrupofacilitador,
             a64estado              = p_idEstado,
             a64fechamodificacion   = sysdate,
             a64usuariomodificacion = p_idFacilitador,
             A64motivorechazo       = p_motivoRechazo
             --a64observacion         = ''
       where a64codigo = p_idSistematizacion;
    
    end if;
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta Pr_ActualizarSistematizacion ok ');
  
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);
    
  END Pr_ActualizarSistematizacion;

  /********************************************************************************
  DESCRIPCION     : Procedimiento para consultar caso de exito
  PARAMETROS      :
    IN  :
      p_IdLiderazgo  
    OUT :
      p_resultado
      p_codError     Codigo de error generado
      p_msjError     Mensaje del error generado
  REALIZADO POR   : Andrés Felipe Cárdenas
  FECHA CREACION  : 04/07/2018
  MODIFICADO POR  :
  FECHA MODIFICA  :
  CAMBIOS         :
  *******************************************************************************/

  PROCEDURE Pr_consultarSistematizacion(p_IdSistematizacion IN number,
                                        p_resultado         OUT PK_UTILS.ty_cursor,
                                        p_codError          OUT NUMBER,
                                        p_msjError          OUT VARCHAR2) IS
  
    codExcepcion         NUMBER;
    NOMBRE_PROCEDIMIENTO VARCHAR2(50) := 'Pr_consultarSistematizacion';
    numReg               NUMBER;
  
    codPeriodo number;
    idRol      number;
  
  BEGIN
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Entro Pr_consultarSistematizacion');
  
    codPeriodo := PK_UTILS.Fn_ObtenerPeriodoVigente;
  
  
    OPEN p_resultado FOR
      select a64codigo  IdSistematizacion,
               a64periodo IdPeriodo,
               a26codigo IdGrupoFacilitador,
               a26sigla SiglaGrupo,
               a25nombre NomGrupo,
               a26facilitador IdFacilitador,
               (p1.a07primernombre || ' ' || p1.a07segundonombre || ' ' ||
               p1.a07primerapellido || ' ' || p1.a07segundoapellido) NomFacilitador,
               a06coordinador IdCoordinador,
               (p2.a07primernombre || ' ' || p2.a07segundonombre || ' ' ||
               p2.a07primerapellido || ' ' || p2.a07segundoapellido) NomCoordinador,
               a26ciudad IdMunicipio,
               a03nombre NomMunicipio,
               a03departamento IdDepartamento,
               a02nombre NomDepartamento,
               a64estado Idestado,
               a77nombre NomEstado,
               a64inscrito IdInscrito,
               (a15primernombre || ' ' || a15segundonombre || ' ' ||
               a15primerapellido || ' ' || a15segundoapellido) NomInscrito,
               a64motivoRechazo MotivoRechazo,
               A76CODIGO IdInstrumento,
               A76NOMBRE NomInstrumento
          from t64_sistematizacion,
               t26_gruposfacilitador,
               t25_grupos,
               t07_personas          p1,
               t07_personas          p2,
               t06_usuarios,
               t03_municipios,
               t02_departamentos,
               T77_ESTADOSSISTEMATIZACION,
               T76_INSTRUMENTOSSIS,
               t15_inscritos
         where a64codigo = p_IdSistematizacion
           and a64periodo = codPeriodo
           and a64grupofacilitador = a26codigo
           AND a26grupo = a25codigo
           AND a26facilitador = p1.a07usuario
           and a06coordinador = p2.a07usuario
           and a06codigo = a26facilitador
           and a03departamento = a02codigo
           and a64estado = a77codigo
           and a26ciudad = a03codigo
           and a64inscrito = a15codigo
           and A64INSTRUMENTO = A76CODIGO;
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta Pr_consultarSistematizacion');
  
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);
    
  END Pr_consultarSistematizacion;

  PROCEDURE Pr_ActualizarCategorizacion(p_idSistematizacion     IN NUMBER,
                                        p_listaCategorizaciones IN VARCHAR2,
                                        p_descotros             IN VARCHAR2 DEFAULT NULL,
                                        p_idUsuario             IN NUMBER,
                                        p_codError              OUT NUMBER,
                                        p_msjError              OUT VARCHAR2)
  IS
    listaCat              PK_UTILS.TOKEN_LIST;
    codExcepcion          NUMBER;
    NOMBRE_PROCEDIMIENTO  VARCHAR2(50) := 'Pr_ActualizarCategorizacion';
  BEGIN
    DELETE FROM T67_SIST_CAT WHERE T67_SIST_CAT.A67INSTRUMENTO = p_idSistematizacion;
    
    listaCat := PK_UTILS.fn_splitStr(p_listaCategorizaciones, ',');
    
    FOR i in 1 .. listaCat.count LOOP
      INSERT INTO T67_SIST_CAT(A67INSTRUMENTO, A67SUBCATEGORIA, A67FECHACREACION, A67USUARIOCREACION) VALUES(p_idSistematizacion,listaCat(i), SYSDATE(), p_idUsuario);
    END LOOP;
    
    IF(p_descotros <> NULL) THEN
      INSERT INTO T67_SIST_CAT(A67INSTRUMENTO, A67SUBCATEGORIA, A67DESCOTRA, A67FECHACREACION, A67USUARIOCREACION) VALUES(p_idSistematizacion, 0, p_descotros, SYSDATE(), p_idUsuario);
    END IF;
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Actualizar Pr_ActualizarCategorizacion');
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);
  END Pr_ActualizarCategorizacion;
                                        
  PROCEDURE Pr_ConsultarCategorizacion(p_idSistematizacion IN NUMBER,
                                       p_resultado         OUT PK_UTILS.ty_cursor,
                                       p_codError          OUT NUMBER,
                                       p_msjError          OUT VARCHAR2)
  IS
    codExcepcion          NUMBER;
    listaCat              VARCHAR2(500) := '';
    NOMBRE_PROCEDIMIENTO  VARCHAR2(50) := 'Pr_ConsultarCategorizacion';
  BEGIN
    OPEN p_resultado FOR
      SELECT 
          A67SUBCATEGORIA             Subcategoria
        , T66_CAT_SUB_SIST.A66CATPRIM Categoria
      FROM T67_SIST_CAT, T66_CAT_SUB_SIST 
      WHERE T67_SIST_CAT.A67INSTRUMENTO = p_idSistematizacion 
      AND   T67_SIST_CAT.A67SUBCATEGORIA = T66_CAT_SUB_SIST.A66CODIGO;
     
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta Pr_ConsultarCategorizacion');  
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);    
  END Pr_ConsultarCategorizacion;

  PROCEDURE Pr_DarCategorias(p_instrumento IN NUMBER,
                             p_periodo     IN NUMBER DEFAULT NULL,
                             p_resultado   OUT PK_UTILS.ty_cursor,
                             p_codError    OUT NUMBER,
                             p_msjError    OUT VARCHAR2)
  IS
    codExcepcion          NUMBER;
    codPeriodo            NUMBER;
    NOMBRE_PROCEDIMIENTO  VARCHAR2(50) := 'Pr_DarCategorias';
  BEGIN
    IF (p_periodo = NULL) THEN
      codPeriodo := PK_UTILS.Fn_ObtenerPeriodoVigente;
    ELSE
      codPeriodo := p_periodo;
    END IF;
    
    OPEN p_resultado FOR
      SELECT 
          t.A65CODIGO       Id
        , t.A65DESCRIPCION  Descripcion
      FROM T65_CAT_PRIM_SIST t
      WHERE t.A65TIPOINST = p_instrumento
      AND   t.A65PERIODO  = codPeriodo;
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta Pr_DarCategorias');  
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);    
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);    
  END Pr_DarCategorias;

  PROCEDURE Pr_DarSubCategorias(p_categoria IN NUMBER,
                                p_resultado OUT PK_UTILS.ty_cursor,
                                p_codError  OUT NUMBER,
                                p_msjError  OUT VARCHAR)
  IS
    codExcepcion          NUMBER;
    NOMBRE_PROCEDIMIENTO  VARCHAR2(50) := 'Pr_DarSubCategorias';
  BEGIN
  
    OPEN p_resultado FOR
      SELECT 
        t.A66CODIGO         Id
        , t.A66CATPRIM      Categoria
        , t.A66DESCRIPCION  Descripcion
      FROM T66_CAT_SUB_SIST t
      WHERE t.A66CATPRIM = p_categoria;    
  
    p_codError := PK_UTILS.COD_OPERACION_CORRECTA;
    p_msjError := PK_UTILS.MSJ_OPERACION_CORRECTA;
  
    PK_UTILS.pr_RegistraLog(PK_UTILS.NIVEL_TRACE,
                            NOMBRE_PAQUETE,
                            NOMBRE_PROCEDIMIENTO,
                            PK_UTILS.COD_USUARIO_DEFECTO,
                            'Consulta Pr_DarSubCategorias');
  EXCEPTION
    WHEN OTHERS THEN
      codExcepcion := pk_utils.fn_RegistraExcepcion(PK_UTILS.COD_USUARIO_DEFECTO,
                                                    NOMBRE_PAQUETE,
                                                    NOMBRE_PROCEDIMIENTO,
                                                    SQLCODE,
                                                    SQLERRM);
      p_codError := PK_UTILS.ERROR_GENERAL;
      p_msjError := PK_UTILS.MSJ_EXCEPCION_GENERAL || TO_CHAR(codExcepcion);
  END Pr_DarSubCategorias;
end PK_OPE_SISTEMATIZACION;