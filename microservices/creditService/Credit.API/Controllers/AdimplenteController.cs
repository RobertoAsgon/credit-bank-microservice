using Credit.Application.Repositories;
using Credit.Domain.Entities;
using Credit.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Credit.API.Controllers
{
    [Route("api/adimplentes")]
    [ApiController]
    public class AdimplenteController : ControllerBase
    {
        private readonly ILogger<AdimplenteController> _logger;
        private readonly IAdimplenteRepository _adimplenteRepository;

        public AdimplenteController(
            ILogger<AdimplenteController> logger,
            AppDbContext appDbContext,
            IAdimplenteRepository adimplenteRepository
            )
        {
            _logger = logger;
            _adimplenteRepository = adimplenteRepository;
        }

        [HttpGet("obterAdimplentesDeSaoPauloSql")]
        [ActionName(nameof(GetAdimplentesSql))]
        public async Task<List<Cliente>> GetAdimplentesSql()
        {
            var test = "Teste";
            Debug.WriteLine("Test" + test);
            return await _adimplenteRepository.GetAdimplentesSql();
        }

        [HttpGet("obterAdimplentesDeSaoPauloSqlLinq")]
        [ActionName(nameof(GetAdimplentesLinqSql))]
        public async Task<List<Cliente>> GetAdimplentesLinqSql()
        {
            return await _adimplenteRepository.GetAdimplentesLinqSql();
        }

        [HttpGet("obterAdimplentesDeSaoPauloLinqMethods")]
        [ActionName(nameof(GetAdimplentesLinqMethods))]
        public async Task<List<Cliente>> GetAdimplentesLinqMethods()
        {
            return await _adimplenteRepository.GetAdimplentesLinqMethods();
        }
    }
}