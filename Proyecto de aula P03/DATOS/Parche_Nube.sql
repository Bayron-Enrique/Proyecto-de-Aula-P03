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
