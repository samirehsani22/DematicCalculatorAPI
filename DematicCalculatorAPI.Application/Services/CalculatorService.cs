using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Interfaces;

namespace DematicCalculatorAPI.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IOperationFactory _factory;

        public CalculatorService(IOperationFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Handle calculation and returning the result
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public double HandleCalculation(OperationDto dto)
        {
            var op = _factory.Create(dto);

            // Handle nested operation recursively
            if (dto.InnerOperation != null)
            {
                // call this method again and handle the inner operation
                var innerResult = HandleCalculation(dto.InnerOperation);
                
                // add the result to the current object list
                op.Values.Add(innerResult);
            }

            // calculate the result
            return op.GetCalculatedResult();
        }
    }
}
