-- Actualización del esquema existente de GOT-AUTO. Ejecutar como TALLER, con la
-- aplicación cerrada, en SQL*Plus o SQL Developer (Ejecutar script / F5).
-- Se puede reejecutar. Oracle confirma cada DDL: ante un error, corregir la causa
-- indicada y volver a ejecutar el archivo completo. No recalcula facturas históricas.
WHENEVER SQLERROR EXIT SQL.SQLCODE ROLLBACK
SET DEFINE OFF
SET SERVEROUTPUT ON
SET SQLBLANKLINES ON

-- La transacción en FacturaRepository se encarga del stock y los importes.
DECLARE
    PROCEDURE quitar_trigger(p_nombre VARCHAR2) IS
        e_ausente EXCEPTION;
        PRAGMA EXCEPTION_INIT(e_ausente, -4080);
    BEGIN
        EXECUTE IMMEDIATE 'DROP TRIGGER TALLER.' || p_nombre;
    EXCEPTION WHEN e_ausente THEN NULL;
    END;
BEGIN
    quitar_trigger('TRG_DETALLE_DESCUENTA_STOCK');
    quitar_trigger('TRG_DETALLE_CALCULO');
END;
/

-- Primero agregar las columnas. El bloque siguiente se compila cuando ya existen.
DECLARE
    PROCEDURE agregar_columna(p_tabla VARCHAR2, p_columna VARCHAR2, p_tipo VARCHAR2) IS
        v_existe NUMBER;
    BEGIN
        SELECT COUNT(*) INTO v_existe FROM ALL_TAB_COLUMNS
         WHERE OWNER = 'TALLER' AND TABLE_NAME = p_tabla AND COLUMN_NAME = p_columna;
        IF v_existe = 0 THEN
            EXECUTE IMMEDIATE 'ALTER TABLE TALLER.' || p_tabla ||
                              ' ADD (' || p_columna || ' ' || p_tipo || ')';
        END IF;
    END;
BEGIN
    agregar_columna('PERSONA', 'PRIMER_NOMBRE',    'VARCHAR2(100)');
    agregar_columna('PERSONA', 'SEGUNDO_NOMBRE',   'VARCHAR2(100)');
    agregar_columna('PERSONA', 'PRIMER_APELLIDO',  'VARCHAR2(100)');
    agregar_columna('PERSONA', 'SEGUNDO_APELLIDO', 'VARCHAR2(100)');
    agregar_columna('EMPLEADO', 'CLAVE_HASH', 'VARCHAR2(200)');
    agregar_columna('EMPLEADO', 'ACTIVO', 'CHAR(1) DEFAULT ''S'' NOT NULL');
    agregar_columna('DETALLE_FACTURA', 'DESCUENTO', 'NUMBER(12,2) DEFAULT 0 NOT NULL');
    agregar_columna('NOTIFICACION_MANTENIMIENTO', 'ESTADO', 'VARCHAR2(20) DEFAULT ''PENDIENTE'' NOT NULL');
END;
/

