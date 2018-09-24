INSERT INTO "HPV"."T10_OPCIONES" (A10CODIGO, A10NOMBRE, A10ORDEN, A10ESTADO) VALUES ('410', 'Lista de sistematización de experiencias', '32', 'A')
INSERT INTO "HPV"."T10_OPCIONES" (A10CODIGO, A10NOMBRE, A10ORDEN, A10ESTADO) VALUES ('411', 'Detalle sistematización de experiencias', '33', 'A')
INSERT INTO "HPV"."T10_OPCIONES" (A10CODIGO, A10NOMBRE, A10ORDEN, A10ESTADO) VALUES ('420', 'Admin lista sistematización', '34', 'A')
INSERT INTO "HPV"."T10_OPCIONES" (A10CODIGO, A10NOMBRE, A10ORDEN, A10ESTADO) VALUES ('421', 'Admin detalle sistematización', '35', 'A')

CREATE TABLE T64_SISTEMATIZACION 
(
  A64CODIGO NUMBER NOT NULL 
, A64PERIODO NUMBER 
, A64GRUPOFACILITADOR NUMBER 
, A64ESTADO CHAR(1) 
, A64FECHACREACION DATE 
, A64FECHAMODIFICACION DATE 
, A64USUARIOCREACION NUMBER 
, A64USUARIOMODIFICACION NUMBER 
, A64LINKVIDEO VARCHAR2(225) 
, A64FACILITADOR NUMBER 
, CONSTRAINT T64_SISTEMATIZACION_PK PRIMARY KEY 
  (
    A64CODIGO 
  )
  ENABLE 
);

COMMENT ON COLUMN T64_SISTEMATIZACION.A64CODIGO IS 'Identificacion del registro';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64PERIODO IS 'Periodo';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64GRUPOFACILITADOR IS 'Grupo facilitador';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64ESTADO IS 'Estado';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64FECHACREACION IS 'Fecha creacion';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64FECHAMODIFICACION IS 'Fecha modificacion';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64USUARIOCREACION IS 'Usuario creacion';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64USUARIOMODIFICACION IS 'Usuario modificacion';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64LINKVIDEO IS 'Link del video con la evidencia';

COMMENT ON COLUMN T64_SISTEMATIZACION.A64FACILITADOR IS 'Id del facilitador';

ALTER TABLE T64_SISTEMATIZACION 
ADD (A64INSTRUMENTO NUMBER );

COMMENT ON COLUMN T64_SISTEMATIZACION.A64INSTRUMENTO IS 'Id del instrumento correspondiente';

CREATE SEQUENCE T64_SISTEMATIZACION_SEQ INCREMENT BY 1 START WITH 1 MINVALUE 1;


CREATE TABLE T76_INSTRUMENTOSSIS 
(
  A76CODIGO NUMBER NOT NULL 
, A76NOMBRE VARCHAR2(225) NOT NULL 
, A76ESTADO CHAR(1) DEFAULT 'I' 
, A76FECHACREACION DATE 
, A76FECHAMODIFICACION DATE 
, A76USUARIOCREACION NUMBER 
, A76USUARIOMODIFICACION NUMBER 
, CONSTRAINT T76_INSTRUMENTOSSIS_PK PRIMARY KEY 
  (
    A76CODIGO 
  )
  ENABLE 
);

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76CODIGO IS 'Identificador del registro';

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76NOMBRE IS 'Nombre del instrumento';

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76ESTADO IS 'A:Activo I:Inactivo';

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76FECHACREACION IS 'Fecha de creacion';

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76FECHAMODIFICACION IS 'Fecha de modificacion';

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76USUARIOCREACION IS 'Usuario creacion';

COMMENT ON COLUMN T76_INSTRUMENTOSSIS.A76USUARIOMODIFICACION IS 'Usuario modificacion';


