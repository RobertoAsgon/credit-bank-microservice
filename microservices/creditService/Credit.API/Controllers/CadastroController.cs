using Credit.Application.Repositories;
using Credit.Application.Requests;
using Credit.Domain.Entities;
using Credit.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Credit.API.Controllers
{
    [Route("api/cadastro")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly ILogger<CadastroController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly ICreditRepository _creditRepository;

        public CadastroController(
            ILogger<CadastroController> logger,
            AppDbContext appDbContext,
            ICreditRepository creditRepository
            )
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _creditRepository = creditRepository;
        }

        [HttpPost("inserirNovoCliente")]
        [ActionName(nameof(PostInserirNovoCliente))]
        public async Task<Cliente> PostInserirNovoCliente()
        {
            //Inadimplentes
            var cliente = new Cliente
            {
                CPF = "12345678901",
                Nome = "João Zester",
                UF = "SP",
                Celular = "(11) 98765-4321"
            };

            _appDbContext.Cliente.Add(cliente);


            return cliente;
        }
    }
}