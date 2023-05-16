
using Credit.Application.Requests;
using Credit.Domain.Entities;

namespace Credit.Application.Repositories
{
    public interface IInadimplenteRepository
    {
        Task<List<Cliente>> GetInadimplentesSql();
        Task<List<Cliente>> GetInadimplentesLinqSql();
        Task<List<Cliente>> GetInadimplentesLinqMethods();
    }
}
 