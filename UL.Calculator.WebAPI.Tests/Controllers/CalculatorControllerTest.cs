using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using UL.Calculator.Core.Model;
using UL.Calculator.Services;
using UL.Calculator.WebAPI.Controllers;

namespace UL.Calculator.WebAPI.Tests.Controllers
{
    [TestFixture]
    public class CalculatorControllerTest
    {
        private CalculatorController _sut;
        private Mock<ICalculatorService> _calculatorService;

        [SetUp]
        public void Setup()
        {
            _calculatorService = new Mock<ICalculatorService>();
            _sut = new CalculatorController(_calculatorService.Object);
        }

        [Test]
        public async Task Should_Return_Calculated_Result_As_Response_For_Valid_Expression()
        {
            // Arrange

            _calculatorService.Setup(x => x.ValidateInput(It.IsAny<InputModel>()))
                           .ReturnsAsync(true);

            _calculatorService.Setup(x => x.CalculateExpression(It.IsAny<InputModel>()))
                           .ReturnsAsync(14);

            //Act
            var result = await _sut.CalculateExpression(new InputModel()) as OkObjectResult;

            //Assert
            _calculatorService.Verify(x => x.ValidateInput(It.IsAny<InputModel>()), Times.Once);
            Assert.IsNotNull(result);
            result.Value.Should().BeOfType<string>().Which.Should().Contain("14");
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Should_Return_Bad_Request_For_InValid_Expression()
        {
            // Arrange

            _calculatorService.Setup(x => x.ValidateInput(It.IsAny<InputModel>()))
                           .ReturnsAsync(false);

            //Act
            var result = await _sut.CalculateExpression(new InputModel()) as BadRequestObjectResult;

            //Assert
            _calculatorService.Verify(x => x.ValidateInput(It.IsAny<InputModel>()), Times.Once);
            Assert.IsNotNull(result);
            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }
    }
}