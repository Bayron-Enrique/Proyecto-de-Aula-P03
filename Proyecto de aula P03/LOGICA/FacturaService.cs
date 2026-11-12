using DATOS;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGICA
{
    public class FacturaService
    {
        FacturaRepository repositorio;
        ClienteRepository clienteRepositorio;
        EmpleadoRepository empleadoRepositorio;
        ArticuloRepository articuloRepositorio;
        int siguienteFacturaId;
        int siguienteDetalleId;

        public FacturaService(FacturaRepository repositorio, ClienteRepository clienteRepositorio, EmpleadoRepository empleadoRepositorio, ArticuloRepository articuloRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.empleadoRepositorio = empleadoRepositorio;
            this.articuloRepositorio = articuloRepositorio;
            siguienteFacturaId = 1;
            siguienteDetalleId = 1;
        }

        public string Guardar(DateTime fecha, double descuento, string observaciones, string estadoPago, string cedulaCliente, string cedulaEmpleado)
        {
            try
            {
                // Fecha
                if (fecha.Year < 2000)
                {
                    return "La fecha de la factura no es valida ...";
                }
                if (fecha > DateTime.Now)
                {
                    return "La fecha de la factura no puede ser futura ...";
                }

                // Descuento
                if (descuento < 0)
                {
                    return "El descuento no puede ser negativo ...";
                }
                if (descuento > 99999999.99)
                {
                    return "El descuento es demasiado alto ...";
                }
                if (Math.Round(descuento, 2) != descuento)
                {
                    return "El descuento solo puede tener 2 decimales ...";
                }

                // Observaciones (opcional)
                if (!string.IsNullOrEmpty(observaciones))
                {
                    if (string.IsNullOrWhiteSpace(observaciones))
                    {
                        return "Las observaciones no pueden tener solo espacios ...";
                    }
                    if (observaciones != observaciones.Trim() || observaciones.Contains("  "))
                    {
                        return "Las observaciones no pueden tener espacios al inicio, al final ni seguidos ...";
                    }
                    if (observaciones.Length < 3 || observaciones.Length > 200)
                    {
                        return "Las observaciones deben tener entre 3 y 200 caracteres ...";
                    }
                    foreach (char c in observaciones)
                    {
                        if (char.IsControl(c))
                        {
                            return "Las observaciones tienen caracteres no permitidos ...";
                        }
                    }
                }

                // Estado de pago
                if (estadoPago != "Pendiente" && estadoPago != "Pagada" && estadoPago != "Anulada")
                {
                    return "El estado de pago debe ser uno de estos: Pendiente, Pagada, Anulada ...";
                }

                // Cliente y empleado
                if (string.IsNullOrWhiteSpace(cedulaCliente))
                {
                    return "Debe seleccionar un cliente valido ...";
                }
                if (string.IsNullOrWhiteSpace(cedulaEmpleado))
                {
                    return "Debe seleccionar un empleado valido ...";
                }

                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "El cliente seleccionado no existe ...";
                }

                Empleado empleado = empleadoRepositorio.BuscarCedula(cedulaEmpleado);

                if (empleado == null)
                {
                    return "El empleado seleccionado no existe ...";
                }

                int facturaId = GenerarFacturaId();

                Factura factura = new Factura();
                factura.FacturaID = facturaId;
                factura.Fecha = fecha;
                factura.Descuento = descuento;
                factura.Observaciones = observaciones;
                factura.EstadoPago = estadoPago;
                factura.Cliente = cliente;
                factura.Empleado = empleado;

                repositorio.Guardar(factura);

                return $"se registro la factura numero {facturaId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al guardar: {ex.Message}";
            }
        }

        public string AgregarDetalle(int facturaId, int articuloId, int cantidad)
        {
            try
            {
                Factura factura = BuscarId(facturaId);

                if (factura == null)
                {
                    return "No se encontro la factura ...";
                }

                if (factura.EstadoPago == "Pagada" || factura.EstadoPago == "Anulada")
                {
                    return $"No se pueden agregar articulos a una factura {factura.EstadoPago.ToLower()} ...";
                }

                if (articuloId <= 0)
                {
                    return "Debe seleccionar un articulo valido ...";
                }

                Articulo articulo = articuloRepositorio.BuscarId(articuloId);

                if (articulo == null)
                {
                    return "El articulo seleccionado no existe ...";
                }

                if (cantidad < 1 || cantidad > 1000000)
                {
                    return "La cantidad debe estar entre 1 y 1000000 ...";
                }

                if (cantidad > articulo.Cantidad)
                {
                    return $"No hay existencias suficientes, solo quedan {articulo.Cantidad} ...";
                }

                foreach (var d in factura.Detalles)
                {
                    if (d.Articulo.ArticuloId == articuloId)
                    {
                        return "Ese articulo ya esta en la factura ...";
                    }
                }

                DetalleFactura detalle = new DetalleFactura();
                detalle.DetalleFacturaID = GenerarDetalleId();
                detalle.Cantidad = cantidad;
                detalle.Articulo = articulo;
                detalle.ValorUnitario = articulo.Precio;
                detalle.ValorIVA = (articulo.Precio * cantidad) * (articulo.PorcentajeIVA / 100);

                factura.Detalles.Add(detalle);
                factura.CalcularTotales();

                articulo.Cantidad -= cantidad;

                return $"se agrego el detalle a la factura {facturaId} desde la logica ...";
            }
            catch (Exception ex)
            {
                return $"ocurrio un error al agregar el detalle: {ex.Message}";
            }
        }

        private int GenerarFacturaId()
        {
            int numero = siguienteFacturaId;

            while (BuscarId(numero) != null)
            {
                numero++;
            }

            siguienteFacturaId = numero + 1;
            return numero;
        }

        private int GenerarDetalleId()
        {
            int numero = siguienteDetalleId;
            siguienteDetalleId = numero + 1;
            return numero;
        }

        public Factura BuscarId(int facturaId)
        {
            return repositorio.BuscarId(facturaId);
        }

        public List<Factura> Consultar()
        {
            return repositorio.Consultar();
        }

        public string Eliminar(int facturaId)
        {
            try
            {
                Factura factura = BuscarId(facturaId);

                if (factura == null)
                {
                    return "No se encontro la factura ...";
                }

                repositorio.Eliminar(factura);

                return "La factura se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}