CREATE TABLE T77_ESTADOSSISTEMATIZACION 
(
  A77CODIGO CHAR(1) NOT NULL 
, A77NOMBRE VARCHAR2(50) 
, A77ESTADO CHAR(1) 
, A77FECHACREACION DATE 
, A77FECHAMODIFICACION DATE 
, A77USUARIOCREACION NUMBER 
, A77USUARIOMODIFICACION NUMBER 
, CONSTRAINT T77_ESTADOSSISTEMATIZACION_PK PRIMARY KEY 
  (
    A77CODIGO 
  )
  ENABLE 
);

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77CODIGO IS 'Identificacion del registro';

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77NOMBRE IS 'Nombre del estado';

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77ESTADO IS 'A:Activo I:Inactivo';

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77FECHACREACION IS 'Fecha de creacion';

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77FECHAMODIFICACION IS 'Fecha de modificacion';

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77USUARIOCREACION IS 'Usuario creacion';

COMMENT ON COLUMN T77_ESTADOSSISTEMATIZACION.A77USUARIOMODIFICACION IS 'Usuario modificacion';


INSERT INTO "HPV"."T77_ESTADOSSISTEMATIZACION" (A77CODIGO, A77NOMBRE, A77ESTADO) VALUES ('P', 'Pendiente de aprobación', 'A')
INSERT INTO "HPV"."T77_ESTADOSSISTEMATIZACION" (A77CODIGO, A77NOMBRE, A77ESTADO) VALUES ('A', 'Activo', 'A')
INSERT INTO "HPV"."T77_ESTADOSSISTEMATIZACION" (A77CODIGO, A77NOMBRE, A77ESTADO) VALUES ('I', 'Inactivo', 'A')
INSERT INTO "HPV"."T77_ESTADOSSISTEMATIZACION" (A77CODIGO, A77NOMBRE, A77ESTADO) VALUES ('R', 'Rechazado', 'A')
INSERT INTO "HPV"."T77_ESTADOSSISTEMATIZACION" (A77CODIGO, A77NOMBRE, A77ESTADO) VALUES ('S', 'Seleccionado', 'A')

INSERT INTO "HPV"."T76_INSTRUMENTOSSIS" (A76CODIGO, A76NOMBRE, A76ESTADO) VALUES ('1', 'Historias de vida', 'A')
INSERT INTO "HPV"."T76_INSTRUMENTOSSIS" (A76CODIGO, A76NOMBRE, A76ESTADO) VALUES ('2', 'Ejemplos de liderazgo', 'A')

INSERT INTO "HPV"."T95_MENSAJERESPUESTA" (A95CODIGO, A95MENSAJE, A95ESTADO) VALUES ('111', 'NO HAY INSTRUMENTOS DE SISTEMATIZACION REGISTRADOS', 'A')
INSERT INTO "HPV"."T95_MENSAJERESPUESTA" (A95CODIGO, A95MENSAJE, A95ESTADO) VALUES ('112', 'YA HAY UN INSTRUMENTO DE  SISTEMATIZACIÓN DEL MISMO TIPO, REGISTRADO PARA ESTE GRUPO', 'A')


reate or replace package PK_OPE_SISTEMATIZACION is

  /********************************************************************************
  DESCRIPCION         : Paquete utilizado para agrupar todos los procedimientos y funciones
                        utilizados para sistematización de experiencias v. 2018  
  REALIZADO POR       : Andrés Felipe Cárdenas
  FECHA CREACION      : 04/07/2018
  FECHA MODIFICA      :
  MODIFICADO POR      :
  DESCRIPCION CAMBIO  :
  
  *******************************************************************************/

  NOMBRE_PAQUETE CONSTANT VARCHAR2(50) := 'PK_OPE_SISTEMATIZACION';

  PROCEDURE Pr_DarSistematizacion(p_idUsuario             IN number,
                                  p_filtroBusqueda        IN varchar2,
                                  p_instrumentoXConsultar IN number,
                                  p_idPeriodo             IN NUMBER,
                                  p_resultado             OUT PK_UTILS.ty_cursor,
                                  p_codError              OUT NUMBER,
                                  p_msjError              OUT VARCHAR2);

  PROCEDURE Pr_ActualizarSistematizacion(p_idSistematizacion        IN number, -- 0 Si es nuevo
                                         p_idGrupoFacilitador       number,
                                         p_idFacilitador            number,
                                         p_idInstrumento            number,
                                         p_idEstado                 varchar2,
                                         p_linkVideo                varchar2,
                                         p_codError                 OUT NUMBER,
                                         p_msjError                 OUT VARCHAR2);

  PROCEDURE Pr_consultarSistematizacion(p_idSistematizacion IN number,
                                        p_resultado         OUT PK_UTILS.ty_cursor,
                                        p_codError          OUT NUMBER,
                                        p_msjError          OUT VARCHAR2);
                                        
