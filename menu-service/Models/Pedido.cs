using System.ComponentModel.DataAnnotations;

namespace pedido_service.Models
{
    public class Pedido
    {
        [Key]
        public Guid Id { get; set; }
        public string ClienteCpf { get; set; } = string.Empty;
        public string ItensResumo { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pendente"; // Pendente, Aceito, Rejeitado, Cancelado
        public string? JustificativaCancelamento { get; set; }
    }
}
