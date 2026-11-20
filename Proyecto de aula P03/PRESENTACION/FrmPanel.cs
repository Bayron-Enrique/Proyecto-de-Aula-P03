using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTIDADES;

namespace PRESENTACION
{
    public partial class FrmPanel : Form
    {

        public FrmPanel()
        {
            InitializeComponent();
            EstiloUI.ConectarTabla(gridCitas);
            EstiloUI.ConectarTabla(gridAlertas);
        }

        public void EstablecerMetricas(int clientes, int vehiculos, int articulos, int citasPendientes, decimal ventas)
        {
            lblClientes.Text = clientes.ToString("N0");
            lblVehiculos.Text = vehiculos.ToString("N0");
            lblArticulos.Text = articulos.ToString("N0");
            lblCitas.Text = citasPendientes.ToString("N0");
            lblVentas.Text = ventas.ToString("C2");
            lblActualizado.Text = "Actualizado: " + DateTime.Now.ToString("g");
        }

        public void MostrarError(string mensaje)
        {
            lblActualizado.Text = "No se pudieron actualizar las métricas: " + mensaje;
        }

        public void EstablecerResumen(IEnumerable<Agenda> citas, IEnumerable<AlertaTaller> alertas)
        {
            gridCitas.DataSource = (citas ?? Enumerable.Empty<Agenda>()).Select(a => new
            {
                Fecha = a.Fecha.ToString("dd/MM HH:mm"), Placa = a.Vehiculo == null ? "" : a.Vehiculo.Placa,
                Cliente = a.Vehiculo == null || a.Vehiculo.Cliente == null ? "" : a.Vehiculo.Cliente.NombreCompleto,
                Servicio = a.ArticuloServicio == null ? "" : a.ArticuloServicio.Nombre,
                Empleado = a.Empleado == null ? "" : a.Empleado.NombreCompleto
            }).ToList();
            gridAlertas.DataSource = (alertas ?? Enumerable.Empty<AlertaTaller>()).ToList();
        }

        public sealed class AlertaTaller
        {
            public string Tipo { get; set; }
            public string Detalle { get; set; }
            public string Estado { get; set; }
        }
    }
}
