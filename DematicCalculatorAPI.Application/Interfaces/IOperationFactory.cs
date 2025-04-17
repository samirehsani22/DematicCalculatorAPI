using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Services.Operations;

namespace DematicCalculatorAPI.Application.Interfaces
{
    public interface IOperationFactory
    {
        /// <summary>
        /// Create AddOperation, MultiplyOperation and etc based on incoming operation type 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        CalculatorBaseService Create(OperationDto dto);
    }
}
