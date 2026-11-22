--------------------------------------------------------
-- Archivo creado  - domingo-noviembre-22-2026   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence SEQ_AGENDAID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_AGENDAID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 4 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_ARTICULOID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_ARTICULOID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 2 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_CARGOID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_CARGOID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 16 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_DETALLEFACTURAID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_DETALLEFACTURAID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 2 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_DETALLE_VEHICULO
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_DETALLE_VEHICULO"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 3 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_FACTURAID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_FACTURAID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 2 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_NOTIFICACIONID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_NOTIFICACIONID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 5 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Sequence SEQ_SOATID
--------------------------------------------------------

   CREATE SEQUENCE  "TALLER"."SEQ_SOATID"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 2 NOCACHE  NOORDER  NOCYCLE  NOKEEP  NOSCALE  GLOBAL ;
--------------------------------------------------------
--  DDL for Table AGENDA
--------------------------------------------------------

  CREATE TABLE "TALLER"."AGENDA" 
   (	"AGENDAID" NUMBER(*,0), 
	"FECHA" DATE, 
	"COMENTARIO" VARCHAR2(200 BYTE) COLLATE "USING_NLS_COMP", 
	"EMPLEADO_CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"ESTADO" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP", 
	"ARTICULO_ARTICULOID" NUMBER, 
	"VEHICULO_PLACA" VARCHAR2(6 CHAR) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table ARTICULO
--------------------------------------------------------

  CREATE TABLE "TALLER"."ARTICULO" 
   (	"ARTICULOID" NUMBER, 
	"NOMBRE" VARCHAR2(100 BYTE) COLLATE "USING_NLS_COMP", 
	"PRECIO" NUMBER(10,2), 
	"PORCENTAJEIVA" NUMBER(5,2), 
	"CANTIDAD" NUMBER, 
	"DESCRIPCION" VARCHAR2(200 BYTE) COLLATE "USING_NLS_COMP", 
	"DURACION" NUMBER, 
	"PROVEEDOR_NIT" VARCHAR2(15 BYTE) COLLATE "USING_NLS_COMP", 
	"TIPO_ARTICULO_ID" NUMBER(38,0)
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table CARGO
--------------------------------------------------------

  CREATE TABLE "TALLER"."CARGO" 
   (	"CARGOID" NUMBER(*,0), 
	"NOMBRECARGO" VARCHAR2(100 BYTE) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table CLIENTE
--------------------------------------------------------

  CREATE TABLE "TALLER"."CLIENTE" 
   (	"CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"TELEGRAM_CHAT_ID" NUMBER(19,0)
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table DETALLE_FACTURA
--------------------------------------------------------

  CREATE TABLE "TALLER"."DETALLE_FACTURA" 
   (	"DETALLEFACTURAID" NUMBER(*,0), 
	"CANTIDAD" NUMBER(*,0), 
	"VALORUNITARIO" NUMBER(10,2), 
	"VALORIVA" NUMBER(10,2), 
	"FACTURA_FACTURAID" NUMBER(*,0), 
	"ARTICULO_ARTICULOID" NUMBER, 
	"PORCENTAJEIVA" NUMBER(5,2), 
	"DESCRIPCION" VARCHAR2(200 BYTE) COLLATE "USING_NLS_COMP", 
	"SUBTOTAL" NUMBER(10,2), 
	"TOTAL" NUMBER(10,2), 
	"DESCUENTO" NUMBER(12,2) DEFAULT 0
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table DETALLE_VEHICULO
--------------------------------------------------------

  CREATE TABLE "TALLER"."DETALLE_VEHICULO" 
   (	"DETALLEVEHICULOID" NUMBER, 
	"FACTURA_FACTURAID" NUMBER, 
	"VEHICULO_PLACA" VARCHAR2(6 CHAR) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table EMPLEADO
--------------------------------------------------------

  CREATE TABLE "TALLER"."EMPLEADO" 
   (	"CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"CARGO_CARGOID" NUMBER(*,0), 
	"ESTADO" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP" DEFAULT 'Activo', 
	"CLAVE_HASH" VARCHAR2(200 BYTE) COLLATE "USING_NLS_COMP", 
	"ACTIVO" CHAR(1 BYTE) COLLATE "USING_NLS_COMP" DEFAULT 'S'
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table FACTURA
--------------------------------------------------------

  CREATE TABLE "TALLER"."FACTURA" 
   (	"FACTURAID" NUMBER(*,0), 
	"FECHA" DATE, 
	"DESCUENTO" NUMBER(10,2), 
	"SUBTOTAL" NUMBER(10,2), 
	"IVA" NUMBER(10,2), 
	"TOTAL" NUMBER(10,2), 
	"OBSERVACIONES" VARCHAR2(200 BYTE) COLLATE "USING_NLS_COMP", 
	"ESTADOPAGO" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP", 
	"CLIENTE_CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"EMPLEADO_CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"TIPO_DOCUMENTO" VARCHAR2(12 BYTE) COLLATE "USING_NLS_COMP" DEFAULT 'VENTA', 
	"DOMICILIO" CHAR(1 BYTE) COLLATE "USING_NLS_COMP" DEFAULT 'N', 
	"DIRECCION_DOMICILIO" VARCHAR2(150 BYTE) COLLATE "USING_NLS_COMP", 
	"AGENDA_AGENDAID" NUMBER
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table NOTIFICACION_MANTENIMIENTO
--------------------------------------------------------

  CREATE TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" 
   (	"NOTIFICACIONID" NUMBER(*,0), 
	"FECHA" DATE, 
	"COMENTARIO" VARCHAR2(200 BYTE) COLLATE "USING_NLS_COMP", 
	"ARTICULO_ARTICULOID" NUMBER, 
	"ESTADO" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP" DEFAULT 'PENDIENTE', 
	"VEHICULO_PLACA" VARCHAR2(6 CHAR) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table PERSONA
--------------------------------------------------------

  CREATE TABLE "TALLER"."PERSONA" 
   (	"CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"TELEFONO" VARCHAR2(15 BYTE) COLLATE "USING_NLS_COMP", 
	"DIRECCION" VARCHAR2(150 BYTE) COLLATE "USING_NLS_COMP", 
	"CORREOELECTRONICO" VARCHAR2(100 BYTE) COLLATE "USING_NLS_COMP", 
	"SEXO" CHAR(1 BYTE) COLLATE "USING_NLS_COMP", 
	"PRIMER_NOMBRE" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP", 
	"SEGUNDO_NOMBRE" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP", 
	"PRIMER_APELLIDO" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP", 
	"SEGUNDO_APELLIDO" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table PROVEEDOR
--------------------------------------------------------

  CREATE TABLE "TALLER"."PROVEEDOR" 
   (	"NIT" VARCHAR2(15 BYTE) COLLATE "USING_NLS_COMP", 
	"RAZONSOCIAL" VARCHAR2(100 BYTE) COLLATE "USING_NLS_COMP", 
	"TELEFONO" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table SOAT
--------------------------------------------------------

  CREATE TABLE "TALLER"."SOAT" 
   (	"SOATID" NUMBER, 
	"NUMEROSOAT" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP", 
	"FECHAINICIO" DATE, 
	"FECHAFIN" DATE, 
	"VEHICULO_PLACA" VARCHAR2(6 CHAR) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table TIPO_ARTICULO
--------------------------------------------------------

  CREATE TABLE "TALLER"."TIPO_ARTICULO" 
   (	"TIPO_ARTICULO_ID" NUMBER(38,0), 
	"NOMBRE" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
--------------------------------------------------------
--  DDL for Table VEHICULO
--------------------------------------------------------

  CREATE TABLE "TALLER"."VEHICULO" 
   (	"MODELO" NUMBER, 
	"MARCA" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP", 
	"CLIENTE_CEDULA" CHAR(10 BYTE) COLLATE "USING_NLS_COMP", 
	"TIPO_VEHICULO" VARCHAR2(20 BYTE) COLLATE "USING_NLS_COMP", 
	"COLOR" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP", 
	"LINEA" VARCHAR2(50 BYTE) COLLATE "USING_NLS_COMP", 
	"PLACA" VARCHAR2(6 CHAR) COLLATE "USING_NLS_COMP"
   )  DEFAULT COLLATION "USING_NLS_COMP" ;
REM INSERTING into TALLER.AGENDA
SET DEFINE OFF;
Insert into TALLER.AGENDA (AGENDAID,FECHA,COMENTARIO,EMPLEADO_CEDULA,ESTADO,ARTICULO_ARTICULOID,VEHICULO_PLACA) values (2,to_date('21/11/26','DD/MM/RR'),null,'9617964659','PENDIENTE',1,'ABC123');
Insert into TALLER.AGENDA (AGENDAID,FECHA,COMENTARIO,EMPLEADO_CEDULA,ESTADO,ARTICULO_ARTICULOID,VEHICULO_PLACA) values (3,to_date('23/11/26','DD/MM/RR'),null,'9617964659','PENDIENTE',1,'IHW962');
Insert into TALLER.AGENDA (AGENDAID,FECHA,COMENTARIO,EMPLEADO_CEDULA,ESTADO,ARTICULO_ARTICULOID,VEHICULO_PLACA) values (1,to_date('22/11/26','DD/MM/RR'),null,'9617964659','EN PROCESO',1,'ABC123');
REM INSERTING into TALLER.ARTICULO
SET DEFINE OFF;
Insert into TALLER.ARTICULO (ARTICULOID,NOMBRE,PRECIO,PORCENTAJEIVA,CANTIDAD,DESCRIPCION,DURACION,PROVEEDOR_NIT,TIPO_ARTICULO_ID) values (1,'Cambio de Aceite',500000,0,null,null,30,null,2);
REM INSERTING into TALLER.CARGO
SET DEFINE OFF;
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (1,'Administrador');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (2,'Gerente De Servicios');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (3,'Jefe De Taller');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (4,'Lider De Calidad');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (5,'Asesor De Servicios');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (6,'Practicante Aux Mecanico');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (7,'Electromecanico');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (8,'Latonero');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (9,'Pintor');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (10,'Asesor De Colision');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (11,'Tecnico Automotriz');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (12,'Recepcionista');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (13,'Chofer');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (14,'Lavador');
Insert into TALLER.CARGO (CARGOID,NOMBRECARGO) values (15,'Personal De Limpieza');
REM INSERTING into TALLER.CLIENTE
SET DEFINE OFF;
Insert into TALLER.CLIENTE (CEDULA,TELEGRAM_CHAT_ID) values ('1234567890',6179678416);
REM INSERTING into TALLER.DETALLE_FACTURA
SET DEFINE OFF;
Insert into TALLER.DETALLE_FACTURA (DETALLEFACTURAID,CANTIDAD,VALORUNITARIO,VALORIVA,FACTURA_FACTURAID,ARTICULO_ARTICULOID,PORCENTAJEIVA,DESCRIPCION,SUBTOTAL,TOTAL,DESCUENTO) values (1,1,500000,0,1,1,0,'Cambio de Aceite',500000,500000,0);
REM INSERTING into TALLER.DETALLE_VEHICULO
SET DEFINE OFF;
Insert into TALLER.DETALLE_VEHICULO (DETALLEVEHICULOID,FACTURA_FACTURAID,VEHICULO_PLACA) values (2,1,'ABC123');
REM INSERTING into TALLER.EMPLEADO
SET DEFINE OFF;
Insert into TALLER.EMPLEADO (CEDULA,CARGO_CARGOID,ESTADO,CLAVE_HASH,ACTIVO) values ('9617964659',1,'Activo','120000.o34GK/AWyDm6ByTJd8Nd0A==.DIb8Kr3g7U4zAox7WiD3VCtKxTlNkx5gmyZ5RZTznCQ=','S');
REM INSERTING into TALLER.FACTURA
SET DEFINE OFF;
Insert into TALLER.FACTURA (FACTURAID,FECHA,DESCUENTO,SUBTOTAL,IVA,TOTAL,OBSERVACIONES,ESTADOPAGO,CLIENTE_CEDULA,EMPLEADO_CEDULA,TIPO_DOCUMENTO,DOMICILIO,DIRECCION_DOMICILIO,AGENDA_AGENDAID) values (1,to_date('20/11/26','DD/MM/RR'),0,500000,0,500000,null,'Pendiente','1234567890','9617964659','VENTA','N',null,1);
REM INSERTING into TALLER.NOTIFICACION_MANTENIMIENTO
SET DEFINE OFF;
Insert into TALLER.NOTIFICACION_MANTENIMIENTO (NOTIFICACIONID,FECHA,COMENTARIO,ARTICULO_ARTICULOID,ESTADO,VEHICULO_PLACA) values (2,to_date('19/11/26','DD/MM/RR'),null,1,'ENVIADA','ABC123');
Insert into TALLER.NOTIFICACION_MANTENIMIENTO (NOTIFICACIONID,FECHA,COMENTARIO,ARTICULO_ARTICULOID,ESTADO,VEHICULO_PLACA) values (3,to_date('21/11/26','DD/MM/RR'),null,1,'ENVIADA','ABC123');
Insert into TALLER.NOTIFICACION_MANTENIMIENTO (NOTIFICACIONID,FECHA,COMENTARIO,ARTICULO_ARTICULOID,ESTADO,VEHICULO_PLACA) values (4,to_date('24/11/26','DD/MM/RR'),null,1,'ENVIADA','IHW962');
REM INSERTING into TALLER.PERSONA
SET DEFINE OFF;
Insert into TALLER.PERSONA (CEDULA,TELEFONO,DIRECCION,CORREOELECTRONICO,SEXO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO) values ('1234567890','3013632371','cr124hvo5','tobias.onate@gmail.com','M','Tobias','Andres','Oñate','Borja');
Insert into TALLER.PERSONA (CEDULA,TELEFONO,DIRECCION,CORREOELECTRONICO,SEXO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,PRIMER_APELLIDO,SEGUNDO_APELLIDO) values ('9617964659',null,null,null,'M','Administrador',null,'Inicial',null);
REM INSERTING into TALLER.PROVEEDOR
SET DEFINE OFF;
REM INSERTING into TALLER.SOAT
SET DEFINE OFF;
Insert into TALLER.SOAT (SOATID,NUMEROSOAT,FECHAINICIO,FECHAFIN,VEHICULO_PLACA) values (1,'1012134556789',to_date('29/09/26','DD/MM/RR'),to_date('29/09/27','DD/MM/RR'),'IHW962');
REM INSERTING into TALLER.TIPO_ARTICULO
SET DEFINE OFF;
Insert into TALLER.TIPO_ARTICULO (TIPO_ARTICULO_ID,NOMBRE) values (1,'Repuesto');
Insert into TALLER.TIPO_ARTICULO (TIPO_ARTICULO_ID,NOMBRE) values (2,'Servicio');
REM INSERTING into TALLER.VEHICULO
SET DEFINE OFF;
Insert into TALLER.VEHICULO (MODELO,MARCA,CLIENTE_CEDULA,TIPO_VEHICULO,COLOR,LINEA,PLACA) values (2020,'FORD','1234567890','Automovil','AZUL','ECOSPORT','ABC123');
Insert into TALLER.VEHICULO (MODELO,MARCA,CLIENTE_CEDULA,TIPO_VEHICULO,COLOR,LINEA,PLACA) values (2016,'FORD','1234567890','Automovil','ROJO','ECOSPORT','IHW962');
--------------------------------------------------------
--  DDL for Trigger TRG_AGENDA_NORMALIZAR_ESTADO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "TALLER"."TRG_AGENDA_NORMALIZAR_ESTADO" 
BEFORE INSERT OR UPDATE ON TALLER.AGENDA
FOR EACH ROW
BEGIN
  :NEW.ESTADO := NVL(UPPER(TRIM(:NEW.ESTADO)), 'PENDIENTE');
END TRG_AGENDA_NORMALIZAR_ESTADO;

/
ALTER TRIGGER "TALLER"."TRG_AGENDA_NORMALIZAR_ESTADO" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRG_FACTURA_VALIDAR_DOMICILIO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "TALLER"."TRG_FACTURA_VALIDAR_DOMICILIO" 
BEFORE INSERT OR UPDATE ON TALLER.FACTURA
FOR EACH ROW
BEGIN
  :NEW.DOMICILIO           := NVL(UPPER(TRIM(:NEW.DOMICILIO)), 'N');
  :NEW.DIRECCION_DOMICILIO := TRIM(:NEW.DIRECCION_DOMICILIO);

  IF :NEW.DOMICILIO NOT IN ('S', 'N') THEN
    RAISE_APPLICATION_ERROR(-20030, 'El campo DOMICILIO solo admite los valores S o N.');
  END IF;

  IF :NEW.DOMICILIO = 'N' THEN
    :NEW.DIRECCION_DOMICILIO := NULL;
  ELSIF :NEW.DIRECCION_DOMICILIO IS NULL THEN
    RAISE_APPLICATION_ERROR(-20031, 'La factura con domicilio (S) requiere una direccion de domicilio.');
  END IF;
END TRG_FACTURA_VALIDAR_DOMICILIO;

/
ALTER TRIGGER "TALLER"."TRG_FACTURA_VALIDAR_DOMICILIO" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRG_SOAT_VALIDAR_VIGENCIA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "TALLER"."TRG_SOAT_VALIDAR_VIGENCIA" 
BEFORE INSERT OR UPDATE ON TALLER.SOAT
FOR EACH ROW
BEGIN
  IF :NEW.FECHAINICIO IS NULL OR :NEW.FECHAFIN IS NULL THEN
    RAISE_APPLICATION_ERROR(-20032, 'El SOAT requiere fecha de inicio y fecha de fin.');
  END IF;

  IF :NEW.FECHAFIN <= :NEW.FECHAINICIO THEN
    RAISE_APPLICATION_ERROR(-20033, 'La fecha de fin del SOAT debe ser posterior a la fecha de inicio.');
  END IF;

  IF :NEW.FECHAFIN > ADD_MONTHS(:NEW.FECHAINICIO, 12) THEN
    RAISE_APPLICATION_ERROR(-20034, 'La vigencia del SOAT no puede superar 12 meses.');
  END IF;
END TRG_SOAT_VALIDAR_VIGENCIA;

/
ALTER TRIGGER "TALLER"."TRG_SOAT_VALIDAR_VIGENCIA" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRG_VEHICULO_FORMATEAR_DATOS
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "TALLER"."TRG_VEHICULO_FORMATEAR_DATOS" 
BEFORE INSERT OR UPDATE ON TALLER.VEHICULO
FOR EACH ROW
BEGIN
  IF :NEW.MARCA IS NOT NULL THEN
    :NEW.MARCA := UPPER(TRIM(:NEW.MARCA));
  END IF;

  IF :NEW.COLOR IS NOT NULL THEN
    :NEW.COLOR := UPPER(TRIM(:NEW.COLOR));
  END IF;
END TRG_VEHICULO_FORMATEAR_DATOS;

/
ALTER TRIGGER "TALLER"."TRG_VEHICULO_FORMATEAR_DATOS" ENABLE;
--------------------------------------------------------
--  DDL for Package PKG_AGENDA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_AGENDA" AS
  PROCEDURE GUARDAR(
    p_id IN OUT NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER);
  PROCEDURE MODIFICAR(
    p_id NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER);
  PROCEDURE CAMBIAR_ESTADO(p_id NUMBER, p_estado VARCHAR2);
  PROCEDURE ELIMINAR(p_id NUMBER);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE CONTAR_PENDIENTES(p_total OUT NUMBER);
  PROCEDURE VALIDAR_DISPONIBILIDAD(
    p_empleado CHAR, p_fecha DATE, p_disponible OUT NUMBER);
  PROCEDURE PRC_GUARDAR(
    p_id IN OUT NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER);
  PROCEDURE PRC_MODIFICAR(
    p_id NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER);
  PROCEDURE PRC_CAMBIAR_ESTADO(p_id NUMBER, p_estado VARCHAR2);
  PROCEDURE PRC_ELIMINAR(p_id NUMBER);
  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_CONTAR_PENDIENTES(p_total OUT NUMBER);
  PROCEDURE PRC_VALIDAR_DISPONIBILIDAD(
    p_empleado CHAR, p_fecha DATE, p_disponible OUT NUMBER);
END PKG_AGENDA;

/
--------------------------------------------------------
--  DDL for Package PKG_ARTICULO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_ARTICULO" AS
  PROCEDURE GUARDAR(p_id OUT NUMBER,p_nombre VARCHAR2,p_precio NUMBER,p_iva NUMBER,p_cantidad NUMBER,
    p_descripcion VARCHAR2,p_duracion NUMBER,p_proveedor VARCHAR2,p_tipo VARCHAR2);
  PROCEDURE MODIFICAR(p_id NUMBER,p_nombre VARCHAR2,p_precio NUMBER,p_iva NUMBER,p_cantidad NUMBER,
    p_descripcion VARCHAR2,p_duracion NUMBER,p_proveedor VARCHAR2,p_tipo VARCHAR2);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER,p_resultado OUT SYS_REFCURSOR);
  PROCEDURE CONTAR(p_total OUT NUMBER);
  PROCEDURE TIENE_MOVIMIENTOS(p_id NUMBER,p_resultado OUT NUMBER);
  PROCEDURE OBTENER_TIPO(p_id NUMBER,p_tipo OUT VARCHAR2);
  PROCEDURE DESCONTAR_STOCK(p_id NUMBER,p_cantidad NUMBER);
  PROCEDURE RESTAURAR_STOCK(p_id NUMBER,p_cantidad NUMBER);
  PROCEDURE ELIMINAR(p_id NUMBER);
END PKG_ARTICULO;

/
--------------------------------------------------------
--  DDL for Package PKG_CARGO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_CARGO" AS
  PROCEDURE SIGUIENTE_ID(p_id OUT NUMBER);
  PROCEDURE GUARDAR(p_id NUMBER, p_nombre VARCHAR2);
  PROCEDURE MODIFICAR(p_id NUMBER, p_nombre VARCHAR2);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE ELIMINAR(p_id NUMBER);
END PKG_CARGO;

/
--------------------------------------------------------
--  DDL for Package PKG_CLIENTE
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_CLIENTE" AS
  PROCEDURE PRC_GUARDAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2);
  PROCEDURE PRC_MODIFICAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2);
  PROCEDURE PRC_ELIMINAR(p_cedula CHAR);
  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_BUSCAR_CEDULA(p_cedula CHAR, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_CONTAR(p_total OUT NUMBER);
  PROCEDURE PRC_VINCULAR_TELEGRAM(p_cedula CHAR, p_chat_id NUMBER);
  PROCEDURE PRC_BUSCAR_POR_TELEGRAM_CHAT_ID(
    p_chat_id NUMBER, p_resultado OUT SYS_REFCURSOR);
END PKG_CLIENTE;

/
--------------------------------------------------------
--  DDL for Package PKG_DETALLE_FACTURA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_DETALLE_FACTURA" AS
  PROCEDURE GUARDAR(p_id OUT NUMBER, p_cantidad NUMBER, p_valor_unitario NUMBER,
    p_valor_iva NUMBER, p_factura_id NUMBER, p_articulo_id NUMBER,
    p_porcentaje_iva NUMBER, p_descripcion VARCHAR2, p_subtotal NUMBER,
    p_total NUMBER, p_descuento NUMBER);
  PROCEDURE MODIFICAR(p_id NUMBER, p_cantidad NUMBER, p_valor_unitario NUMBER,
    p_valor_iva NUMBER, p_articulo_id NUMBER, p_porcentaje_iva NUMBER,
    p_descripcion VARCHAR2, p_subtotal NUMBER, p_total NUMBER, p_descuento NUMBER);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE CONSULTAR_POR_FACTURA(p_factura_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE ELIMINAR(p_id NUMBER);
END PKG_DETALLE_FACTURA;

/
--------------------------------------------------------
--  DDL for Package PKG_DETALLE_VEHICULO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_DETALLE_VEHICULO" AS
  PROCEDURE GUARDAR(p_id IN OUT NUMBER, p_factura_id NUMBER, p_placa VARCHAR2);
  PROCEDURE MODIFICAR(p_id NUMBER, p_factura_id NUMBER, p_placa VARCHAR2);
  PROCEDURE ELIMINAR(p_id NUMBER);
  PROCEDURE CONSULTAR_POR_FACTURA(
    p_factura_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR);
END PKG_DETALLE_VEHICULO;

/
--------------------------------------------------------
--  DDL for Package PKG_EMPLEADO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_EMPLEADO" AS
  PROCEDURE GUARDAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR);
  PROCEDURE MODIFICAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR);
  PROCEDURE ELIMINAR(p_cedula CHAR);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_CEDULA(p_cedula CHAR, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE CONTAR(p_total OUT NUMBER);
  PROCEDURE OBTENER_CLAVE(p_cedula CHAR, p_hash OUT VARCHAR2);
  PROCEDURE GUARDAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2);
  PROCEDURE CAMBIAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2);
  PROCEDURE PRC_GUARDAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR);
  PROCEDURE PRC_MODIFICAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR);
  PROCEDURE PRC_ELIMINAR(p_cedula CHAR);
  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_BUSCAR_CEDULA(p_cedula CHAR, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_CONTAR(p_total OUT NUMBER);
  PROCEDURE PRC_OBTENER_CLAVE(p_cedula CHAR, p_hash OUT VARCHAR2);
  PROCEDURE PRC_GUARDAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2);
  PROCEDURE PRC_CAMBIAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2);
END PKG_EMPLEADO;

/
--------------------------------------------------------
--  DDL for Package PKG_FACTURA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_FACTURA" AS
  PROCEDURE SIGUIENTE_ID(p_id OUT NUMBER);
  PROCEDURE GUARDAR(p_id OUT NUMBER, p_fecha DATE, p_descuento NUMBER, p_subtotal NUMBER,
    p_iva NUMBER, p_total NUMBER, p_observaciones VARCHAR2, p_estado_pago VARCHAR2,
    p_cliente CHAR, p_empleado CHAR, p_tipo_documento VARCHAR2, p_domicilio CHAR,
    p_direccion_domicilio VARCHAR2, p_agenda_id NUMBER);
  PROCEDURE MODIFICAR(p_id NUMBER, p_fecha DATE, p_descuento NUMBER, p_subtotal NUMBER,
    p_iva NUMBER, p_total NUMBER, p_observaciones VARCHAR2, p_estado_pago VARCHAR2,
    p_cliente CHAR, p_empleado CHAR, p_tipo_documento VARCHAR2, p_domicilio CHAR,
    p_direccion_domicilio VARCHAR2, p_agenda_id NUMBER);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE TOTAL_VENTAS(p_total OUT NUMBER);
  PROCEDURE VALIDAR_CITA(p_agenda_id NUMBER, p_placa VARCHAR2, p_cliente CHAR);
  PROCEDURE COMPLETAR_CITA(p_agenda_id NUMBER);
  PROCEDURE CONVERTIR_COTIZACION(p_id NUMBER);
  PROCEDURE ANULAR_DOCUMENTO(p_id NUMBER);
  PROCEDURE ELIMINAR(p_id NUMBER);
END PKG_FACTURA;

/
--------------------------------------------------------
--  DDL for Package PKG_NOTIFICACION_MANTENIMIENTO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_NOTIFICACION_MANTENIMIENTO" AS
  PROCEDURE GUARDAR(p_id OUT NUMBER,p_fecha DATE,p_comentario VARCHAR2,p_estado VARCHAR2,p_placa VARCHAR2,p_articulo_id NUMBER);
  PROCEDURE MODIFICAR(p_id NUMBER,p_fecha DATE,p_comentario VARCHAR2,p_estado VARCHAR2,p_placa VARCHAR2,p_articulo_id NUMBER);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER,p_resultado OUT SYS_REFCURSOR);
  PROCEDURE ELIMINAR(p_id NUMBER);
END PKG_NOTIFICACION_MANTENIMIENTO;

/
--------------------------------------------------------
--  DDL for Package PKG_PERSONA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_PERSONA" AS
  PROCEDURE PR_INSERTAR_PERSONA (
    p_cedula             IN  PERSONA.CEDULA%TYPE,
    p_primer_nombre      IN  PERSONA.PRIMER_NOMBRE%TYPE,
    p_segundo_nombre     IN  PERSONA.SEGUNDO_NOMBRE%TYPE,
    p_primer_apellido    IN  PERSONA.PRIMER_APELLIDO%TYPE,
    p_segundo_apellido   IN  PERSONA.SEGUNDO_APELLIDO%TYPE,
    p_telefono           IN  PERSONA.TELEFONO%TYPE,
    p_direccion          IN  PERSONA.DIRECCION%TYPE,
    p_correo_electronico IN  PERSONA.CORREOELECTRONICO%TYPE,
    p_sexo               IN  PERSONA.SEXO%TYPE
  );
  PROCEDURE PR_ACTUALIZAR_PERSONA (
    p_cedula             IN  PERSONA.CEDULA%TYPE,
    p_primer_nombre      IN  PERSONA.PRIMER_NOMBRE%TYPE,
    p_segundo_nombre     IN  PERSONA.SEGUNDO_NOMBRE%TYPE,
    p_primer_apellido    IN  PERSONA.PRIMER_APELLIDO%TYPE,
    p_segundo_apellido   IN  PERSONA.SEGUNDO_APELLIDO%TYPE,
    p_telefono           IN  PERSONA.TELEFONO%TYPE,
    p_direccion          IN  PERSONA.DIRECCION%TYPE,
    p_correo_electronico IN  PERSONA.CORREOELECTRONICO%TYPE,
    p_sexo               IN  PERSONA.SEXO%TYPE
  );
  PROCEDURE PR_ELIMINAR_PERSONA (
    p_cedula IN PERSONA.CEDULA%TYPE
  );
  FUNCTION FN_OBTENER_PERSONA_POR_CEDULA (
    p_cedula IN PERSONA.CEDULA%TYPE
  ) RETURN SYS_REFCURSOR;
  FUNCTION FN_OBTENER_PERSONAS
    RETURN SYS_REFCURSOR;
END PKG_PERSONA;

/
--------------------------------------------------------
--  DDL for Package PKG_PROVEEDOR
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_PROVEEDOR" AS
  PROCEDURE GUARDAR(p_nit VARCHAR2,p_razon_social VARCHAR2,p_telefono VARCHAR2);
  PROCEDURE MODIFICAR(p_nit VARCHAR2,p_razon_social VARCHAR2,p_telefono VARCHAR2);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_NIT(p_nit VARCHAR2,p_resultado OUT SYS_REFCURSOR);
  PROCEDURE ELIMINAR(p_nit VARCHAR2);
END PKG_PROVEEDOR;

/
--------------------------------------------------------
--  DDL for Package PKG_SOAT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_SOAT" AS
  PROCEDURE GUARDAR(p_id OUT NUMBER,p_numero VARCHAR2,p_inicio DATE,p_fin DATE,p_placa VARCHAR2);
  PROCEDURE MODIFICAR(p_id NUMBER,p_numero VARCHAR2,p_inicio DATE,p_fin DATE,p_placa VARCHAR2);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_ID(p_id NUMBER,p_resultado OUT SYS_REFCURSOR);
  PROCEDURE ELIMINAR(p_id NUMBER);
END PKG_SOAT;

/
--------------------------------------------------------
--  DDL for Package PKG_VEHICULO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "TALLER"."PKG_VEHICULO" AS
  PROCEDURE GUARDAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2);
  PROCEDURE MODIFICAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2);
  PROCEDURE ELIMINAR(p_placa VARCHAR2);
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE BUSCAR_PLACA(p_placa VARCHAR2, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE CONTAR(p_total OUT NUMBER);
  PROCEDURE PRC_GUARDAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2);
  PROCEDURE PRC_MODIFICAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2);
  PROCEDURE PRC_ELIMINAR(p_placa VARCHAR2);
  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_BUSCAR_PLACA(p_placa VARCHAR2, p_resultado OUT SYS_REFCURSOR);
  PROCEDURE PRC_CONTAR(p_total OUT NUMBER);
END PKG_VEHICULO;

/
--------------------------------------------------------
--  DDL for Function FN_VALIDAR_DISPONIBILIDAD_EMPLEADO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE FUNCTION "TALLER"."FN_VALIDAR_DISPONIBILIDAD_EMPLEADO" (
  p_cedula_empleado TALLER.EMPLEADO.CEDULA%TYPE,
  p_fecha_cita TALLER.AGENDA.FECHA%TYPE,
  p_duracion_minutos NUMBER DEFAULT 1,
  p_agenda_id TALLER.AGENDA.AGENDAID%TYPE DEFAULT NULL
) RETURN NUMBER IS
  v_total NUMBER;
BEGIN
  IF TRIM(p_cedula_empleado) IS NULL OR p_fecha_cita IS NULL
     OR p_duracion_minutos IS NULL OR p_duracion_minutos < 1 THEN
    RETURN 0;
  END IF;

  SELECT COUNT(*) INTO v_total
    FROM TALLER.AGENDA A
    LEFT JOIN TALLER.ARTICULO S ON S.ARTICULOID = A.ARTICULO_ARTICULOID
   WHERE TRIM(A.EMPLEADO_CEDULA) = TRIM(p_cedula_empleado)
     AND (p_agenda_id IS NULL OR A.AGENDAID <> p_agenda_id)
     AND UPPER(TRIM(A.ESTADO)) <> 'CANCELADA'
     AND (A.FECHA IS NULL
          OR (A.FECHA < p_fecha_cita + p_duracion_minutos / 1440
              AND p_fecha_cita < A.FECHA + NVL(S.DURACION, 1) / 1440));

  RETURN CASE WHEN v_total = 0 THEN 1 ELSE 0 END;
END;
/
--------------------------------------------------------
--  DDL for Package Body PKG_AGENDA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_AGENDA" AS
  PROCEDURE VALIDAR(
    p_id NUMBER, p_fecha DATE, p_placa VARCHAR2, p_empleado CHAR,
    p_estado VARCHAR2, p_articulo NUMBER) IS
    v_total    NUMBER;
    v_duracion NUMBER;
    v_activo   CHAR(1);
  BEGIN
    IF p_estado IS NULL
       OR p_estado NOT IN ('PENDIENTE', 'EN PROCESO', 'COMPLETADA', 'CANCELADA') THEN
      RAISE_APPLICATION_ERROR(-20004, 'Estado de cita inválido.');
    END IF;

    IF p_estado = 'CANCELADA' THEN
      RETURN;
    END IF;

    IF p_fecha IS NULL OR p_articulo IS NULL THEN
      RAISE_APPLICATION_ERROR(-20005, 'La cita requiere fecha y servicio.');
    END IF;

    SELECT COUNT(*)
      INTO v_total
      FROM TALLER.ARTICULO A
      JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
     WHERE A.ARTICULOID = p_articulo
       AND UPPER(T.NOMBRE) = 'SERVICIO'
       AND A.DURACION >= 1;
    IF v_total = 0 THEN
      RAISE_APPLICATION_ERROR(-20008, 'El servicio no existe o no tiene duración válida.');
    END IF;

    SELECT A.DURACION
      INTO v_duracion
      FROM TALLER.ARTICULO A
     WHERE A.ARTICULOID = p_articulo;

    SELECT COUNT(*) INTO v_total FROM TALLER.EMPLEADO WHERE TRIM(CEDULA) = TRIM(p_empleado);
    IF v_total = 0 THEN
      RAISE_APPLICATION_ERROR(-20008, 'El empleado no existe.');
    END IF;

    SELECT ACTIVO INTO v_activo FROM TALLER.EMPLEADO WHERE TRIM(CEDULA) = TRIM(p_empleado);
    IF v_activo <> 'S' THEN
      RAISE_APPLICATION_ERROR(-20006, 'El empleado está inactivo.');
    END IF;

    IF TALLER.FN_VALIDAR_DISPONIBILIDAD_EMPLEADO(p_empleado, p_fecha, v_duracion, p_id) = 0 THEN
      RAISE_APPLICATION_ERROR(-20007,
        'El empleado o vehículo ya tiene una cita en ese horario.');
    END IF;

    SELECT COUNT(*)
      INTO v_total
      FROM TALLER.AGENDA A
      LEFT JOIN TALLER.ARTICULO S ON S.ARTICULOID = A.ARTICULO_ARTICULOID
     WHERE A.AGENDAID <> NVL(p_id, 0)
       AND UPPER(TRIM(A.ESTADO)) <> 'CANCELADA'
       AND TRIM(A.VEHICULO_PLACA) = TRIM(p_placa)
       AND (A.FECHA IS NULL
            OR (A.FECHA < p_fecha + v_duracion / 1440
                AND p_fecha < A.FECHA + NVL(S.DURACION, 1) / 1440));
    IF v_total > 0 THEN
      RAISE_APPLICATION_ERROR(-20007,
        'El empleado o vehículo ya tiene una cita en ese horario.');
    END IF;
  END;

  PROCEDURE GUARDAR(
    p_id IN OUT NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER) IS
  BEGIN
    SAVEPOINT SP_AGENDA;
    VALIDAR(NULL, p_fecha, p_placa, p_empleado, p_estado, p_articulo);
    SELECT TALLER.SEQ_AGENDAID.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.AGENDA
      (AGENDAID, FECHA, COMENTARIO, VEHICULO_PLACA, EMPLEADO_CEDULA,
       ESTADO, ARTICULO_ARTICULOID)
    VALUES
      (p_id, p_fecha, p_comentario, p_placa, p_empleado, p_estado, p_articulo);
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_AGENDA;
      RAISE;
  END;

  PROCEDURE MODIFICAR(
    p_id NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER) IS
  BEGIN
    SAVEPOINT SP_AGENDA;
    VALIDAR(p_id, p_fecha, p_placa, p_empleado, p_estado, p_articulo);
    UPDATE TALLER.AGENDA
       SET FECHA               = p_fecha,
           COMENTARIO          = p_comentario,
           VEHICULO_PLACA      = TRIM(p_placa),
           EMPLEADO_CEDULA     = TRIM(p_empleado),
           ESTADO              = p_estado,
           ARTICULO_ARTICULOID = p_articulo
     WHERE AGENDAID = p_id;
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe la cita.');
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_AGENDA;
      RAISE;
  END;

  PROCEDURE CAMBIAR_ESTADO(p_id NUMBER, p_estado VARCHAR2) IS
    v_total    NUMBER;
    v_fecha    DATE;
    v_placa    VARCHAR2(6);
    v_empleado CHAR(10);
    v_articulo NUMBER;
  BEGIN
    SAVEPOINT SP_AGENDA;
    SELECT COUNT(*) INTO v_total FROM TALLER.AGENDA WHERE AGENDAID = p_id;
    IF v_total = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe la cita.');
    END IF;
    SELECT FECHA, VEHICULO_PLACA, EMPLEADO_CEDULA, ARTICULO_ARTICULOID
      INTO v_fecha, v_placa, v_empleado, v_articulo
      FROM TALLER.AGENDA
     WHERE AGENDAID = p_id;
    VALIDAR(p_id, v_fecha, v_placa, v_empleado, p_estado, v_articulo);
    UPDATE TALLER.AGENDA SET ESTADO = p_estado WHERE AGENDAID = p_id;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_AGENDA;
      RAISE;
  END;

  PROCEDURE ELIMINAR(p_id NUMBER) IS
  BEGIN
    DELETE FROM TALLER.AGENDA WHERE AGENDAID = p_id;
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe la cita.');
    END IF;
  END;

  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT A.AGENDAID, A.FECHA, A.COMENTARIO, A.VEHICULO_PLACA,
             A.EMPLEADO_CEDULA, A.ESTADO, A.ARTICULO_ARTICULOID,
             CL.CEDULA            CLIENTE_CEDULA,
             VP.PRIMER_NOMBRE     CLIENTE_PRIMER_NOMBRE,
             VP.SEGUNDO_NOMBRE    CLIENTE_SEGUNDO_NOMBRE,
             VP.PRIMER_APELLIDO   CLIENTE_PRIMER_APELLIDO,
             VP.SEGUNDO_APELLIDO  CLIENTE_SEGUNDO_APELLIDO,
             EP.PRIMER_NOMBRE     EMPLEADO_PRIMER_NOMBRE,
             EP.SEGUNDO_NOMBRE    EMPLEADO_SEGUNDO_NOMBRE,
             EP.PRIMER_APELLIDO   EMPLEADO_PRIMER_APELLIDO,
             EP.SEGUNDO_APELLIDO  EMPLEADO_SEGUNDO_APELLIDO,
             C.CARGOID, C.NOMBRECARGO,
             AR.NOMBRE            NOMBRE_ARTICULO,
             AR.DURACION          DURACION_SERVICIO
        FROM TALLER.AGENDA   A
        JOIN TALLER.VEHICULO V  ON TRIM(V.PLACA)   = TRIM(A.VEHICULO_PLACA)
        JOIN TALLER.CLIENTE  CL ON TRIM(CL.CEDULA) = TRIM(V.CLIENTE_CEDULA)
        JOIN TALLER.PERSONA  VP ON TRIM(VP.CEDULA) = TRIM(CL.CEDULA)
        JOIN TALLER.EMPLEADO E  ON TRIM(E.CEDULA)  = TRIM(A.EMPLEADO_CEDULA)
        JOIN TALLER.PERSONA  EP ON TRIM(EP.CEDULA) = TRIM(E.CEDULA)
        JOIN TALLER.CARGO    C  ON C.CARGOID = E.CARGO_CARGOID
        LEFT JOIN TALLER.ARTICULO AR ON AR.ARTICULOID = A.ARTICULO_ARTICULOID
       ORDER BY A.FECHA;
  END;

  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT A.AGENDAID, A.FECHA, A.COMENTARIO, A.VEHICULO_PLACA,
             A.EMPLEADO_CEDULA, A.ESTADO, A.ARTICULO_ARTICULOID,
             CL.CEDULA            CLIENTE_CEDULA,
             VP.PRIMER_NOMBRE     CLIENTE_PRIMER_NOMBRE,
             VP.SEGUNDO_NOMBRE    CLIENTE_SEGUNDO_NOMBRE,
             VP.PRIMER_APELLIDO   CLIENTE_PRIMER_APELLIDO,
             VP.SEGUNDO_APELLIDO  CLIENTE_SEGUNDO_APELLIDO,
             EP.PRIMER_NOMBRE     EMPLEADO_PRIMER_NOMBRE,
             EP.SEGUNDO_NOMBRE    EMPLEADO_SEGUNDO_NOMBRE,
             EP.PRIMER_APELLIDO   EMPLEADO_PRIMER_APELLIDO,
             EP.SEGUNDO_APELLIDO  EMPLEADO_SEGUNDO_APELLIDO,
             C.CARGOID, C.NOMBRECARGO,
             AR.NOMBRE            NOMBRE_ARTICULO,
             AR.DURACION          DURACION_SERVICIO
        FROM TALLER.AGENDA   A
        JOIN TALLER.VEHICULO V  ON TRIM(V.PLACA)   = TRIM(A.VEHICULO_PLACA)
        JOIN TALLER.CLIENTE  CL ON TRIM(CL.CEDULA) = TRIM(V.CLIENTE_CEDULA)
        JOIN TALLER.PERSONA  VP ON TRIM(VP.CEDULA) = TRIM(CL.CEDULA)
        JOIN TALLER.EMPLEADO E  ON TRIM(E.CEDULA)  = TRIM(A.EMPLEADO_CEDULA)
        JOIN TALLER.PERSONA  EP ON TRIM(EP.CEDULA) = TRIM(E.CEDULA)
        JOIN TALLER.CARGO    C  ON C.CARGOID = E.CARGO_CARGOID
        LEFT JOIN TALLER.ARTICULO AR ON AR.ARTICULOID = A.ARTICULO_ARTICULOID
       WHERE A.AGENDAID = p_id;
  END;

  PROCEDURE CONTAR_PENDIENTES(p_total OUT NUMBER) IS
  BEGIN
    SELECT COUNT(*) INTO p_total FROM TALLER.AGENDA WHERE UPPER(ESTADO) = 'PENDIENTE';
  END;

  PROCEDURE VALIDAR_DISPONIBILIDAD(
    p_empleado CHAR, p_fecha DATE, p_disponible OUT NUMBER) IS
  BEGIN
    p_disponible := TALLER.FN_VALIDAR_DISPONIBILIDAD_EMPLEADO(p_empleado, p_fecha);
  END;

  PROCEDURE PRC_GUARDAR(
    p_id IN OUT NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER) IS
  BEGIN
    GUARDAR(p_id, p_fecha, p_comentario, p_placa, p_empleado, p_estado, p_articulo);
  END;

  PROCEDURE PRC_MODIFICAR(
    p_id NUMBER, p_fecha DATE, p_comentario VARCHAR2, p_placa VARCHAR2,
    p_empleado CHAR, p_estado VARCHAR2, p_articulo NUMBER) IS
  BEGIN
    MODIFICAR(p_id, p_fecha, p_comentario, p_placa, p_empleado, p_estado, p_articulo);
  END;

  PROCEDURE PRC_CAMBIAR_ESTADO(p_id NUMBER, p_estado VARCHAR2) IS
  BEGIN
    CAMBIAR_ESTADO(p_id, p_estado);
  END;

  PROCEDURE PRC_ELIMINAR(p_id NUMBER) IS
  BEGIN
    ELIMINAR(p_id);
  END;

  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    CONSULTAR(p_resultado);
  END;

  PROCEDURE PRC_BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    BUSCAR_ID(p_id, p_resultado);
  END;

  PROCEDURE PRC_CONTAR_PENDIENTES(p_total OUT NUMBER) IS
  BEGIN
    CONTAR_PENDIENTES(p_total);
  END;

  PROCEDURE PRC_VALIDAR_DISPONIBILIDAD(
    p_empleado CHAR, p_fecha DATE, p_disponible OUT NUMBER) IS
  BEGIN
    VALIDAR_DISPONIBILIDAD(p_empleado, p_fecha, p_disponible);
  END;
END PKG_AGENDA;

/
--------------------------------------------------------
--  DDL for Package Body PKG_ARTICULO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_ARTICULO" AS
  PROCEDURE OBTENER_TIPO_ID(p_tipo VARCHAR2,p_tipo_id OUT NUMBER) IS
  BEGIN SELECT TIPO_ARTICULO_ID INTO p_tipo_id FROM TALLER.TIPO_ARTICULO WHERE UPPER(NOMBRE)=UPPER(p_tipo);
  EXCEPTION WHEN NO_DATA_FOUND THEN RAISE_APPLICATION_ERROR(-20020,'El tipo de artículo no existe.'); END;

  PROCEDURE GUARDAR(p_id OUT NUMBER,p_nombre VARCHAR2,p_precio NUMBER,p_iva NUMBER,p_cantidad NUMBER,
    p_descripcion VARCHAR2,p_duracion NUMBER,p_proveedor VARCHAR2,p_tipo VARCHAR2) IS v_tipo_id NUMBER;
  BEGIN
    OBTENER_TIPO_ID(p_tipo,v_tipo_id);
    IF UPPER(p_tipo)='REPUESTO' AND p_proveedor IS NULL THEN RAISE_APPLICATION_ERROR(-20021,'El repuesto debe tener proveedor.'); END IF;
    SELECT TALLER.SEQ_ARTICULOID.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.ARTICULO(ARTICULOID,NOMBRE,PRECIO,PORCENTAJEIVA,CANTIDAD,DESCRIPCION,DURACION,PROVEEDOR_NIT,TIPO_ARTICULO_ID)
    VALUES(p_id,p_nombre,p_precio,p_iva,p_cantidad,p_descripcion,p_duracion,p_proveedor,v_tipo_id);
  END;

  PROCEDURE MODIFICAR(p_id NUMBER,p_nombre VARCHAR2,p_precio NUMBER,p_iva NUMBER,p_cantidad NUMBER,
    p_descripcion VARCHAR2,p_duracion NUMBER,p_proveedor VARCHAR2,p_tipo VARCHAR2) IS
    v_tipo_id NUMBER; v_tipo_actual VARCHAR2(100); v_movimientos NUMBER;
  BEGIN
    SELECT T.NOMBRE INTO v_tipo_actual FROM TALLER.ARTICULO A JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID=A.TIPO_ARTICULO_ID
     WHERE A.ARTICULOID=p_id FOR UPDATE OF A.TIPO_ARTICULO_ID;
    OBTENER_TIPO_ID(p_tipo,v_tipo_id);
    IF UPPER(v_tipo_actual)<>UPPER(p_tipo) THEN
      SELECT COUNT(*) INTO v_movimientos FROM TALLER.DETALLE_FACTURA WHERE ARTICULO_ARTICULOID=p_id;
      IF v_movimientos>0 THEN RAISE_APPLICATION_ERROR(-20022,'No se puede cambiar el tipo de un artículo con movimientos de facturación.'); END IF;
    END IF;
    IF UPPER(p_tipo)='REPUESTO' AND p_proveedor IS NULL THEN RAISE_APPLICATION_ERROR(-20021,'El repuesto debe tener proveedor.'); END IF;
    UPDATE TALLER.ARTICULO SET NOMBRE=p_nombre,PRECIO=p_precio,PORCENTAJEIVA=p_iva,CANTIDAD=p_cantidad,
      DESCRIPCION=p_descripcion,DURACION=p_duracion,PROVEEDOR_NIT=p_proveedor,TIPO_ARTICULO_ID=v_tipo_id
     WHERE ARTICULOID=p_id;
  EXCEPTION WHEN NO_DATA_FOUND THEN RAISE_APPLICATION_ERROR(-20001,'El artículo ya no existe.');
  END;

  PROCEDURE ABRIR(p_resultado OUT SYS_REFCURSOR,p_id NUMBER DEFAULT NULL) IS
  BEGIN OPEN p_resultado FOR
    SELECT A.ARTICULOID,A.NOMBRE,A.PRECIO,A.PORCENTAJEIVA,A.CANTIDAD,A.DESCRIPCION,A.DURACION,A.PROVEEDOR_NIT,
      T.NOMBRE AS TIPO_ARTICULO,P.RAZONSOCIAL AS PROVEEDOR_NOMBRE
    FROM TALLER.ARTICULO A JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID=A.TIPO_ARTICULO_ID
    LEFT JOIN TALLER.PROVEEDOR P ON P.NIT=A.PROVEEDOR_NIT
    WHERE p_id IS NULL OR A.ARTICULOID=p_id ORDER BY A.NOMBRE; END;
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado); END;
  PROCEDURE BUSCAR_ID(p_id NUMBER,p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado,p_id); END;
  PROCEDURE CONTAR(p_total OUT NUMBER) IS BEGIN SELECT COUNT(*) INTO p_total FROM TALLER.ARTICULO; END;
  PROCEDURE TIENE_MOVIMIENTOS(p_id NUMBER,p_resultado OUT NUMBER) IS BEGIN
    SELECT COUNT(*) INTO p_resultado FROM TALLER.DETALLE_FACTURA WHERE ARTICULO_ARTICULOID=p_id; END;
  PROCEDURE OBTENER_TIPO(p_id NUMBER,p_tipo OUT VARCHAR2) IS BEGIN
    SELECT T.NOMBRE INTO p_tipo FROM TALLER.ARTICULO A JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID=A.TIPO_ARTICULO_ID WHERE A.ARTICULOID=p_id;
  EXCEPTION WHEN NO_DATA_FOUND THEN RAISE_APPLICATION_ERROR(-20001,'El artículo ya no existe.'); END;
  PROCEDURE DESCONTAR_STOCK(p_id NUMBER,p_cantidad NUMBER) IS
  BEGIN UPDATE TALLER.ARTICULO SET CANTIDAD=CANTIDAD-p_cantidad WHERE ARTICULOID=p_id AND CANTIDAD>=p_cantidad;
    IF SQL%ROWCOUNT<>1 THEN RAISE_APPLICATION_ERROR(-20023,'El stock cambió o no alcanza para el artículo.'); END IF; END;
  PROCEDURE RESTAURAR_STOCK(p_id NUMBER,p_cantidad NUMBER) IS
  BEGIN UPDATE TALLER.ARTICULO SET CANTIDAD=NVL(CANTIDAD,0)+p_cantidad WHERE ARTICULOID=p_id;
    IF SQL%ROWCOUNT<>1 THEN RAISE_APPLICATION_ERROR(-20024,'No se pudo devolver el artículo al inventario.'); END IF; END;
  PROCEDURE ELIMINAR(p_id NUMBER) IS v_movimientos NUMBER;
  BEGIN
    SELECT COUNT(*) INTO v_movimientos FROM TALLER.DETALLE_FACTURA WHERE ARTICULO_ARTICULOID=p_id;
    IF v_movimientos>0 THEN RAISE_APPLICATION_ERROR(-20025,'No se puede eliminar un artículo con movimientos de facturación.'); END IF;
    DELETE FROM TALLER.ARTICULO WHERE ARTICULOID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe el artículo.'); END IF;
  END;
END PKG_ARTICULO;

/
--------------------------------------------------------
--  DDL for Package Body PKG_CARGO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_CARGO" AS
  PROCEDURE SIGUIENTE_ID(p_id OUT NUMBER) IS BEGIN SELECT TALLER.SEQ_CARGOID.NEXTVAL INTO p_id FROM DUAL; END;
  PROCEDURE GUARDAR(p_id NUMBER, p_nombre VARCHAR2) IS
  BEGIN INSERT INTO TALLER.CARGO(CARGOID,NOMBRECARGO) VALUES(p_id,p_nombre); END;
  PROCEDURE MODIFICAR(p_id NUMBER, p_nombre VARCHAR2) IS
  BEGIN UPDATE TALLER.CARGO SET NOMBRECARGO=p_nombre WHERE CARGOID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe el cargo.'); END IF; END;
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN OPEN p_resultado FOR SELECT CARGOID,NOMBRECARGO FROM TALLER.CARGO ORDER BY NOMBRECARGO; END;
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN OPEN p_resultado FOR SELECT CARGOID,NOMBRECARGO FROM TALLER.CARGO WHERE CARGOID=p_id; END;
  PROCEDURE ELIMINAR(p_id NUMBER) IS
  BEGIN DELETE FROM TALLER.CARGO WHERE CARGOID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe el cargo.'); END IF; END;
END PKG_CARGO;

/
--------------------------------------------------------
--  DDL for Package Body PKG_CLIENTE
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_CLIENTE" AS
  PROCEDURE PRC_GUARDAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2) IS
    v_existe NUMBER;
  BEGIN
    SAVEPOINT SP_CLIENTE;
    SELECT COUNT(*) INTO v_existe FROM TALLER.PERSONA WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_existe = 0 THEN
      INSERT INTO TALLER.PERSONA
        (CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
         SEXO, TELEFONO, DIRECCION, CORREOELECTRONICO)
      VALUES
        (p_cedula, p_pnombre, p_snombre, p_papellido, p_sapellido,
         p_sexo, p_telefono, p_direccion, p_correo);
    ELSE
      UPDATE TALLER.PERSONA
         SET PRIMER_NOMBRE     = p_pnombre,
             SEGUNDO_NOMBRE    = p_snombre,
             PRIMER_APELLIDO   = p_papellido,
             SEGUNDO_APELLIDO  = p_sapellido,
             SEXO              = p_sexo,
             TELEFONO          = p_telefono,
             DIRECCION         = p_direccion,
             CORREOELECTRONICO = p_correo
       WHERE TRIM(CEDULA) = TRIM(p_cedula);
    END IF;
    INSERT INTO TALLER.CLIENTE(CEDULA) VALUES (p_cedula);
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_CLIENTE;
      RAISE;
  END;

  PROCEDURE PRC_MODIFICAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2) IS
    v_total NUMBER;
  BEGIN
    SELECT COUNT(*) INTO v_total FROM TALLER.CLIENTE WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_total = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el cliente.');
    END IF;
    UPDATE TALLER.PERSONA
       SET PRIMER_NOMBRE     = p_pnombre,
           SEGUNDO_NOMBRE    = p_snombre,
           PRIMER_APELLIDO   = p_papellido,
           SEGUNDO_APELLIDO  = p_sapellido,
           SEXO              = p_sexo,
           TELEFONO          = p_telefono,
           DIRECCION         = p_direccion,
           CORREOELECTRONICO = p_correo
     WHERE TRIM(CEDULA) = TRIM(p_cedula);
  END;

  PROCEDURE PRC_ELIMINAR(p_cedula CHAR) IS
    v_es_empleado NUMBER;
  BEGIN
    SAVEPOINT SP_CLIENTE;
    DELETE FROM TALLER.CLIENTE WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el cliente.');
    END IF;
    SELECT COUNT(*) INTO v_es_empleado FROM TALLER.EMPLEADO WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_es_empleado = 0 THEN
      DELETE FROM TALLER.PERSONA WHERE TRIM(CEDULA) = TRIM(p_cedula);
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_CLIENTE;
      RAISE;
  END;

  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO, C.TELEGRAM_CHAT_ID
        FROM TALLER.CLIENTE C
        JOIN TALLER.PERSONA P ON TRIM(P.CEDULA) = TRIM(C.CEDULA)
       ORDER BY P.PRIMER_APELLIDO, P.PRIMER_NOMBRE;
  END;

  PROCEDURE PRC_BUSCAR_CEDULA(p_cedula CHAR, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO, C.TELEGRAM_CHAT_ID
        FROM TALLER.CLIENTE C
        JOIN TALLER.PERSONA P ON TRIM(P.CEDULA) = TRIM(C.CEDULA)
       WHERE TRIM(C.CEDULA) = TRIM(p_cedula);
  END;

  PROCEDURE PRC_CONTAR(p_total OUT NUMBER) IS
  BEGIN
    SELECT COUNT(*) INTO p_total FROM TALLER.CLIENTE;
  END;

  PROCEDURE PRC_VINCULAR_TELEGRAM(p_cedula CHAR, p_chat_id NUMBER) IS
    v_existe NUMBER;
    v_en_uso NUMBER;
  BEGIN
    IF p_chat_id IS NULL THEN
      RAISE_APPLICATION_ERROR(-20040, 'El identificador de Telegram es obligatorio.');
    END IF;
    SELECT COUNT(*) INTO v_existe FROM TALLER.CLIENTE WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_existe = 0 THEN
      RAISE_APPLICATION_ERROR(-20041, 'No existe el cliente que se desea vincular.');
    END IF;
    SELECT COUNT(*) INTO v_en_uso
      FROM TALLER.CLIENTE
     WHERE TELEGRAM_CHAT_ID = p_chat_id
       AND TRIM(CEDULA) <> TRIM(p_cedula);
    IF v_en_uso > 0 THEN
      RAISE_APPLICATION_ERROR(-20042,
        'Esta cuenta de Telegram ya está vinculada a otro cliente.');
    END IF;
    UPDATE TALLER.CLIENTE
       SET TELEGRAM_CHAT_ID = p_chat_id
     WHERE TRIM(CEDULA) = TRIM(p_cedula);
  END;

  PROCEDURE PRC_BUSCAR_POR_TELEGRAM_CHAT_ID(
    p_chat_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO, C.TELEGRAM_CHAT_ID
        FROM TALLER.CLIENTE C
        JOIN TALLER.PERSONA P ON TRIM(P.CEDULA) = TRIM(C.CEDULA)
       WHERE C.TELEGRAM_CHAT_ID = p_chat_id;
  END;
END PKG_CLIENTE;

/
--------------------------------------------------------
--  DDL for Package Body PKG_DETALLE_FACTURA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_DETALLE_FACTURA" AS
  PROCEDURE ABRIR(p_resultado OUT SYS_REFCURSOR, p_id NUMBER DEFAULT NULL, p_factura_id NUMBER DEFAULT NULL) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT D.DETALLEFACTURAID, D.CANTIDAD, D.VALORUNITARIO, D.VALORIVA,
             D.FACTURA_FACTURAID, D.ARTICULO_ARTICULOID, D.PORCENTAJEIVA,
             D.DESCUENTO, D.DESCRIPCION, A.NOMBRE AS NOMBRE_ARTICULO,
             D.SUBTOTAL, D.TOTAL, T.NOMBRE AS TIPODEARTICULO
        FROM TALLER.DETALLE_FACTURA D
        JOIN TALLER.ARTICULO A ON A.ARTICULOID = D.ARTICULO_ARTICULOID
        JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID = A.TIPO_ARTICULO_ID
       WHERE (p_id IS NULL OR D.DETALLEFACTURAID = p_id)
         AND (p_factura_id IS NULL OR D.FACTURA_FACTURAID = p_factura_id)
       ORDER BY D.DETALLEFACTURAID;
  END;

  PROCEDURE GUARDAR(p_id OUT NUMBER, p_cantidad NUMBER, p_valor_unitario NUMBER,
    p_valor_iva NUMBER, p_factura_id NUMBER, p_articulo_id NUMBER,
    p_porcentaje_iva NUMBER, p_descripcion VARCHAR2, p_subtotal NUMBER,
    p_total NUMBER, p_descuento NUMBER) IS
  BEGIN
    SELECT TALLER.SEQ_DETALLEFACTURAID.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.DETALLE_FACTURA
      (DETALLEFACTURAID, CANTIDAD, VALORUNITARIO, VALORIVA, FACTURA_FACTURAID,
       ARTICULO_ARTICULOID, PORCENTAJEIVA, DESCRIPCION, SUBTOTAL, TOTAL, DESCUENTO)
    VALUES
      (p_id, p_cantidad, p_valor_unitario, p_valor_iva, p_factura_id,
       p_articulo_id, p_porcentaje_iva, p_descripcion, p_subtotal, p_total, p_descuento);
  END;

  PROCEDURE MODIFICAR(p_id NUMBER, p_cantidad NUMBER, p_valor_unitario NUMBER,
    p_valor_iva NUMBER, p_articulo_id NUMBER, p_porcentaje_iva NUMBER,
    p_descripcion VARCHAR2, p_subtotal NUMBER, p_total NUMBER, p_descuento NUMBER) IS
  BEGIN
    UPDATE TALLER.DETALLE_FACTURA
       SET CANTIDAD = p_cantidad, VALORUNITARIO = p_valor_unitario,
           VALORIVA = p_valor_iva, ARTICULO_ARTICULOID = p_articulo_id,
           PORCENTAJEIVA = p_porcentaje_iva, DESCRIPCION = p_descripcion,
           SUBTOTAL = p_subtotal, TOTAL = p_total, DESCUENTO = p_descuento
     WHERE DETALLEFACTURAID = p_id;
    IF SQL%ROWCOUNT = 0 THEN RAISE_APPLICATION_ERROR(-20001, 'No existe el detalle de factura.'); END IF;
  END;

  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado); END;
  PROCEDURE CONSULTAR_POR_FACTURA(p_factura_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN ABRIR(p_resultado, NULL, p_factura_id); END;
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado, p_id); END;
  PROCEDURE ELIMINAR(p_id NUMBER) IS
  BEGIN
    DELETE FROM TALLER.DETALLE_FACTURA WHERE DETALLEFACTURAID = p_id;
    IF SQL%ROWCOUNT = 0 THEN RAISE_APPLICATION_ERROR(-20001, 'No existe el detalle de factura.'); END IF;
  END;
END PKG_DETALLE_FACTURA;

/
--------------------------------------------------------
--  DDL for Package Body PKG_DETALLE_VEHICULO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_DETALLE_VEHICULO" AS
  PROCEDURE GUARDAR(p_id IN OUT NUMBER, p_factura_id NUMBER, p_placa VARCHAR2) IS
  BEGIN
    SELECT TALLER.SEQ_DETALLE_VEHICULO.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.DETALLE_VEHICULO
      (DETALLEVEHICULOID, FACTURA_FACTURAID, VEHICULO_PLACA)
    VALUES
      (p_id, p_factura_id, p_placa);
  END;

  PROCEDURE MODIFICAR(p_id NUMBER, p_factura_id NUMBER, p_placa VARCHAR2) IS
  BEGIN
    UPDATE TALLER.DETALLE_VEHICULO
       SET FACTURA_FACTURAID = p_factura_id,
           VEHICULO_PLACA    = p_placa
     WHERE DETALLEVEHICULOID = p_id;
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el detalle de vehículo.');
    END IF;
  END;

  PROCEDURE ELIMINAR(p_id NUMBER) IS
  BEGIN
    DELETE FROM TALLER.DETALLE_VEHICULO WHERE DETALLEVEHICULOID = p_id;
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el detalle de vehículo.');
    END IF;
  END;

  PROCEDURE CONSULTAR_POR_FACTURA(
    p_factura_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT DV.DETALLEVEHICULOID, DV.FACTURA_FACTURAID,
             V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO, V.COLOR, V.LINEA,
             V.CLIENTE_CEDULA
        FROM TALLER.DETALLE_VEHICULO DV
        JOIN TALLER.VEHICULO V ON V.PLACA = DV.VEHICULO_PLACA
       WHERE DV.FACTURA_FACTURAID = p_factura_id
       ORDER BY DV.DETALLEVEHICULOID;
  END;

  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT DV.DETALLEVEHICULOID, DV.FACTURA_FACTURAID,
             V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO, V.COLOR, V.LINEA,
             V.CLIENTE_CEDULA
        FROM TALLER.DETALLE_VEHICULO DV
        JOIN TALLER.VEHICULO V ON V.PLACA = DV.VEHICULO_PLACA
       WHERE DV.DETALLEVEHICULOID = p_id;
  END;
END PKG_DETALLE_VEHICULO;

/
--------------------------------------------------------
--  DDL for Package Body PKG_EMPLEADO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_EMPLEADO" AS
  PROCEDURE GUARDAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR) IS
    v_existe NUMBER;
  BEGIN
    SAVEPOINT SP_EMPLEADO;
    SELECT COUNT(*) INTO v_existe FROM TALLER.PERSONA WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_existe = 0 THEN
      INSERT INTO TALLER.PERSONA
        (CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
         SEXO, TELEFONO, DIRECCION, CORREOELECTRONICO)
      VALUES
        (p_cedula, p_pnombre, p_snombre, p_papellido, p_sapellido,
         p_sexo, p_telefono, p_direccion, p_correo);
    ELSE
      UPDATE TALLER.PERSONA
         SET PRIMER_NOMBRE     = p_pnombre,
             SEGUNDO_NOMBRE    = p_snombre,
             PRIMER_APELLIDO   = p_papellido,
             SEGUNDO_APELLIDO  = p_sapellido,
             SEXO              = p_sexo,
             TELEFONO          = p_telefono,
             DIRECCION         = p_direccion,
             CORREOELECTRONICO = p_correo
       WHERE TRIM(CEDULA) = TRIM(p_cedula);
    END IF;
    INSERT INTO TALLER.EMPLEADO(CEDULA, CARGO_CARGOID, ACTIVO)
    VALUES (p_cedula, p_cargo, p_activo);
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_EMPLEADO;
      RAISE;
  END;

  PROCEDURE MODIFICAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR) IS
  BEGIN
    SAVEPOINT SP_EMPLEADO;
    UPDATE TALLER.PERSONA
       SET PRIMER_NOMBRE     = p_pnombre,
           SEGUNDO_NOMBRE    = p_snombre,
           PRIMER_APELLIDO   = p_papellido,
           SEGUNDO_APELLIDO  = p_sapellido,
           SEXO              = p_sexo,
           TELEFONO          = p_telefono,
           DIRECCION         = p_direccion,
           CORREOELECTRONICO = p_correo
     WHERE TRIM(CEDULA) = TRIM(p_cedula);
    UPDATE TALLER.EMPLEADO
       SET CARGO_CARGOID = p_cargo,
           ACTIVO        = p_activo
     WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el empleado.');
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_EMPLEADO;
      RAISE;
  END;

  PROCEDURE ELIMINAR(p_cedula CHAR) IS
    v_es_cliente NUMBER;
  BEGIN
    SAVEPOINT SP_EMPLEADO;
    DELETE FROM TALLER.EMPLEADO WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el empleado.');
    END IF;
    SELECT COUNT(*) INTO v_es_cliente FROM TALLER.CLIENTE WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_es_cliente = 0 THEN
      DELETE FROM TALLER.PERSONA WHERE TRIM(CEDULA) = TRIM(p_cedula);
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK TO SP_EMPLEADO;
      RAISE;
  END;

  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO, E.ACTIVO, C.CARGOID, C.NOMBRECARGO
        FROM TALLER.EMPLEADO E
        JOIN TALLER.PERSONA P ON TRIM(P.CEDULA)  = TRIM(E.CEDULA)
        JOIN TALLER.CARGO   C ON C.CARGOID = E.CARGO_CARGOID
       ORDER BY P.PRIMER_APELLIDO, P.PRIMER_NOMBRE;
  END;

  PROCEDURE BUSCAR_CEDULA(p_cedula CHAR, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT P.CEDULA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO, E.ACTIVO, C.CARGOID, C.NOMBRECARGO
        FROM TALLER.EMPLEADO E
        JOIN TALLER.PERSONA P ON TRIM(P.CEDULA)  = TRIM(E.CEDULA)
        JOIN TALLER.CARGO   C ON C.CARGOID = E.CARGO_CARGOID
       WHERE TRIM(E.CEDULA) = TRIM(p_cedula);
  END;

  PROCEDURE CONTAR(p_total OUT NUMBER) IS
  BEGIN
    SELECT COUNT(*) INTO p_total FROM TALLER.EMPLEADO;
  END;

  PROCEDURE OBTENER_CLAVE(p_cedula CHAR, p_hash OUT VARCHAR2) IS
    v_existe NUMBER;
  BEGIN
    p_hash := NULL;
    SELECT COUNT(*) INTO v_existe FROM TALLER.EMPLEADO WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF v_existe > 0 THEN
      SELECT CLAVE_HASH INTO p_hash FROM TALLER.EMPLEADO WHERE TRIM(CEDULA) = TRIM(p_cedula);
    END IF;
  END;

  PROCEDURE GUARDAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2) IS
  BEGIN
    UPDATE TALLER.EMPLEADO
       SET CLAVE_HASH = p_hash
     WHERE TRIM(CEDULA) = TRIM(p_cedula)
       AND CLAVE_HASH IS NULL
       AND ACTIVO = 'S';
    IF SQL%ROWCOUNT <> 1 THEN
      RAISE_APPLICATION_ERROR(-20002, 'No se pudo asignar la contraseña inicial.');
    END IF;
  END;

  PROCEDURE CAMBIAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2) IS
  BEGIN
    UPDATE TALLER.EMPLEADO
       SET CLAVE_HASH = p_hash
     WHERE TRIM(CEDULA) = TRIM(p_cedula);
    IF SQL%ROWCOUNT <> 1 THEN
      RAISE_APPLICATION_ERROR(-20003, 'No se pudo cambiar la contraseña.');
    END IF;
  END;

  PROCEDURE PRC_GUARDAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR) IS
  BEGIN
    GUARDAR(p_cedula, p_pnombre, p_snombre, p_papellido, p_sapellido, p_sexo,
            p_telefono, p_direccion, p_correo, p_cargo, p_activo);
  END;

  PROCEDURE PRC_MODIFICAR(
    p_cedula CHAR, p_pnombre VARCHAR2, p_snombre VARCHAR2,
    p_papellido VARCHAR2, p_sapellido VARCHAR2, p_sexo CHAR,
    p_telefono VARCHAR2, p_direccion VARCHAR2, p_correo VARCHAR2,
    p_cargo NUMBER, p_activo CHAR) IS
  BEGIN
    MODIFICAR(p_cedula, p_pnombre, p_snombre, p_papellido, p_sapellido, p_sexo,
              p_telefono, p_direccion, p_correo, p_cargo, p_activo);
  END;

  PROCEDURE PRC_ELIMINAR(p_cedula CHAR) IS
  BEGIN
    ELIMINAR(p_cedula);
  END;

  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    CONSULTAR(p_resultado);
  END;

  PROCEDURE PRC_BUSCAR_CEDULA(p_cedula CHAR, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    BUSCAR_CEDULA(p_cedula, p_resultado);
  END;

  PROCEDURE PRC_CONTAR(p_total OUT NUMBER) IS
  BEGIN
    CONTAR(p_total);
  END;

  PROCEDURE PRC_OBTENER_CLAVE(p_cedula CHAR, p_hash OUT VARCHAR2) IS
  BEGIN
    OBTENER_CLAVE(p_cedula, p_hash);
  END;

  PROCEDURE PRC_GUARDAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2) IS
  BEGIN
    GUARDAR_CLAVE(p_cedula, p_hash);
  END;

  PROCEDURE PRC_CAMBIAR_CLAVE(p_cedula CHAR, p_hash VARCHAR2) IS
  BEGIN
    CAMBIAR_CLAVE(p_cedula, p_hash);
  END;
