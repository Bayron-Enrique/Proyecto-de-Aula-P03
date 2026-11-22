-- Ejecutar una vez en el esquema TALLER antes de usar las notificaciones.
-- Agrega el estado requerido por el módulo y asigna PENDIENTE a filas existentes.
ALTER TABLE TALLER.NOTIFICACION_MANTENIMIENTO
    ADD (ESTADO VARCHAR2(20) DEFAULT 'PENDIENTE' NOT NULL);
