
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
        DetalleFacturaRepository detalleRepositorio;
        ClienteRepository clienteRepositorio;
        EmpleadoRepository empleadoRepositorio;
        ArticuloRepository articuloRepositorio;
        private readonly VehiculoRepository vehiculoRepositorio;
        private readonly AgendaRepository agendaRepositorio;

        public FacturaService(
            FacturaRepository repositorio,
            ClienteRepository clienteRepositorio,
            EmpleadoRepository empleadoRepositorio,
            ArticuloRepository articuloRepositorio)
            : this(
                repositorio,
                clienteRepositorio,
                empleadoRepositorio,
                articuloRepositorio,
                new DetalleFacturaRepository())
        {
        }

        public FacturaService(
            FacturaRepository repositorio,
            ClienteRepository clienteRepositorio,
            EmpleadoRepository empleadoRepositorio,
            ArticuloRepository articuloRepositorio,
            DetalleFacturaRepository detalleRepositorio)
        {
            this.repositorio = repositorio;
            this.clienteRepositorio = clienteRepositorio;
            this.empleadoRepositorio = empleadoRepositorio;
            this.articuloRepositorio = articuloRepositorio;
            this.detalleRepositorio = detalleRepositorio;
            this.vehiculoRepositorio = new VehiculoRepository();
            this.agendaRepositorio = new AgendaRepository();
        }

        public string GuardarDocumento(Factura factura)
        {
            try
            {
                if (factura == null) return "No hay datos del documento para guardar.";
                if (factura.Fecha.Year < 2000 || factura.Fecha > DateTime.Now) return "La fecha del documento debe ser válida y no futura.";
                factura.TipoDocumento = (factura.TipoDocumento ?? string.Empty).Trim().ToUpperInvariant();
                if (factura.TipoDocumento != "VENTA" && factura.TipoDocumento != "COTIZACION") return "Selecciona VENTA o COTIZACION.";
                if (factura.Cliente == null || string.IsNullOrWhiteSpace(factura.Cliente.Cedula)) return "Selecciona un cliente.";
                if (factura.Empleado == null || string.IsNullOrWhiteSpace(factura.Empleado.Cedula)) return "Selecciona el empleado que atiende.";
                string estadoPago = (factura.EstadoPago ?? "Pendiente").Trim();
                if (string.Equals(estadoPago, "Anulada", StringComparison.OrdinalIgnoreCase))
                    return "No se puede crear un documento anulado. Usa Anular documento sobre una venta o cotización existente.";
                if (!string.Equals(estadoPago, "Pendiente", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(estadoPago, "Pagada", StringComparison.OrdinalIgnoreCase))
                    return "El estado de pago debe ser Pendiente o Pagada.";
                factura.EstadoPago = string.Equals(estadoPago, "Pagada", StringComparison.OrdinalIgnoreCase) ? "Pagada" : "Pendiente";
                if (factura.Detalles == null || factura.Detalles.Count == 0) return "Agrega al menos un artículo al detalle.";
                if (factura.Domicilio && string.IsNullOrWhiteSpace(factura.DireccionDomicilio)) return "Escribe la dirección de domicilio.";
                if (factura.DireccionDomicilio != null && factura.DireccionDomicilio.Trim().Length > 150) return "La dirección admite hasta 150 caracteres.";
                if (!string.IsNullOrWhiteSpace(factura.Observaciones) && factura.Observaciones.Trim().Length > 200) return "Las observaciones admiten hasta 200 caracteres.";

                Cliente cliente = clienteRepositorio.BuscarCedula(factura.Cliente.Cedula);
                Empleado empleado = empleadoRepositorio.BuscarCedula(factura.Empleado.Cedula);
                if (cliente == null) return "El cliente seleccionado ya no existe.";
                if (empleado == null || !empleado.Activo) return "El empleado seleccionado no existe o está inactivo.";
                factura.Cliente = cliente;
                factura.Empleado = empleado;

                if (factura.Vehiculo != null)
                {
                    Vehiculo vehiculo = vehiculoRepositorio.BuscarPlaca(factura.Vehiculo.Placa);
                    if (vehiculo == null) return "El vehículo seleccionado ya no existe.";
                    if (vehiculo.Cliente == null || vehiculo.Cliente.Cedula != cliente.Cedula) return "El vehículo no pertenece al cliente seleccionado.";
                    factura.Vehiculo = vehiculo;
                }
                if (factura.AgendaID.HasValue)
                {
                    if (factura.TipoDocumento != "VENTA") return "Una cotización no puede completar una cita. Asocia la cita al registrar una venta.";
                    if (factura.Vehiculo == null) return "Selecciona el vehículo de la cita.";
                    Agenda agenda = agendaRepositorio.BuscarId(factura.AgendaID.Value);
                    if (agenda == null) return "La cita seleccionada ya no existe.";
                    if (agenda.Vehiculo == null || agenda.Vehiculo.Cliente == null || agenda.Vehiculo.Cliente.Cedula != cliente.Cedula || agenda.Vehiculo.Placa != factura.Vehiculo.Placa) return "La cita no corresponde al cliente y vehículo seleccionados.";
                    if (string.Equals(agenda.Estado, "CANCELADA", StringComparison.OrdinalIgnoreCase)) return "No se puede facturar una cita cancelada.";
                    if (agenda.ArticuloServicio == null) return "La cita no tiene un servicio asociado para incluir en la factura.";
                    if (!factura.Detalles.Any(d => d != null && d.Articulo is Servicio &&
                        d.Articulo.ArticuloId == agenda.ArticuloServicio.ArticuloId))
                        return "Incluye en la factura el servicio agendado: " + agenda.ArticuloServicio.Nombre + ".";
                }

                foreach (DetalleFactura detalle in factura.Detalles)
                {
                    if (detalle == null || detalle.Articulo == null || detalle.Cantidad < 1) return "Cada línea debe tener un artículo y una cantidad positiva.";
                    Articulo articulo = articuloRepositorio.BuscarId(detalle.Articulo.ArticuloId);
                    if (articulo == null) return "Uno de los artículos ya no existe.";
                    decimal bruto = articulo.PrecioVenta * detalle.Cantidad;
                    if (bruto > 99999999.99m) return "El subtotal de cada línea no puede superar 99.999.999,99 por el límite de Oracle.";
                    if (detalle.Descuento < 0 || detalle.Descuento > bruto || Math.Round(detalle.Descuento, 2) != detalle.Descuento)
                        return "El descuento de cada línea debe estar entre cero y el subtotal, con máximo dos decimales.";
                    if (factura.TipoDocumento == "VENTA" && articulo is Repuesto repuesto && detalle.Cantidad > repuesto.Cantidad)
                        return "Stock insuficiente para " + articulo.Nombre + ". Disponibles: " + repuesto.Cantidad + ".";
                    detalle.Articulo = articulo;
                    detalle.ValorUnitario = articulo.PrecioVenta;
                    detalle.PorcentajeIVA = articulo.PorcentajeIVA;
                    detalle.Descripcion = articulo.Nombre;
                }

                factura.EstadoPago = string.IsNullOrWhiteSpace(factura.EstadoPago) ? "Pendiente" : factura.EstadoPago;
                factura.DireccionDomicilio = factura.Domicilio ? factura.DireccionDomicilio.Trim() : null;
                factura.CalcularTotales();
                if (factura.Total < 0) return "El total del documento no puede ser negativo.";
                if (factura.SubTotal > 99999999.99m || factura.Iva > 99999999.99m || factura.Descuento > 99999999.99m || factura.Total > 99999999.99m)
                    return "Los totales del documento superan el máximo permitido por las columnas Oracle NUMBER(10,2).";
                repositorio.GuardarCompleto(factura);
                return "Se registró el documento " + factura.TipoDocumento + " número " + factura.FacturaID + ".";
            }
            catch (Exception ex) { return "No se pudo guardar el documento: " + ex.Message; }
        }

        public string ConvertirCotizacionEnVenta(int facturaId)
        {
            try
            {
                repositorio.ConvertirCotizacionEnVenta(facturaId);
                return "La cotización se convirtió en venta y se actualizó el inventario.";
            }
            catch (Exception ex) { return "No se pudo convertir la cotización: " + ex.Message; }
        }

        public decimal TotalVentas()
        {
            return repositorio.ObtenerTotalVentas();
        }

        public string AnularDocumento(int facturaId)
        {
            try
            {
                if (facturaId <= 0) return "Selecciona un documento válido.";
                repositorio.AnularDocumento(facturaId);
                return "Documento anulado; si era una venta, el stock de repuestos fue devuelto al inventario.";
            }
            catch (Exception ex) { return "No se pudo anular el documento: " + ex.Message; }
        }


        // BUSCAR FACTURA POR ID
        public Factura BuscarId(int facturaId)
        {
            Factura factura = repositorio.BuscarId(facturaId);

            if (factura != null)
            {
                factura.Detalles = detalleRepositorio.ConsultarPorFactura(facturaId);
            }

            return factura;
        }

        // CONSULTAR TODAS LAS FACTURAS
        public List<Factura> Consultar()
        {
            List<Factura> facturas = repositorio.Consultar();

            foreach (Factura factura in facturas)
            {
                factura.Detalles = detalleRepositorio.ConsultarPorFactura(factura.FacturaID);
            }

            return facturas;
        }

        // ELIMINAR FACTURA
        [Obsolete("Usa AnularDocumento: los documentos contables no se borran físicamente.")]
        public string Eliminar(int facturaId)
        {
            return AnularDocumento(facturaId);
        }
    }
}
