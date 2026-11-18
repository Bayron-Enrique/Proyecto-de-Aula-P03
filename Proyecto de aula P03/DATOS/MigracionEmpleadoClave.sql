-- Ejecutar una sola vez en el esquema TALLER si EMPLEADO aún no contiene esta columna.
ALTER TABLE TALLER.EMPLEADO ADD (CLAVE_HASH VARCHAR2(200) NULL);