end PK_OPE_SISTEMATIZACION;

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
               a64linkvideo LinkVideo,
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
               T76_INSTRUMENTOSSIS
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
           and a26facilitador = p_idUsuario
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
               a64linkvideo LinkVideo,
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
               T76_INSTRUMENTOSSIS
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
           and a26facilitador = p_idUsuario
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
               a64linkvideo LinkVideo,
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
               T76_INSTRUMENTOSSIS
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
           and a26facilitador = p_idUsuario
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
                                         p_linkVideo                varchar2,
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
         a64linkvideo,
         a64facilitador,
         a64instrumento)
      values
        (idSistematizacion,
         codPeriodo,
         p_idGrupofacilitador,
         'A',
         sysdate,
         sysdate,
         p_idFacilitador,
         p_idFacilitador,
         p_linkVideo,
         p_idFacilitador,
         p_idInstrumento
         );
    else
      idSistematizacion := p_idSistematizacion;
    
      update t64_sistematizacion
         set --a64grupofacilitador    = p_idGrupofacilitador,
             a64estado              = p_idEstado,
             a64fechamodificacion   = sysdate,
             a64usuariomodificacion = p_idFacilitador,
             A64LINKVIDEO           = p_linkVideo
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
  
    CURSOR curLogro IS
      SELECT a61cualidad, a61respuesta
        FROM t61_liderazgoCualidad
       WHERE a61liderazgo = p_IdSistematizacion
       order by a61cualidad;
  
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
               a64linkvideo LinkVideo,
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
               T76_INSTRUMENTOSSIS
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

end PK_OPE_SISTEMATIZACION;

ALTER TABLE T64_SISTEMATIZACION RENAME COLUMN A64LINKVIDEO TO A64INSCRITO;

ALTER TABLE T64_SISTEMATIZACION  
MODIFY (A64INSCRITO NUMBER );

COMMENT ON COLUMN T64_SISTEMATIZACION.A64INSCRITO IS 'Id del inscrito seleccionado para este instrumento';

ALTER TABLE T64_SISTEMATIZACION 
ADD (A64MOTIVORECHAZO VARCHAR2(200) );

COMMENT ON COLUMN T64_SISTEMATIZACION.A64MOTIVORECHAZO IS 'Motivo del rechazo del instrumento';


INSERT INTO "HPV"."T10_OPCIONES" (A10CODIGO, A10NOMBRE, A10ORDEN, A10ESTADO) VALUES ('430', 'Super lista sistematización', '36', 'A')
INSERT INTO "HPV"."T10_OPCIONES" (A10CODIGO, A10NOMBRE, A10ORDEN, A10ESTADO) VALUES ('431', 'Super detalle sistematización', '37', 'A')

UPDATE "HPV"."T77_ESTADOSSISTEMATIZACION" SET A77NOMBRE = 'Aceptado por coordinador' WHERE A77CODIGO = 'A'

