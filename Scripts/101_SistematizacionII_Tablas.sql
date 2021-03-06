CREATE TABLE T65_CAT_PRIM_SIST 
(
  A65CODIGO NUMBER NOT NULL 
, A65TIPOINST NUMBER NOT NULL 
, A65DESCRIPCION VARCHAR2(250) NOT NULL 
, A65PERIODO NUMBER NOT NULL 
, A65FECHACREACION DATE 
, A65FECHAMODIFICACION DATE 
, CONSTRAINT T65_CAT_PRIM_SISTEMATIZACI_PK PRIMARY KEY 
  (
    A65CODIGO 
  )
  ENABLE 
);

ALTER TABLE T65_CAT_PRIM_SIST
ADD CONSTRAINT T65_CAT_PRIM_SIST_FK1 FOREIGN KEY
(
  A65TIPOINST 
)
REFERENCES T76_INSTRUMENTOSSIS
(
  A76CODIGO 
)
ENABLE;

ALTER TABLE T65_CAT_PRIM_SIST
ADD CONSTRAINT T65_CAT_PRIM_SIST_FK2 FOREIGN KEY
(
  A65PERIODO
)
REFERENCES T12_PERIODOS
(
  A12CODIGO 
)
ENABLE;

COMMENT ON COLUMN T65_CAT_PRIM_SIST.A65CODIGO IS 'Identificación del registro en la tabla';
COMMENT ON COLUMN T65_CAT_PRIM_SIST.A65DESCRIPCION IS 'Nombre de la categoria';
COMMENT ON COLUMN T65_CAT_PRIM_SIST.A65FECHACREACION IS 'Fecha de creacion del registro';
COMMENT ON COLUMN T65_CAT_PRIM_SIST.A65FECHAMODIFICACION IS 'Fecha de modificacion del registro';
COMMENT ON COLUMN T65_CAT_PRIM_SIST.A65TIPOINST IS 'Tipo de instrumento al que pertenece la categoria';
COMMENT ON COLUMN T65_CAT_PRIM_SIST.A65PERIODO IS 'Periodo de la vigencia de las categorizaciones';

CREATE SEQUENCE T65_CAT_PRIM_SIST_SEQ;

CREATE TRIGGER T65_CAT_PRIM_SIST_TRG 
BEFORE INSERT ON T65_CAT_PRIM_SIST 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.A65CODIGO IS NULL THEN
      SELECT T65_CAT_PRIM_SIST_SEQ.NEXTVAL INTO :NEW.A65CODIGO FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/

CREATE TABLE T66_CAT_SUB_SIST 
(
  A66CODIGO NUMBER NOT NULL 
, A66CATPRIM NUMBER NOT NULL 
, A66DESCRIPCION VARCHAR2(255) NOT NULL 
, A66FECHACREACION DATE 
, A66FECHAMODIFICACION DATE 
, CONSTRAINT T66_CAT_SUB_SIST_PK PRIMARY KEY 
  (
    A66CODIGO 
  )
  ENABLE 
);

ALTER TABLE T66_CAT_SUB_SIST
ADD CONSTRAINT T66_CAT_SUB_SIST_FK1 FOREIGN KEY
(
  A66CATPRIM
)
REFERENCES T65_CAT_PRIM_SIST
(
  A65CODIGO 
)
ENABLE;

COMMENT ON COLUMN T66_CAT_SUB_SIST.A66CODIGO IS 'Identificador del registro';

COMMENT ON COLUMN T66_CAT_SUB_SIST.A66CATPRIM IS 'Identificador de la categoria primaria asociada';

COMMENT ON COLUMN T66_CAT_SUB_SIST.A66DESCRIPCION IS 'Nombre de la subcategoria';

COMMENT ON COLUMN T66_CAT_SUB_SIST.A66FECHACREACION IS 'Fecha de creacion del registro';

COMMENT ON COLUMN T66_CAT_SUB_SIST.A66FECHAMODIFICACION IS 'Fecha de modificacion del registro';

CREATE SEQUENCE T66_CAT_SUB_SIST_SEQ;

CREATE TRIGGER T66_CAT_SUB_SIST_TRG 
BEFORE INSERT ON T66_CAT_SUB_SIST 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.A66CODIGO IS NULL THEN
      SELECT T66_CAT_SUB_SIST_SEQ.NEXTVAL INTO :NEW.A66CODIGO FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/

CREATE TABLE T67_SIST_CAT 
(
  A67CODIGO NUMBER NOT NULL 
, A67INSTRUMENTO NUMBER NOT NULL 
, A67SUBCATEGORIA NUMBER 
, A67DESCOTRA VARCHAR2(500) 
, A67FECHACREACION DATE
, A67USUARIOCREACION NUMBER
, A67FECHAMODIFICACION DATE
, A67USUARIOMODIFICACION NUMBER
, CONSTRAINT T67_SIST_CAT_PK PRIMARY KEY 
  (
    A67CODIGO 
  )
  ENABLE 
);

COMMENT ON COLUMN T67_SIST_CAT.A67CODIGO IS 'Identificador del registro';
COMMENT ON COLUMN T67_SIST_CAT.A67INSTRUMENTO IS 'Codigo del instrumento al que se asocia la subcategoria';
COMMENT ON COLUMN T67_SIST_CAT.A67SUBCATEGORIA IS 'Subcategoria asociada al instrumento, NULL en caso de que sea una no existente';
COMMENT ON COLUMN T67_SIST_CAT.A67DESCOTRA IS 'Descripcion en caso de que sea una categoria nueva';
COMMENT ON COLUMN T67_SIST_CAT.A67FECHACREACION IS 'Fecha en la que se crea el registro';
COMMENT ON COLUMN T67_SIST_CAT.A67USUARIOCREACION IS 'Usuario que crea el registro';
COMMENT ON COLUMN T67_SIST_CAT.A67FECHAMODIFICACION IS 'Ultima fecha en la que se modifica el registro';
COMMENT ON COLUMN T67_SIST_CAT.A67USUARIOMODIFICACION IS 'Ultimo usuario que modifica el registro';

CREATE SEQUENCE T67_SIST_CAT_SEQ;

CREATE TRIGGER T67_SIST_CAT_TRG 
BEFORE INSERT ON T67_SIST_CAT 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.A67CODIGO IS NULL THEN
      SELECT T67_SIST_CAT_SEQ.NEXTVAL INTO :NEW.A67CODIGO FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
