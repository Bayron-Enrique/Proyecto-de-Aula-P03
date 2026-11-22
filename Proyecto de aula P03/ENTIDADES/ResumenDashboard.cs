using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public sealed class ResumenDashboard
    {
        public DateTime Actualizado { get; set; }
        public int Clientes { get; set; }
        public int ClientesTelegram { get; set; }
        public int Vehiculos { get; set; }
        public int Automoviles { get; set; }
        public int Motocicletas { get; set; }
        public int Articulos { get; set; }
        public int Repuestos { get; set; }
        public int Servicios { get; set; }
        public int CitasPendientes { get; set; }
        public int CitasEnProceso { get; set; }
        public int CitasHoy { get; set; }
        public decimal VentasActivas { get; set; }
        public decimal FacturasPendientes { get; set; }
        public IReadOnlyList<PuntoGrafico> ResumenFinanciero { get; set; } = Array.Empty<PuntoGrafico>();
        public IReadOnlyList<PuntoGrafico> EstadosCitas { get; set; } = Array.Empty<PuntoGrafico>();
        public IReadOnlyList<Agenda> ProximasCitas { get; set; } = Array.Empty<Agenda>();
        public IReadOnlyList<AlertaOperativa> Alertas { get; set; } = Array.Empty<AlertaOperativa>();
    }
}
