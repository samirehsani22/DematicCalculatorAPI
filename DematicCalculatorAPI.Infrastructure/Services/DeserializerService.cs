using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Interfaces;
using DematicCalculatorAPI.Application.Response;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml.Serialization;

namespace DematicCalculatorAPI.Services
{
    public class DeserializerService : IDeserializerService
    {
        private readonly ILogger<DeserializerService> _logger;
        public DeserializerService(ILogger<DeserializerService> logger)
        {
            _logger = logger;
        }

        public ServiceResponse<OperationDto> DeserializeJson(string json)
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(json);
                var operationElement = jsonDoc.RootElement
                    .GetProperty("Maths")
                    .GetProperty("Operation");

                var operationJson = operationElement.GetRawText();
                if (operationJson == null)
                {
                    _logger.LogError("Input data is missing [Maths] or [Operation] fields.");
                    return new ServiceResponse<OperationDto>("Input data is missing required fields.");
                }

                var result = JsonSerializer.Deserialize<OperationDto>(operationJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result == null)
                {
                    _logger.LogError("Failed to Deserialize json data");
                    return new ServiceResponse<OperationDto>("Invalid operation format.");
                }

                return new ServiceResponse<OperationDto>(result);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Invalid json input. Error = {ex}");
                return new ServiceResponse<OperationDto>("Invalid JSON format.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Derserialize. Error = {ex}");
                return new ServiceResponse<OperationDto>("An unexpected error occurred while processing the request.");
            }
        }

        public ServiceResponse<OperationDto> DeserializeXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(MathDto));
            using var stringReader = new StringReader(xml);
            var result = serializer.Deserialize(stringReader);
            if (result == null)
            {
                _logger.LogError("Failed to Deserialize xml data");
                return new ServiceResponse<OperationDto>("Invalid operation format.");
            }

            var response = ((MathDto)result).Operation;
            if (response == null)
            {
                _logger.LogError($"Failed to parse object to {nameof(MathDto)}");
                return new ServiceResponse<OperationDto>("Invalid operation format.");
            }

            if (string.IsNullOrEmpty(response.Id) || !response.Values.Any())
            {
                _logger.LogError("Input data is missing fields.");
                return new ServiceResponse<OperationDto>("Input data is missing required fields.");
            }

            return new ServiceResponse<OperationDto>(response);
        }
    }
}
