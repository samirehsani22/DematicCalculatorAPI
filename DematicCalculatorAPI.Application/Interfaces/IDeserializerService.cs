using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Response;

namespace DematicCalculatorAPI.Application.Interfaces
{
    public interface IDeserializerService
    {
        /// <summary>
        /// Deserialize json data to OperationDto
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        ServiceResponse<OperationDto> DeserializeJson(string json);
        ServiceResponse<OperationDto> DeserializeXml(string xml);
    }
}
