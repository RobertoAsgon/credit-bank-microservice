using Credit.Application.Repositories;
using Credit.Domain.Entities;
using Credit.Infrastructure.Data;

namespace Credit.Infrastructure.Repositories
{
    public class CreditRepository : ICreditRepository
    {
        private readonly AppDbContext _appDbContext;
        public CreditRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task RegistrarCredito(
            string clientCpf, 
            int quantidadeParcelas,
            decimal valorCredito, 
            DateTime dataVencimento, 
            TipoFinanciamento tipoFinanciamento,
            decimal valorJuros
            )
        {
            var cliente = _appDbContext.Cliente.FirstOrDefault(c => c.CPF == clientCpf);

            var financiamento = new Financiamento
            {
                CPF = cliente.CPF,
                TipoFinanciamento = tipoFinanciamento,
                ValorTotal = valorCredito,
                DataVencimento = dataVencimento
            };

            _appDbContext.Financiamento.Add(financiamento);
            _appDbContext.SaveChanges();

            var parcelas = Enumerable.Range(1, quantidadeParcelas)
                .Select(i => new Parcela
                {
                    IDFInanciamento = financiamento.ID,
                    NumeroParcela = i,
                    ValorParcela = valorCredito / quantidadeParcelas,
                    DataVencimento = dataVencimento.AddMonths(i - 1)
                })
                .ToList();

            _appDbContext.Parcela.AddRange(parcelas);
            _appDbContext.SaveChanges();
        }
            
    }
}
