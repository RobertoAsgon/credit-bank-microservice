using System;

namespace Credit.Domain.Entities
{
    public class Parcela
    {
        public int ID { get; set; }
        public int IDFInanciamento { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
