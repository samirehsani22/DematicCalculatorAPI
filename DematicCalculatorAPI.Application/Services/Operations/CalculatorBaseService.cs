namespace DematicCalculatorAPI.Application.Services.Operations
{
    public abstract class CalculatorBaseService
    {
        // must be virtual to support Mock during testing
        public virtual List<double> Values { get; set; } = new();
        public abstract double GetCalculatedResult();
    }
}
