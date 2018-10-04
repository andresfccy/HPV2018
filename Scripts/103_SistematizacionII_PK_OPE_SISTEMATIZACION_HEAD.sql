create or replace package PK_OPE_SISTEMATIZACION is

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
                                         p_idInscrito               number,
                                         p_motivoRechazo            varchar2,
                                         p_codError                 OUT NUMBER,
                                         p_msjError                 OUT VARCHAR2);

  PROCEDURE Pr_consultarSistematizacion(p_IdSistematizacion IN number,
                                        p_resultado         OUT PK_UTILS.ty_cursor,
                                        p_codError          OUT NUMBER,
                                        p_msjError          OUT VARCHAR2);

  PROCEDURE Pr_ActualizarCategorizacion(p_idSistematizacion     IN NUMBER,
                                        p_listaCategorizaciones IN VARCHAR2,
                                        p_descotros             IN VARCHAR2 DEFAULT NULL,
                                        p_idUsuario             IN NUMBER,
                                        p_codError              OUT NUMBER,
                                        p_msjError              OUT VARCHAR2);
                                        
  PROCEDURE Pr_ConsultarCategorizacion(p_idSistematizacion IN NUMBER,
                                       p_resultado         OUT PK_UTILS.ty_cursor,
                                       p_codError          OUT NUMBER,
                                       p_msjError          OUT VARCHAR2);

  PROCEDURE Pr_DarCategorias(p_instrumento IN NUMBER,
                             p_periodo     IN NUMBER DEFAULT NULL,
                             p_resultado   OUT PK_UTILS.ty_cursor,
                             p_codError    OUT NUMBER,
                             p_msjError    OUT VARCHAR2);

  PROCEDURE Pr_DarSubCategorias(p_categoria IN NUMBER,
                                p_resultado OUT PK_UTILS.ty_cursor,
                                p_codError  OUT NUMBER,
                                p_msjError  OUT VARCHAR);
end PK_OPE_SISTEMATIZACION;