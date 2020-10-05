using System;
using NUnit.Framework;

namespace UL.Calculator.Tests
{
    public class ExpressionCalculatorTest
    {
        private ExpressionCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ExpressionCalculator();
        }

        [TestCase("2+5+15/3-8", 4)]
        [TestCase("515+8449-77918/12", 2470.83d)]
        [TestCase("552/2", 276)]
        [TestCase("2-5/5+3", 4)]
        [TestCase("2-15/3+8", 5)]
        [TestCase("2+5*3+2", 19)]
        [TestCase("4 + 5 * 2", 14)]
        [TestCase("4+5/2", 6.5d)]
        [TestCase("4 + 5 / 2 - 1", 5.5d)]
        [TestCase("4 + 5 / 5 - 9 -5/2", -6.5d)]
        [TestCase("4+15/3-9-5/2", -2.5d)]
        public void Should_Calculate_Expression(string input, double expected)
        {
            var actual = _sut.Calculate(input);

            Assert.AreEqual(expected, Math.Round(actual, 2, MidpointRounding.AwayFromZero));
        }
    }
}