END PKG_EMPLEADO;

/
--------------------------------------------------------
--  DDL for Package Body PKG_FACTURA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_FACTURA" AS
  PROCEDURE ABRIR(p_resultado OUT SYS_REFCURSOR, p_id NUMBER DEFAULT NULL) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT F.FACTURAID, F.FECHA, F.DESCUENTO, F.SUBTOTAL, F.IVA, F.TOTAL,
             F.OBSERVACIONES, F.ESTADOPAGO, F.CLIENTE_CEDULA, F.EMPLEADO_CEDULA,
             F.TIPO_DOCUMENTO, F.DOMICILIO, F.DIRECCION_DOMICILIO, F.AGENDA_AGENDAID,
             CP.PRIMER_NOMBRE AS CLIENTE_PRIMER_NOMBRE, CP.SEGUNDO_NOMBRE AS CLIENTE_SEGUNDO_NOMBRE,
             CP.PRIMER_APELLIDO AS CLIENTE_PRIMER_APELLIDO, CP.SEGUNDO_APELLIDO AS CLIENTE_SEGUNDO_APELLIDO,
             EP.PRIMER_NOMBRE AS EMPLEADO_PRIMER_NOMBRE, EP.SEGUNDO_NOMBRE AS EMPLEADO_SEGUNDO_NOMBRE,
             EP.PRIMER_APELLIDO AS EMPLEADO_PRIMER_APELLIDO, EP.SEGUNDO_APELLIDO AS EMPLEADO_SEGUNDO_APELLIDO,
             V.PLACA AS VEHICULO_PLACA, V.MODELO AS VEHICULO_MODELO, V.MARCA AS VEHICULO_MARCA,
             V.TIPO_VEHICULO AS VEHICULO_TIPO, V.COLOR AS VEHICULO_COLOR, V.LINEA AS VEHICULO_LINEA
        FROM TALLER.FACTURA F
        LEFT JOIN TALLER.CLIENTE C ON C.CEDULA = F.CLIENTE_CEDULA
        LEFT JOIN TALLER.PERSONA CP ON CP.CEDULA = C.CEDULA
        LEFT JOIN TALLER.EMPLEADO E ON E.CEDULA = F.EMPLEADO_CEDULA
        LEFT JOIN TALLER.PERSONA EP ON EP.CEDULA = E.CEDULA
        LEFT JOIN TALLER.DETALLE_VEHICULO DV ON DV.FACTURA_FACTURAID = F.FACTURAID
        LEFT JOIN TALLER.VEHICULO V ON V.PLACA = DV.VEHICULO_PLACA
       WHERE p_id IS NULL OR F.FACTURAID = p_id
       ORDER BY F.FECHA DESC, F.FACTURAID DESC;
  END;

  PROCEDURE SIGUIENTE_ID(p_id OUT NUMBER) IS
  BEGIN SELECT TALLER.SEQ_FACTURAID.NEXTVAL INTO p_id FROM DUAL; END;

  PROCEDURE GUARDAR(p_id OUT NUMBER, p_fecha DATE, p_descuento NUMBER, p_subtotal NUMBER,
    p_iva NUMBER, p_total NUMBER, p_observaciones VARCHAR2, p_estado_pago VARCHAR2,
    p_cliente CHAR, p_empleado CHAR, p_tipo_documento VARCHAR2, p_domicilio CHAR,
    p_direccion_domicilio VARCHAR2, p_agenda_id NUMBER) IS
  BEGIN
    SELECT TALLER.SEQ_FACTURAID.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.FACTURA
      (FACTURAID, FECHA, DESCUENTO, SUBTOTAL, IVA, TOTAL, OBSERVACIONES, ESTADOPAGO,
       CLIENTE_CEDULA, EMPLEADO_CEDULA, TIPO_DOCUMENTO, DOMICILIO, DIRECCION_DOMICILIO, AGENDA_AGENDAID)
    VALUES
      (p_id, p_fecha, p_descuento, p_subtotal, p_iva, p_total, p_observaciones, p_estado_pago,
       p_cliente, p_empleado, p_tipo_documento, p_domicilio, p_direccion_domicilio, p_agenda_id);
  END;

  PROCEDURE MODIFICAR(p_id NUMBER, p_fecha DATE, p_descuento NUMBER, p_subtotal NUMBER,
    p_iva NUMBER, p_total NUMBER, p_observaciones VARCHAR2, p_estado_pago VARCHAR2,
    p_cliente CHAR, p_empleado CHAR, p_tipo_documento VARCHAR2, p_domicilio CHAR,
    p_direccion_domicilio VARCHAR2, p_agenda_id NUMBER) IS
  BEGIN
    UPDATE TALLER.FACTURA SET FECHA=p_fecha, DESCUENTO=p_descuento, SUBTOTAL=p_subtotal,
      IVA=p_iva, TOTAL=p_total, OBSERVACIONES=p_observaciones, ESTADOPAGO=p_estado_pago,
      CLIENTE_CEDULA=p_cliente, EMPLEADO_CEDULA=p_empleado, TIPO_DOCUMENTO=p_tipo_documento,
      DOMICILIO=p_domicilio, DIRECCION_DOMICILIO=p_direccion_domicilio, AGENDA_AGENDAID=p_agenda_id
    WHERE FACTURAID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001, 'No existe la factura.'); END IF;
  END;

  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado); END;
  PROCEDURE BUSCAR_ID(p_id NUMBER, p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado, p_id); END;
  PROCEDURE TOTAL_VENTAS(p_total OUT NUMBER) IS
  BEGIN SELECT NVL(SUM(TOTAL),0) INTO p_total FROM TALLER.FACTURA
    WHERE TIPO_DOCUMENTO='VENTA' AND UPPER(ESTADOPAGO)<>'ANULADA'; END;

  PROCEDURE VALIDAR_CITA(p_agenda_id NUMBER, p_placa VARCHAR2, p_cliente CHAR) IS
    v_placa VARCHAR2(6); v_cliente CHAR(10); v_estado VARCHAR2(20);
  BEGIN
    SELECT A.VEHICULO_PLACA, V.CLIENTE_CEDULA, A.ESTADO
      INTO v_placa, v_cliente, v_estado
      FROM TALLER.AGENDA A JOIN TALLER.VEHICULO V ON V.PLACA=A.VEHICULO_PLACA
     WHERE A.AGENDAID=p_agenda_id FOR UPDATE OF A.ESTADO;
    IF TRIM(v_placa)<>TRIM(p_placa) OR TRIM(v_cliente)<>TRIM(p_cliente) OR UPPER(TRIM(v_estado))='CANCELADA' THEN
      RAISE_APPLICATION_ERROR(-20002, 'La cita no corresponde al cliente/vehículo o fue cancelada.');
    END IF;
  EXCEPTION WHEN NO_DATA_FOUND THEN RAISE_APPLICATION_ERROR(-20003, 'No se encontró la cita asociada.');
  END;

  PROCEDURE COMPLETAR_CITA(p_agenda_id NUMBER) IS
  BEGIN
    UPDATE TALLER.AGENDA SET ESTADO='COMPLETADA' WHERE AGENDAID=p_agenda_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20003, 'No se encontró la cita asociada.'); END IF;
  END;

  PROCEDURE CONVERTIR_COTIZACION(p_id NUMBER) IS
    v_tipo VARCHAR2(12); v_estado VARCHAR2(20);
  BEGIN
    SELECT TIPO_DOCUMENTO, ESTADOPAGO INTO v_tipo, v_estado
      FROM TALLER.FACTURA WHERE FACTURAID=p_id FOR UPDATE;
    IF UPPER(v_tipo)<>'COTIZACION' THEN RAISE_APPLICATION_ERROR(-20004, 'Solo se pueden convertir cotizaciones.'); END IF;
    IF UPPER(v_estado)='ANULADA' THEN RAISE_APPLICATION_ERROR(-20005, 'No se puede convertir una cotización anulada.'); END IF;
    FOR d IN (
      SELECT D.ARTICULO_ARTICULOID, D.CANTIDAD
        FROM TALLER.DETALLE_FACTURA D JOIN TALLER.ARTICULO A ON A.ARTICULOID=D.ARTICULO_ARTICULOID
        JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID=A.TIPO_ARTICULO_ID
       WHERE D.FACTURA_FACTURAID=p_id AND UPPER(T.NOMBRE)='REPUESTO'
       ORDER BY D.ARTICULO_ARTICULOID
    ) LOOP
      UPDATE TALLER.ARTICULO SET CANTIDAD=CANTIDAD-d.CANTIDAD
       WHERE ARTICULOID=d.ARTICULO_ARTICULOID AND CANTIDAD>=d.CANTIDAD;
      IF SQL%ROWCOUNT<>1 THEN RAISE_APPLICATION_ERROR(-20006, 'No hay stock suficiente para convertir esta cotización.'); END IF;
    END LOOP;
    UPDATE TALLER.FACTURA SET TIPO_DOCUMENTO='VENTA', ESTADOPAGO='Pendiente' WHERE FACTURAID=p_id;
  EXCEPTION WHEN NO_DATA_FOUND THEN RAISE_APPLICATION_ERROR(-20001, 'No se encontró el documento.');
  END;

  PROCEDURE ANULAR_DOCUMENTO(p_id NUMBER) IS
    v_tipo VARCHAR2(12); v_estado VARCHAR2(20);
  BEGIN
    SELECT TIPO_DOCUMENTO, ESTADOPAGO INTO v_tipo, v_estado
      FROM TALLER.FACTURA WHERE FACTURAID=p_id FOR UPDATE;
    IF UPPER(v_estado)='ANULADA' THEN RAISE_APPLICATION_ERROR(-20007, 'El documento ya está anulado.'); END IF;
    IF UPPER(v_tipo)='VENTA' THEN
      FOR d IN (
        SELECT D.ARTICULO_ARTICULOID, D.CANTIDAD
          FROM TALLER.DETALLE_FACTURA D JOIN TALLER.ARTICULO A ON A.ARTICULOID=D.ARTICULO_ARTICULOID
          JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID=A.TIPO_ARTICULO_ID
         WHERE D.FACTURA_FACTURAID=p_id AND UPPER(T.NOMBRE)='REPUESTO'
         ORDER BY D.ARTICULO_ARTICULOID
      ) LOOP
        UPDATE TALLER.ARTICULO SET CANTIDAD=NVL(CANTIDAD,0)+d.CANTIDAD WHERE ARTICULOID=d.ARTICULO_ARTICULOID;
        IF SQL%ROWCOUNT<>1 THEN RAISE_APPLICATION_ERROR(-20008, 'No se pudo devolver un repuesto al inventario.'); END IF;
      END LOOP;
    END IF;
    UPDATE TALLER.FACTURA SET ESTADOPAGO='Anulada' WHERE FACTURAID=p_id;
    IF SQL%ROWCOUNT<>1 THEN RAISE_APPLICATION_ERROR(-20001, 'No se pudo anular el documento.'); END IF;
  EXCEPTION WHEN NO_DATA_FOUND THEN RAISE_APPLICATION_ERROR(-20001, 'No se encontró el documento.');
  END;

  PROCEDURE ELIMINAR(p_id NUMBER) IS BEGIN ANULAR_DOCUMENTO(p_id); END;
