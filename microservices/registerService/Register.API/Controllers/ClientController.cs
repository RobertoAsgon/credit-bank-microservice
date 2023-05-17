using Microsoft.AspNetCore.Mvc;
using Register.API.Models;
using Register.API.Services;

namespace Register.API.Controllers
{
    [ApiController]
    [Route("api/cadastro/")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClienteService _clientService;

        public ClientController(ILogger<ClientController> logger, IClienteService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpPost("cadastrarNovoCliente")]
        [ActionName(nameof(PostAddCliente))]
        public Cliente PostAddCliente(Cliente cliente)
        {
        
            var result = _clientService.AddCliente(cliente);

            return result;

        }
    }
}