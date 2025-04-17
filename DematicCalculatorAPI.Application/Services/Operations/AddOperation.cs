using System.Xml.Serialization;

namespace DematicCalculatorAPI.Application.Services.Operations
{

    [XmlRoot("Operation")]
    public class AddOperation : CalculatorBaseService
    {
        public override double GetCalculatedResult() => Values.Sum();
    }
}
