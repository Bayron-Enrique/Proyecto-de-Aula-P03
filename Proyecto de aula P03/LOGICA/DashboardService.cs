using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DATOS;
using ENTIDADES;

namespace LOGICA
{
    public sealed class DashboardService
    {
        private readonly ClienteService clientes;
        private readonly VehiculoService vehiculos;
        private readonly ArticuloService articulos;
        private readonly AgendaService agenda;
        private readonly FacturaService facturas;
        private readonly SoatService soat;
        private readonly NotificacionMantenimientoService mantenimiento;

        public DashboardService()
        {
            var clienteRepo = new ClienteRepository();
            var vehiculoRepo = new VehiculoRepository();
            var articuloRepo = new ArticuloRepository();
            var empleadoRepo = new EmpleadoRepository();

            clientes = new ClienteService(clienteRepo);
            vehiculos = new VehiculoService(vehiculoRepo, clienteRepo);
            articulos = new ArticuloService(articuloRepo, new ProveedorRepository());
            agenda = new AgendaService(new AgendaRepository(), clienteRepo, vehiculoRepo, empleadoRepo, articuloRepo);
            facturas = new FacturaService(new FacturaRepository(), clienteRepo, empleadoRepo, articuloRepo);
            soat = new SoatService(new SoatRepository(), vehiculoRepo);
            mantenimiento = new NotificacionMantenimientoService(new NotificacionMantenimientoRepository(), vehiculoRepo, articuloRepo);
        }

        public ResumenDashboard Cargar()
        {
            var listaClientes = clientes.Consultar();
            var listaVehiculos = vehiculos.Consultar();
            var listaArticulos = articulos.Consultar();
            var citas = agenda.Consultar();
            var documentos = facturas.ConsultarCabeceras();
            var polizas = soat.Consultar();
            var recordatorios = mantenimiento.Consultar();
            var ahora = DateTime.Now;

            var repuestos = listaArticulos.OfType<Repuesto>().ToList();
            var pendientes = citas.Where(c => Es(c.Estado, "PENDIENTE")).ToList();
            var enProceso = citas.Where(c => Es(c.Estado, "EN PROCESO")).ToList();
            var ventas = documentos.Where(d => Es(d.TipoDocumento, "VENTA") && !Es(d.EstadoPago, "Anulada")).ToList();
            var porCobrar = ventas.Where(d => Es(d.EstadoPago, "Pendiente")).ToList();

            return new ResumenDashboard
            {
                Actualizado = ahora,
                Clientes = listaClientes.Count,
                ClientesTelegram = listaClientes.Count(c => c.TelegramChatId.HasValue),
                Vehiculos = listaVehiculos.Count,
                Automoviles = listaVehiculos.Count(v => Es(v.TipoVehiculo, "Automovil") || Es(v.TipoVehiculo, "Automóvil")),
                Motocicletas = listaVehiculos.Count(v => Es(v.TipoVehiculo, "Motocicleta")),
                Articulos = listaArticulos.Count,
                Repuestos = repuestos.Count,
                Servicios = listaArticulos.OfType<Servicio>().Count(),
                CitasPendientes = pendientes.Count,
                CitasEnProceso = enProceso.Count,
                CitasHoy = citas.Count(c => c.Fecha.Date == ahora.Date && !Es(c.Estado, "CANCELADA")),
                VentasActivas = ventas.Sum(d => d.Total),
                FacturasPendientes = documentos.Where(d => Es(d.EstadoPago, "Pendiente")).Sum(d => d.Total),
                ResumenFinanciero = new[]
                {
                    new PuntoGrafico("Ventas pagadas", ventas.Where(d => Es(d.EstadoPago, "Pagada")).Sum(d => d.Total)),
                    new PuntoGrafico("Ventas pendientes", porCobrar.Sum(d => d.Total)),
                    new PuntoGrafico("Cotizaciones activas", documentos.Where(d => Es(d.TipoDocumento, "COTIZACION") && !Es(d.EstadoPago, "Anulada")).Sum(d => d.Total))
                },
                EstadosCitas = new[]
                {
                    new PuntoGrafico("Pendiente", pendientes.Count),
                    new PuntoGrafico("En proceso", enProceso.Count),
                    new PuntoGrafico("Completada", citas.Count(c => Es(c.Estado, "COMPLETADA"))),
                    new PuntoGrafico("Cancelada", citas.Count(c => Es(c.Estado, "CANCELADA")))
                },
                ProximasCitas = pendientes.Where(c => c.Fecha >= ahora)
                    .Concat(enProceso.Where(c => c.Fecha >= ahora.Date))
                    .OrderBy(c => c.Fecha).ThenBy(c => c.AgendaID).Take(5).ToList(),
                Alertas = CrearAlertas(repuestos, polizas, recordatorios, porCobrar, ahora.Date)
                    .OrderByDescending(a => a.Prioridad).ThenBy(a => a.Tipo).ThenBy(a => a.Detalle).ToList()
            };
        }

        private static IEnumerable<AlertaOperativa> CrearAlertas(IEnumerable<Repuesto> repuestos, IEnumerable<Soat> polizas,
            IEnumerable<NotificacionMantenimiento> recordatorios, IEnumerable<Factura> porCobrar, DateTime hoy)
        {
            foreach (var repuesto in repuestos.Where(r => r.Cantidad <= 5))
            {
                bool agotado = repuesto.Cantidad <= 0;
                yield return new AlertaOperativa
                {
                    Tipo = "Stock",
                    Detalle = $"{repuesto.Nombre} · {repuesto.Cantidad:N0} unidades",
                    Estado = agotado ? "AGOTADO" : "STOCK BAJO",
                    Prioridad = agotado ? PrioridadAlerta.Critica : PrioridadAlerta.Advertencia
                };
            }

            DateTime limite = hoy.AddDays(30);
            foreach (var poliza in polizas.Where(p => p.FechaFin != DateTime.MinValue && p.FechaFin.Date <= limite))
            {
                bool vencido = poliza.FechaFin.Date < hoy;
                yield return new AlertaOperativa
                {
                    Tipo = "SOAT",
                    Detalle = $"{poliza.Vehiculo?.Placa} · vence {poliza.FechaFin:dd/MM/yyyy}",
                    Estado = vencido ? "VENCIDO" : "POR VENCER",
                    Prioridad = vencido ? PrioridadAlerta.Critica : PrioridadAlerta.Advertencia
                };
            }

            foreach (var aviso in recordatorios.Where(n => Es(n.Estado, "PENDIENTE")))
            {
                string fecha = aviso.Fecha == DateTime.MinValue ? "Sin fecha" : aviso.Fecha.ToString("dd/MM/yyyy");
                yield return new AlertaOperativa
                {
                    Tipo = "Mantenimiento",
                    Detalle = $"#{aviso.NotificacionID} · {aviso.Vehiculo?.Placa} · {fecha}",
                    Estado = "PENDIENTE",
                    Prioridad = PrioridadAlerta.Pendiente
                };
            }

            foreach (var factura in porCobrar)
            {
                yield return new AlertaOperativa
                {
                    Tipo = "Cartera",
                    Detalle = $"Venta #{factura.FacturaID} · {factura.Cliente?.NombreCompleto} · {factura.Total.ToString("C0", CultureInfo.GetCultureInfo("es-CO"))}",
                    Estado = "PENDIENTE",
                    Prioridad = PrioridadAlerta.Pendiente
                };
            }
        }

        private static bool Es(string valor, string esperado)
        {
            return string.Equals(valor?.Trim(), esperado, StringComparison.OrdinalIgnoreCase);
        }
    }
}
