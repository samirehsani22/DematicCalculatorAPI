namespace DematicCalculatorAPI.Application.Services.Operations
{
    public class SubstructionOperation : CalculatorBaseService
    {
        public override double GetCalculatedResult()
            => Values.Aggregate((accumulator, currentValue) => accumulator - currentValue);
    }
}
