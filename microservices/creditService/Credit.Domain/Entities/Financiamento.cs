namespace Credit.Domain.Entities
{
    public enum TipoFinanciamento
    {
        Direto = 1,
        Consignado = 2,
        PessoaJuridica = 3,
        PessoaFisica = 4,
        Imobiliario = 5,
    }
    public class Financiamento
    {
        public int ID { get; set; }
        public string CPF { get; set; }
        public TipoFinanciamento TipoFinanciamento { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
