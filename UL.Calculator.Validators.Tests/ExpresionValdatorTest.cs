using System.Diagnostics;
using NUnit.Framework;
using UL.Calculator.Validators;

namespace UL.Calculator.Tests
{
    public class ExpresionValdatorTest
    {
        private ExpressionValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ExpressionValidator();
        }

        [TestCase("44aABsa")]//Arrange
        [TestCase("44~#@")]
        [TestCase(@"\¬`%£$@")]
        public void Sould_InValidate_Symbols(string invalidExpression)
        {
            //Act
            var result = _sut.IsValid(invalidExpression);

            //Assert
            Assert.IsFalse(result);
        }

        [TestCase("4*2^7")]//Arrange
        [TestCase("4()%&&")]
        [TestCase(@"\¬`%£$@")]
        [TestCase("^%%")]
        public void Sould_InValidate_Unsupported_Operators(string invalidExpression)
        {
            //Act
            var result = _sut.IsValid(invalidExpression);

            //Assert
            Assert.IsFalse(result);
        }

        [TestCase("-2")]
        [TestCase("-2*3")]
        [TestCase("2*-3")]
        [TestCase("2**3")]
        [TestCase("2+3**")]
        [TestCase("2+3*")]
        public void Sould_InValidate_Expression_For_Bad_Input(string badInput)
        {
            Debug.WriteLine((int)'*');
            Debug.WriteLine((int)'/');
            Debug.WriteLine((int)'+');
            Debug.WriteLine((int)'-');

            //Act

            var result = _sut.IsValid(badInput);

            //Assert
            Assert.IsFalse(result);
        }
    }
}