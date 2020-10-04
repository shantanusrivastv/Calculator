using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //[TestCase("2+5*3+2", 19)]
        //[TestCase("4 + 5 * 2", 14)]
        //[TestCase("4+5/2", 6.5d)]
        //[TestCase("4 + 5 / 2 - 1", 5.5d)]
        [TestCase("4 + 15 / 3 - 10 -5/2", -3.5d)]
        public void Should_Calculate_Expresson(string input, double expected)
        {
            var actual = _sut.Calculate(input);

            Assert.AreEqual(expected, actual);
        }
    }
}