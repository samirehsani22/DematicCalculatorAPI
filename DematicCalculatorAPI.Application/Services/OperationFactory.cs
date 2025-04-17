using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Interfaces;
using DematicCalculatorAPI.Application.Services.Operations;

namespace DematicCalculatorAPI.Services
{
    public class OperationFactory : IOperationFactory
    {
        /// <summary>
        /// Create AddOperation, MultiplyOperation and etc based on incoming operation type 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public CalculatorBaseService Create(OperationDto dto)
        {
            // create operation object based on operation type 
            CalculatorBaseService operation = dto.Id switch
            {
                "Plus" => new AddOperation(),
                "Multiplication" => new MultiplyOperation(),
                _ => throw new NotSupportedException($"Operation {dto.Id} is not supported.")
            };

            // parse string value to double and save the result to the list of current object
            operation.Values = dto.Values.Select(x=> double.Parse(x)).ToList();
            return operation;
        }
    }
}
