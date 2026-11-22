namespace ENTIDADES
{
    public enum PrioridadAlerta
    {
        Pendiente,
        Advertencia,
        Critica
    }

    public class AlertaOperativa
    {
        public string Tipo { get; set; }
        public string Detalle { get; set; }
        public string Estado { get; set; }
        public PrioridadAlerta Prioridad { get; set; }
    }
}
