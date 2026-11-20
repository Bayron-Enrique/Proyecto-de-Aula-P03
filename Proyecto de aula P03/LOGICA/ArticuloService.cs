using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;

namespace LOGICA
{
    public class ArticuloService
    {
        public int TotalArticulos() { return repositorio.Contar(); }
        private readonly ArticuloRepository repositorio;
        private readonly ProveedorRepository proveedorRepositorio;

        public ArticuloService(ArticuloRepository repositorio, ProveedorRepository proveedorRepositorio)
        {
            this.repositorio = repositorio ?? throw new ArgumentNullException("repositorio");
            this.proveedorRepositorio = proveedorRepositorio ?? throw new ArgumentNullException("proveedorRepositorio");
        }

        public string Guardar(string nombre, double precio, double porcentajeIVA, string tipoArticulo,
            int cantidad, string descripcion, int duracion, string nitProveedor)
        {
            try
            {
                string error = Validar(nombre, precio, porcentajeIVA, tipoArticulo, cantidad, descripcion, duracion, nitProveedor);
                if (error != null) return error;
                Proveedor proveedor;
                Articulo articulo = CrearArticulo(0, nombre, precio, porcentajeIVA, tipoArticulo,
                    cantidad, descripcion, duracion, nitProveedor, out proveedor);
                error = ValidarDuplicado(articulo, null);
                if (error != null) return error;

                repositorio.Guardar(articulo);
                return "Se registró el artículo número " + articulo.ArticuloId + ".";
            }
            catch (Exception ex)
            {
                return "No se pudo guardar el artículo: " + ex.Message;
            }
        }

        public string Modificar(int articuloId, string nombre, double precio, double porcentajeIVA,
            string tipoArticulo, int cantidad, string descripcion, int duracion, string nitProveedor)
        {
            try
            {
                Articulo actual = repositorio.BuscarId(articuloId);
                if (actual == null) return "No se encontró el artículo para modificar.";
                if ((actual is Repuesto) != string.Equals(tipoArticulo, "Repuesto", StringComparison.OrdinalIgnoreCase) && repositorio.TieneMovimientos(articuloId))
                    return "No se puede cambiar el tipo de un artículo con movimientos de facturación.";
                string error = Validar(nombre, precio, porcentajeIVA, tipoArticulo, cantidad, descripcion, duracion, nitProveedor);
                if (error != null) return error;

                Proveedor proveedor;
                Articulo actualizado = CrearArticulo(articuloId, nombre, precio, porcentajeIVA, tipoArticulo,
                    cantidad, descripcion, duracion, nitProveedor, out proveedor);
                error = ValidarDuplicado(actualizado, articuloId);
                if (error != null) return error;

                repositorio.Modificar(actualizado);
                return "El artículo se modificó correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo modificar el artículo: " + ex.Message;
            }
        }

        public Articulo BuscarId(int articuloId)
        {
            return repositorio.BuscarId(articuloId);
        }

        public List<Articulo> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int articuloId)
        {
            try
            {
                if (repositorio.BuscarId(articuloId) == null) return "No se encontró el artículo.";
                repositorio.Eliminar(articuloId);
                return "El artículo se eliminó correctamente.";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el artículo: " + ex.Message;
            }
        }

        private string ValidarDuplicado(Articulo articulo, int? idExcluir)
        {
            foreach (Articulo existente in repositorio.Consultar())
            {
                if (idExcluir.HasValue && existente.ArticuloId == idExcluir.Value) continue;
                if (articulo is Repuesto nuevoRepuesto && existente is Repuesto repuestoExistente &&
                    string.Equals(existente.Nombre, articulo.Nombre, StringComparison.OrdinalIgnoreCase) &&
                    repuestoExistente.Proveedor != null && nuevoRepuesto.Proveedor != null &&
                    string.Equals(repuestoExistente.Proveedor.Nit, nuevoRepuesto.Proveedor.Nit, StringComparison.OrdinalIgnoreCase))
                    return "Ya existe un repuesto con ese nombre para el proveedor seleccionado.";
                if (articulo is Servicio && existente is Servicio &&
                    string.Equals(existente.Nombre, articulo.Nombre, StringComparison.OrdinalIgnoreCase))
                    return "Ya existe un servicio con ese nombre.";
            }
            return null;
        }

        private Articulo CrearArticulo(int id, string nombre, double precio, double iva, string tipo,
            int cantidad, string descripcion, int duracion, string nitProveedor, out Proveedor proveedor)
        {
            proveedor = null;
            if (string.Equals(tipo, "Repuesto", StringComparison.OrdinalIgnoreCase))
            {
                proveedor = proveedorRepositorio.BuscarNit(nitProveedor);
                return new Repuesto
                {
                    ArticuloId = id, Nombre = nombre.Trim(), PrecioVenta = Convert.ToDecimal(precio),
                    PorcentajeIVA = Convert.ToDecimal(iva), Cantidad = cantidad, Proveedor = proveedor
                };
            }
            return new Servicio
            {
                ArticuloId = id, Nombre = nombre.Trim(), PrecioVenta = Convert.ToDecimal(precio),
                PorcentajeIVA = Convert.ToDecimal(iva), Duracion = duracion,
                Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim()
            };
        }

        private string Validar(string nombre, double precio, double iva, string tipo, int cantidad,
            string descripcion, int duracion, string nitProveedor)
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre != nombre.Trim() || nombre.Contains("  ") || nombre.Length < 3 || nombre.Length > 100)
                return "El nombre debe tener entre 3 y 100 caracteres y no tener espacios sobrantes.";
            foreach (char c in nombre)
                if (!char.IsLetterOrDigit(c) && c != ' ' && "-./".IndexOf(c) < 0)
                    return "El nombre contiene caracteres no permitidos.";

            if (double.IsNaN(precio) || double.IsInfinity(precio) || precio <= 0 || precio > 99999999.99 || Math.Round(precio, 2) != precio)
                return "El precio debe ser mayor que cero, no superar 99.999.999,99 y tener máximo dos decimales.";
            if (iva != 0 && iva != 5 && iva != 19) return "El IVA solo puede ser 0, 5 o 19 por ciento.";

            bool repuesto = string.Equals(tipo, "Repuesto", StringComparison.OrdinalIgnoreCase);
            bool servicio = string.Equals(tipo, "Servicio", StringComparison.OrdinalIgnoreCase);
            if (!repuesto && !servicio) return "Selecciona Repuesto o Servicio.";

            if (repuesto)
            {
                if (cantidad < 0 || cantidad > 1000000) return "El stock debe estar entre 0 y 1.000.000 unidades.";
                if (string.IsNullOrWhiteSpace(nitProveedor)) return "Selecciona un proveedor para el repuesto.";
                if (proveedorRepositorio.BuscarNit(nitProveedor) == null) return "El proveedor seleccionado no existe.";
            }
            else
            {
                if (duracion < 1 || duracion > 10000) return "La duración debe estar entre 1 y 10.000 minutos.";
                if (!string.IsNullOrWhiteSpace(descripcion))
                {
                    if (descripcion != descripcion.Trim() || descripcion.Contains("  ") || descripcion.Length < 3 || descripcion.Length > 200)
                        return "La descripción debe tener entre 3 y 200 caracteres y no tener espacios sobrantes.";
                    foreach (char c in descripcion)
                        if (char.IsControl(c)) return "La descripción contiene caracteres no permitidos.";
                }
            }
            return null;
        }
    }
}
