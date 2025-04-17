using DematicCalculatorAPI.Application.DTOs;

namespace DematicCalculatorAPI.Application.Interfaces
{
    public interface ICalculatorService
    {
        /// <summary>
        /// Handle calculation and returning the result
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        double HandleCalculation(OperationDto dto);
    }
}
