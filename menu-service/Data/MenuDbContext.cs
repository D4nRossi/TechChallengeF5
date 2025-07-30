using menu_service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace menu_service.Data
{
    public class MenuDbContext : DbContext
    {
        public MenuDbContext(DbContextOptions<MenuDbContext> options) : base(options) { }
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    }
}
