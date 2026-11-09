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
                if (string.IsNullOrEmpty(estadoPago))
                {
                    return "El estado de pago es obligatorio ...";
                }
                if (descuento < 0)
                {
                    return "El descuento no puede ser negativo ...";
                }

                Cliente cliente = clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "Debe seleccionar un cliente valido ...";
                }

                Empleado empleado = empleadoRepositorio.BuscarCedula(cedulaEmpleado);

                if (empleado == null)
                {
                    return "Debe seleccionar un empleado valido ...";
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

                Articulo articulo = articuloRepositorio.BuscarId(articuloId);

                if (articulo == null)
                {
                    return "Debe seleccionar un articulo valido ...";
                }

                if (cantidad <= 0)
                {
                    return "La cantidad debe ser mayor que cero ...";
                }

                if (cantidad > articulo.Cantidad)
                {
                    return $"No hay existencias suficientes, solo quedan {articulo.Cantidad} ...";
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