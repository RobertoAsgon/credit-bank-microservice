using Credit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<Cliente> Cliente { get; set; }
        DbSet<Financiamento> Financiamento { get; set; }
        DbSet<Parcela> Parcela { get; set; }
    }
}