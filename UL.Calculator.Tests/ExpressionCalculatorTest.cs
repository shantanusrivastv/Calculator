using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UL.Calculator.Common;

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

        [TestCase("552/2", 276)]
        [TestCase("2-5/5+3", 4)]
        [TestCase("2-15/3+8", 5)]
        [TestCase("2+5*3+2", 19)]
        [TestCase("4 + 5 * 2", 14)]
        [TestCase("4+5/2", 6.5d)]
        [TestCase("4 + 5 / 2 - 1", 5.5d)]
        [TestCase("4 + 5 / 5 - 9 -5/2", -6.5d)]
        public void Should_Calculate_Expression(string input, double expected)
        {
            var actual = _sut.Calculate(input);

            Assert.AreEqual(expected, actual);
        }
    }
}