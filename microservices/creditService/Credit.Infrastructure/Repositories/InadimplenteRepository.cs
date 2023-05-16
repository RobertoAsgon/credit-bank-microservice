using Credit.Application.Repositories;
using Credit.Domain.Entities;
using Credit.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infrastructure.Repositories
{
    public class InadimplenteRepository : IInadimplenteRepository
    {
        private readonly AppDbContext _appDbContext;
        public InadimplenteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Cliente>> GetInadimplentesSql()
        {
            var query = @"
                SELECT TOP 4 c.CPF, c.Nome, c.UF, c.Celular
                FROM Cliente c
                WHERE EXISTS (
                    SELECT 1
                    FROM Financiamento f
                    INNER JOIN Parcela p ON f.ID = p.IDFinanciamento
                    WHERE f.CPF = c.CPF
                        AND p.DataVencimento < @DataAtual
                        AND p.DataPagamento IS NULL
                        AND DATEDIFF(DAY, p.DataVencimento, GETDATE()) > 5
                )
                ORDER BY c.ID";

            var clientesInadimplentes = _appDbContext.Cliente
                .FromSqlRaw(query, new SqlParameter("@DataAtual", DateTime.Now))
                .Select(c => new Cliente
                {
                    CPF = c.CPF,
                    Nome = c.Nome,
                    UF = c.UF,
                    Celular = c.Celular
                })
                .ToList();

            return clientesInadimplentes;
        }

        public async Task<List<Cliente>> GetInadimplentesLinqSql()
        {
            var clientesInadimplentes = (from c in _appDbContext.Cliente
                            where c.UF == "SP"
                                && _appDbContext.Financiamento.Any(f => f.CPF == c.CPF
                                && _appDbContext.Parcela.Any(p => p.IDFInanciamento == f.ID)
                                && (decimal)(_appDbContext.Parcela.Count(p => p.IDFInanciamento == f.ID)
                                    / _appDbContext.Parcela.Count(p => p.IDFInanciamento == f.ID)) > 0.6m)
                            orderby c.ID
                            select new Cliente
                            {
                                CPF = c.CPF,
                                Nome = c.Nome,
                                UF = c.UF,
                                Celular = c.Celular
                            })
                            .Take(4)
                            .ToList();

            return clientesInadimplentes;
        }

        public async Task<List<Cliente>> GetInadimplentesLinqMethods()
        {
            var clientesInadimplentes = _appDbContext.Cliente
                .Where(cliente => cliente.UF == "SP"
                    && _appDbContext.Financiamento.Any(f => f.CPF == cliente.CPF
                    && _appDbContext.Parcela.Where(p => p.IDFInanciamento == f.ID).Count() > 0
                    && ((decimal)_appDbContext.Parcela.Count(p => p.IDFInanciamento == f.ID)
                        / _appDbContext.Parcela.Count(p => p.IDFInanciamento == f.ID)) > 0.6m))
                .Select(c => new Cliente
                {
                    CPF = c.CPF,
                    Nome = c.Nome,
                    UF = c.UF,
                    Celular = c.Celular
                })
                .Take(4)
                .ToList();


            return clientesInadimplentes;
        }

    }
}