END PKG_FACTURA;

/
--------------------------------------------------------
--  DDL for Package Body PKG_NOTIFICACION_MANTENIMIENTO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_NOTIFICACION_MANTENIMIENTO" AS
  PROCEDURE GUARDAR(p_id OUT NUMBER,p_fecha DATE,p_comentario VARCHAR2,p_estado VARCHAR2,p_placa VARCHAR2,p_articulo_id NUMBER) IS
  BEGIN
    SELECT TALLER.SEQ_NOTIFICACIONID.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.NOTIFICACION_MANTENIMIENTO(NOTIFICACIONID,FECHA,COMENTARIO,ESTADO,VEHICULO_PLACA,ARTICULO_ARTICULOID)
    VALUES(p_id,p_fecha,p_comentario,p_estado,p_placa,p_articulo_id);
  END;
  PROCEDURE MODIFICAR(p_id NUMBER,p_fecha DATE,p_comentario VARCHAR2,p_estado VARCHAR2,p_placa VARCHAR2,p_articulo_id NUMBER) IS
  BEGIN UPDATE TALLER.NOTIFICACION_MANTENIMIENTO SET FECHA=p_fecha,COMENTARIO=p_comentario,ESTADO=p_estado,
    VEHICULO_PLACA=p_placa,ARTICULO_ARTICULOID=p_articulo_id WHERE NOTIFICACIONID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe la notificación.'); END IF; END;
  PROCEDURE ABRIR(p_resultado OUT SYS_REFCURSOR,p_id NUMBER DEFAULT NULL) IS
  BEGIN OPEN p_resultado FOR
    SELECT N.NOTIFICACIONID,N.FECHA,N.COMENTARIO,N.ESTADO,N.VEHICULO_PLACA,N.ARTICULO_ARTICULOID,
      A.NOMBRE AS ARTICULO_NOMBRE,A.CANTIDAD,A.DESCRIPCION,A.DURACION,A.PROVEEDOR_NIT,T.NOMBRE AS TIPO_ARTICULO,
      V.MARCA AS VEHICULO_MARCA,V.LINEA AS VEHICULO_LINEA,C.CEDULA AS CLIENTE_CEDULA,
      P.PRIMER_NOMBRE,P.SEGUNDO_NOMBRE,P.PRIMER_APELLIDO,P.SEGUNDO_APELLIDO,P.CORREOELECTRONICO
    FROM TALLER.NOTIFICACION_MANTENIMIENTO N
    JOIN TALLER.VEHICULO V ON V.PLACA=N.VEHICULO_PLACA
    JOIN TALLER.CLIENTE C ON C.CEDULA=V.CLIENTE_CEDULA
    JOIN TALLER.PERSONA P ON P.CEDULA=C.CEDULA
    JOIN TALLER.ARTICULO A ON A.ARTICULOID=N.ARTICULO_ARTICULOID
    JOIN TALLER.TIPO_ARTICULO T ON T.TIPO_ARTICULO_ID=A.TIPO_ARTICULO_ID
    WHERE p_id IS NULL OR N.NOTIFICACIONID=p_id ORDER BY N.FECHA DESC; END;
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado); END;
  PROCEDURE BUSCAR_ID(p_id NUMBER,p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado,p_id); END;
  PROCEDURE ELIMINAR(p_id NUMBER) IS BEGIN DELETE FROM TALLER.NOTIFICACION_MANTENIMIENTO WHERE NOTIFICACIONID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe la notificación.'); END IF; END;
