using Credit.Application.Requests;
using Credit.Domain.Entities;
using FluentValidation;

public class CreditoRequestValidator : AbstractValidator<CreditoRequest>
{
    public CreditoRequestValidator()
    {
        RuleFor(x => x.ValorCredito)
            .InclusiveBetween(1, 1_000_000)
            .WithMessage("O valor máximo a ser liberado para qualquer TipoFinanciamento é de R$ 1.000.000,00.")
            .GreaterThan(0)
            .WithMessage("O valor mínimo a ser liberado é de R$ 1,00.");

        RuleFor(x => x.QuantidadeParcelas)
            .InclusiveBetween(5, 72)
            .WithMessage("A quantidade mínima de parcelas é de 5x e máxima de 72x.");

        RuleFor(x => x.TipoFinanciamento)
            .Equal(TipoFinanciamento.PessoaJuridica)
            .When(x => x.ValorCredito < 15_000)
            .WithMessage("Para TipoFinanciamento.PessoaJuridica, o valor mínimo a ser liberado é de R$ 15.000,00.");

        RuleFor(x => x.DataVencimento)
            .GreaterThanOrEqualTo(DateTime.Today.AddDays(15))
            .WithMessage("A DataVencimento deve ser no mínimo 15 dias a partir da data atual.")
            .LessThanOrEqualTo(DateTime.Today.AddDays(40))
            .WithMessage("A DataVencimento deve ser no máximo 40 dias a partir da data atual.");
    }
}