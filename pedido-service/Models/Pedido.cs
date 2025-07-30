using System.ComponentModel.DataAnnotations;

namespace pedido_service.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public decimal Total { get; set; }
        public string? Status { get; set; } // Pendente, Aprovado, Rejeitado, etc
        public string FormaEntrega { get; set; } // Balcão, Drive-thru, Delivery
        public string? JustificativaCancelamento { get; set; }
    }

    public class PedidoItem
    {
        public Guid Id { get; set; }
        public Guid MenuItemId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class CancelamentoDTO { public string Justificativa { get; set; } }
}
