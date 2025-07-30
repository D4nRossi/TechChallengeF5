using System.ComponentModel.DataAnnotations;

namespace menu_service.Models
{
    public class MenuItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public string? Category { get; set; } // <-- Adicionado!
    }
}
