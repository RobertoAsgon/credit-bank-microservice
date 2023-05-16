
using Credit.Domain.Entities;

namespace Credit.Application.Repositories
{
    public interface IAdimplenteRepository
    {
        Task<List<Cliente>> GetAdimplentesSql();
        Task<List<Cliente>> GetAdimplentesLinqSql();
        Task<List<Cliente>> GetAdimplentesLinqMethods();
    }
}
 