
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
                if (factura.Detalles == null || factura.Detalles.Count == 0) return "Agrega al menos un artículo al detalle.";
                if (factura.Domicilio && string.IsNullOrWhiteSpace(factura.DireccionDomicilio)) return "Escribe la dirección de domicilio.";
                if (factura.DireccionDomicilio != null && factura.DireccionDomicilio.Trim().Length > 150) return "La dirección admite hasta 150 caracteres.";
                if (!string.IsNullOrWhiteSpace(factura.Observaciones) && factura.Observaciones.Trim().Length > 200) return "Las observaciones admiten hasta 200 caracteres.";

                Cliente cliente = clienteRepositorio.BuscarCedula(factura.Cliente.Cedula);
                Empleado empleado = empleadoRepositorio.BuscarCedula(factura.Empleado.Cedula);
                if (cliente == null) return "El cliente seleccionado ya no existe.";
                if (empleado == null) return "El empleado seleccionado ya no existe.";
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
                    Agenda agenda = agendaRepositorio.BuscarId(factura.AgendaID.Value);
                    if (agenda == null) return "La cita seleccionada ya no existe.";
                    if (factura.Vehiculo != null && agenda.Vehiculo.Placa != factura.Vehiculo.Placa) return "La cita no corresponde al vehículo seleccionado.";
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

        // GUARDAR FACTURA O COTIZACION
        public string Guardar(
            DateTime fecha,
            decimal descuento,
            string observaciones,
            string estadoPago,
            string cedulaCliente,
            string cedulaEmpleado,
            string tipoDocumento = "VENTA")
        {
            try
            {
                // Validaciones de fecha
                if (fecha.Year < 2000)
                {
                    return "La fecha de la factura no es valida ...";
                }

                if (fecha > DateTime.Now)
                {
                    return "La fecha de la factura no puede ser futura ...";
                }

                // Validaciones de descuento
                if (descuento < 0)
                {
                    return "El descuento no puede ser negativo ...";
                }

                if (descuento > 99999999.99m)
                {
                    return "El descuento es demasiado alto ...";
                }

                if (Math.Round(descuento, 2) != descuento)
                {
                    return "El descuento solo puede tener 2 decimales ...";
                }

                // Validaciones de observaciones
                if (!string.IsNullOrEmpty(observaciones))
                {
                    if (string.IsNullOrWhiteSpace(observaciones))
                    {
                        return "Las observaciones no pueden tener solo espacios ...";
                    }

                    if (observaciones != observaciones.Trim() ||
                        observaciones.Contains("  "))
                    {
                        return "Las observaciones no pueden tener espacios al inicio, al final ni seguidos ...";
                    }

                    if (observaciones.Length < 3 ||
                        observaciones.Length > 200)
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

                // Validacion del estado de pago
                if (estadoPago != "Pendiente" &&
                    estadoPago != "Pagada" &&
                    estadoPago != "Anulada")
                {
                    return "El estado de pago debe ser Pendiente, Pagada o Anulada ...";
                }

                // Validacion del tipo de documento
                if (string.IsNullOrWhiteSpace(tipoDocumento))
                {
                    return "Debe seleccionar un tipo de documento ...";
                }

                tipoDocumento = tipoDocumento.Trim().ToUpperInvariant();

                if (tipoDocumento != "VENTA" &&
                    tipoDocumento != "COTIZACION")
                {
                    return "El tipo de documento debe ser VENTA o COTIZACION ...";
                }

                // Validaciones de cliente y empleado
                if (string.IsNullOrWhiteSpace(cedulaCliente))
                {
                    return "Debe seleccionar un cliente valido ...";
                }

                if (string.IsNullOrWhiteSpace(cedulaEmpleado))
                {
                    return "Debe seleccionar un empleado valido ...";
                }

                Cliente cliente =
                    clienteRepositorio.BuscarCedula(cedulaCliente);

                if (cliente == null)
                {
                    return "El cliente seleccionado no existe ...";
                }

                Empleado empleado =
                    empleadoRepositorio.BuscarCedula(cedulaEmpleado);

                if (empleado == null)
                {
                    return "El empleado seleccionado no existe ...";
                }

                // Obtener el ID directamente desde Oracle
                int facturaId =
                    repositorio.ObtenerSiguienteFacturaId();

                // Crear factura o cotizacion
                Factura factura = new Factura
                {
                    FacturaID = facturaId,
                    Fecha = fecha,
                    Descuento = descuento,
                    Observaciones = observaciones,
                    EstadoPago = estadoPago,
                    TipoDocumento = tipoDocumento,
                    Cliente = cliente,
                    Empleado = empleado,

                    // Valores iniciales antes de agregar detalles
                    SubTotal = 0,
                    Iva = 0,
                    Total = 0
                };

                // Persistir en Oracle
                repositorio.Guardar(factura);

                return $"Se registro el documento {tipoDocumento} numero {facturaId} correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al guardar: {ex.Message}";
            }
        }

        // AGREGAR DETALLE A FACTURA O COTIZACION
        public string AgregarDetalle(
            int facturaId,
            int articuloId,
            int cantidad)
        {
            try
            {
                Factura factura = BuscarId(facturaId);

                if (factura == null)
                {
                    return "No se encontro la factura ...";
                }

                if (factura.EstadoPago == "Pagada" ||
                    factura.EstadoPago == "Anulada")
                {
                    return $"No se pueden agregar articulos a una factura {factura.EstadoPago.ToLower()} ...";
                }

                if (articuloId <= 0)
                {
                    return "Debe seleccionar un articulo valido ...";
                }

                Articulo articulo =
                    articuloRepositorio.BuscarId(articuloId);

                if (articulo == null)
                {
                    return "El articulo seleccionado no existe ...";
                }

                if (cantidad < 1 || cantidad > 1000000)
                {
                    return "La cantidad debe estar entre 1 y 1000000 ...";
                }

                bool esVenta = string.Equals(
                    factura.TipoDocumento,
                    "VENTA",
                    StringComparison.OrdinalIgnoreCase);

                bool esCotizacion = string.Equals(
                    factura.TipoDocumento,
                    "COTIZACION",
                    StringComparison.OrdinalIgnoreCase);

                if (!esVenta && !esCotizacion)
                {
                    return "La factura tiene un tipo de documento invalido ...";
                }

                // Validar existencias solo para repuestos vendidos
                Repuesto repuesto = articulo as Repuesto;

                if (esVenta && repuesto != null)
                {
                    if (cantidad > repuesto.Cantidad)
                    {
                        return $"No hay existencias suficientes, solo quedan {repuesto.Cantidad} ...";
                    }
                }

                // Obtener los detalles que ya existen en Oracle
                factura.Detalles =
                    detalleRepositorio.ConsultarPorFactura(facturaId);

                // Evitar articulos duplicados
                foreach (DetalleFactura existente in factura.Detalles)
                {
                    if (existente.Articulo != null &&
                        existente.Articulo.ArticuloId == articuloId)
                    {
                        return "Ese articulo ya esta en la factura ...";
                    }
                }

                // Crear detalle utilizando el constructor existente
                DetalleFactura detalle =
                    new DetalleFactura(cantidad, articulo);

                detalle.FacturaID = facturaId;

                // Agregar a la factura en memoria para calcular
                factura.Detalles.Add(detalle);

                // Recalcular subtotal, IVA y total
                factura.CalcularTotales();

                if (factura.Total < 0)
                {
                    return "El descuento no puede superar el valor de la factura ...";
                }

                // Guardar el nuevo detalle en Oracle
                detalleRepositorio.Guardar(detalle);

                // Actualizar los totales de la factura
                repositorio.Modificar(factura);

                // Descontar inventario solamente en ventas
                if (esVenta && repuesto != null)
                {
                    repuesto.Cantidad -= cantidad;

                    articuloRepositorio.Modificar(repuesto);
                }

                return $"Se agrego el detalle a la factura {facturaId} correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al agregar el detalle: {ex.Message}";
            }
        }

        // BUSCAR FACTURA POR ID
        public Factura BuscarId(int facturaId)
        {
            Factura factura = repositorio.BuscarId(facturaId);

            if (factura != null)
            {
                factura.Detalles =
                    detalleRepositorio.ConsultarPorFactura(facturaId);
                CompletarRelaciones(factura);
            }

            return factura;
        }

        // CONSULTAR TODAS LAS FACTURAS
        public List<Factura> Consultar()
        {
            List<Factura> facturas = repositorio.Consultar();

            foreach (Factura factura in facturas)
            {
                factura.Detalles =
                    detalleRepositorio.ConsultarPorFactura(
                        factura.FacturaID);
                CompletarRelaciones(factura);
            }

            return facturas;
        }

        private void CompletarRelaciones(Factura factura)
        {
            if (factura.Cliente != null) factura.Cliente = clienteRepositorio.BuscarCedula(factura.Cliente.Cedula);
            if (factura.Empleado != null) factura.Empleado = empleadoRepositorio.BuscarCedula(factura.Empleado.Cedula);
            factura.Vehiculo = repositorio.ConsultarDetallesVehiculo(factura.FacturaID).Select(d => d.Vehiculo).FirstOrDefault();
        }

        // ELIMINAR FACTURA
        public string Eliminar(int facturaId)
        {
            try
            {
                Factura factura = BuscarId(facturaId);

                if (factura == null)
                {
                    return "No se encontro la factura ...";
                }

                repositorio.Eliminar(facturaId);

                return "La factura se elimino correctamente ...";
            }
            catch (Exception ex)
            {
                return $"Ocurrio un error al eliminar: {ex.Message}";
            }
        }
    }
}
