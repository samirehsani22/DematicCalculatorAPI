using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Interfaces;
using DematicCalculatorAPI.Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace DematicCalculatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IDeserializerService _deserializerService;
        private readonly ICalculatorService _calculatorService;
        private readonly ILogger<CalculatorController> _logger;


        public CalculatorController(
            ILogger<CalculatorController> logger,
            IDeserializerService deserializerService,
            ICalculatorService calculatorService)
        {
            _logger = logger;
            _deserializerService = deserializerService;
            _calculatorService = calculatorService;
        }

        [HttpPost]
        [Consumes("application/json", "application/xml")]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> CalculateAsync()
        {
            var validContentTypes = new[] { "application/json", "application/xml" };
            var contentType = Request.ContentType?.ToLower();

            if (contentType == null || !validContentTypes.Contains(contentType))
                return BadRequest("Unsupported content type");

            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            if (string.IsNullOrEmpty(body))
                return BadRequest("Invalid input data");

            ServiceResponse<OperationDto> serviceResponse;
            if (contentType.Contains("application/json"))
                serviceResponse = _deserializerService.DeserializeJson(body);
            else if (contentType.Contains("application/xml"))
                serviceResponse = _deserializerService.DeserializeXml(body);
            else
                return BadRequest("Unsupported content type");

            if (!serviceResponse.Success)
                return BadRequest(serviceResponse.Message);

            var result = _calculatorService.HandleCalculation(serviceResponse.Entity);
            return Ok(new { Result = result });
        }
    }
}
