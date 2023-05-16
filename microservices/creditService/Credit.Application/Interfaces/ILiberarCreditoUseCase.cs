
using Credit.Application.Requests;
using Credit.Application.Responses;

namespace Credit.Application.Repositories
{
    public interface ILiberarCreditoUseCase
    {
        Task<CreditoResponse> Execute(CreditoRequest body);
    }
}
 