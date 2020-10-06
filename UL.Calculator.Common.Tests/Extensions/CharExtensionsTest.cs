using NUnit.Framework;
using UL.Calculator.Common.Extensions;

namespace UL.Calculator.Common.Tests
{
    [TestFixture]
    public class CharExtensionTest
    {
        [TestCase('2')]
        [TestCase('1')]
        public void Should_Convert_To_Int(char input)
        {
            Assert.That(input.ToInt(), Is.InstanceOf<int>());
        }

        [TestCase('2')]
        [TestCase('1')]
        public void Should_Convert_To_Double(char input)
        {
            Assert.That(input.ToDouble(), Is.InstanceOf<double>());
        }
    }
}