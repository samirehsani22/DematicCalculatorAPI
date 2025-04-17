using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DematicCalculatorAPI.Application.DTOs
{
    [XmlRoot("Maths")]
    public class MathDto
    {
        [XmlElement("Operation")]
        [JsonPropertyName("Operation")]
        public OperationDto Operation { get; set; }
    }

    public class OperationDto
    {
        [Required]
        [XmlAttribute("ID")]
        [JsonPropertyName("@ID")]
        public string Id { get; set; }

        [Required]
        [XmlElement("Value")]
        [JsonPropertyName("Value")]
        public List<string> Values { get; set; }

        [Required]
        [XmlElement("Operation")]
        [JsonPropertyName("Operation")]
        public OperationDto InnerOperation { get; set; }
    }
}
