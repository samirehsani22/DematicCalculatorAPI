using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Interfaces;
using DematicCalculatorAPI.Application.Services.Operations;
using DematicCalculatorAPI.Services;
using Moq;

namespace DematicCalculatorApiTest
{
    public class CalculatorServiceTests
    {
        private readonly Mock<IOperationFactory> _mockFactory;
        private readonly CalculatorService _calculatorService;
        public CalculatorServiceTests()
        {
            _mockFactory = new Mock<IOperationFactory>();
            _calculatorService = new CalculatorService( _mockFactory.Object );
        }

        [Fact]
        public void HandleCalculation_ShouldReturnCorrectResult_WhenGivenAddOperation()
        {
            var mockAddOperation = new Mock<AddOperation>();
            var mockMultiplyOperation = new Mock<MultiplyOperation>();

            mockMultiplyOperation.Setup(m => m.GetCalculatedResult()).Returns(20);
            mockMultiplyOperation.Setup(m => m.Values).Returns(new List<double> { 4, 5 });

            mockAddOperation.Setup(m => m.GetCalculatedResult()).Returns(25);
            mockAddOperation.Setup(m => m.Values).Returns(new List<double> { 2, 3, 20 });

            _mockFactory.Setup(f => f.Create(It.Is<OperationDto>(o => o.Id == "Plus"))).Returns(mockAddOperation.Object);
            _mockFactory.Setup(f => f.Create(It.Is<OperationDto>(o => o.Id == "Multiplication"))).Returns(mockMultiplyOperation.Object);

            var result = _calculatorService.HandleCalculation(GetTestData());

            Assert.Equal(25, result);
        }

        [Fact]
        public void HandleCalculation_ShouldThrowNotSupportedException_WhenOperationIsNotSupported()
        {
            var dto = new OperationDto
            {
                Id = "BadData",
                Values = new List<string> { "2", "3" }
            };

            var mockFactory = new Mock<IOperationFactory>();

            mockFactory
                .Setup(f => f.Create(It.IsAny<OperationDto>()))
                .Throws(new NotSupportedException($"Operation {dto.Id} is not supported."));

            var calculatorService = new CalculatorService(mockFactory.Object);

            var exception = Assert.Throws<NotSupportedException>(() => calculatorService.HandleCalculation(dto));
            Assert.Equal("Operation UnsupportedOperation is not supported.", exception.Message);
        }

        private OperationDto GetTestData()
            => new OperationDto
            {
                Id = "Plus",
                Values =  new List<string> { "2", "3"},
                InnerOperation = new OperationDto
                {
                    Id = "Multiplication",
                    Values = new List<string> { "4", "5"}
                }
            };
    }
}
