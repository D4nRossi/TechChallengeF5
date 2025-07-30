namespace pedido_service.Events
{
    public class PedidoCriadoEvent
    {
        public Guid PedidoId { get; set; }
        public string ClienteCpf { get; set; } = string.Empty;
        public string ItensResumo { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; }
    }
}
