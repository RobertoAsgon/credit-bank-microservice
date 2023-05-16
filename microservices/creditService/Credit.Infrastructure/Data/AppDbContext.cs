using Credit.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Credit.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Financiamento> Financiamento { get; set; }
        public DbSet<Parcela> Parcela { get; set; }
    }
}