END PKG_NOTIFICACION_MANTENIMIENTO;

/
--------------------------------------------------------
--  DDL for Package Body PKG_PERSONA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_PERSONA" AS
  PROCEDURE PR_INSERTAR_PERSONA (
    p_cedula             IN PERSONA.CEDULA%TYPE,
    p_primer_nombre      IN PERSONA.PRIMER_NOMBRE%TYPE,
    p_segundo_nombre     IN PERSONA.SEGUNDO_NOMBRE%TYPE,
    p_primer_apellido    IN PERSONA.PRIMER_APELLIDO%TYPE,
    p_segundo_apellido   IN PERSONA.SEGUNDO_APELLIDO%TYPE,
    p_telefono           IN PERSONA.TELEFONO%TYPE,
    p_direccion          IN PERSONA.DIRECCION%TYPE,
    p_correo_electronico IN PERSONA.CORREOELECTRONICO%TYPE,
    p_sexo               IN PERSONA.SEXO%TYPE
  ) IS
  BEGIN
    INSERT INTO PERSONA (
      CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
      TELEFONO, DIRECCION, CORREOELECTRONICO, SEXO
    ) VALUES (
      p_cedula, p_primer_nombre, p_segundo_nombre, p_primer_apellido, p_segundo_apellido,
      p_telefono, p_direccion, p_correo_electronico, p_sexo
    );
  END PR_INSERTAR_PERSONA;

  PROCEDURE PR_ACTUALIZAR_PERSONA (
    p_cedula             IN PERSONA.CEDULA%TYPE,
    p_primer_nombre      IN PERSONA.PRIMER_NOMBRE%TYPE,
    p_segundo_nombre     IN PERSONA.SEGUNDO_NOMBRE%TYPE,
    p_primer_apellido    IN PERSONA.PRIMER_APELLIDO%TYPE,
    p_segundo_apellido   IN PERSONA.SEGUNDO_APELLIDO%TYPE,
    p_telefono           IN PERSONA.TELEFONO%TYPE,
    p_direccion          IN PERSONA.DIRECCION%TYPE,
    p_correo_electronico IN PERSONA.CORREOELECTRONICO%TYPE,
    p_sexo               IN PERSONA.SEXO%TYPE
  ) IS
  BEGIN
    UPDATE PERSONA
       SET PRIMER_NOMBRE      = p_primer_nombre,
           SEGUNDO_NOMBRE     = p_segundo_nombre,
           PRIMER_APELLIDO    = p_primer_apellido,
           SEGUNDO_APELLIDO   = p_segundo_apellido,
           TELEFONO           = p_telefono,
           DIRECCION          = p_direccion,
           CORREOELECTRONICO  = p_correo_electronico,
           SEXO               = p_sexo
     WHERE CEDULA = p_cedula;

    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe una persona con esa cedula.');
    END IF;
  END PR_ACTUALIZAR_PERSONA;

  PROCEDURE PR_ELIMINAR_PERSONA (
    p_cedula IN PERSONA.CEDULA%TYPE
  ) IS
  BEGIN
    DELETE FROM PERSONA
     WHERE CEDULA = p_cedula;

    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20002, 'No existe una persona con esa cedula.');
    END IF;
  END PR_ELIMINAR_PERSONA;

  FUNCTION FN_OBTENER_PERSONA_POR_CEDULA (
    p_cedula IN PERSONA.CEDULA%TYPE
  ) RETURN SYS_REFCURSOR IS
    v_resultado SYS_REFCURSOR;
  BEGIN
    OPEN v_resultado FOR
      SELECT CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO,
             SEGUNDO_APELLIDO, TELEFONO, DIRECCION, CORREOELECTRONICO, SEXO
        FROM PERSONA
       WHERE CEDULA = p_cedula;
    RETURN v_resultado;
  END FN_OBTENER_PERSONA_POR_CEDULA;

  FUNCTION FN_OBTENER_PERSONAS
    RETURN SYS_REFCURSOR IS
    v_resultado SYS_REFCURSOR;
  BEGIN
    OPEN v_resultado FOR
      SELECT CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO,
             SEGUNDO_APELLIDO, TELEFONO, DIRECCION, CORREOELECTRONICO, SEXO
        FROM PERSONA
       ORDER BY PRIMER_APELLIDO, SEGUNDO_APELLIDO, PRIMER_NOMBRE, SEGUNDO_NOMBRE;
    RETURN v_resultado;
  END FN_OBTENER_PERSONAS;