CREATE TABLE T116_RPT_SISTEMATIZACION 
(
  PERIODO VARCHAR2(50) 
, DIVIPOLA VARCHAR2(50) 
, NOMBRE_DEPARTAMENTO VARCHAR2(250) 
, NOMBRE_MUNICIPIO VARCHAR2(250) 
, SIGRA_GRUPO VARCHAR2(50) 
, CEDULA_FACILITADOR VARCHAR2(50) 
, NOMBRE_FACILITADOR VARCHAR2(250) 
, PRIMERNOMBRE_PARTICIPANTE VARCHAR2(250) 
, SEGUNDONOMBRE_PARTICIPANTE VARCHAR2(250) 
, PRIMERAPELLIDO_PARTICIPANTE VARCHAR2(250) 
, SEGUNDOAPELLIDO_PARTICIPANTE VARCHAR2(250) 
, GENERO VARCHAR2(20) 
, TIPO_DOCUMENTO_PARTICIPANTE VARCHAR2(50) 
, DOCUMENTO_PARTICIPANTE VARCHAR2(50) 
, CODIGO_BENEFICIARIO VARCHAR2(50) 
, RUTA_ARCHIVO VARCHAR2(250) 
);

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.PERIODO IS 'Período del instrumento';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.DIVIPOLA IS 'Código de división política';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.NOMBRE_DEPARTAMENTO IS 'Nombre del departamento del estudiante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.NOMBRE_MUNICIPIO IS 'Nombre del municipio';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.SIGRA_GRUPO IS 'Sigla del grupo en el que se encontraba el participante al ser escogido';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.CEDULA_FACILITADOR IS 'Cédula del facilitador';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.NOMBRE_FACILITADOR IS 'Nombre del facilitador';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.PRIMERNOMBRE_PARTICIPANTE IS 'Nombre del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.SEGUNDONOMBRE_PARTICIPANTE IS 'Segundo nombre del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.PRIMERAPELLIDO_PARTICIPANTE IS 'Primer apellido del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.SEGUNDOAPELLIDO_PARTICIPANTE IS 'Segundo apeliido del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.GENERO IS 'Género del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.TIPO_DOCUMENTO_PARTICIPANTE IS 'Tipo de documento del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.DOCUMENTO_PARTICIPANTE IS 'Documento del participante';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.CODIGO_BENEFICIARIO IS 'Código del beneficiario';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.RUTA_ARCHIVO IS 'Ruta del archivo';

ALTER TABLE T116_RPT_SISTEMATIZACION 
ADD (FEC_CORTE DATE );

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.FEC_CORTE IS 'Fecha de corte del reporte';

ALTER TABLE T116_RPT_SISTEMATIZACION RENAME COLUMN SIGRA_GRUPO TO SIGLA_GRUPO;

UPDATE "HPV"."T62_REPORTE" SET A62ESTADO = 'I' WHERE A62CODIGO = '6'
UPDATE "HPV"."T62_REPORTE" SET A62ESTADO = 'I' WHERE A62CODIGO = '7'
UPDATE "HPV"."T62_REPORTE" SET A62ESTADO = 'I' WHERE A62CODIGO = '8'
INSERT INTO "HPV"."T62_REPORTE" (A62CODIGO, A62CATEGORIA, A62NOMBRE, A62DESCRIPCION, A62URL, A62ESTADO) VALUES ('15', 'Sistematizacion experiencias', 'Sistematización', 'Reporte Sistematización de experiencias', '/Reportes/Sistematizacion/Sistematizacion.aspx', 'A')

ALTER TABLE T116_RPT_SISTEMATIZACION 
ADD (TIPO_INSTRUMENTO VARCHAR2(225) );

ALTER TABLE T116_RPT_SISTEMATIZACION 
ADD (ESTADO_INSTRUMENTO VARCHAR2(50) );

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.TIPO_INSTRUMENTO IS 'Tipo de instrumento del registro';

COMMENT ON COLUMN T116_RPT_SISTEMATIZACION.ESTADO_INSTRUMENTO IS 'Estado del instrumento';
