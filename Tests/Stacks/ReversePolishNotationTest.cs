using Elasmobranch.Stacks;
using NUnit.Framework;

namespace Tests.Stacks
{
    public class ReversePolishNotationTest
    {
        [Test]
        [TestCase(5.0, "3 2 +")]
        [TestCase(12.0, "10 2 +")]
        [TestCase(22.0, "-2 24 +")]
        [TestCase(2.0, "5 3 -")]
        [TestCase(2.0, "22 20 -")]
        [TestCase(2.0, "-4 -6 -")]
        [TestCase(2.0, "4 2 /")]
        [TestCase(0.0, "0 3 /")]
        [TestCase(0.2, "1 5 /")]
        [TestCase(-4.0, "-20 5 /")]
        [TestCase(24.0, "8 3 *")]
        [TestCase(24.0, "3 8 *")]
        [TestCase(-21.0, "-7 3 *")]
        [TestCase(100.0, "-10 -10 *")]
        public void SingleOperationTest(decimal output, string input)
        {
            Assert.AreEqual(output, new ReversePolishNotation(input).Evaluate());
        }
        
        [Test]
        [TestCase(40.0, "3 5 + 7 2 - *")]
        [TestCase(11.0, "9 6 3 / +")]
        [TestCase(6.0, "5 3 - 4 +")]
        [TestCase(29.0, "3 7 + 2 / 5 * 6 8 - 2 * -")]
        public void MultipleOperationTest(decimal output, string input)
        {
            Assert.AreEqual(output, new ReversePolishNotation(input).Evaluate());
        }
    }
}