END PKG_PERSONA;

/
--------------------------------------------------------
--  DDL for Package Body PKG_PROVEEDOR
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_PROVEEDOR" AS
  PROCEDURE GUARDAR(p_nit VARCHAR2,p_razon_social VARCHAR2,p_telefono VARCHAR2) IS
  BEGIN INSERT INTO TALLER.PROVEEDOR(NIT,RAZONSOCIAL,TELEFONO) VALUES(p_nit,p_razon_social,p_telefono); END;
  PROCEDURE MODIFICAR(p_nit VARCHAR2,p_razon_social VARCHAR2,p_telefono VARCHAR2) IS
  BEGIN UPDATE TALLER.PROVEEDOR SET RAZONSOCIAL=p_razon_social,TELEFONO=p_telefono WHERE NIT=p_nit;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe el proveedor.'); END IF; END;
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN OPEN p_resultado FOR SELECT NIT,RAZONSOCIAL,TELEFONO FROM TALLER.PROVEEDOR ORDER BY RAZONSOCIAL; END;
  PROCEDURE BUSCAR_NIT(p_nit VARCHAR2,p_resultado OUT SYS_REFCURSOR) IS
  BEGIN OPEN p_resultado FOR SELECT NIT,RAZONSOCIAL,TELEFONO FROM TALLER.PROVEEDOR WHERE NIT=p_nit; END;
  PROCEDURE ELIMINAR(p_nit VARCHAR2) IS
  BEGIN DELETE FROM TALLER.PROVEEDOR WHERE NIT=p_nit;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe el proveedor.'); END IF; END;
