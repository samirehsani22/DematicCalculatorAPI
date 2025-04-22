namespace DematicCalculatorAPI.Application.Services.Operations
{
    public class DivisionOperation : CalculatorBaseService
    {
        public override double GetCalculatedResult()
            => Values.Aggregate((accumulator, currentValue) => accumulator / currentValue);
    }
}
