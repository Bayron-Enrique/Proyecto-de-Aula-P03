using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class ArticuloService
    {
        ArticuloRepository repositorio;
        ProveedorRepository proveedorRepositorio;
        int siguienteArticuloId;

        public ArticuloService(ArticuloRepository repositorio, ProveedorRepository proveedorRepositorio)
        {
            this.repositorio = repositorio;
            this.proveedorRepositorio = proveedorRepositorio;
            siguienteArticuloId = 1;
        }

        public string Guardar(string nombre, double precio, double porcentajeIVA, string tipoArticulo, int cantidad, string descripcion, int duracion, string nitProveedor)
        {
            try
            {
                // Nombre
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return "El nombre del articulo es obligatorio y no puede estar vacio ni tener solo espacios ...";
                }
                if (nombre != nombre.Trim())
                {
                    return "El nombre del articulo no puede empezar ni terminar con espacios ...";
                }
                if (nombre.Contains("  "))
                {
                    return "El nombre del articulo no puede tener espacios seguidos ...";
                }
                if (nombre.Length < 3 || nombre.Length > 100)
                {
                    return "El nombre del articulo debe tener entre 3 y 100 caracteres ...";
                }
                foreach (char c in nombre)
                {
                    if (!char.IsLetterOrDigit(c) && c != ' ' && "-./".IndexOf(c) < 0)
                    {
                        return $"El nombre del articulo tiene el caracter '{c}' que no esta permitido ...";
                    }
                }

                // Precio
                if (precio <= 0)
                {
                    return "El precio debe ser mayor que cero ...";
                }
                if (precio > 99999999.99)
                {
                    return "El precio es demasiado alto ...";
                }
                if (Math.Round(precio, 2) != precio)
                {
                    return "El precio solo puede tener 2 decimales ...";
                }

                // IVA
                if (porcentajeIVA != 0 && porcentajeIVA != 5 && porcentajeIVA != 19)
                {
                    return "El porcentaje de IVA solo puede ser 0, 5 o 19 ...";
                }

                // Tipo de articulo (opcional)
                if (!string.IsNullOrEmpty(tipoArticulo))
                {
                    if (string.IsNullOrWhiteSpace(tipoArticulo))
                    {
                        return "El tipo de articulo no puede tener solo espacios ...";
                    }
                    if (tipoArticulo != tipoArticulo.Trim() || tipoArticulo.Contains("  "))
                    {
                        return "El tipo de articulo no puede tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (tipoArticulo.Length < 3 || tipoArticulo.Length > 20)
                    {
                        return "El tipo de articulo debe tener entre 3 y 20 caracteres ...";
                    }
                    foreach (char c in tipoArticulo)
                    {
                        if (!char.IsLetter(c) && c != ' ')
                        {
                            return $"El tipo de articulo tiene el caracter '{c}' que no esta permitido, solo letras ...";
                        }
                    }
                }

                // Cantidad y duracion
                if (cantidad < 0 || cantidad > 1000000)
                {
                    return "La cantidad debe estar entre 0 y 1000000 ...";
                }
                if (duracion < 0 || duracion > 10000)
                {
                    return "La duracion debe estar entre 0 y 10000 ...";
                }

                // Descripcion (opcional)
                if (!string.IsNullOrEmpty(descripcion))
                {
                    if (string.IsNullOrWhiteSpace(descripcion))
                    {
                        return "La descripcion no puede tener solo espacios ...";
                    }
                    if (descripcion != descripcion.Trim() || descripcion.Contains("  "))
                    {
                        return "La descripcion no puede tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (descripcion.Length < 3 || descripcion.Length > 200)
                    {
                        return "La descripcion debe tener entre 3 y 200 caracteres ...";
                    }
                    foreach (char c in descripcion)
                    {
                        if (char.IsControl(c))
                        {
                            return "La descripcion tiene caracteres no permitidos ...";
                        }
                    }
                }

                // Proveedor
                if (string.IsNullOrWhiteSpace(nitProveedor))
                {
                    return "Debe seleccionar un proveedor valido ...";
                }

                Proveedor proveedor = proveedorRepositorio.BuscarNit(nitProveedor);

                if (proveedor == null)
                {
                    return "El proveedor seleccionado no existe ...";
                }

                // Duplicados
                foreach (var existente in repositorio.Consultar())
                {
                    if (string.Equals(existente.Nombre, nombre, StringComparison.OrdinalIgnoreCase) && existente.Proveedor.Nit == nitProveedor)
                    {
                        return "Ya existe un articulo con ese nombre para ese proveedor ...";
                    }
                }

                int articuloId = GenerarArticuloId();

                Articulo articulo = new Articulo(articuloId, nombre, precio, porcentajeIVA, tipoArticulo, cantidad, descripcion, duracion, proveedor);
                repositorio.Guardar(articulo);

                return $"se registro el articulo numero {articuloId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        private int GenerarArticuloId()
        {
            int numero = siguienteArticuloId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteArticuloId = numero + 1;
            return numero;
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
                Articulo articulo = BuscarId(articuloId);

                if (articulo == null)
                {
                    return "No se encontro el articulo ...";
                }

                repositorio.Eliminar(articulo);

                return "El articulo se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}