END PKG_PROVEEDOR;

/
--------------------------------------------------------
--  DDL for Package Body PKG_SOAT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_SOAT" AS
  PROCEDURE VALIDAR(p_id NUMBER,p_numero VARCHAR2,p_inicio DATE,p_fin DATE,p_placa VARCHAR2) IS v_total NUMBER;
  BEGIN
    IF TRIM(p_numero) IS NULL OR p_inicio IS NULL OR p_fin IS NULL OR p_fin<=p_inicio OR p_fin>ADD_MONTHS(p_inicio,12) THEN
      RAISE_APPLICATION_ERROR(-20010,'El SOAT requiere número y fechas válidas sin superar un año.');
    END IF;
    SELECT COUNT(*) INTO v_total FROM TALLER.SOAT S
     WHERE S.SOATID<>NVL(p_id,0) AND (S.NUMEROSOAT=p_numero OR
       (S.VEHICULO_PLACA=p_placa AND (S.FECHAINICIO IS NULL OR S.FECHAFIN IS NULL OR
        (p_inicio<=S.FECHAFIN AND p_fin>=S.FECHAINICIO))));
    IF v_total>0 THEN RAISE_APPLICATION_ERROR(-20011,'Ya existe ese número de SOAT o una póliza superpuesta para el vehículo.'); END IF;
  END;
  PROCEDURE GUARDAR(p_id OUT NUMBER,p_numero VARCHAR2,p_inicio DATE,p_fin DATE,p_placa VARCHAR2) IS
  BEGIN
    LOCK TABLE TALLER.SOAT IN SHARE ROW EXCLUSIVE MODE;
    VALIDAR(NULL,p_numero,p_inicio,p_fin,p_placa);
    SELECT TALLER.SEQ_SOATID.NEXTVAL INTO p_id FROM DUAL;
    INSERT INTO TALLER.SOAT(SOATID,NUMEROSOAT,FECHAINICIO,FECHAFIN,VEHICULO_PLACA)
    VALUES(p_id,p_numero,p_inicio,p_fin,p_placa);
  END;
  PROCEDURE MODIFICAR(p_id NUMBER,p_numero VARCHAR2,p_inicio DATE,p_fin DATE,p_placa VARCHAR2) IS
  BEGIN
    LOCK TABLE TALLER.SOAT IN SHARE ROW EXCLUSIVE MODE;
    VALIDAR(p_id,p_numero,p_inicio,p_fin,p_placa);
    UPDATE TALLER.SOAT SET NUMEROSOAT=p_numero,FECHAINICIO=p_inicio,FECHAFIN=p_fin,VEHICULO_PLACA=p_placa WHERE SOATID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'La póliza ya no existe.'); END IF;
  END;
  PROCEDURE ABRIR(p_resultado OUT SYS_REFCURSOR,p_id NUMBER DEFAULT NULL) IS
  BEGIN OPEN p_resultado FOR
    SELECT S.SOATID,S.NUMEROSOAT,S.FECHAINICIO,S.FECHAFIN,V.PLACA AS VEHICULO_PLACA,V.MARCA,V.LINEA,V.MODELO,V.COLOR,V.TIPO_VEHICULO,V.CLIENTE_CEDULA,
      P.PRIMER_NOMBRE,P.SEGUNDO_NOMBRE,P.PRIMER_APELLIDO,P.SEGUNDO_APELLIDO,P.CORREOELECTRONICO
    FROM TALLER.SOAT S JOIN TALLER.VEHICULO V ON V.PLACA=S.VEHICULO_PLACA
    JOIN TALLER.CLIENTE C ON C.CEDULA=V.CLIENTE_CEDULA JOIN TALLER.PERSONA P ON P.CEDULA=C.CEDULA
    WHERE p_id IS NULL OR S.SOATID=p_id ORDER BY S.FECHAINICIO DESC; END;
  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado); END;
  PROCEDURE BUSCAR_ID(p_id NUMBER,p_resultado OUT SYS_REFCURSOR) IS BEGIN ABRIR(p_resultado,p_id); END;
  PROCEDURE ELIMINAR(p_id NUMBER) IS BEGIN DELETE FROM TALLER.SOAT WHERE SOATID=p_id;
    IF SQL%ROWCOUNT=0 THEN RAISE_APPLICATION_ERROR(-20001,'No existe la póliza SOAT.'); END IF; END;
