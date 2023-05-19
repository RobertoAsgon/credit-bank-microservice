using Credit.Application.Repositories;
using Credit.Domain.Entities;
using Credit.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Credit.API.Controllers
{
    [Route("api/inadimplentes")]
    [ApiController]
    public class InadimplenteController : ControllerBase
    {
        private readonly ILogger<InadimplenteController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly IInadimplenteRepository _inadimplenteRepository;

        public InadimplenteController(
            ILogger<InadimplenteController> logger,
            IInadimplenteRepository inadimplenteRepository
            )
        {
            _logger = logger;
            _inadimplenteRepository = inadimplenteRepository;
        }

        [HttpGet("obterPrimeirosQuatroClientesInadimplentesSql")]
        [ActionName(nameof(GetInadimplentesSql))]
        public async Task<List<Cliente>> GetInadimplentesSql()
        {
            return await _inadimplenteRepository.GetInadimplentesSql();
        }

        [HttpGet("obterPrimeirosQuatroClientesInadimplentesLinqSql")]
        [ActionName(nameof(GetInadimplentesLinqSql))]
        public async Task<List<Cliente>> GetInadimplentesLinqSql()
        {
            return await _inadimplenteRepository.GetInadimplentesLinqSql();
        }

        [HttpGet("obterPrimeirosQuatroClientesInadimplentesLinqMethods")]
        [ActionName(nameof(GetInadimplentesLinqMethods))]
        public async Task<List<Cliente>> GetInadimplentesLinqMethods()
        {
            return await _inadimplenteRepository.GetInadimplentesLinqMethods();
        }

    }
}