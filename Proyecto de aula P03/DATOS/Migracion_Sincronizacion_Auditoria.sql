-- Ejecutar como TALLER (o con permisos DDL sobre TALLER), con la aplicación cerrada.
-- SQL*Plus / SQL Developer: ejecutar como script (F5). Reejecutable sin perder datos.
-- Oracle confirma DDL automáticamente; un fallo detiene el script y permite reanudarlo.
WHENEVER SQLERROR EXIT SQL.SQLCODE ROLLBACK
SET DEFINE OFF

DECLARE
    PROCEDURE agregar(tabla VARCHAR2, columna VARCHAR2, definicion VARCHAR2) IS
    BEGIN
        EXECUTE IMMEDIATE 'ALTER TABLE TALLER.' || tabla || ' ADD (' || columna || ' ' || definicion || ')';
    EXCEPTION WHEN OTHERS THEN
        IF SQLCODE <> -1430 THEN RAISE; END IF; -- columna existente
    END;
    PROCEDURE obligatorio(tabla VARCHAR2, columna VARCHAR2) IS
        anulable VARCHAR2(1);
    BEGIN
        SELECT NULLABLE INTO anulable FROM ALL_TAB_COLUMNS
        WHERE OWNER = 'TALLER' AND TABLE_NAME = tabla AND COLUMN_NAME = columna;
        IF anulable = 'Y' THEN
            EXECUTE IMMEDIATE 'ALTER TABLE TALLER.' || tabla || ' MODIFY (' || columna || ' NOT NULL)';
        END IF;
    END;
    PROCEDURE opcional(tabla VARCHAR2, columna VARCHAR2) IS
        anulable VARCHAR2(1);
    BEGIN
        SELECT NULLABLE INTO anulable FROM ALL_TAB_COLUMNS
        WHERE OWNER = 'TALLER' AND TABLE_NAME = tabla AND COLUMN_NAME = columna;
        IF anulable = 'N' THEN
            EXECUTE IMMEDIATE 'ALTER TABLE TALLER.' || tabla || ' MODIFY (' || columna || ' NULL)';
        END IF;
    END;
    PROCEDURE migrar_nombre(antiguo VARCHAR2, primero VARCHAR2, segundo VARCHAR2) IS
        cantidad NUMBER;
        anulable VARCHAR2(1);
    BEGIN
        SELECT COUNT(*), MAX(NULLABLE) INTO cantidad, anulable FROM ALL_TAB_COLUMNS
        WHERE OWNER = 'TALLER' AND TABLE_NAME = 'PERSONA' AND COLUMN_NAME = antiguo;
        IF cantidad > 0 THEN
            -- Conserva el primer término y todo el resto; no elimina nombres compuestos.
            -- Solo completa identidades aún no migradas: las ediciones posteriores se respetan.
            EXECUTE IMMEDIATE 'UPDATE TALLER.PERSONA SET ' || primero ||
                ' = REGEXP_SUBSTR(TRIM(' || antiguo || '), ''^[^[:space:]]+''), ' ||
                segundo || ' = COALESCE(' || segundo || ', TRIM(REGEXP_REPLACE(TRIM(' || antiguo ||
                '), ''^[^[:space:]]+[[:space:]]*'', ''''))) WHERE ' || primero || ' IS NULL';
            IF anulable = 'N' THEN
                -- Se conservan las columnas antiguas sin exigirlas a los nuevos INSERT de C#.
                EXECUTE IMMEDIATE 'ALTER TABLE TALLER.PERSONA MODIFY (' || antiguo || ' NULL)';
            END IF;
        END IF;
    END;
    faltantes NUMBER;
BEGIN
    agregar('PERSONA', 'PRIMER_NOMBRE', 'VARCHAR2(100)');
    agregar('PERSONA', 'SEGUNDO_NOMBRE', 'VARCHAR2(100) NULL');
    agregar('PERSONA', 'PRIMER_APELLIDO', 'VARCHAR2(100)');
    agregar('PERSONA', 'SEGUNDO_APELLIDO', 'VARCHAR2(100) NULL');
    migrar_nombre('NOMBRES', 'PRIMER_NOMBRE', 'SEGUNDO_NOMBRE');
    migrar_nombre('APELLIDOS', 'PRIMER_APELLIDO', 'SEGUNDO_APELLIDO');
    EXECUTE IMMEDIATE 'SELECT COUNT(*) FROM TALLER.PERSONA WHERE PRIMER_NOMBRE IS NULL OR PRIMER_APELLIDO IS NULL' INTO faltantes;
    IF faltantes > 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Hay personas sin primer nombre/apellido: corregir los datos y reejecutar.');
    END IF;
    obligatorio('PERSONA', 'PRIMER_NOMBRE');
    obligatorio('PERSONA', 'PRIMER_APELLIDO');

    agregar('EMPLEADO', 'CLAVE_HASH', 'VARCHAR2(200) NULL');
    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.EMPLEADO MODIFY (CLAVE_HASH VARCHAR2(200))';
    opcional('EMPLEADO', 'CLAVE_HASH');
    agregar('EMPLEADO', 'ACTIVO', 'CHAR(1) DEFAULT ''S'' NOT NULL');
    EXECUTE IMMEDIATE 'UPDATE TALLER.EMPLEADO SET ACTIVO = ''S'' WHERE ACTIVO IS NULL';
    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.EMPLEADO MODIFY (ACTIVO DEFAULT ''S'')';
    obligatorio('EMPLEADO', 'ACTIVO');

    agregar('DETALLE_FACTURA', 'DESCUENTO', 'NUMBER(12,2) DEFAULT 0 NOT NULL');
    EXECUTE IMMEDIATE 'UPDATE TALLER.DETALLE_FACTURA SET DESCUENTO = 0 WHERE DESCUENTO IS NULL';
    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.DETALLE_FACTURA MODIFY (DESCUENTO NUMBER(12,2) DEFAULT 0)';
    obligatorio('DETALLE_FACTURA', 'DESCUENTO');

    agregar('NOTIFICACION_MANTENIMIENTO', 'ESTADO', 'VARCHAR2(20) DEFAULT ''PENDIENTE'' NOT NULL');
    EXECUTE IMMEDIATE 'UPDATE TALLER.NOTIFICACION_MANTENIMIENTO SET ESTADO = ''PENDIENTE'' WHERE ESTADO IS NULL';
    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.NOTIFICACION_MANTENIMIENTO MODIFY (ESTADO DEFAULT ''PENDIENTE'')';
    obligatorio('NOTIFICACION_MANTENIMIENTO', 'ESTADO');
    COMMIT;
END;
/

DECLARE
    existentes NUMBER;
BEGIN
    SELECT COUNT(*) INTO existentes FROM ALL_CONSTRAINTS
    WHERE OWNER = 'TALLER' AND TABLE_NAME = 'EMPLEADO' AND CONSTRAINT_NAME = 'CK_EMPLEADO_ACTIVO';
    IF existentes = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE TALLER.EMPLEADO ADD CONSTRAINT CK_EMPLEADO_ACTIVO CHECK (ACTIVO IN (''S'', ''N''))';
    END IF;
END;
/

-- Catálogo inicial: bloqueo para asignar IDs sin colisiones y respetar IDs existentes.
DECLARE
    siguiente NUMBER;
BEGIN
    LOCK TABLE TALLER.TIPO_ARTICULO IN EXCLUSIVE MODE;
    FOR tipo IN (SELECT 'Repuesto' nombre FROM DUAL UNION ALL SELECT 'Servicio' FROM DUAL) LOOP
        SELECT NVL(MAX(TIPO_ARTICULO_ID), 0) + 1 INTO siguiente FROM TALLER.TIPO_ARTICULO;
        INSERT INTO TALLER.TIPO_ARTICULO (TIPO_ARTICULO_ID, NOMBRE)
        SELECT siguiente, tipo.nombre FROM DUAL
        WHERE NOT EXISTS (SELECT 1 FROM TALLER.TIPO_ARTICULO WHERE UPPER(TRIM(NOMBRE)) = UPPER(tipo.nombre));
    END LOOP;
    COMMIT;
END;
/

-- FacturaRepository es la única fuente de movimientos y cálculos de documentos.
DECLARE
    PROCEDURE quitar(nombre VARCHAR2) IS
    BEGIN
        EXECUTE IMMEDIATE 'DROP TRIGGER TALLER.' || nombre;
    EXCEPTION WHEN OTHERS THEN
        IF SQLCODE <> -4080 THEN RAISE; END IF; -- trigger ya inexistente
    END;
BEGIN
    quitar('TRG_DETALLE_DESCUENTA_STOCK');
    quitar('TRG_DETALLE_CALCULO');
END;
/

CREATE OR REPLACE FUNCTION TALLER.FN_VALIDAR_DISPONIBILIDAD_EMPLEADO(
    p_cedula_empleado TALLER.EMPLEADO.CEDULA%TYPE,
    p_fecha_cita TALLER.AGENDA.FECHA%TYPE
) RETURN NUMBER IS
    cantidad NUMBER;
BEGIN
    SELECT COUNT(*) INTO cantidad FROM TALLER.AGENDA
    WHERE EMPLEADO_CEDULA = p_cedula_empleado AND FECHA = p_fecha_cita
      AND UPPER(TRIM(ESTADO)) <> 'CANCELADA';
    RETURN CASE WHEN cantidad = 0 THEN 1 ELSE 0 END;
END;
/
CREATE OR REPLACE FUNCTION TALLER.FN_OBTENER_EMPLEADOS RETURN SYS_REFCURSOR IS
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
CREATE OR REPLACE FUNCTION TALLER.FN_OBTENER_EMPLEADO_POR_CEDULA(
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
