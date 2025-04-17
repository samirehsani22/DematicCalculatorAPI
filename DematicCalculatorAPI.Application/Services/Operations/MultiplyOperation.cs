namespace DematicCalculatorAPI.Application.Services.Operations
{
    public class MultiplyOperation : CalculatorBaseService
    {
        public override double GetCalculatedResult()
            => Values.Aggregate(1.0, (accumulator, currentValue) => accumulator * currentValue);
    }
}
