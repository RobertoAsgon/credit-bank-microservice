using Credit.Application.Repositories;
using Credit.Application.Requests;
using Credit.Application.Responses;
using Credit.Domain.Entities;
using System.Globalization;

namespace Credit.Application.UseCases
{
    public class LiberarCreditoUseCase : ILiberarCreditoUseCase
    {
        private readonly ICreditRepository _creditRepository;
        public LiberarCreditoUseCase(ICreditRepository creditRepository)
        {
            _creditRepository = creditRepository;
        }

        private static readonly Dictionary<TipoFinanciamento, decimal> TaxasJuros = new Dictionary<TipoFinanciamento, decimal>
            {
                { TipoFinanciamento.Direto, 0.02m },
                { TipoFinanciamento.Consignado, 0.01m },
                { TipoFinanciamento.PessoaJuridica, 0.05m },
                { TipoFinanciamento.PessoaFisica, 0.03m },
                { TipoFinanciamento.Imobiliario, 0.09m }
            };

        private decimal CalcularJuros(int quantidadeParcelas, decimal valorCredito, TipoFinanciamento tipoFinanciamento)
        {
            decimal valorJuros = 0;

            if (TaxasJuros.TryGetValue(tipoFinanciamento, out decimal taxaJuros))
            {
                decimal jurosComBaseQtdParcelas = taxaJuros * quantidadeParcelas;
                valorJuros = valorCredito * jurosComBaseQtdParcelas;
            }

            return valorJuros;
        }

        private string ObterValorFormatado(decimal valor)
        {
            var valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor);
            return valorFormatado;
        }

        public async Task<CreditoResponse> Execute(CreditoRequest body)
        {

            var (valorCredito, tipoFinanciamento, quantidadeParcelas, dataVencimento, cpf) = body;
            var juros = CalcularJuros(quantidadeParcelas, valorCredito, tipoFinanciamento);

            await _creditRepository.RegistrarCredito(
                cpf,
                quantidadeParcelas,
                valorCredito,
                dataVencimento,
                tipoFinanciamento,
                juros);

            var response = new CreditoResponse
            {
                Aprovado = true,
                ValorJuros = ObterValorFormatado(juros),
                ValorTotalComJuros = ObterValorFormatado(valorCredito + juros)
            };

            return response;
        }
    }
}
