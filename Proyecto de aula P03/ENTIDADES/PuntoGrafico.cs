namespace ENTIDADES
{
    public sealed class PuntoGrafico
    {
        public PuntoGrafico(string etiqueta, decimal valor)
        {
            Etiqueta = etiqueta;
            Valor = valor;
        }

        public string Etiqueta { get; }
        public decimal Valor { get; }
    }
}
