using cliente_service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace cliente_service.Data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }

}