DECLARE
    v_faltantes NUMBER;
    PROCEDURE migrar_nombre(p_antiguo VARCHAR2, p_primero VARCHAR2, p_segundo VARCHAR2) IS
        v_existe NUMBER;
    BEGIN
        SELECT COUNT(*) INTO v_existe FROM ALL_TAB_COLUMNS
         WHERE OWNER = 'TALLER' AND TABLE_NAME = 'PERSONA' AND COLUMN_NAME = p_antiguo;
        IF v_existe > 0 THEN
            -- El primer término va en el primer campo y todo el resto en el segundo.
            -- Un primer campo ya migrado puede haber sido editado desde la aplicación.
            EXECUTE IMMEDIATE 'UPDATE TALLER.PERSONA SET ' || p_primero ||
                ' = REGEXP_SUBSTR(' || p_antiguo || ', ''[^[:space:]]+''), ' || p_segundo ||
                ' = COALESCE(' || p_segundo || ', RTRIM(REGEXP_REPLACE(' || p_antiguo ||
                ', ''^[[:space:]]*[^[:space:]]+[[:space:]]*'', ''''))) WHERE ' || p_primero || ' IS NULL';
        END IF;
    END;
BEGIN
    migrar_nombre('NOMBRES', 'PRIMER_NOMBRE', 'SEGUNDO_NOMBRE');
    migrar_nombre('APELLIDOS', 'PRIMER_APELLIDO', 'SEGUNDO_APELLIDO');
    SELECT COUNT(*) INTO v_faltantes FROM TALLER.PERSONA
     WHERE TRIM(PRIMER_NOMBRE) IS NULL OR TRIM(PRIMER_APELLIDO) IS NULL;
    IF v_faltantes > 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Revisar PERSONA: ' || v_faltantes ||
            ' fila(s) sin primer nombre o primer apellido recuperable.');
    END IF;

    UPDATE TALLER.EMPLEADO SET ACTIVO = 'S' WHERE ACTIVO IS NULL;
    UPDATE TALLER.EMPLEADO SET ACTIVO = UPPER(ACTIVO) WHERE ACTIVO IN ('s', 'n');
    SELECT COUNT(*) INTO v_faltantes FROM TALLER.EMPLEADO WHERE ACTIVO NOT IN ('S', 'N');
    IF v_faltantes > 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Revisar EMPLEADO.ACTIVO: existen valores distintos de S/N.');
    END IF;
    UPDATE TALLER.DETALLE_FACTURA SET DESCUENTO = 0 WHERE DESCUENTO IS NULL;
    UPDATE TALLER.NOTIFICACION_MANTENIMIENTO SET ESTADO = 'PENDIENTE' WHERE ESTADO IS NULL;
END;
/

DECLARE
    v_existe NUMBER;
    v_habilitada VARCHAR2(8);
    v_validada VARCHAR2(13);
    PROCEDURE nulabilidad(p_tabla VARCHAR2, p_columna VARCHAR2, p_admite_null CHAR) IS
        v_actual CHAR(1);
    BEGIN
        -- MAX devuelve NULL para las columnas de legado ausentes en bases nuevas.
        SELECT MAX(NULLABLE) INTO v_actual FROM ALL_TAB_COLUMNS
         WHERE OWNER = 'TALLER' AND TABLE_NAME = p_tabla AND COLUMN_NAME = p_columna;
        IF v_actual <> p_admite_null THEN
            EXECUTE IMMEDIATE 'ALTER TABLE TALLER.' || p_tabla || ' MODIFY (' || p_columna ||
                CASE p_admite_null WHEN 'Y' THEN ' NULL)' ELSE ' NOT NULL)' END;
        END IF;
    END;
BEGIN
    nulabilidad('PERSONA', 'PRIMER_NOMBRE', 'N');
    nulabilidad('PERSONA', 'PRIMER_APELLIDO', 'N');
    nulabilidad('PERSONA', 'SEGUNDO_NOMBRE', 'Y');
    nulabilidad('PERSONA', 'SEGUNDO_APELLIDO', 'Y');
    nulabilidad('PERSONA', 'NOMBRES', 'Y');
    nulabilidad('PERSONA', 'APELLIDOS', 'Y');
    nulabilidad('EMPLEADO', 'CLAVE_HASH', 'Y');
    nulabilidad('EMPLEADO', 'ACTIVO', 'N');
    nulabilidad('DETALLE_FACTURA', 'DESCUENTO', 'N');
    nulabilidad('NOTIFICACION_MANTENIMIENTO', 'ESTADO', 'N');

    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.EMPLEADO MODIFY (ACTIVO DEFAULT ''S'')';
    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.DETALLE_FACTURA MODIFY (DESCUENTO DEFAULT 0)';
    EXECUTE IMMEDIATE 'ALTER TABLE TALLER.NOTIFICACION_MANTENIMIENTO MODIFY (ESTADO DEFAULT ''PENDIENTE'')';

    SELECT COUNT(*), MAX(STATUS), MAX(VALIDATED) INTO v_existe, v_habilitada, v_validada
      FROM ALL_CONSTRAINTS WHERE OWNER = 'TALLER' AND TABLE_NAME = 'EMPLEADO'
       AND CONSTRAINT_NAME = 'CK_EMPLEADO_ACTIVO';
    IF v_existe = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE TALLER.EMPLEADO ADD CONSTRAINT CK_EMPLEADO_ACTIVO CHECK (ACTIVO IN (''S'', ''N''))';
    ELSIF v_habilitada <> 'ENABLED' OR v_validada <> 'VALIDATED' THEN
        EXECUTE IMMEDIATE 'ALTER TABLE TALLER.EMPLEADO ENABLE VALIDATE CONSTRAINT CK_EMPLEADO_ACTIVO';
    END IF;
END;
/

-- Algunas instalaciones anteriores al módulo de mantenimiento carecen de su secuencia.
DECLARE
    v_existe NUMBER;
    v_inicio NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_existe FROM ALL_SEQUENCES
     WHERE SEQUENCE_OWNER = 'TALLER' AND SEQUENCE_NAME = 'SEQ_NOTIFICACIONID';
    IF v_existe = 0 THEN
        SELECT GREATEST(1, NVL(MAX(NOTIFICACIONID), 0) + 1) INTO v_inicio FROM TALLER.NOTIFICACION_MANTENIMIENTO;
        EXECUTE IMMEDIATE 'CREATE SEQUENCE TALLER.SEQ_NOTIFICACIONID START WITH ' ||
            TO_CHAR(v_inicio, 'TM9') || ' INCREMENT BY 1 NOCACHE NOCYCLE';
    END IF;
END;
/

CREATE OR REPLACE FUNCTION TALLER.Obtener_FacturaID
RETURN NUMBER IS
    v_nuevo_id NUMBER;
BEGIN
    SELECT TALLER.SEQ_FACTURAID.NEXTVAL INTO v_nuevo_id FROM DUAL;
    RETURN v_nuevo_id;
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

-- CREATE OR REPLACE puede dejar un objeto INVALID sin detener SQL*Plus.
DECLARE
    v_validas NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_validas FROM ALL_OBJECTS
     WHERE OWNER = 'TALLER' AND OBJECT_TYPE = 'FUNCTION' AND STATUS = 'VALID'
       AND OBJECT_NAME IN ('OBTENER_FACTURAID', 'FN_VALIDAR_DISPONIBILIDAD_EMPLEADO',
                           'FN_OBTENER_EMPLEADOS', 'FN_OBTENER_EMPLEADO_POR_CEDULA');
    IF v_validas <> 4 THEN
        FOR e IN (SELECT NAME, LINE, TEXT FROM ALL_ERRORS WHERE OWNER = 'TALLER' AND TYPE = 'FUNCTION'
                   AND NAME IN ('OBTENER_FACTURAID', 'FN_VALIDAR_DISPONIBILIDAD_EMPLEADO',
                                'FN_OBTENER_EMPLEADOS', 'FN_OBTENER_EMPLEADO_POR_CEDULA')
                   ORDER BY NAME, SEQUENCE) LOOP
            DBMS_OUTPUT.PUT_LINE(e.NAME || ', línea ' || e.LINE || ': ' || e.TEXT);
        END LOOP;
        RAISE_APPLICATION_ERROR(-20003, 'Una función de TALLER no compiló. Revisar los errores anteriores.');
    END IF;
END;
/

-- Catálogos y primer administrador en una sola transacción.
DECLARE
    v_id_tipo TALLER.TIPO_ARTICULO.TIPO_ARTICULO_ID%TYPE;
    v_id_cargo TALLER.CARGO.CARGOID%TYPE;
    v_admin_cargo TALLER.CARGO.CARGOID%TYPE;
    v_cedula TALLER.PERSONA.CEDULA%TYPE;
    v_existe NUMBER;
BEGIN
    LOCK TABLE TALLER.TIPO_ARTICULO IN EXCLUSIVE MODE;
    FOR tipo IN (SELECT 'Repuesto' nombre FROM DUAL UNION ALL SELECT 'Servicio' FROM DUAL) LOOP
        SELECT COUNT(*) INTO v_existe FROM TALLER.TIPO_ARTICULO WHERE UPPER(TRIM(NOMBRE)) = UPPER(tipo.nombre);
        IF v_existe = 0 THEN
            SELECT NVL(MAX(TIPO_ARTICULO_ID), 0) + 1 INTO v_id_tipo FROM TALLER.TIPO_ARTICULO;
            INSERT INTO TALLER.TIPO_ARTICULO (TIPO_ARTICULO_ID, NOMBRE) VALUES (v_id_tipo, tipo.nombre);
        END IF;
    END LOOP;

    LOCK TABLE TALLER.CARGO IN EXCLUSIVE MODE;
    FOR cargo IN (SELECT COLUMN_VALUE nombre FROM TABLE(SYS.ODCIVARCHAR2LIST(
        'Administrador', 'Gerente De Servicios', 'Jefe De Taller', 'Lider De Calidad',
        'Asesor De Servicios', 'Practicante Aux Mecanico', 'Electromecanico', 'Latonero',
        'Pintor', 'Asesor De Colision', 'Tecnico Automotriz', 'Recepcionista', 'Chofer',
        'Lavador', 'Personal De Limpieza'))) LOOP
        SELECT MIN(CARGOID) INTO v_id_cargo FROM TALLER.CARGO
         WHERE (cargo.nombre = 'Administrador' AND UPPER(TRIM(NOMBRECARGO)) = 'ADMINISTRADOR')
            OR (cargo.nombre <> 'Administrador' AND
                NLSSORT(TRIM(NOMBRECARGO), 'NLS_SORT=BINARY_AI') = NLSSORT(cargo.nombre, 'NLS_SORT=BINARY_AI'));
        IF v_id_cargo IS NULL THEN
            -- La secuencia puede ir por detrás de IDs importados manualmente.
            LOOP
                SELECT TALLER.SEQ_CARGOID.NEXTVAL INTO v_id_cargo FROM DUAL;
                SELECT COUNT(*) INTO v_existe FROM TALLER.CARGO WHERE CARGOID = v_id_cargo;
                EXIT WHEN v_existe = 0;
            END LOOP;
            INSERT INTO TALLER.CARGO (CARGOID, NOMBRECARGO) VALUES (v_id_cargo, cargo.nombre);
        END IF;
        IF cargo.nombre = 'Administrador' THEN v_admin_cargo := v_id_cargo; END IF;
    END LOOP;

    LOCK TABLE TALLER.PERSONA IN SHARE ROW EXCLUSIVE MODE;
    LOCK TABLE TALLER.EMPLEADO IN SHARE ROW EXCLUSIVE MODE;
    SELECT COUNT(*) INTO v_existe FROM TALLER.EMPLEADO e JOIN TALLER.CARGO c ON c.CARGOID = e.CARGO_CARGOID
     WHERE UPPER(TRIM(c.NOMBRECARGO)) = 'ADMINISTRADOR';
    IF v_existe = 0 THEN
        -- Identificador técnico local compatible con la validación de cédulas.
        -- No se reutiliza la identidad de un cliente ni la de otro empleado.
        LOOP
            v_cedula := TO_CHAR(TRUNC(DBMS_RANDOM.VALUE(1000000000, 10000000000)), 'FM0000000000');
            SELECT COUNT(*) INTO v_existe FROM TALLER.PERSONA WHERE CEDULA = v_cedula;
            EXIT WHEN v_existe = 0;
        END LOOP;
        INSERT INTO TALLER.PERSONA (CEDULA, PRIMER_NOMBRE, PRIMER_APELLIDO, SEXO)
        VALUES (v_cedula, 'Administrador', 'Inicial', 'M');
        INSERT INTO TALLER.EMPLEADO (CEDULA, CARGO_CARGOID, CLAVE_HASH, ACTIVO)
        VALUES (v_cedula, v_admin_cargo, NULL, 'S');
        DBMS_OUTPUT.PUT_LINE('Administrador inicial creado. Usuario (cédula): ' || TRIM(v_cedula));
        DBMS_OUTPUT.PUT_LINE('Primer acceso en FrmLogin: introducir esa cédula como clave temporal y definir la contraseña personal.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Ya existe un empleado con cargo Administrador. Se conservan su estado y contraseña.');
    END IF;
END;
/

COMMIT;
