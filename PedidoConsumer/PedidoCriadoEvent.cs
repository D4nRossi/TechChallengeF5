namespace PedidoConsumer
{
    public class PedidoCriadoEvent
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public string ItensResumo { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; }
    }
}
