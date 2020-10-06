using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UL.Calculator.Core.Model;
using UL.Calculator.Validators;

namespace UL.Calculator.Services.Tests
{
    [TestFixture]
    public class CalculatorServiceTest
    {
        private Mock<IExpressionValidator> _validator;
        private Mock<IExpressionCalculator> _calculator;
        private CalculatorService _sut;

        [SetUp]
        public void Setup()
        {
            _validator = new Mock<IExpressionValidator>();
            _calculator = new Mock<IExpressionCalculator>();
            _sut = new CalculatorService(_validator.Object, _calculator.Object);
        }

        [Test]
        public async Task Should_Return_True_For_Valid_Input()
        {
            // Arrange
            _validator.Setup(x => x.IsValid(It.IsAny<string>()))
                       .Returns(true);
            //Act
            var result = await _sut.ValidateInput(new InputModel());

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Should_Return_False_For_InValid_Input()
        {
            // Arrange
            _validator.Setup(x => x.IsValid(It.IsAny<string>()))
                       .Returns(false)
                       .Verifiable();
            //Act
            var result = await _sut.ValidateInput(new InputModel());

            //Assert
            _validator.Verify();
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task Should_Return_Calculated_Expression()
        {
            // Arrange
            _calculator.Setup(x => x.Calculate(It.IsAny<string>()))
                       .Returns(12)
                       .Verifiable();
            //Act
            var result = await _sut.CalculateExpression(new InputModel());

            //Assert
            _calculator.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual(12, result);
        }
    }
}