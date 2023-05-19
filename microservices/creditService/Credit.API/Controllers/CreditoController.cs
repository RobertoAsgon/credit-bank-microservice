using Credit.Application.Repositories;
using Credit.Application.Requests;
using Credit.Application.Responses;
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
                var validator = new CreditoRequestValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    List<string> errorList = new List<string>();

                    foreach (var error in validationResult.Errors)
                    {
                        errorList.Add(error.ErrorMessage);
                    }

                    var errorResponse = new CreditoErrorResponse()
                    {
                        Aprovado = false,
                        Errors = errorList,
                    };

                    return BadRequest(errorResponse);
                }


                var response = await _liberarCreditoUseCase.Execute(request);

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}