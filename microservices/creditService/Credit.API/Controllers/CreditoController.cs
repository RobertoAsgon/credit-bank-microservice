using Credit.Application.Repositories;
using Credit.Application.Requests;
using Credit.Application.Responses;
using Credit.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Credit.API.Controllers
{
    [Route("api/credito")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly ILogger<CreditoController> _logger;
        private readonly ILiberarCreditoUseCase _liberarCreditoUseCase;

        public CreditoController(
            ILogger<CreditoController> logger,
            ILiberarCreditoUseCase creditoUseCase
            )
        {
            _logger = logger;
            _liberarCreditoUseCase = creditoUseCase;
        }

        [HttpPost("liberarCredito")]
        [ActionName(nameof(PostLiberarCredito))]
        public async Task<IActionResult> PostLiberarCredito(CreditoRequest request)
        {
            try
            {
                var response = await _liberarCreditoUseCase.Execute(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}