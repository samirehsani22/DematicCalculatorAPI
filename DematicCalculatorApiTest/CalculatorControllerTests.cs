using DematicCalculatorAPI.Application.DTOs;
using DematicCalculatorAPI.Application.Interfaces;
using DematicCalculatorAPI.Application.Response;
using DematicCalculatorAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text;

namespace DematicCalculatorApiTest
{
    public class CalculatorControllerTests
    {
        private readonly Mock<ICalculatorService> _calculatorService;
        private readonly Mock<IDeserializerService> _mockDeserializerService;
        private readonly CalculatorController _calculatorController;
        private readonly Mock<ILogger<CalculatorController>> _logger;
        public CalculatorControllerTests()
        {
            _logger = new Mock<ILogger<CalculatorController>>();
            _calculatorService = new Mock<ICalculatorService>();
            _mockDeserializerService = new Mock<IDeserializerService>();
            _calculatorController = new CalculatorController(_logger.Object, _mockDeserializerService.Object, _calculatorService.Object);
        }

        [Fact]
        public async Task CalculateAsync_ShouldReturnCurrectResult_WhenGivingAddOperation()
        {
            // Arrange
            var dto = new OperationDto
            {
                Id = "PLus",
                Values = new List<string> { "2", "3"}
            };

            _mockDeserializerService.Setup(d => d.DeserializeXml(GetXmlData()))
                .Returns(new ServiceResponse<OperationDto>(dto));

            _calculatorService.Setup(x => x.HandleCalculation(dto))
                .Returns(5);

            var context = new DefaultHttpContext();
            context.Request.ContentType = "application/xml";
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(GetXmlData()));

            _calculatorController.ControllerContext = new ControllerContext() { HttpContext = context };

            // Act
            var result = await _calculatorController.CalculateAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = (double)okResult.Value.GetType().GetProperty("Result").GetValue(okResult.Value, null);
            Assert.Equal(5, response);
            Assert.True(okResult.StatusCode == 200);
        }

        [Fact]
        public async Task CalculateAsync_ShouldReturnBadRequest_WhenGivingInvalidContentType()
        {
            // Arrange
            var dto = new OperationDto
            {
                Id = "PLus",
                Values = new List<string> { "2", "3" }
            };

            _mockDeserializerService.Setup(d => d.DeserializeXml(GetXmlData()))
                .Returns(new ServiceResponse<OperationDto>(dto));

            _calculatorService.Setup(x => x.HandleCalculation(dto))
                .Returns(5);

            var context = new DefaultHttpContext();
            context.Request.ContentType = "application/test"; // bad content type
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(GetXmlData()));

            _calculatorController.ControllerContext = new ControllerContext() { HttpContext = context };

            // Act
            var result = await _calculatorController.CalculateAsync();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(badRequestResult.Value, "Unsupported content type");
            Assert.True(badRequestResult.StatusCode == (int)HttpStatusCode.BadRequest);
        }
        private string GetXmlData()
            => @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <Maths>
                    <Operation ID=""substruction"">
                        <Value>20</Value>    
                        <Value>4</Value>    
                        <Operation ID=""division"">
                            <Value>40</Value>
                            <Value>5</Value>
                        </Operation>
                    </Operation>
                </Maths>";
    }
}
