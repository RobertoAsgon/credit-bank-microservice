using Credit.Domain.Entities;

namespace Credit.Application.Requests;

public class CreditoRequest
{
    public decimal ValorCredito { get; set; }
    public TipoFinanciamento TipoFinanciamento { get; set; }
    public int QuantidadeParcelas { get; set; }
    public DateTime DataVencimento { get; set; }
    public string CPF { get; set; }

    public CreditoRequest(decimal valorCredito, TipoFinanciamento tipoFinanciamento, int quantidadeParcelas, DateTime dataVencimento, string cpf)
    {
        ValorCredito = valorCredito;
        TipoFinanciamento = tipoFinanciamento;
        QuantidadeParcelas = quantidadeParcelas;
        DataVencimento = dataVencimento;
        CPF = cpf;
    }
    public void Deconstruct(out decimal valorCredito, out TipoFinanciamento tipoFinanciamento, out int quantidadeParcelas, out DateTime dataVencimento, out string cpf)
    {
        valorCredito = ValorCredito;
        tipoFinanciamento = TipoFinanciamento;
        quantidadeParcelas = QuantidadeParcelas;
        dataVencimento = DataVencimento;
        cpf = CPF;
    }
}
