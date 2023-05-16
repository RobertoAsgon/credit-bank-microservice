
using Credit.Application.Requests;
using Credit.Application.Responses;
using Credit.Domain.Entities;

namespace Credit.Application.Repositories
{
    public interface ICreditRepository
    {
        Task RegistrarCredito(
            string clientCpf, 
            int quantidadeParcelas, 
            decimal valorCredito, 
            DateTime dataVencimento, 
            TipoFinanciamento tipoFinanciamento,
            decimal valorJuros);
    }
}
 