END PKG_SOAT;

/
--------------------------------------------------------
--  DDL for Package Body PKG_VEHICULO
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "TALLER"."PKG_VEHICULO" AS
  PROCEDURE GUARDAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2) IS
  BEGIN
    INSERT INTO TALLER.VEHICULO
      (PLACA, MODELO, MARCA, CLIENTE_CEDULA, TIPO_VEHICULO, COLOR, LINEA)
    VALUES
      (p_placa, p_modelo, p_marca, p_cliente, p_tipo, p_color, p_linea);
  END;

  PROCEDURE MODIFICAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2) IS
  BEGIN
    UPDATE TALLER.VEHICULO
       SET MODELO         = p_modelo,
           MARCA          = p_marca,
           CLIENTE_CEDULA = p_cliente,
           TIPO_VEHICULO  = p_tipo,
           COLOR          = p_color,
           LINEA          = p_linea
     WHERE PLACA = p_placa;
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el vehículo.');
    END IF;
  END;

  PROCEDURE ELIMINAR(p_placa VARCHAR2) IS
  BEGIN
    DELETE FROM TALLER.VEHICULO WHERE PLACA = p_placa;
    IF SQL%ROWCOUNT = 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'No existe el vehículo.');
    END IF;
  END;

  PROCEDURE CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO, V.COLOR, V.LINEA,
             V.CLIENTE_CEDULA,
             P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO
        FROM TALLER.VEHICULO V
        JOIN TALLER.CLIENTE  C ON C.CEDULA = V.CLIENTE_CEDULA
        JOIN TALLER.PERSONA  P ON P.CEDULA = C.CEDULA
       ORDER BY V.PLACA;
  END;

  PROCEDURE BUSCAR_PLACA(p_placa VARCHAR2, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN p_resultado FOR
      SELECT V.PLACA, V.MODELO, V.MARCA, V.TIPO_VEHICULO, V.COLOR, V.LINEA,
             V.CLIENTE_CEDULA,
             P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO,
             P.SEGUNDO_APELLIDO, P.SEXO, P.TELEFONO, P.DIRECCION,
             P.CORREOELECTRONICO
        FROM TALLER.VEHICULO V
        JOIN TALLER.CLIENTE  C ON C.CEDULA = V.CLIENTE_CEDULA
        JOIN TALLER.PERSONA  P ON P.CEDULA = C.CEDULA
       WHERE V.PLACA = p_placa;
  END;

  PROCEDURE CONTAR(p_total OUT NUMBER) IS
  BEGIN
    SELECT COUNT(*) INTO p_total FROM TALLER.VEHICULO;
  END;

  PROCEDURE PRC_GUARDAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2) IS
  BEGIN
    GUARDAR(p_placa, p_modelo, p_marca, p_cliente, p_tipo, p_color, p_linea);
  END;

  PROCEDURE PRC_MODIFICAR(
    p_placa VARCHAR2, p_modelo NUMBER, p_marca VARCHAR2, p_cliente CHAR,
    p_tipo VARCHAR2, p_color VARCHAR2, p_linea VARCHAR2) IS
  BEGIN
    MODIFICAR(p_placa, p_modelo, p_marca, p_cliente, p_tipo, p_color, p_linea);
  END;

  PROCEDURE PRC_ELIMINAR(p_placa VARCHAR2) IS
  BEGIN
    ELIMINAR(p_placa);
  END;

  PROCEDURE PRC_CONSULTAR(p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    CONSULTAR(p_resultado);
  END;

  PROCEDURE PRC_BUSCAR_PLACA(p_placa VARCHAR2, p_resultado OUT SYS_REFCURSOR) IS
  BEGIN
    BUSCAR_PLACA(p_placa, p_resultado);
  END;

  PROCEDURE PRC_CONTAR(p_total OUT NUMBER) IS
  BEGIN
    CONTAR(p_total);
  END;
END PKG_VEHICULO;

/
--------------------------------------------------------
--  DDL for Function FN_OBTENER_EMPLEADO_POR_CEDULA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE FUNCTION "TALLER"."FN_OBTENER_EMPLEADO_POR_CEDULA" (
  p_cedula TALLER.EMPLEADO.CEDULA%TYPE
) RETURN SYS_REFCURSOR IS
  resultado SYS_REFCURSOR;
BEGIN
  OPEN resultado FOR SELECT E.CEDULA,
    TRIM(P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE) NOMBRES,
    TRIM(P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO) APELLIDOS,
    P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
    P.SEXO, P.TELEFONO, P.DIRECCION, P.CORREOELECTRONICO, E.ACTIVO,
    C.CARGOID, C.NOMBRECARGO
    FROM TALLER.EMPLEADO E JOIN TALLER.PERSONA P ON P.CEDULA = E.CEDULA
    JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
    WHERE TRIM(E.CEDULA) = TRIM(p_cedula);
  RETURN resultado;
END;

/
--------------------------------------------------------
--  DDL for Function FN_OBTENER_EMPLEADOS
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE FUNCTION "TALLER"."FN_OBTENER_EMPLEADOS" RETURN SYS_REFCURSOR IS
  resultado SYS_REFCURSOR;
BEGIN
  OPEN resultado FOR SELECT E.CEDULA,
    TRIM(P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE) NOMBRES,
    TRIM(P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO) APELLIDOS,
    P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
    P.SEXO, P.TELEFONO, P.DIRECCION, P.CORREOELECTRONICO, E.ACTIVO,
    C.CARGOID, C.NOMBRECARGO
    FROM TALLER.EMPLEADO E JOIN TALLER.PERSONA P ON P.CEDULA = E.CEDULA
    JOIN TALLER.CARGO C ON C.CARGOID = E.CARGO_CARGOID
    ORDER BY P.PRIMER_APELLIDO, P.PRIMER_NOMBRE;
  RETURN resultado;
END;

/
--------------------------------------------------------
--  DDL for Function FN_OBTENER_FACTURAID
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE FUNCTION "TALLER"."FN_OBTENER_FACTURAID" 
RETURN NUMBER IS
  v_nuevo_id NUMBER;
BEGIN
  SELECT TALLER.SEQ_FACTURAID.NEXTVAL INTO v_nuevo_id FROM DUAL;
  RETURN v_nuevo_id;
END;

/
--------------------------------------------------------
--  DDL for Function OBTENER_FACTURAID
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE FUNCTION "TALLER"."OBTENER_FACTURAID" 
RETURN NUMBER IS
  v_nuevo_id NUMBER;
BEGIN
  SELECT TALLER.SEQ_FACTURAID.NEXTVAL INTO v_nuevo_id FROM DUAL;
  RETURN v_nuevo_id;
END;

/
--------------------------------------------------------
--  Constraints for Table AGENDA
--------------------------------------------------------

  ALTER TABLE "TALLER"."AGENDA" MODIFY ("AGENDAID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."AGENDA" MODIFY ("EMPLEADO_CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."AGENDA" MODIFY ("ESTADO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."AGENDA" MODIFY ("VEHICULO_PLACA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."AGENDA" ADD CONSTRAINT "AGENDA_PK" PRIMARY KEY ("AGENDAID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table ARTICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."ARTICULO" MODIFY ("ARTICULOID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."ARTICULO" MODIFY ("NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."ARTICULO" MODIFY ("PRECIO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."ARTICULO" MODIFY ("PORCENTAJEIVA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."ARTICULO" MODIFY ("TIPO_ARTICULO_ID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."ARTICULO" ADD CONSTRAINT "CK_ARTICULO_STOCK" CHECK (CANTIDAD >= 0) ENABLE;
  ALTER TABLE "TALLER"."ARTICULO" ADD CONSTRAINT "ARTICULO_PK" PRIMARY KEY ("ARTICULOID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table CARGO
--------------------------------------------------------

  ALTER TABLE "TALLER"."CARGO" MODIFY ("CARGOID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."CARGO" ADD CONSTRAINT "CARGO_PK" PRIMARY KEY ("CARGOID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table CLIENTE
--------------------------------------------------------

  ALTER TABLE "TALLER"."CLIENTE" MODIFY ("CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."CLIENTE" ADD CONSTRAINT "CLIENTE_PK" PRIMARY KEY ("CEDULA")
  USING INDEX  ENABLE;
  ALTER TABLE "TALLER"."CLIENTE" ADD CONSTRAINT "UK_CLIENTE_TELEGRAM_CHAT" UNIQUE ("TELEGRAM_CHAT_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table DETALLE_FACTURA
--------------------------------------------------------

  ALTER TABLE "TALLER"."DETALLE_FACTURA" MODIFY ("DETALLEFACTURAID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_FACTURA" MODIFY ("CANTIDAD" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_FACTURA" MODIFY ("VALORUNITARIO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_FACTURA" MODIFY ("FACTURA_FACTURAID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_FACTURA" MODIFY ("ARTICULO_ARTICULOID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_FACTURA" MODIFY ("DESCUENTO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_FACTURA" ADD CONSTRAINT "CK_DETALLE_CANTIDAD" CHECK (CANTIDAD > 0) ENABLE;
  ALTER TABLE "TALLER"."DETALLE_FACTURA" ADD CONSTRAINT "CK_VALORUNITARIO" CHECK (VALORUNITARIO >= 0) ENABLE;
  ALTER TABLE "TALLER"."DETALLE_FACTURA" ADD CONSTRAINT "CK_PORCENTAJE_IVA" CHECK (PORCENTAJEIVA BETWEEN 0 AND 100) ENABLE;
  ALTER TABLE "TALLER"."DETALLE_FACTURA" ADD CONSTRAINT "DETALLE_FACTURA_PK" PRIMARY KEY ("DETALLEFACTURAID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table DETALLE_VEHICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."DETALLE_VEHICULO" MODIFY ("DETALLEVEHICULOID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_VEHICULO" MODIFY ("FACTURA_FACTURAID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_VEHICULO" MODIFY ("VEHICULO_PLACA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."DETALLE_VEHICULO" ADD PRIMARY KEY ("DETALLEVEHICULOID")
  USING INDEX  ENABLE;
  ALTER TABLE "TALLER"."DETALLE_VEHICULO" ADD CONSTRAINT "UQ_DETALLE_VEHICULO" UNIQUE ("FACTURA_FACTURAID", "VEHICULO_PLACA")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table EMPLEADO
--------------------------------------------------------

  ALTER TABLE "TALLER"."EMPLEADO" MODIFY ("CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."EMPLEADO" MODIFY ("CARGO_CARGOID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."EMPLEADO" MODIFY ("ESTADO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."EMPLEADO" MODIFY ("ACTIVO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."EMPLEADO" ADD CONSTRAINT "CK_EMPLEADO_ESTADO" CHECK (ESTADO IN ('Activo', 'Inactivo')) ENABLE;
  ALTER TABLE "TALLER"."EMPLEADO" ADD CONSTRAINT "CK_EMPLEADO_ACTIVO" CHECK (ACTIVO IN ('S', 'N')) ENABLE;
  ALTER TABLE "TALLER"."EMPLEADO" ADD CONSTRAINT "EMPLEADO_PK" PRIMARY KEY ("CEDULA")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table FACTURA
--------------------------------------------------------

  ALTER TABLE "TALLER"."FACTURA" MODIFY ("FACTURAID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("FECHA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("DESCUENTO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("SUBTOTAL" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("IVA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("ESTADOPAGO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("CLIENTE_CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("TIPO_DOCUMENTO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("DOMICILIO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" ADD CONSTRAINT "CK_FACTURA_TIPO" CHECK (TIPO_DOCUMENTO IN ('COTIZACION', 'VENTA')) ENABLE;
  ALTER TABLE "TALLER"."FACTURA" MODIFY ("EMPLEADO_CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."FACTURA" ADD CONSTRAINT "CK_FACTURA_DOMICILIO" CHECK (DOMICILIO IN ('S', 'N')) ENABLE;
  ALTER TABLE "TALLER"."FACTURA" ADD CONSTRAINT "FACTURA_PK" PRIMARY KEY ("FACTURAID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table NOTIFICACION_MANTENIMIENTO
--------------------------------------------------------

  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" MODIFY ("NOTIFICACIONID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" MODIFY ("ARTICULO_ARTICULOID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" MODIFY ("ESTADO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" MODIFY ("VEHICULO_PLACA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" ADD CONSTRAINT "NOTIFICACION_MANTENIMIENTO_PK" PRIMARY KEY ("NOTIFICACIONID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table PERSONA
--------------------------------------------------------

  ALTER TABLE "TALLER"."PERSONA" MODIFY ("CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."PERSONA" MODIFY ("SEXO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."PERSONA" MODIFY ("PRIMER_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."PERSONA" MODIFY ("PRIMER_APELLIDO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."PERSONA" ADD CONSTRAINT "CK_PERSONA_SEXO" CHECK (SEXO IN ('F', 'M')) ENABLE;
  ALTER TABLE "TALLER"."PERSONA" ADD CONSTRAINT "PERSONA_PK" PRIMARY KEY ("CEDULA")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table PROVEEDOR
--------------------------------------------------------

  ALTER TABLE "TALLER"."PROVEEDOR" MODIFY ("NIT" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."PROVEEDOR" MODIFY ("RAZONSOCIAL" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."PROVEEDOR" ADD CONSTRAINT "PROVEEDOR_PK" PRIMARY KEY ("NIT")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table SOAT
--------------------------------------------------------

  ALTER TABLE "TALLER"."SOAT" MODIFY ("SOATID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."SOAT" MODIFY ("VEHICULO_PLACA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."SOAT" ADD CONSTRAINT "SOAT_PK" PRIMARY KEY ("SOATID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table TIPO_ARTICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."TIPO_ARTICULO" MODIFY ("TIPO_ARTICULO_ID" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."TIPO_ARTICULO" MODIFY ("NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."TIPO_ARTICULO" ADD CONSTRAINT "PK_TIPO_ARTICULO" PRIMARY KEY ("TIPO_ARTICULO_ID")
  USING INDEX  ENABLE;
  ALTER TABLE "TALLER"."TIPO_ARTICULO" ADD CONSTRAINT "UK_TIPO_ARTICULO_NOMBRE" UNIQUE ("NOMBRE")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table VEHICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."VEHICULO" MODIFY ("CLIENTE_CEDULA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."VEHICULO" MODIFY ("TIPO_VEHICULO" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."VEHICULO" MODIFY ("PLACA" NOT NULL ENABLE);
  ALTER TABLE "TALLER"."VEHICULO" ADD CHECK (TIPO_VEHICULO IN ('Automovil', 'Motocicleta')) ENABLE;
  ALTER TABLE "TALLER"."VEHICULO" ADD CONSTRAINT "VEHICULO_PK" PRIMARY KEY ("PLACA")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table AGENDA
--------------------------------------------------------

  ALTER TABLE "TALLER"."AGENDA" ADD CONSTRAINT "AGENDA_EMPLEADO_FK" FOREIGN KEY ("EMPLEADO_CEDULA")
	  REFERENCES "TALLER"."EMPLEADO" ("CEDULA") ENABLE;
  ALTER TABLE "TALLER"."AGENDA" ADD CONSTRAINT "AGENDA_ARTICULO_FK" FOREIGN KEY ("ARTICULO_ARTICULOID")
	  REFERENCES "TALLER"."ARTICULO" ("ARTICULOID") ENABLE;
  ALTER TABLE "TALLER"."AGENDA" ADD CONSTRAINT "AGENDA_VEHICULO_FK" FOREIGN KEY ("VEHICULO_PLACA")
	  REFERENCES "TALLER"."VEHICULO" ("PLACA") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table ARTICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."ARTICULO" ADD CONSTRAINT "ARTICULO_PROVEEDOR_FK" FOREIGN KEY ("PROVEEDOR_NIT")
	  REFERENCES "TALLER"."PROVEEDOR" ("NIT") ENABLE;
  ALTER TABLE "TALLER"."ARTICULO" ADD CONSTRAINT "FK_ARTICULO_TIPO" FOREIGN KEY ("TIPO_ARTICULO_ID")
	  REFERENCES "TALLER"."TIPO_ARTICULO" ("TIPO_ARTICULO_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table CLIENTE
--------------------------------------------------------

  ALTER TABLE "TALLER"."CLIENTE" ADD CONSTRAINT "CLIENTE_PERSONA_FK" FOREIGN KEY ("CEDULA")
	  REFERENCES "TALLER"."PERSONA" ("CEDULA") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DETALLE_FACTURA
--------------------------------------------------------

  ALTER TABLE "TALLER"."DETALLE_FACTURA" ADD CONSTRAINT "DETALLE_FACTURA_ARTICULO_FK" FOREIGN KEY ("ARTICULO_ARTICULOID")
	  REFERENCES "TALLER"."ARTICULO" ("ARTICULOID") ENABLE;
  ALTER TABLE "TALLER"."DETALLE_FACTURA" ADD CONSTRAINT "DETALLE_FACTURA_FACTURA_FK" FOREIGN KEY ("FACTURA_FACTURAID")
	  REFERENCES "TALLER"."FACTURA" ("FACTURAID") ON DELETE CASCADE ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DETALLE_VEHICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."DETALLE_VEHICULO" ADD CONSTRAINT "DETALLE_VEHICULO_FACTURA_FK" FOREIGN KEY ("FACTURA_FACTURAID")
	  REFERENCES "TALLER"."FACTURA" ("FACTURAID") ON DELETE CASCADE ENABLE;
  ALTER TABLE "TALLER"."DETALLE_VEHICULO" ADD CONSTRAINT "DETALLE_VEHICULO_PLACA_FK" FOREIGN KEY ("VEHICULO_PLACA")
	  REFERENCES "TALLER"."VEHICULO" ("PLACA") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table EMPLEADO
--------------------------------------------------------

  ALTER TABLE "TALLER"."EMPLEADO" ADD CONSTRAINT "EMPLEADO_CARGO_FK" FOREIGN KEY ("CARGO_CARGOID")
	  REFERENCES "TALLER"."CARGO" ("CARGOID") ENABLE;
  ALTER TABLE "TALLER"."EMPLEADO" ADD CONSTRAINT "EMPLEADO_PERSONA_FK" FOREIGN KEY ("CEDULA")
	  REFERENCES "TALLER"."PERSONA" ("CEDULA") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table FACTURA
--------------------------------------------------------

  ALTER TABLE "TALLER"."FACTURA" ADD CONSTRAINT "FACTURA_CLIENTE_FK" FOREIGN KEY ("CLIENTE_CEDULA")
	  REFERENCES "TALLER"."CLIENTE" ("CEDULA") ENABLE;
  ALTER TABLE "TALLER"."FACTURA" ADD CONSTRAINT "FACTURA_EMPLEADO_FK" FOREIGN KEY ("EMPLEADO_CEDULA")
	  REFERENCES "TALLER"."EMPLEADO" ("CEDULA") ENABLE;
  ALTER TABLE "TALLER"."FACTURA" ADD CONSTRAINT "FACTURA_AGENDA_FK" FOREIGN KEY ("AGENDA_AGENDAID")
	  REFERENCES "TALLER"."AGENDA" ("AGENDAID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table NOTIFICACION_MANTENIMIENTO
--------------------------------------------------------

  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" ADD CONSTRAINT "NOTIF_MANT_ARTICULO_FK" FOREIGN KEY ("ARTICULO_ARTICULOID")
	  REFERENCES "TALLER"."ARTICULO" ("ARTICULOID") ENABLE;
  ALTER TABLE "TALLER"."NOTIFICACION_MANTENIMIENTO" ADD CONSTRAINT "NOTIF_MANT_VEHICULO_FK" FOREIGN KEY ("VEHICULO_PLACA")
	  REFERENCES "TALLER"."VEHICULO" ("PLACA") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table SOAT
--------------------------------------------------------

  ALTER TABLE "TALLER"."SOAT" ADD CONSTRAINT "SOAT_VEHICULO_FK" FOREIGN KEY ("VEHICULO_PLACA")
	  REFERENCES "TALLER"."VEHICULO" ("PLACA") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table VEHICULO
--------------------------------------------------------

  ALTER TABLE "TALLER"."VEHICULO" ADD CONSTRAINT "VEHICULO_CLIENTE_FK" FOREIGN KEY ("CLIENTE_CEDULA")
	  REFERENCES "TALLER"."CLIENTE" ("CEDULA") ENABLE;

