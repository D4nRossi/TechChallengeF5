namespace pedido_service.Data{
using Microsoft.EntityFrameworkCore;
using pedido_service.Models;
using System.Collections.Generic;


    public class PedidoDbContext : DbContext
{
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
}
}

