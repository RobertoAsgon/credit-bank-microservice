using Credit.Application.Repositories;
using Credit.Domain.Entities;
using Credit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infrastructure.Repositories
{
    public class AdimplenteRepository : IAdimplenteRepository
    {
        private readonly AppDbContext _appDbContext;
        public AdimplenteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Cliente>> GetAdimplentesSql()
        {
            var query = @"
                SELECT c.CPF, c.Nome, c.UF, c.Celular
                FROM Cliente c
                WHERE c.UF = 'SP' AND EXISTS (
                    SELECT 1
                    FROM Financiamento f
                    INNER JOIN Parcela p ON f.ID = p.IDFinanciamento
                    WHERE f.CPF = c.CPF
                    GROUP BY f.ID, f.CPF
                    HAVING CAST(COUNT(*) AS decimal) / COUNT(p.ID) > 0.6
                )";

            var clientesAdmimplentesQuery = _appDbContext.Cliente
                .FromSqlRaw(query)
                .Select(c => new Cliente
                {
                    CPF = c.CPF,
                    Nome = c.Nome,
                    UF = c.UF,
                    Celular = c.Celular
                })
                .ToList();

            return clientesAdmimplentesQuery;
        }

        public async Task<List<Cliente>> GetAdimplentesLinqSql()
        {
            var clientesAdimplentesSqlLinqSintaxe = (
                from c in _appDbContext.Cliente
                where c.UF == "SP" &&
                    (
                        from f in _appDbContext.Financiamento
                        join p in _appDbContext.Parcela on f.ID equals p.IDFInanciamento
                        where f.CPF == c.CPF
                        group f by new { f.ID, f.CPF } into g
                        where (decimal)g.Count() / _appDbContext.Parcela.Count(p => p.IDFInanciamento == g.Key.ID) > 0.9m
                        select g
                    ).Any()
                select new Cliente
                {
                    CPF = c.CPF,
                    Nome = c.Nome,
                    UF = c.UF,
                    Celular = c.Celular
                }
            ).ToList();

            return clientesAdimplentesSqlLinqSintaxe;
        }

        public async Task<List<Cliente>> GetAdimplentesLinqMethods()
        {
            var clientesAdimplentesEntity = _appDbContext.Cliente
                .Where(c => c.UF == "SP" && _appDbContext.Financiamento
                    .Join(
                        _appDbContext.Parcela,
                        f => f.ID,
                        p => p.IDFInanciamento,
                        (f, p) => new { Financiamento = f, Parcela = p }
                    )
                    .GroupBy(fp => new { fp.Financiamento.ID, fp.Financiamento.CPF })
                    .Where(g => (decimal)g.Count() / _appDbContext.Parcela.Count(p => p.IDFInanciamento == g.Key.ID) > 0.6m)
                    .Select(g => g.Key.CPF)
                    .Any(cp => cp == c.CPF)
                )
                .Select(c => new Cliente
                {
                    CPF = c.CPF,
                    Nome = c.Nome,
                    UF = c.UF,
                    Celular = c.Celular
                })
                .ToList();

            return clientesAdimplentesEntity;
        }

    }
}
